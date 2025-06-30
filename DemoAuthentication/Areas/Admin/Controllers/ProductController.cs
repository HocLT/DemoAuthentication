using AutoMapper;
using DemoAuthentication.Dto;
using DemoAuthentication.Models;
using DemoAuthentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DemoAuthentication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        readonly IProductService productService;
        readonly ICategoryService categoryService;
        readonly IWebHostEnvironment env;
        readonly IMapper mapper;

        public ProductController(IProductService productService, IMapper mapper, ICategoryService categoryService, IWebHostEnvironment env)
        {
            this.productService = productService;
            this.mapper = mapper;
            this.categoryService = categoryService;
            this.env = env;
        }

        public async Task<IActionResult> Index()
        {
            return View(await productService.GetProducts());
        }

        public async Task<IActionResult> Create()
        {
            var cates = await categoryService.GetCategories();
            ViewBag.cates = cates;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            Product p = mapper.Map<Product>(dto);

            // xử lý upload
            string? imgFile = null;
            if (dto.Photo != null && dto.Photo.Length > 0)
            {
                try
                {
                    var imageFolder = Path.Combine(env.WebRootPath, "images");
                    // kiểm tra thư mục có hay chưa?
                    if (!Directory.Exists(imageFolder))
                    {
                        Directory.CreateDirectory(imageFolder);
                    }

                    string? imgPath = Path.Combine(imageFolder, dto.Photo.FileName);
                    using (var fs = new FileStream(imgPath, FileMode.Create))
                    {
                        await dto.Photo.CopyToAsync(fs);
                    }
                    imgFile = dto.Photo.FileName;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = $"Upload image error: {ex.Message}";
                    return View(dto);
                }
            }

            p.Image = imgFile;

            await productService.Create(p);

            return RedirectToAction("Index");
        }
    }
}

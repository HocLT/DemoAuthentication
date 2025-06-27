using AutoMapper;
using DemoAuthentication.Dto;
using DemoAuthentication.Models;
using DemoAuthentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoAuthentication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        readonly IProductService productService;
        readonly IMapper mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await productService.GetProducts());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            Product p = mapper.Map<Product>(dto);

            // xử lý upload

            await productService.Create(p);

            return RedirectToAction("Index");
        }
    }
}

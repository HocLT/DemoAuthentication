using DemoAuthentication.Dto;
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

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await productService.GetProducts());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto dto)
        {
            return RedirectToAction("Index");
        }
    }
}

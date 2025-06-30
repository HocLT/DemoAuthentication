using System.Diagnostics;
using DemoAuthentication.Dto;
using DemoAuthentication.Models;
using DemoAuthentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DemoAuthentication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        readonly IProductService productService;
        readonly IOrderService orderService;

        public const string CART = "cart";

        List<CartItemDto> GetCartItems()
        {
            var session = HttpContext.Session;
            string? jsonCart = session.GetString(CART);
            if (jsonCart != null)
            {
                var result = JsonConvert.DeserializeObject<List<CartItemDto>>(jsonCart);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return new List<CartItemDto>();
                }
            }
            return new List<CartItemDto>();
        }

        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CART);
        }

        void SaveCartSession(List<CartItemDto> items)
        {
            var session = HttpContext.Session;
            string jsonCart = JsonConvert.SerializeObject(items);
            session.SetString(CART, jsonCart);
        }

        public HomeController(ILogger<HomeController> logger, IProductService productService, IOrderService orderService)
        {
            _logger = logger;
            this.productService = productService;
            this.orderService = orderService;
        }


        public async Task<IActionResult> Index()
        {
            var products = await productService.GetProducts();
            ViewBag.TopProducts = products.Take(10);
            return View();
        }

        public async Task<IActionResult> ProductDetails(int id)
        {
            return View(await productService.GetProduct(id));
        }

        public async Task<IActionResult> AddCart(int id)
        {
            Product? p = await productService.GetProduct(id);
            if (p == null)
            {
                return NotFound("Không có sản phẩm");
            }

            // Xử lý add cart
            var cart = GetCartItems();
            // tìm trong giỏ hàng xem đã có sp chưa?
            var cartItem = cart.SingleOrDefault(o=>o.Product!.Id==id);
            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItemDto { Quantity = 1, Product = p });
            }

            SaveCartSession(cart);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveCart(int id)
        {
            // Xử lý

            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart(int pid, int quantity)
        {
            // Xử lý

            return RedirectToAction("ViewCart");
        }

        public IActionResult ViewCart()
        {
            return View(GetCartItems());
        }

        public IActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveCart()
        {
            var cart = GetCartItems();
            orderService.CreateOrder(cart);
            ClearCart();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

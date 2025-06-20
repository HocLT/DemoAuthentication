using DemoAuthentication.Dto;
using DemoAuthentication.Models;
using DemoAuthentication.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DemoAuthentication.Controllers
{
    public class AuthenticationController : Controller
    {
        readonly Services.IAuthenticationService authenService;

        public AuthenticationController(Services.IAuthenticationService authenService)
        {
            this.authenService = authenService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            Account? account = await authenService.Login(dto);
            if (account == null)
            {
                ViewBag.Message = "Invalid Username or Password!";
                return View(dto);
            }

            // register login information to http context
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Email!),
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Role, account.Role!),
            };

            var identity = new ClaimsIdentity(claims, "CustomLogin");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            if (await authenService.RegisterAccount(dto))
            {
                return RedirectToAction("Login");
            }

            ViewBag.Message = "Register Account Error!";
            return View(dto);
        }
    }
}

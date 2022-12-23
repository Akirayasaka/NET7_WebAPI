using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Models.Dto.Login;

namespace Web.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController(IServiceProvider provider) : base(provider)
        {
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDTO loginRequestDTO = new();
            return View(loginRequestDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginRequestDTO loginRequestDTO)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterationRequestDTO requestDTO)
        {
            ApiResponse result = await _authService.RegisterAsync<ApiResponse>(requestDTO);
            if (result != null & result.IsSuccess)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}

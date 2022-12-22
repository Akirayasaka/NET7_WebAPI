using Microsoft.AspNetCore.Mvc;
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
    }
}

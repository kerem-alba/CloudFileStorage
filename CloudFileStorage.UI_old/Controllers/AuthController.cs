using CloudFileStorage.UI.Models;
using CloudFileStorage.UI.Services;
using CloudFileStorage.Common.Constants;
using Microsoft.AspNetCore.Mvc;

namespace CloudFileStorage.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _authService.LoginAsync(model);

            if (result is not null && result.Success && result.Data is not null)
            {
                HttpContext.Session.SetString("token", result.Data.AccessToken);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = result?.Message ?? ResponseMessages.GenericError;
            return View(model);
        }
    }
}

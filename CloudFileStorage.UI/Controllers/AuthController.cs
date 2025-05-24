using AutoMapper;
using CloudFileStorage.Common.Constants;
using CloudFileStorage.UI.Constants;
using CloudFileStorage.UI.Models;
using CloudFileStorage.UI.Models.DTOs;
using CloudFileStorage.UI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CloudFileStorage.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var dto = _mapper.Map<LoginUserDto>(model);
            var result = await _authService.LoginAsync(dto);

            if (result is not null && result.Success && result.Data is not null)
            {
                HttpContext.Session.SetString("token", result.Data.AccessToken);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = result?.Message ?? UiMessages.SomethingWentWrong;
            return View(model);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ViewBag.Error = UiMessages.PasswordMismatch;
                return View(model);
            }

            var dto = _mapper.Map<RegisterUserDto>(model);
            var result = await _authService.RegisterAsync(dto);

            if (result is not null && result.Success && result.Data is not null)
            {
                HttpContext.Session.SetString("token", result.Data.AccessToken);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = result?.Message ?? UiMessages.SomethingWentWrong;
            return View(model);
        }
    }
}

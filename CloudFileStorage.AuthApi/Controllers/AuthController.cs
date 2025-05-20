using CloudFileStorage.AuthApi.Constants;
using CloudFileStorage.AuthApi.DTOs;
using CloudFileStorage.AuthApi.Extensions;
using CloudFileStorage.AuthApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudFileStorage.AuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            var response = await _authService.RegisterAsync(dto);
            return this.HandleResponse(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            var response = await _authService.LoginAsync(dto);
            return this.HandleResponse(response);

        }
    }
}

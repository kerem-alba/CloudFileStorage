using CloudFileStorage.Common.Extensions;
using CloudFileStorage.AuthApi.Models.DTOs;
using CloudFileStorage.AuthApi.Services.Interfaces;
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

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto request)
        {
            var response = await _authService.RefreshTokenAsync(request.RefreshToken);
            return this.HandleResponse(response);
        }


    }
}

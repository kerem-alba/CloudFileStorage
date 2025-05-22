using CloudFileStorage.Common.Extensions;
using CloudFileStorage.AuthApi.CQRS.Auth.Commands;
using CloudFileStorage.AuthApi.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using SMediator.Core.Abstractions;

namespace CloudFileStorage.AuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var response = await _mediator.Send(new RegisterCommand(dto));
            return this.HandleResponse(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            var response = await _mediator.Send(new LoginCommand(dto));
            return this.HandleResponse(response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto request)
        {
            var response = await _mediator.Send(new RefreshTokenCommand(request.RefreshToken));
            return this.HandleResponse(response);
        }
    }
}

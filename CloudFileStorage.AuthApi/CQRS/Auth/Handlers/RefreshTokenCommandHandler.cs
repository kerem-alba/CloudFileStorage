using SMediator.Core.Abstractions;
using CloudFileStorage.AuthApi.CQRS.Auth.Commands;
using CloudFileStorage.AuthApi.Services.Interfaces;
using CloudFileStorage.Common.Models;
using CloudFileStorage.AuthApi.Models.DTOs;

namespace CloudFileStorage.AuthApi.CQRS.Auth.Handlers
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ServiceResponse<AuthResponseDto>>
    {
        private readonly IAuthService _authService;

        public RefreshTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<ServiceResponse<AuthResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _authService.RefreshTokenAsync(request.RefreshToken);
        }
    }
}

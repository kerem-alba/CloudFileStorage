using SMediator.Core.Abstractions;
using CloudFileStorage.Common.Models;
using CloudFileStorage.AuthApi.Models.DTOs;

namespace CloudFileStorage.AuthApi.CQRS.Auth.Commands
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<ServiceResponse<AuthResponseDto>>;
}

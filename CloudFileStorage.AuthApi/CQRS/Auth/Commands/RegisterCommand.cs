using SMediator.Core.Abstractions;
using CloudFileStorage.AuthApi.Models.DTOs;
using CloudFileStorage.Common.Models;

namespace CloudFileStorage.AuthApi.CQRS.Auth.Commands
{
    public record RegisterCommand(RegisterUserDto Dto) : IRequest<ServiceResponse<AuthResponseDto>>;
}

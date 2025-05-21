using CloudFileStorage.Common.Models;
using CloudFileStorage.AuthApi.Models.DTOs;

namespace CloudFileStorage.AuthApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<AuthResponseDto>> RegisterAsync(RegisterUserDto dto);
        Task<ServiceResponse<AuthResponseDto>> LoginAsync(LoginUserDto dto);
        Task<ServiceResponse<AuthResponseDto>> RefreshTokenAsync(string refreshToken);
    }
}

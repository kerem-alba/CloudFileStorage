using CloudFileStorage.AuthApi.Common;
using CloudFileStorage.AuthApi.DTOs;

namespace CloudFileStorage.AuthApi.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<AuthResponseDto>> RegisterAsync(RegisterUserDto dto);
        Task<ServiceResponse<AuthResponseDto>> LoginAsync(LoginUserDto dto);
        Task<ServiceResponse<AuthResponseDto>> RefreshTokenAsync(string refreshToken);
    }
}

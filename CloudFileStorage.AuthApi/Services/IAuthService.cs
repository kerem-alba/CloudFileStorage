using CloudFileStorage.AuthApi.Common;
using CloudFileStorage.AuthApi.DTOs;

namespace CloudFileStorage.AuthApi.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> RegisterAsync(RegisterUserDto dto);
        Task<ServiceResponse<AuthResponseDto>> LoginAsync(LoginUserDto dto);
    }
}

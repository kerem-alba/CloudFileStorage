using CloudFileStorage.UI.Models;
using CloudFileStorage.Common.Models;
using CloudFileStorage.UI.Models.DTOs;

namespace CloudFileStorage.UI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<AuthResponseDto>?> LoginAsync(LoginUserDto dto);
        Task<ServiceResponse<AuthResponseDto>?> RegisterAsync(RegisterUserDto dto);

    }
}

using CloudFileStorage.Common.Models;
using CloudFileStorage.UI.Models.DTOs;

namespace CloudFileStorage.UI.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<ServiceResponse<AuthResponseDto>?> RefreshTokenAsync(string refreshToken);
    }
}

using CloudFileStorage.UI.Models;

namespace CloudFileStorage.UI.Services
{
    public interface IAuthService
    {
        Task<TokenResponseDto?> LoginAsync(LoginViewModel model);
    }
}

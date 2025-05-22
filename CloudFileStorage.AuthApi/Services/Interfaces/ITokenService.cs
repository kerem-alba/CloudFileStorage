using CloudFileStorage.AuthApi.Models.Entities;

namespace CloudFileStorage.AuthApi.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwt(User user);
        (string RefreshToken, DateTime RefreshExpire) GenerateRefreshToken();
        (string AccessToken, string RefreshToken, DateTime RefreshExpire) GenerateTokens(User user);

    }
}

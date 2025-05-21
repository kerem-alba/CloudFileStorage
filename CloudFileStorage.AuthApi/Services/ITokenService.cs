using CloudFileStorage.AuthApi.Models;

namespace CloudFileStorage.AuthApi.Services
{
    public interface ITokenService
    {
        string GenerateJwt(User user);
        (string Token, DateTime ExpireDate) GenerateRefreshToken();
        (string AccessToken, string RefreshToken, DateTime RefreshExpire) GenerateTokens(User user);

    }
}

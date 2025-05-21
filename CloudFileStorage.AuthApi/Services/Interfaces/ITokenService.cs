using CloudFileStorage.AuthApi.Models.Entities;

namespace CloudFileStorage.AuthApi.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwt(User user);
        (string Token, DateTime ExpireDate) GenerateRefreshToken();
        (string AccessToken, string RefreshToken, DateTime RefreshExpire) GenerateTokens(User user);

    }
}

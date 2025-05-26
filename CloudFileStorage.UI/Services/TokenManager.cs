using CloudFileStorage.UI.Services.Interfaces;

namespace CloudFileStorage.UI.Services
{
    public class TokenManager : ITokenManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRefreshTokenService _refreshTokenService;

        public TokenManager(IHttpContextAccessor httpContextAccessor, IRefreshTokenService refreshTokenService)
        {
            _httpContextAccessor = httpContextAccessor;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<string> GetValidAccessTokenAsync()
        {
            var context = _httpContextAccessor.HttpContext;
            var accessToken = context?.Session.GetString("token");
            var refreshToken = context?.Session.GetString("refreshToken");

            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
                return string.Empty;

            if (JwtHelper.IsTokenExpired(accessToken))
            {
                var response = await _refreshTokenService.RefreshTokenAsync(refreshToken);

                if (response != null && response.Success && response.Data != null)
                {
                    SaveTokens(response.Data.AccessToken, response.Data.RefreshToken);
                    return response.Data.AccessToken;
                }

                return string.Empty;
            }

            return accessToken;
        }

        public void SaveTokens(string accessToken, string refreshToken)
        {
            var context = _httpContextAccessor.HttpContext;
            context?.Session.SetString("token", accessToken);
            context?.Session.SetString("refreshToken", refreshToken);
        }

        public void ClearTokens()
        {
            var context = _httpContextAccessor.HttpContext;
            context?.Session.Remove("token");
            context?.Session.Remove("refreshToken");
        }
    }
}

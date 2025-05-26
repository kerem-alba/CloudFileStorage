using CloudFileStorage.Common.Constants;
using CloudFileStorage.Common.Models;
using CloudFileStorage.UI.Constants;
using CloudFileStorage.UI.Helpers;
using CloudFileStorage.UI.Models.DTOs;
using CloudFileStorage.UI.Services.Interfaces;
using System.Net.Http;

namespace CloudFileStorage.UI.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RefreshTokenService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ServiceResponse<AuthResponseDto>?> RefreshTokenAsync(string refreshToken)
        {
            var client = _httpClientFactory.CreateClient();
            var dto = new RefreshTokenDto { RefreshToken = refreshToken };
            var content = HttpHelper.CreateJsonContent(dto);
            var response = await client.PostAsync(ApiEndpoints.Auth.Refresh, content);
            return await HttpHelper.DeserializeResponseAsync<AuthResponseDto>(response);
        }
    }
}

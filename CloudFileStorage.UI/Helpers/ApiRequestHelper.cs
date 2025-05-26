using CloudFileStorage.Common.Models;
using CloudFileStorage.UI.Services.Interfaces;

namespace CloudFileStorage.UI.Helpers
{
    public class ApiRequestHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenManager _tokenManager;

        public ApiRequestHelper(IHttpClientFactory httpClientFactory, ITokenManager tokenManager)
        {
            _httpClientFactory = httpClientFactory;
            _tokenManager = tokenManager;
        }

        public async Task<ServiceResponse<TResponse>?> PostAsync<TRequest, TResponse>(string url, TRequest data)
        {
            var token = await _tokenManager.GetValidAccessTokenAsync();
            var client = HttpHelper.CreateClient(_httpClientFactory, token);
            var content = HttpHelper.CreateJsonContent(data);
            var response = await client.PostAsync(url, content);
            return await HttpHelper.DeserializeResponseAsync<TResponse>(response);
        }

        public async Task<ServiceResponse<TResponse>?> PutAsync<TRequest, TResponse>(string url, TRequest data)
        {
            var token = await _tokenManager.GetValidAccessTokenAsync();
            var client = HttpHelper.CreateClient(_httpClientFactory, token);
            var content = HttpHelper.CreateJsonContent(data);
            var response = await client.PutAsync(url, content);
            return await HttpHelper.DeserializeResponseAsync<TResponse>(response);
        }

        public async Task<ServiceResponse<TResponse>?> GetAsync<TResponse>(string url)
        {
            var token = await _tokenManager.GetValidAccessTokenAsync();
            var client = HttpHelper.CreateClient(_httpClientFactory, token);
            var response = await client.GetAsync(url);
            return await HttpHelper.DeserializeResponseAsync<TResponse>(response);
        }

        public async Task<ServiceResponse<TResponse>?> DeleteAsync<TResponse>(string url)
        {
            var token = await _tokenManager.GetValidAccessTokenAsync();
            var client = HttpHelper.CreateClient(_httpClientFactory, token);
            var response = await client.DeleteAsync(url);
            return await HttpHelper.DeserializeResponseAsync<TResponse>(response);
        }
    }
}

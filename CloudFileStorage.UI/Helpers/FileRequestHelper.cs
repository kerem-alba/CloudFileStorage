using CloudFileStorage.Common.Models;
using CloudFileStorage.UI.Constants;
using CloudFileStorage.UI.Services.Interfaces;
using System.Net.Http.Headers;

namespace CloudFileStorage.UI.Helpers
{
    public class FileRequestHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenManager _tokenManager;

        public FileRequestHelper(IHttpClientFactory httpClientFactory, ITokenManager tokenManager)
        {
            _httpClientFactory = httpClientFactory;
            _tokenManager = tokenManager;
        }

        public async Task<ServiceResponse<TResponse>?> PostFileAsync<TResponse>(string url, IFormFile file)
        {
            var token = await _tokenManager.GetValidAccessTokenAsync();
            var client = HttpHelper.CreateClient(_httpClientFactory, token);
            using var content = new MultipartFormDataContent();
            using var fileContent = new StreamContent(file.OpenReadStream());
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            content.Add(fileContent, "file", file.FileName);

            var response = await client.PostAsync(url, content);
            return await HttpHelper.DeserializeResponseAsync<TResponse>(response);
        }

        public async Task<ServiceResponse<byte[]>> GetFileAsync(string url)
        {
            var token = await _tokenManager.GetValidAccessTokenAsync();
            var client = HttpHelper.CreateClient(_httpClientFactory, token);
            var response = await client.GetAsync(url);
            return await WrapBinaryResponseAsync(response);
        }

        private static async Task<ServiceResponse<byte[]>> WrapBinaryResponseAsync(HttpResponseMessage response)
        {
            var result = new ServiceResponse<byte[]>
            {
                Success = response.IsSuccessStatusCode,
                StatusCode = (int)response.StatusCode,
                Message = response.IsSuccessStatusCode
                    ? UiMessages.FileDownloadSuccess
                    : UiMessages.FileDownloadFailed
            };

            if (response.IsSuccessStatusCode)
            {
                result.Data = await response.Content.ReadAsByteArrayAsync();
            }

            return result;
        }

    }
}

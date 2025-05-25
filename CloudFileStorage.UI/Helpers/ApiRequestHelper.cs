using CloudFileStorage.Common.Constants;
using CloudFileStorage.Common.Models;
using CloudFileStorage.UI.Constants;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CloudFileStorage.UI.Helpers
{
    public class ApiRequestHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiRequestHelper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ServiceResponse<TResponse>?> PostAsync<TRequest, TResponse>(string url, TRequest data, string? token = null)
        {
            var client = CreateClient(_httpClientFactory, token);
            var content = CreateJsonContent(data);
            var response = await client.PostAsync(url, content);
            return await DeserializeResponseAsync<TResponse>(response);
        }

        public async Task<ServiceResponse<TResponse>?> PutAsync<TRequest, TResponse>(string url, TRequest data, string? token = null)
        {
            var client = CreateClient(_httpClientFactory, token);
            var content = CreateJsonContent(data);
            var response = await client.PutAsync(url, content);
            return await DeserializeResponseAsync<TResponse>(response);
        }

        public async Task<ServiceResponse<TResponse>?> GetAsync<TResponse>(string url, string? token = null)
        {
            var client = CreateClient(_httpClientFactory, token);
            var response = await client.GetAsync(url);
            return await DeserializeResponseAsync<TResponse>(response);
        }

        public async Task<ServiceResponse<TResponse>?> DeleteAsync<TResponse>(string url, string? token = null)
        {
            var client = CreateClient(_httpClientFactory, token);
            var response = await client.DeleteAsync(url);
            return await DeserializeResponseAsync<TResponse>(response);
        }

        public async Task<ServiceResponse<TResponse>?> PostFileAsync<TResponse>(string url, IFormFile file, string? token = null)
        {
            var client = CreateClient(_httpClientFactory, token);

            using var content = new MultipartFormDataContent();
            using var fileContent = new StreamContent(file.OpenReadStream());
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            content.Add(fileContent, "file", file.FileName);

            var response = await client.PostAsync(url, content);
            return await DeserializeResponseAsync<TResponse>(response);
        }
        public async Task<ServiceResponse<byte[]>?> GetFileAsync(string url, string? token = null)
        {
            var client = CreateClient(_httpClientFactory, token);
            var response = await client.GetAsync(url);
            return await WrapBinaryResponseAsync(response);
        }





        public static HttpClient CreateClient(IHttpClientFactory _httpClientFactory, string? token)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(ApiEndpoints.GatewayBase);

            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        public static StringContent CreateJsonContent<T>(T data)
        {
            var json = JsonSerializer.Serialize(data);
            return new StringContent(json, Encoding.UTF8, HttpContentTypes.Json);
        }

        public static async Task<ServiceResponse<T>?> DeserializeResponseAsync<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(content))
            {
                return new ServiceResponse<T>
                {
                    Success = false,
                    Message = UiMessages.EmptyApiResponse,
                    StatusCode = (int)response.StatusCode
                };
            }
            try
            {
                return JsonSerializer.Deserialize<ServiceResponse<T>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (JsonException ex)
            {
                return new ServiceResponse<T>
                {
                    Success = false,
                    Message = string.Format(UiMessages.JsonDeserializationError, ex.Message),
                    StatusCode = (int)response.StatusCode
                };
            }
        }

        public static async Task<ServiceResponse<byte[]>> WrapBinaryResponseAsync(HttpResponseMessage response)
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

using CloudFileStorage.Common.Constants;
using CloudFileStorage.Common.Models;
using CloudFileStorage.UI.Constants;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CloudFileStorage.UI.Helpers
{
    public static class HttpHelper
    {
        public static HttpClient CreateClient(IHttpClientFactory factory, string? token)
        {
            var client = factory.CreateClient();
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
    }
}

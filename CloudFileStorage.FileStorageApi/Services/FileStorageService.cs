using CloudFileStorage.Common.Constants;
using CloudFileStorage.Common.Models;
using CloudFileStorage.FileStorageApi.Models.DTOs;
using CloudFileStorage.FileStorageApi.Services;
using System.Net.Http;
using System.Text.Json;

public class FileStorageService : IFileStorageService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHttpClientFactory _httpClientFactory;

    public FileStorageService(IConfiguration configuration, HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ServiceResponse<string>> UploadFileAsync(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = ResponseMessages.FileNotSelected,
                    StatusCode = 400
                };
            }

            var uploadsPath = _configuration["UploadFolder"];
            Directory.CreateDirectory(uploadsPath!);

            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsPath!, uniqueFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return new ServiceResponse<string>
            {
                Data = uniqueFileName,
                Message = ResponseMessages.FileUploadSuccess,
                StatusCode = 201
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<string>
            {
                Success = false,
                Message = string.Format(ResponseMessages.FileUploadFail, ex.Message),
                StatusCode = 500
            };
        }
    }
    public async Task<ServiceResponse<byte[]>> DownloadFileAsync(int fileId, string fileName, int? userId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return new ServiceResponse<byte[]>
                {
                    Success = false,
                    Message = ResponseMessages.FileNameRequired,
                    StatusCode = 400
                };
            }

            var accessInfo = await GetAccessInfoAsync(userId.Value, fileId);
            if (accessInfo == null || !accessInfo.HasAccess)
            {
                return new ServiceResponse<byte[]>
                {
                    Success = false,
                    Message = ResponseMessages.FileAccessDenied,
                    StatusCode = 403
                };
            }

            var uploadsPath = _configuration["UploadFolder"];
            var filePath = Path.Combine(uploadsPath!, fileName);

            if (!File.Exists(filePath))
            {
                return new ServiceResponse<byte[]>
                {
                    Success = false,
                    Message = ResponseMessages.FileNotFound,
                    StatusCode = 404
                };
            }

            var fileBytes = await File.ReadAllBytesAsync(filePath);

            return new ServiceResponse<byte[]>
            {
                Data = fileBytes,
                Message = ResponseMessages.FileDownloadSuccess,
                StatusCode = 200
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<byte[]>
            {
                Success = false,
                Message = string.Format(ResponseMessages.FileDownloadFail, ex.Message),
                StatusCode = 500
            };
        }
    }


    private async Task<FileAccessResultDto?> GetAccessInfoAsync(int userId, int fileMetadataId)
    {
        var url = $"{ApiEndpoints.FileShares.CheckAccess}?userId={userId}&fileMetadataId={fileMetadataId}";

        try
        {
            var token = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", token);

            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ServiceResponse<FileAccessResultDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result?.Data;
        }
        catch
        {
            return null;
        }
    }
}

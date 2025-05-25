using System.Net.Http.Headers;
using CloudFileStorage.Common.Constants;
using CloudFileStorage.Common.Models;
using CloudFileStorage.UI.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

public class FileStorageService : IFileStorageService
{
    private readonly ApiRequestHelper _api;

    public FileStorageService(ApiRequestHelper api)
    {
        _api = api;
    }

    public Task<ServiceResponse<string>?> UploadAsync(IFormFile file, string token)
    {
        return _api.PostFileAsync<string>(ApiEndpoints.FileStorage.Upload, file, token);
    }

    public Task<ServiceResponse<byte[]>?> DownloadAsync(string fileName, string token)
    {
        var url = $"{ApiEndpoints.FileStorage.Download}?fileName={Uri.EscapeDataString(fileName)}";
        return _api.GetAsync<byte[]>(url, token);
    }
}

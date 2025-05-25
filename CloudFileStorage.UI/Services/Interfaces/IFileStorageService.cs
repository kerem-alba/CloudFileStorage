using CloudFileStorage.Common.Models;

public interface IFileStorageService
{
    Task<ServiceResponse<string>> UploadAsync(IFormFile file, string token);
    Task<ServiceResponse<byte[]>> DownloadAsync(string fileName, string token);
}

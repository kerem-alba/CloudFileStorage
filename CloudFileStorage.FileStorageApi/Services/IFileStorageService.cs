using CloudFileStorage.Common.Models;

namespace CloudFileStorage.FileStorageApi.Services
{
    public interface IFileStorageService
    {
        Task<ServiceResponse<string>> UploadFileAsync(IFormFile file);
        Task<ServiceResponse<byte[]>> DownloadFileAsync(int fileId, string fileName, int? userId);
    }
}

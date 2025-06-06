using CloudFileStorage.Common.Models;
using CloudFileStorage.UI.Models.DTOs;

namespace CloudFileStorage.UI.Services.Interfaces
{
    public interface IFileService
    {
        Task<ServiceResponse<List<FileMetadataDto>>?> GetAllAsync();
        Task<ServiceResponse<FileMetadataDto>?> GetByIdAsync(int id);
        Task<ServiceResponse<FileMetadataDto>?> GetAccessibleByIdAsync(int id);
        Task<ServiceResponse<FileMetadataDto>?> CreateAsync(CreateFileDto dto);
        Task<ServiceResponse<FileMetadataDto>?> UpdateAsync(int id, UpdateFileDto dto);
        Task<ServiceResponse<object>?> DeleteAsync(int id);
        Task<ServiceResponse<string>?> UploadAsync(IFormFile file);
        Task<ServiceResponse<byte[]>?> DownloadAsync(int fileId, string fileName);
    }
}

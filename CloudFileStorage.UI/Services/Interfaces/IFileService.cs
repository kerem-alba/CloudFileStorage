using CloudFileStorage.Common.Models;
using CloudFileStorage.UI.Models.DTOs;

namespace CloudFileStorage.UI.Services.Interfaces
{
    public interface IFileService
    {
        Task<ServiceResponse<List<FileMetadataDto>>?> GetAllAsync(string token);
        Task<ServiceResponse<FileMetadataDto>?> GetByIdAsync(int id, string token);
        Task<ServiceResponse<FileMetadataDto>?> CreateAsync(CreateFileDto dto, string token);
        Task<ServiceResponse<FileMetadataDto>?> UpdateAsync(int id, UpdateFileDto dto, string token);
        Task<ServiceResponse<bool>?> DeleteAsync(int id, string token);
    }
}

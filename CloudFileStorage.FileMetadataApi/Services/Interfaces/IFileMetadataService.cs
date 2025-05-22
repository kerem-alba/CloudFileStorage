using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.FileMetadataApi.Models.Entities;
using CloudFileStorage.Common.Models;

namespace CloudFileStorage.FileMetadataApi.Services.Interfaces
{
    public interface IFileMetadataService
    {
        Task<ServiceResponse<List<FileMetadata>>> GetAllFilesAsync(int ownerId);
        Task<ServiceResponse<FileMetadata?>> GetFileByIdAsync(int id, int ownerId);
        Task<ServiceResponse<string>> CreateFileAsync(CreateFileDto dto, int ownerId);
        Task<ServiceResponse<string>> UpdateFileAsync(int id, int ownerId, UpdateFileDto dto);
        Task<ServiceResponse<string>> DeleteFileAsync(int id, int ownerId);
    }
}

using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.FileMetadataApi.Models.Entities;
using CloudFileStorage.Common.Models;

namespace CloudFileStorage.FileMetadataApi.Services
{
    public interface IFileService
    {
        Task<ServiceResponse<string>> CreateFileAsync(CreateFileDto dto, int ownerId);
        Task<ServiceResponse<List<FileMetadata>>> GetAllFilesAsync(int ownerId);
        Task<ServiceResponse<FileMetadata?>> GetFileByIdAsync(int id, int ownerId);
        Task<ServiceResponse<string>> UpdateFileAsync(int id, int ownerId, CreateFileDto dto);
        Task<ServiceResponse<string>> DeleteFileAsync(int id, int ownerId);
    }
}

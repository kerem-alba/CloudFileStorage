using CloudFileStorage.Common.Models;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.FileMetadataApi.Models.Entities;

namespace CloudFileStorage.FileMetadataApi.Services.Interfaces
{
    public interface IFileShareMetadataService
    {
        Task<FileShareMetadata?> GetAsync(int userId, int fileMetadataId);
        Task<ServiceResponse<List<FileMetadataDto>>> GetFilesSharedWithUserAsync(int userId);
        Task<ServiceResponse<List<FileShareDto>>> GetFileSharesByFileIdAsync(int fileMetadataId);
        Task<ServiceResponse<string>> CreateAsync(CreateFileShareMetadataDto dto);
        Task<ServiceResponse<string>> CreateFileSharesAsync(int fileMetadataId, List<FileShareDto> shares);
        Task<ServiceResponse<string>> UpdateAsync(int id, UpdateFileShareMetadataDto dto);
        Task<ServiceResponse<string>> DeleteAsync(int id);
        Task<ServiceResponse<string>> DeleteByFileIdAsync(int fileMetadataId);
        Task<ServiceResponse<HasAccessResultDto>> GetAccessInfoAsync(int userId, int fileMetadataId);

    }
}

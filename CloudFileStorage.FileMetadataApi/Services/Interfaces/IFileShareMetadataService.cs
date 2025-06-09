using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.Common.Models;

namespace CloudFileStorage.FileMetadataApi.Services.Interfaces
{
    public interface IFileShareMetadataService
    {
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

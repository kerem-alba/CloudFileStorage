using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.Common.Models;

namespace CloudFileStorage.FileMetadataApi.Services.Interfaces
{
    public interface IFileShareMetadataService
    {
        Task<ServiceResponse<List<FileMetadataDto>>> GetFilesSharedWithUserAsync(int userId);
        Task<ServiceResponse<string>> CreateAsync(CreateFileShareMetadataDto dto);
        Task<ServiceResponse<string>> UpdateAsync(int id, UpdateFileShareMetadataDto dto);
        Task<ServiceResponse<string>> DeleteAsync(int id);
        Task<HasAccessResultDto> GetAccessInfoAsync(int userId, int fileMetadataId);

    }
}

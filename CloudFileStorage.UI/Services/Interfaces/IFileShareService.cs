using CloudFileStorage.Common.Models;
using CloudFileStorage.UI.Models.DTOs;

namespace CloudFileStorage.UI.Services.Interfaces
{
    public interface IFileShareService
    {
        Task<ServiceResponse<List<FileMetadataDto>>?> GetSharedWithMeAsync();
        Task<ServiceResponse<bool>?> ShareFileAsync(CreateFileShareMetadataDto dto);
        Task<ServiceResponse<bool>?> UpdateShareAsync(int id, UpdateFileShareMetadataDto dto);
        Task<ServiceResponse<bool>?> DeleteShareAsync(int id);
    }
}

using CloudFileStorage.Common.Models;
using CloudFileStorage.UI.Models.DTOs;

namespace CloudFileStorage.UI.Services.Interfaces
{
    public interface IFileShareService
    {
        Task<ServiceResponse<List<FileMetadataDto>>?> GetSharedWithMeAsync(string token);
        Task<ServiceResponse<bool>?> ShareFileAsync(CreateFileShareMetadataDto dto, string token);
        Task<ServiceResponse<bool>?> UpdateShareAsync(int id, UpdateFileShareMetadataDto dto, string token);
        Task<ServiceResponse<bool>?> DeleteShareAsync(int id, string token);
    }
}

using CloudFileStorage.Common.Models;
using CloudFileStorage.UI.Models.DTOs;

namespace CloudFileStorage.UI.Services.Interfaces
{
    public interface IFileShareService
    {
        Task<ServiceResponse<List<FileMetadataDto>>?> GetSharedWithMeAsync();
        Task<ServiceResponse<bool>?> ShareFileWithMultipleUsersAsync(int fileMetadataId, List<UserShareSelection> selectedUsers);

    }
}

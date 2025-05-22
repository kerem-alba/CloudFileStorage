using CloudFileStorage.FileMetadataApi.Models.Entities;

namespace CloudFileStorage.FileMetadataApi.Repositories.Interfaces
{
    public interface IFileShareMetadataRepository
    {
        Task<List<int>> GetFileIdsSharedWithUserAsync(int userId);
        Task AddAsync(FileShareMetadata entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, string newPermission);

    }
}

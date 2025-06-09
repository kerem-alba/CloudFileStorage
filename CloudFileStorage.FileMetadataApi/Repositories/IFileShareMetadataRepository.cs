using CloudFileStorage.Common.Enums;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.FileMetadataApi.Models.Entities;

namespace CloudFileStorage.FileMetadataApi.Repositories.Interfaces
{
    public interface IFileShareMetadataRepository
    {
        Task<List<FileMetadataDto>> GetSharedFileMetadataListAsync(int userId);
        Task<int?> GetFileOwnerIdAsync(int fileMetadataId);
        Task<List<FileShareMetadata>> GetByFileMetadataIdAsync(int fileMetadataId);
        Task<FileMetadata?> GetFileMetadataAsync(int fileMetadataId);
        Task<FileShareMetadata?> GetAsync(int userId, int fileMetadataId);
        Task AddAsync(FileShareMetadata entity);
        Task AddRangeAsync(List<FileShareMetadata> entities);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteByFileMetadataIdAsync(int fileMetadataId);
        Task<bool> UpdateAsync(int id, Permission newPermission);
        Task<bool> ExistsAsync(int fileMetadataId, int userId);

    }
}

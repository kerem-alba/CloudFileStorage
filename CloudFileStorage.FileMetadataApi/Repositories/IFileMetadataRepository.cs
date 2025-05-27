using CloudFileStorage.FileMetadataApi.Models.Entities;
using System.Threading.Tasks;

namespace CloudFileStorage.FileMetadataApi.Repositories
{
    public interface IFileMetadataRepository
    {
        Task AddAsync(FileMetadata file);
        Task<List<FileMetadata>> GetAllByOwnerIdAsync(int ownerId);
        Task<FileMetadata?> GetByIdAsync(int id);
        Task UpdateAsync(FileMetadata file);
        Task DeleteAsync(FileMetadata file);
    }
}

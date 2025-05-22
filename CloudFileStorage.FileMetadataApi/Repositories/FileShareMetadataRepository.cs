using CloudFileStorage.FileMetadataApi.Contexts;
using CloudFileStorage.FileMetadataApi.Models.Entities;
using CloudFileStorage.FileMetadataApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CloudFileStorage.FileMetadataApi.Repositories
{
    public class FileShareMetadataRepository : IFileShareMetadataRepository
    {
        private readonly FileMetadataDbContext _context;

        public FileShareMetadataRepository(FileMetadataDbContext context)
        {
            _context = context;
        }

        public async Task<List<int>> GetFileIdsSharedWithUserAsync(int userId)
        {
            return await _context.FileShareMetadatas
                .Where(fs => fs.UserId == userId)
                .Select(fs => fs.FileMetadataId)
                .ToListAsync();
        }

        public async Task AddAsync(FileShareMetadata entity)
        {
            _context.FileShareMetadatas.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(int id, string newPermission)
        {
            var entity = await _context.FileShareMetadatas.FindAsync(id);
            if (entity == null) return false;

            entity.Permission = newPermission;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.FileShareMetadatas.FindAsync(id);
            if (entity == null) return false;

            _context.FileShareMetadatas.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }



    }
}

using CloudFileStorage.FileMetadataApi.Contexts;
using CloudFileStorage.FileMetadataApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudFileStorage.FileMetadataApi.Repositories
{
    public class FileMetadataRepository : IFileMetadataRepository
    {
        private readonly FileMetadataDbContext _context;

        public FileMetadataRepository(FileMetadataDbContext context)
        {
            _context = context;
        }

        public async Task<List<FileMetadata>> GetAllByOwnerIdAsync(int ownerId)
        {
            return await _context.Files
                .Where(f => f.OwnerId == ownerId)
                .OrderByDescending(f => f.UploadDate)
                .ToListAsync();
        }

        public async Task<FileMetadata?> GetByIdAsync(int id, int ownerId)
        {
            return await _context.Files
                .FirstOrDefaultAsync(f => f.Id == id && f.OwnerId == ownerId);
        }
        public async Task AddAsync(FileMetadata file)
        {
            await _context.Files.AddAsync(file);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FileMetadata file)
        {
            _context.Files.Update(file);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(FileMetadata file)
        {
            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
        }
    }
}

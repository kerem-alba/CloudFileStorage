using CloudFileStorage.Common.Enums;
using CloudFileStorage.FileMetadataApi.Contexts;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
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

        public async Task<List<FileMetadataDto>> GetSharedFileMetadataListAsync(int userId)
        {
            var fileIds = await _context.FileShareMetadatas
                .Where(fs => fs.UserId == userId)
                .Select(fs => fs.FileMetadataId)
                .ToListAsync();

            return await _context.Files
                .Where(fm => fileIds.Contains(fm.Id))
                .Select(fm => new FileMetadataDto
                {
                    Id = fm.Id,
                    Name = fm.Name,
                    FileName = fm.FileName,
                    Description = fm.Description,
                    UploadDate = fm.UploadDate,
                    OwnerId = fm.OwnerId,
                    IsPublic = fm.IsPublic,
                }).ToListAsync();
        }



        public async Task<int?> GetFileOwnerIdAsync(int fileMetadataId)
        {
            return await _context.Files
                .Where(f => f.Id == fileMetadataId)
                .Select(f => (int?)f.OwnerId)
                .FirstOrDefaultAsync();
        }

        public async Task<FileMetadata?> GetFileMetadataAsync(int fileMetadataId)
        {
            return await _context.Files
                .FirstOrDefaultAsync(fm => fm.Id == fileMetadataId);
        }

        public async Task<List<FileShareMetadata>> GetByFileMetadataIdAsync(int fileMetadataId)
        {
            return await _context.FileShareMetadatas
                .Where(fs => fs.FileMetadataId == fileMetadataId)
                .ToListAsync();
        }


        public async Task<FileShareMetadata?> GetAsync(int userId, int fileMetadataId)
        {
            var result = await _context.FileShareMetadatas
                .FirstOrDefaultAsync(fs => fs.UserId == userId && fs.FileMetadataId == fileMetadataId);
            return result;
        }

        public async Task AddAsync(FileShareMetadata entity)
        {
            _context.FileShareMetadatas.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task AddRangeAsync(List<FileShareMetadata> entities)
        {
            await _context.FileShareMetadatas.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(int id, Permission newPermission)
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

        public async Task<bool> ExistsAsync(int fileMetadataId, int userId)
        {
            return await _context.FileShareMetadatas
                .AnyAsync(f => f.FileMetadataId == fileMetadataId && f.UserId == userId);
        }

        public async Task<bool> DeleteByFileMetadataIdAsync(int fileMetadataId)
        {
            var shares = await _context.FileShareMetadatas
                .Where(fs => fs.FileMetadataId == fileMetadataId)
                .ToListAsync();

            if (!shares.Any())
                return false;

            _context.FileShareMetadatas.RemoveRange(shares);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}

using Microsoft.EntityFrameworkCore;
using CloudFileStorage.FileMetadataApi.Models.Entities;
using System.Collections.Generic;

namespace CloudFileStorage.FileMetadataApi.Contexts
{
    public class FileMetadataDbContext : DbContext
    {
        public FileMetadataDbContext(DbContextOptions<FileMetadataDbContext> options) : base(options)
        {
        }
        public DbSet<FileMetadata> Files { get; set; }
    }
}

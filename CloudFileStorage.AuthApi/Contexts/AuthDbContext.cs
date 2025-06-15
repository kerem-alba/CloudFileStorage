using CloudFileStorage.AuthApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudFileStorage.AuthApi.Contexts
{
    public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
    }
}

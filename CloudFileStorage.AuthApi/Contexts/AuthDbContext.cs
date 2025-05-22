using CloudFileStorage.AuthApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CloudFileStorage.AuthApi.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
    }
}

using CloudFileStorage.AuthApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CloudFileStorage.AuthApi.Data
{
    public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
    }
}

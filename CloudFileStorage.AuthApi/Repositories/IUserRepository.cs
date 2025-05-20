using CloudFileStorage.AuthApi.Models;

namespace CloudFileStorage.AuthApi.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task SaveAsync();
    }
}

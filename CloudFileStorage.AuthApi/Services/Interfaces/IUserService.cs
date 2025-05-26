using CloudFileStorage.Common.Models;

namespace CloudFileStorage.AuthApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<string>> GetUserNameByIdAsync(int id);
    }
}
using CloudFileStorage.Common.Models;
using CloudFileStorage.UI.Models.DTOs;

namespace CloudFileStorage.UI.Services.Interfaces;

public interface IUserService
{
    Task<ServiceResponse<List<UserDto>>> GetAllUsersAsync();
    Task<ServiceResponse<string>> GetUserNameByIdAsync(int id);
}

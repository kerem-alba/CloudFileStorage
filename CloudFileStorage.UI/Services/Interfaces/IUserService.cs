using CloudFileStorage.Common.Models;
using CloudFileStorage.UI.Models.DTOs;

public interface IUserService
{
    Task<ServiceResponse<List<UserDto>>> GetAllUsersAsync(string token);
}

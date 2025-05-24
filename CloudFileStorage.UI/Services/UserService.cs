using CloudFileStorage.Common.Models;
using CloudFileStorage.UI.Helpers;
using CloudFileStorage.UI.Models.DTOs;

public class UserService : IUserService
{
    private readonly ApiRequestHelper _api;

    public UserService(ApiRequestHelper api)
    {
        _api = api;
    }

    public async Task<ServiceResponse<List<UserDto>>> GetAllUsersAsync(string token)
    {
        return await _api.GetAsync<List<UserDto>>("api/Users", token);
    }
}

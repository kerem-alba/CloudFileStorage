using CloudFileStorage.Common.Constants;
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

    public Task<ServiceResponse<List<UserDto>>> GetAllUsersAsync(string token)
    {
        return _api.GetAsync<List<UserDto>>(ApiEndpoints.Auth.GetAllUsers, token);
    }

    public Task<ServiceResponse<string>> GetUserNameByIdAsync(int id, string token)
    {
        var url = ApiEndpoints.Auth.GetUserNameById.Replace("{id}", id.ToString());
        return _api.GetAsync<string>(url, token);
    }
}

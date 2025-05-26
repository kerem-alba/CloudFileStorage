using CloudFileStorage.Common.Constants;
using CloudFileStorage.Common.Models;
using CloudFileStorage.UI.Helpers;
using CloudFileStorage.UI.Models.DTOs;

public class UserService : IUserService
{
    private readonly ApiRequestHelper _apiRequestHelper;

    public UserService(ApiRequestHelper apiRequestHelper)
    {
        _apiRequestHelper = apiRequestHelper;
    }

    public Task<ServiceResponse<List<UserDto>>> GetAllUsersAsync()
    {
        return _apiRequestHelper.GetAsync<List<UserDto>>(ApiEndpoints.Auth.GetAllUsers);
    }

    public Task<ServiceResponse<string>> GetUserNameByIdAsync(int id)
    {
        var url = ApiEndpoints.Auth.GetUserNameById.Replace("{id}", id.ToString());
        return _apiRequestHelper.GetAsync<string>(url);
    }
}

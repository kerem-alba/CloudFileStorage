using CloudFileStorage.Common.Constants;
using CloudFileStorage.Common.Models;
using CloudFileStorage.UI.Models;
using CloudFileStorage.UI.Models.DTOs;
using CloudFileStorage.UI.Services.Interfaces;
using CloudFileStorage.UI.Helpers;


namespace CloudFileStorage.UI.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApiRequestHelper _apiRequestHelper;

        public AuthService(ApiRequestHelper ApiRequestHelper)
        {
            _apiRequestHelper = ApiRequestHelper;
        }

        public Task<ServiceResponse<AuthResponseDto>?> LoginAsync(LoginUserDto dto)
        {
            return _apiRequestHelper.PostAsync<LoginUserDto, AuthResponseDto>(ApiEndpoints.Auth.Login, dto);
        }


        public Task<ServiceResponse<AuthResponseDto>?> RegisterAsync(RegisterUserDto dto)
        {
            return _apiRequestHelper.PostAsync<RegisterUserDto, AuthResponseDto>(ApiEndpoints.Auth.Register, dto);
        }

    }

}

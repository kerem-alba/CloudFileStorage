using CloudFileStorage.AuthApi.Repositories;
using CloudFileStorage.AuthApi.Services.Interfaces;
using CloudFileStorage.Common.Models;
using CloudFileStorage.Common.Constants;

namespace CloudFileStorage.AuthApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ServiceResponse<string>> GetUserNameByIdAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);

                if (user == null)
                {
                    return new ServiceResponse<string>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = ResponseMessages.UserNotFound,
                    };
                }

                return new ServiceResponse<string>
                {
                    Data = user.Name,
                    StatusCode = 200,
                    Message = ResponseMessages.UserFetched,
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }
    }

}

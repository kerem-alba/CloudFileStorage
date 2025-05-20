using AutoMapper;
using CloudFileStorage.AuthApi.Common;
using CloudFileStorage.AuthApi.Constants;
using CloudFileStorage.AuthApi.DTOs;
using CloudFileStorage.AuthApi.Helpers;
using CloudFileStorage.AuthApi.Models;
using CloudFileStorage.AuthApi.Repositories;

namespace CloudFileStorage.AuthApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository, IConfiguration config, IMapper mapper)
        {
            _userRepository = userRepository;
            _config = config;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<string>> RegisterAsync(RegisterUserDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = ResponseMessages.UserAlreadyExists,
                    StatusCode = 400
                };
            }

            var user = _mapper.Map<User>(dto);
            await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();

            return new ServiceResponse<string>
            {
                Message = ResponseMessages.UserCreated,
                StatusCode = 200
            };
        }

        public async Task<ServiceResponse<AuthResponseDto>> LoginAsync(LoginUserDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return new ServiceResponse<AuthResponseDto>
                {
                    Success = false,
                    Message = ResponseMessages.LoginFailed,
                    StatusCode = 401
                };
            }

            var token = JwtHelper.GenerateToken(user, _config);

            return new ServiceResponse<AuthResponseDto>
            {
                Data = new AuthResponseDto { Token = token },
                Message = ResponseMessages.LoginSuccess,
                StatusCode = 200
            };
        }
    }
}

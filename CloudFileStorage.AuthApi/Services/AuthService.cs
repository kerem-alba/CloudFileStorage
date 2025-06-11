using AutoMapper;
using CloudFileStorage.AuthApi.Common.Enums;
using CloudFileStorage.AuthApi.Models.DTOs;
using CloudFileStorage.AuthApi.Models.Entities;
using CloudFileStorage.AuthApi.Repositories;
using CloudFileStorage.AuthApi.Services.Interfaces;
using CloudFileStorage.Common.Constants;
using CloudFileStorage.Common.Models;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public AuthService(IUserRepository userRepository, ITokenService tokenService, IMapper mapper)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<AuthResponseDto>> RegisterAsync(RegisterUserDto dto)
    {
        try
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                return new ServiceResponse<AuthResponseDto>
                {
                    Success = false,
                    Message = ResponseMessages.UserAlreadyExists,
                    StatusCode = 400
                };
            }

            var user = _mapper.Map<User>(dto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            user.Role = UserRole.User;

            await _userRepository.AddAsync(user);

            var (accessToken, refreshToken, refreshExpire) = _tokenService.GenerateTokens(user);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireDate = refreshExpire;

            return new ServiceResponse<AuthResponseDto>
            {
                Data = new AuthResponseDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                },
                Message = ResponseMessages.RegisterSuccess,
                StatusCode = 201
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<AuthResponseDto>
            {
                Success = false,
                StatusCode = 500,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<AuthResponseDto>> LoginAsync(LoginUserDto dto)
    {
        try
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

            var (accessToken, refreshToken, refreshExpire) = _tokenService.GenerateTokens(user);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireDate = refreshExpire;

            await _userRepository.UpdateAsync(user);

            return new ServiceResponse<AuthResponseDto>
            {
                Data = new AuthResponseDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                },
                Message = ResponseMessages.LoginSuccess,
                StatusCode = 200
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<AuthResponseDto>
            {
                Success = false,
                StatusCode = 500,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<AuthResponseDto>> RefreshTokenAsync(string refreshToken)
    {
        try
        {
            var user = await _userRepository.GetByRefreshTokenAsync(refreshToken);

            if (user == null || user.RefreshTokenExpireDate == null || user.RefreshTokenExpireDate < DateTime.Now)
            {
                return new ServiceResponse<AuthResponseDto>
                {
                    Success = false,
                    Message = ResponseMessages.RefreshTokenInvalid,
                    StatusCode = 401
                };
            }

            var (newAccessToken, newRefreshToken, newRefreshExpire) = _tokenService.GenerateTokens(user);

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpireDate = newRefreshExpire;

            await _userRepository.UpdateAsync(user);

            return new ServiceResponse<AuthResponseDto>
            {
                Data = new AuthResponseDto
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                },
                Message = ResponseMessages.RefreshTokenSuccess,
                StatusCode = 200
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<AuthResponseDto>
            {
                Success = false,
                StatusCode = 500,
                Message = ex.Message
            };
        }
    }
}

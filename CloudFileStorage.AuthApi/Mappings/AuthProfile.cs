using AutoMapper;
using CloudFileStorage.AuthApi.DTOs;
using CloudFileStorage.AuthApi.Enums;
using CloudFileStorage.AuthApi.Models;

namespace CloudFileStorage.AuthApi.Mappings
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterUserDto, User>()
                .ForMember(user => user.PasswordHash, opt => opt.MapFrom(dto => BCrypt.Net.BCrypt.HashPassword(dto.Password)))
                .ForMember(user => user.Role, opt => opt.MapFrom(dto => UserRole.User));
        }
    }
}

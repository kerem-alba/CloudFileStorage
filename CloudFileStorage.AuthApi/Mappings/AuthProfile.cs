using AutoMapper;
using CloudFileStorage.AuthApi.Common.Enums;
using CloudFileStorage.AuthApi.Models.DTOs;
using CloudFileStorage.AuthApi.Models.Entities;

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

using AutoMapper;
using CloudFileStorage.UI.Models;
using CloudFileStorage.UI.Models.DTOs;

namespace CloudFileStorage.UI.Mappings
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterViewModel, RegisterUserDto>();
            CreateMap<LoginViewModel, LoginUserDto>();
        }
    }
}

using AutoMapper;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.FileMetadataApi.Models.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CloudFileStorage.FileMetadataApi.Mappings
{
    public class FileMappingProfile : Profile
    {
        public FileMappingProfile()
        {
            CreateMap<CreateFileDto, FileMetadata>();
        }
    }
}

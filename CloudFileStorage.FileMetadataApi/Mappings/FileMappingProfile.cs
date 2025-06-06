using AutoMapper;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.FileMetadataApi.Models.Entities;

namespace CloudFileStorage.FileMetadataApi.Mappings
{
    public class FileMappingProfile : Profile
    {
        public FileMappingProfile()
        {
            CreateMap<CreateFileDto, FileMetadata>();
            CreateMap<UpdateFileDto, FileMetadata>();
            CreateMap<CreateFileShareMetadataDto, FileShareMetadata>();
            CreateMap<FileMetadata, FileMetadataDto>();
        }
    }
}

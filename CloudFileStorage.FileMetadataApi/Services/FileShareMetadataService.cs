using AutoMapper;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.FileMetadataApi.Models.Entities;
using CloudFileStorage.FileMetadataApi.Services.Interfaces;
using CloudFileStorage.Common.Models;
using CloudFileStorage.Common.Constants;
using CloudFileStorage.FileMetadataApi.Repositories.Interfaces;

namespace CloudFileStorage.FileMetadataApi.Services
{
    public class FileShareMetadataService : IFileShareMetadataService
    {
        private readonly IMapper _mapper;
        private readonly IFileShareMetadataRepository _repository;

        public FileShareMetadataService(IMapper mapper, IFileShareMetadataRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ServiceResponse<List<FileMetadataDto>>> GetFilesSharedWithUserAsync(int userId)
        {
            try
            {
                var files = await _repository.GetSharedFileMetadataListAsync(userId);

                return new ServiceResponse<List<FileMetadataDto>>
                {
                    Data = files,
                    StatusCode = 200,
                    Message = files.Any()
                        ? ResponseMessages.FileShareFetched
                        : ResponseMessages.NoFilesSharedWithYou
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<FileMetadataDto>>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }


        public async Task<HasAccessResultDto> GetAccessInfoAsync(int userId, int fileMetadataId)
        {
            var ownerId = await _repository.GetFileOwnerIdAsync(fileMetadataId);
            if (ownerId == userId)
            {
                return new HasAccessResultDto
                {
                    HasAccess = true,
                    Permission = "Edit"
                };
            }

            var entity = await _repository.GetAsync(userId, fileMetadataId);

            return new HasAccessResultDto
            {
                HasAccess = entity != null,
                Permission = entity?.Permission.ToString()
            };
        }

        public async Task<ServiceResponse<string>> CreateAsync(CreateFileShareMetadataDto dto)
        {
            try
            {
                var entity = new FileShareMetadata
                {
                    FileMetadataId = dto.FileMetadataId,
                    UserId = dto.UserId,
                    Permission = dto.Permission
                };

                await _repository.AddAsync(entity);

                return new ServiceResponse<string>
                {
                    Success = true,
                    StatusCode = 201,
                    Message = ResponseMessages.FileShareCreated
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = string.Format(ResponseMessages.FileShareFailed, ex.Message)
                };
            }
        }



        public async Task<ServiceResponse<string>> UpdateAsync(int id, UpdateFileShareMetadataDto dto)
        {
            try
            {
                var updated = await _repository.UpdateAsync(id, dto.Permission);
                if (!updated)
                {
                    return new ServiceResponse<string>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = ResponseMessages.FileShareNotFound
                    };
                }

                return new ServiceResponse<string>
                {
                    Message = ResponseMessages.FileShareUpdated
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

        public async Task<ServiceResponse<string>> DeleteAsync(int id)
        {
            try
            {
                var result = await _repository.DeleteAsync(id);
                if (!result)
                {
                    return new ServiceResponse<string>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = ResponseMessages.FileShareNotFound
                    };
                }

                return new ServiceResponse<string>
                {
                    Message = ResponseMessages.FileShareDeleted
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

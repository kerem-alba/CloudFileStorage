using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.FileMetadataApi.Models.Entities;
using CloudFileStorage.FileMetadataApi.Services.Interfaces;
using CloudFileStorage.Common.Models;
using CloudFileStorage.Common.Constants;
using CloudFileStorage.FileMetadataApi.Repositories;

namespace CloudFileStorage.FileMetadataApi.Services
{
    public class FileShareMetadataService : IFileShareMetadataService
    {
        private readonly IFileShareMetadataRepository _repository;

        public FileShareMetadataService(IFileShareMetadataRepository repository)
        {
            _repository = repository;
        }

        public async Task<FileShareMetadata?> GetAsync(int userId, int fileMetadataId)
        {
            return await _repository.GetAsync(userId, fileMetadataId);
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

        public async Task<ServiceResponse<List<FileShareDto>>> GetFileSharesByFileIdAsync(int fileMetadataId)
        {
            var shares = await _repository.GetByFileMetadataIdAsync(fileMetadataId);

            var dtoList = shares.Select(s => new FileShareDto
            {
                UserId = s.UserId,
                Permission = s.Permission
            }).ToList();

            return new ServiceResponse<List<FileShareDto>>
            {
                Success = true,
                StatusCode = 200,
                Message = ResponseMessages.FileSharesRetrieved,
                Data = dtoList
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

        public async Task<ServiceResponse<string>> CreateFileSharesAsync(int fileMetadataId, List<FileShareDto> shares)
        {
            try
            {
                var entities = shares.Select(s => new FileShareMetadata
                {
                    FileMetadataId = fileMetadataId,
                    UserId = s.UserId,
                    Permission = s.Permission
                }).ToList();

                await _repository.AddRangeAsync(entities);

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

        public async Task<ServiceResponse<string>> DeleteByFileIdAsync(int fileMetadataId)
        {
            try
            {
                var result = await _repository.DeleteByFileMetadataIdAsync(fileMetadataId);
                return new ServiceResponse<string>
                {
                    Success = result,
                    StatusCode = result ? 200 : 404,
                    Message = result ? ResponseMessages.FileShareDeleted : ResponseMessages.FileShareNotFound
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


        public async Task<ServiceResponse<HasAccessResultDto>> GetAccessInfoAsync(int userId, int fileMetadataId)
        {
            try
            {
                var file = await _repository.GetFileMetadataAsync(fileMetadataId);
                if (file == null)
                {
                    return new ServiceResponse<HasAccessResultDto>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = ResponseMessages.FileNotFound
                    };
                }
                if (file.IsPublic)
                {
                    return new ServiceResponse<HasAccessResultDto>
                    {
                        Success = true,
                        StatusCode = 200,
                        Data = new HasAccessResultDto
                        {
                            HasAccess = true,
                            Permission = file.Permission.ToString()
                        }
                    };
                }

                if (file.OwnerId == userId)
                {
                    return new ServiceResponse<HasAccessResultDto>
                    {
                        Success = true,
                        StatusCode = 200,
                        Data = new HasAccessResultDto
                        {
                            HasAccess = true,
                            Permission = "Edit"
                        }
                    };
                }

                var entity = await GetAsync(userId, fileMetadataId);
                if (entity == null)
                {
                    return new ServiceResponse<HasAccessResultDto>
                    {
                        Success = false,
                        StatusCode = 403,
                        Message = ResponseMessages.FileAccessDenied,
                        Data = new HasAccessResultDto
                        {
                            HasAccess = false,
                            Permission = null
                        }
                    };
                }

                return new ServiceResponse<HasAccessResultDto>
                {
                    Success = true,
                    StatusCode = 200,
                    Data = new HasAccessResultDto
                    {
                        HasAccess = true,
                        Permission = entity.Permission.ToString()
                    }
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<HasAccessResultDto>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }
    }
}

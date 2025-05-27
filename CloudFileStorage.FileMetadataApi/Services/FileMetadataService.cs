using AutoMapper;
using CloudFileStorage.Common.Models;
using CloudFileStorage.Common.Constants;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.FileMetadataApi.Models.Entities;
using CloudFileStorage.FileMetadataApi.Repositories;
using CloudFileStorage.FileMetadataApi.Services.Interfaces;
using CloudFileStorage.Common.Enums;

namespace CloudFileStorage.FileMetadataApi.Services
{
    public class FilemetadataService : IFileMetadataService
    {
        private readonly IFileMetadataRepository _repository;
        private readonly IMapper _mapper;

        public FilemetadataService(IFileMetadataRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<FileMetadata>>> GetAllFilesAsync(int ownerId)
        {
            try
            {
                var files = await _repository.GetAllByOwnerIdAsync(ownerId);

                return new ServiceResponse<List<FileMetadata>>
                {
                    Data = files,
                    StatusCode = 200,
                    Message = files.Any()
                        ? ResponseMessages.FilesFetched
                        : ResponseMessages.NoFilesFound
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<FileMetadata>>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<FileMetadata?>> GetFileByIdAsync(int id)
        {
            try
            {
                var file = await _repository.GetByIdAsync(id);

                if (file == null)
                {
                    return new ServiceResponse<FileMetadata?>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = ResponseMessages.FileNotFound
                    };
                }

                return new ServiceResponse<FileMetadata?>
                {
                    Data = file
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<FileMetadata?>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<string>> CreateFileAsync(CreateFileDto dto, int ownerId)
        {
            try
            {
                var file = _mapper.Map<FileMetadata>(dto);
                file.OwnerId = ownerId;
                file.UploadDate = DateTime.UtcNow;
                file.IsPublic = dto.ShareType == ShareType.Public;
                file.Permission = dto.Permission ?? Permission.Edit;


                await _repository.AddAsync(file);

                return new ServiceResponse<string>
                {
                    Message = ResponseMessages.FileCreated
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

        public async Task<ServiceResponse<string>> UpdateFileAsync(int id, int ownerId, UpdateFileDto dto)
        {
            try
            {
                var file = await _repository.GetByIdAsync(id);
                if (file == null)
                {
                    return new ServiceResponse<string>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = ResponseMessages.FileNotFound
                    };
                }

                if (file.OwnerId != ownerId && (!file.IsPublic || file.Permission != Permission.Edit))
                {
                    return new ServiceResponse<string>
                    {
                        Success = false,
                        StatusCode = 403,
                        Message = ResponseMessages.FileUpdateNotAllowed
                    };
                }

                _mapper.Map(dto, file);
                await _repository.UpdateAsync(file);

                return new ServiceResponse<string>
                {
                    Message = ResponseMessages.FileUpdated
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

        public async Task<ServiceResponse<string>> DeleteFileAsync(int id)
        {
            try
            {
                var file = await _repository.GetByIdAsync(id);
                if (file == null)
                {
                    return new ServiceResponse<string>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = ResponseMessages.FileNotFound
                    };
                }

                await _repository.DeleteAsync(file);

                return new ServiceResponse<string>
                {
                    Message = ResponseMessages.FileDeleted
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

        public async Task<ServiceResponse<FileMetadataDto>> GetAccessibleByIdAsync(int fileId, int userId)
        {
            try
            {
                var file = await _repository.GetByIdAsync(fileId);
                if (file == null)
                {
                    return new ServiceResponse<FileMetadataDto>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = ResponseMessages.FileNotFound
                    };
                }

                if (file.OwnerId == userId)
                {
                    var dto = _mapper.Map<FileMetadataDto>(file);
                    dto.Permission = Permission.Edit;
                    return new ServiceResponse<FileMetadataDto>
                    {
                        Data = dto,
                        StatusCode = 200,
                        Message = ResponseMessages.FileFetched
                    };
                }

                if (file.IsPublic)
                {
                    var dto = _mapper.Map<FileMetadataDto>(file);
                    dto.Permission = file.Permission;
                    return new ServiceResponse<FileMetadataDto>
                    {
                        Data = dto,
                        StatusCode = 200,
                        Message = ResponseMessages.FileFetched
                    };
                }


                return new ServiceResponse<FileMetadataDto>
                {
                    Success = false,
                    StatusCode = 403,
                    Message = ResponseMessages.FileAccessDenied
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<FileMetadataDto>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }


    }
}

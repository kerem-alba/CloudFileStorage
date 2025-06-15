using AutoMapper;
using CloudFileStorage.Common.Constants;
using CloudFileStorage.Common.Enums;
using CloudFileStorage.Common.Models;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.FileMetadataApi.Models.Entities;
using CloudFileStorage.FileMetadataApi.Repositories;
using CloudFileStorage.FileMetadataApi.Repositories.Interfaces;
using CloudFileStorage.FileMetadataApi.Services.Interfaces;

namespace CloudFileStorage.FileMetadataApi.Services
{
    public class FileMetadataService : IFileMetadataService
    {
        private readonly IFileMetadataRepository _fileRepository;
        private readonly IMapper _mapper;
        private readonly IFileShareMetadataService _fileShareService;

        public FileMetadataService(IFileMetadataRepository fileMetadataRepository, IMapper mapper, IFileShareMetadataService fileShareService)
        {
            _fileRepository = fileMetadataRepository;
            _mapper = mapper;
            _fileShareService = fileShareService;
        }

        public async Task<ServiceResponse<List<FileMetadata>>> GetAllFilesAsync(int ownerId)
        {
            try
            {
                var files = await _fileRepository.GetAllByOwnerIdAsync(ownerId);

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
                var file = await _fileRepository.GetByIdAsync(id);

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

        public async Task<ServiceResponse<FileMetadataDto>> CreateFileAsync(CreateFileDto dto, int ownerId)
        {
            try
            {
                var file = _mapper.Map<FileMetadata>(dto);
                file.OwnerId = ownerId;
                file.UploadDate = DateTime.UtcNow;
                file.IsPublic = dto.ShareType == ShareType.Public;
                file.Permission = dto.Permission ?? Permission.Edit;


                await _fileRepository.AddAsync(file);

                var fileDto = _mapper.Map<FileMetadataDto>(file);

                return new ServiceResponse<FileMetadataDto>
                {
                    Success = true,
                    Data = fileDto,
                    Message = ResponseMessages.FileCreated,
                    StatusCode = 201
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

        public async Task<ServiceResponse<string>> UpdateFileAsync(int id, int userId, UpdateFileDto dto)
        {
            try
            {
                var file = await _fileRepository.GetByIdAsync(id);
                if (file == null)
                {
                    return new ServiceResponse<string>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = ResponseMessages.FileNotFound
                    };
                }

                // public değil, dosya sahibi değil, edit yetkisi yok
                if (!file.IsPublic && file.OwnerId != userId)
                {
                    var access = await _fileShareService.GetAccessInfoAsync(userId, id);
                    if (!access.Success || !access.Data.HasAccess || access.Data.Permission != "Edit")
                    {
                        return new ServiceResponse<string>
                        {
                            Success = false,
                            StatusCode = 403,
                            Message = ResponseMessages.FileUpdateNotAllowed
                        };
                    }
                }
                // public, dosya sahibi değil, dosya readonly
                else if (file.IsPublic && file.Permission == Permission.ReadOnly && file.OwnerId != userId)
                {
                    return new ServiceResponse<string>
                    {
                        Success = false,
                        StatusCode = 403,
                        Message = ResponseMessages.FileUpdateNotAllowed
                    };
                }

                _mapper.Map(dto, file);
                await _fileRepository.UpdateAsync(file);

                if (dto.ShareType == ShareType.Specific && dto.SelectedUsers != null)
                {
                    await _fileShareService.DeleteByFileIdAsync(id);
                    var newShares = dto.SelectedUsers.Select(u => new FileShareDto
                    {
                        UserId = u.UserId,
                        Permission = u.Permission
                    }).ToList();

                    var shareResult = await _fileShareService.CreateFileSharesAsync(id, newShares);

                    if (!shareResult.Success)
                    {
                        return new ServiceResponse<string>
                        {
                            Success = false,
                            StatusCode = 500,
                            Message = ResponseMessages.FileShareFailed
                        };
                    }
                }

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
                var file = await _fileRepository.GetByIdAsync(id);
                if (file == null)
                {
                    return new ServiceResponse<string>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = ResponseMessages.FileNotFound
                    };
                }

                await _fileRepository.DeleteAsync(file);

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
                var file = await _fileRepository.GetByIdAsync(fileId);
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
                    return new ServiceResponse<FileMetadataDto>
                    {
                        Data = dto,
                        StatusCode = 200,
                        Message = ResponseMessages.FileFetched
                    };
                }

                var shared = await _fileShareService.GetAsync(userId, fileId);

                if (shared is not null)
                {
                    var dto = _mapper.Map<FileMetadataDto>(file);
                    dto.Permission = shared.Permission;
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

using AutoMapper;
using CloudFileStorage.Common.Constants;
using CloudFileStorage.Common.Enums;
using CloudFileStorage.Common.Models;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.FileMetadataApi.Models.Entities;
using CloudFileStorage.FileMetadataApi.Repositories;
using CloudFileStorage.FileMetadataApi.Services.Interfaces;

namespace CloudFileStorage.FileMetadataApi.Services
{
    public class FileMetadataService(
        IFileMetadataRepository fileMetadataRepository,
        IMapper mapper,
        IFileShareMetadataService fileShareService)
        : IFileMetadataService
    {
        public async Task<ServiceResponse<List<FileMetadata>>> GetAllFilesAsync(int ownerId)
        {
            try
            {
                var files = await fileMetadataRepository.GetAllByOwnerIdAsync(ownerId);

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
                var file = await fileMetadataRepository.GetByIdAsync(id);

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
                var file = mapper.Map<FileMetadata>(dto);
                file.OwnerId = ownerId;
                file.UploadDate = DateTime.UtcNow;
                file.IsPublic = dto.ShareType == ShareType.Public;
                file.Permission = dto.Permission ?? Permission.Edit;


                await fileMetadataRepository.AddAsync(file);

                var fileDto = mapper.Map<FileMetadataDto>(file);

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
                var file = await fileMetadataRepository.GetByIdAsync(id);
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
                    var access = await fileShareService.GetAccessInfoAsync(userId, id);
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

                mapper.Map(dto, file);
                await fileMetadataRepository.UpdateAsync(file);

                if (dto.ShareType == ShareType.Specific && dto.SelectedUsers != null)
                {
                    await fileShareService.DeleteByFileIdAsync(id);
                    var newShares = dto.SelectedUsers.Select(u => new FileShareDto
                    {
                        UserId = u.UserId,
                        Permission = u.Permission
                    }).ToList();

                    var shareResult = await fileShareService.CreateFileSharesAsync(id, newShares);

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
                var file = await fileMetadataRepository.GetByIdAsync(id);
                if (file == null)
                {
                    return new ServiceResponse<string>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = ResponseMessages.FileNotFound
                    };
                }

                await fileMetadataRepository.DeleteAsync(file);

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
                var file = await fileMetadataRepository.GetByIdAsync(fileId);
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
                    var dto = mapper.Map<FileMetadataDto>(file);
                    return new ServiceResponse<FileMetadataDto>
                    {
                        Data = dto,
                        StatusCode = 200,
                        Message = ResponseMessages.FileFetched
                    };
                }

                var shared = await fileShareService.GetAsync(userId, fileId);

                if (shared is not null)
                {
                    var dto = mapper.Map<FileMetadataDto>(file);
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
                    var dto = mapper.Map<FileMetadataDto>(file);
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

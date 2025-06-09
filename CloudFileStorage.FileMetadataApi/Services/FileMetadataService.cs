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
    public class FileMetadataService : IFileMetadataService
    {
        private readonly IFileMetadataRepository _repository;
        private readonly IMapper _mapper;
        private readonly IFileShareMetadataService _fileShareService;

        public FileMetadataService(IFileMetadataRepository repository, IMapper mapper, IFileShareMetadataService fileShareService)
        {
            _repository = repository;
            _mapper = mapper;
            _fileShareService = fileShareService;
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

        public async Task<ServiceResponse<FileMetadataDto>> CreateFileAsync(CreateFileDto dto, int ownerId)
        {
            try
            {
                var file = _mapper.Map<FileMetadata>(dto);
                file.OwnerId = ownerId;
                file.UploadDate = DateTime.UtcNow;
                file.IsPublic = dto.ShareType == ShareType.Public;
                file.Permission = dto.Permission ?? Permission.Edit;


                await _repository.AddAsync(file);

                var fileDto = _mapper.Map<FileMetadataDto>(file);

                return new ServiceResponse<FileMetadataDto>
                {
                    Success = true,
                    Data = fileDto,
                    Message = ResponseMessages.FileCreated
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

                // 1. Eğer private ise sadece sahibi düzenleyebilir
                if (!file.IsPublic && file.OwnerId != userId)
                {
                    // Kullanıcı özel yetkili mi kontrol et
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
                // 2. Eğer public ve readonly ise yine düzenlenemez
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
                await _repository.UpdateAsync(file);

                if (dto.ShareType == ShareType.Specific && dto.SelectedUsers != null)
                {
                    // Önce tüm mevcut paylaşım kayıtlarını sil
                    await _fileShareService.DeleteByFileIdAsync(id);

                    // Yeni paylaşım kayıtlarını ekle
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

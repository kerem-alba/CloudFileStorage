using AutoMapper;
using CloudFileStorage.Common.Models;
using CloudFileStorage.Common.Constants;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.FileMetadataApi.Models.Entities;
using CloudFileStorage.FileMetadataApi.Repositories;
using CloudFileStorage.FileMetadataApi.Services.Interfaces;

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

        public async Task<ServiceResponse<FileMetadata?>> GetFileByIdAsync(int id, int ownerId)
        {
            try
            {
                var file = await _repository.GetByIdAsync(id, ownerId);

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
                var file = await _repository.GetByIdAsync(id, ownerId);
                if (file == null)
                {
                    return new ServiceResponse<string>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = ResponseMessages.FileNotFound
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

        public async Task<ServiceResponse<string>> DeleteFileAsync(int id, int ownerId)
        {
            try
            {
                var file = await _repository.GetByIdAsync(id, ownerId);
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
    }
}

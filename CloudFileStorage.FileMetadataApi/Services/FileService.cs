using AutoMapper;
using CloudFileStorage.Common.Models;
using CloudFileStorage.Common.Constants;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.FileMetadataApi.Models.Entities;
using CloudFileStorage.FileMetadataApi.Repositories;

namespace CloudFileStorage.FileMetadataApi.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _repository;
        private readonly IMapper _mapper;

        public FileService(IFileRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<string>> CreateFileAsync(CreateFileDto dto, int ownerId)
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

        public async Task<ServiceResponse<List<FileMetadata>>> GetAllFilesAsync(int ownerId)
        {
            var files = await _repository.GetAllByOwnerIdAsync(ownerId);

            return new ServiceResponse<List<FileMetadata>>
            {
                Data = files
            };
        }

        public async Task<ServiceResponse<FileMetadata?>> GetFileByIdAsync(int id, int ownerId)
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

        public async Task<ServiceResponse<string>> UpdateFileAsync(int id, int ownerId, CreateFileDto dto)
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

            file.Name = dto.Name;
            file.Description = dto.Description;

            await _repository.UpdateAsync(file);

            return new ServiceResponse<string>
            {
                Message = ResponseMessages.FileUpdated
            };
        }

        public async Task<ServiceResponse<string>> DeleteFileAsync(int id, int ownerId)
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
    }
}

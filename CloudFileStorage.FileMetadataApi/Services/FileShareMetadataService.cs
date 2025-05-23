﻿using AutoMapper;
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

        public async Task<ServiceResponse<List<int>>> GetFilesSharedWithUserAsync(int userId)
        {
            try
            {
                var fileIds = await _repository.GetFileIdsSharedWithUserAsync(userId);

                return new ServiceResponse<List<int>>
                {
                    Data = fileIds,
                    StatusCode = 200,
                    Message = fileIds.Any()
                        ? ResponseMessages.FileShareFetched
                        : ResponseMessages.NoFilesSharedWithYou
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<int>>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<string>> CreateAsync(CreateFileShareMetadataDto dto)
        {
            try
            {
                var entity = _mapper.Map<FileShareMetadata>(dto);
                await _repository.AddAsync(entity);

                return new ServiceResponse<string>
                {
                    Message = ResponseMessages.FileShareCreated,
                    StatusCode = 201
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

using CloudFileStorage.Common.Constants;
using CloudFileStorage.Common.Models;
using CloudFileStorage.UI.Helpers;
using CloudFileStorage.UI.Models.DTOs;
using CloudFileStorage.UI.Services.Interfaces;

namespace CloudFileStorage.UI.Services
{
    public class FileService : IFileService
    {
        private readonly ApiRequestHelper _api;

        public FileService(ApiRequestHelper api)
        {
            _api = api;
        }

        public Task<ServiceResponse<List<FileMetadataDto>>?> GetAllAsync(string token)
        {
            return _api.GetAsync<List<FileMetadataDto>>(ApiEndpoints.FileMetadata.GetAll, token);
        }

        public Task<ServiceResponse<FileMetadataDto>?> GetByIdAsync(int id, string token)
        {
            var url = ApiEndpoints.FileMetadata.GetById.Replace("{id}", id.ToString());
            return _api.GetAsync<FileMetadataDto>(url, token);
        }

        public Task<ServiceResponse<FileMetadataDto>?> CreateAsync(CreateFileDto dto, string token)
        {
            return _api.PostAsync<CreateFileDto, FileMetadataDto>(ApiEndpoints.FileMetadata.Create, dto, token);
        }

        public Task<ServiceResponse<FileMetadataDto>?> UpdateAsync(int id, UpdateFileDto dto, string token)
        {
            var url = ApiEndpoints.FileMetadata.Update.Replace("{id}", id.ToString());
            return _api.PutAsync<UpdateFileDto, FileMetadataDto>(url, dto, token);
        }

        public Task<ServiceResponse<bool>?> DeleteAsync(int id, string token)
        {
            var url = ApiEndpoints.FileMetadata.Delete.Replace("{id}", id.ToString());
            return _api.DeleteAsync<bool>(url, token);
        }
    }
}

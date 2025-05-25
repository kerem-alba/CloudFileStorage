using CloudFileStorage.Common.Constants;
using CloudFileStorage.Common.Models;
using CloudFileStorage.UI.Helpers;
using CloudFileStorage.UI.Models.DTOs;
using CloudFileStorage.UI.Services.Interfaces;

namespace CloudFileStorage.UI.Services
{
    public class FileService : IFileService
    {
        private readonly ApiRequestHelper _apiRequestHelper;

        public FileService(ApiRequestHelper ApiRequestHelper)
        {
            _apiRequestHelper = ApiRequestHelper;
        }

        public Task<ServiceResponse<List<FileMetadataDto>>?> GetAllAsync(string token)
        {
            return _apiRequestHelper.GetAsync<List<FileMetadataDto>>(ApiEndpoints.FileMetadata.GetAll, token);
        }

        public Task<ServiceResponse<FileMetadataDto>?> GetByIdAsync(int id, string token)
        {
            var url = ApiEndpoints.FileMetadata.GetById.Replace("{id}", id.ToString());
            return _apiRequestHelper.GetAsync<FileMetadataDto>(url, token);
        }

        public Task<ServiceResponse<FileMetadataDto>?> CreateAsync(CreateFileDto dto, string token)
        {
            return _apiRequestHelper.PostAsync<CreateFileDto, FileMetadataDto>(ApiEndpoints.FileMetadata.Create, dto, token);
        }

        public Task<ServiceResponse<FileMetadataDto>?> UpdateAsync(int id, UpdateFileDto dto, string token)
        {
            var url = ApiEndpoints.FileMetadata.Update.Replace("{id}", id.ToString());
            return _apiRequestHelper.PutAsync<UpdateFileDto, FileMetadataDto>(url, dto, token);
        }

        public Task<ServiceResponse<object>?> DeleteAsync(int id, string token)
        {
            var url = ApiEndpoints.FileMetadata.Delete.Replace("{id}", id.ToString());
            return _apiRequestHelper.DeleteAsync<object>(url, token);
        }

        public Task<ServiceResponse<string>?> UploadAsync(IFormFile file, string token)
        {
            return _apiRequestHelper.PostFileAsync<string>(ApiEndpoints.FileStorage.Upload, file, token);
        }

        public Task<ServiceResponse<byte[]>?> DownloadAsync(string fileName, string token)
        {
            var encodedFileName = Uri.EscapeDataString(fileName);
            var url = ApiEndpoints.FileStorage.Download + $"?fileName={encodedFileName}";
            return _apiRequestHelper.GetFileAsync(url, token);
        }



    }
}

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
        private readonly FileRequestHelper _fileRequestHelper;

        public FileService(ApiRequestHelper apiRequestHelper, FileRequestHelper fileRequestHelper)
        {
            _apiRequestHelper = apiRequestHelper;
            _fileRequestHelper = fileRequestHelper;
        }

        public Task<ServiceResponse<List<FileMetadataDto>>?> GetAllAsync()
        {
            return _apiRequestHelper.GetAsync<List<FileMetadataDto>>(ApiEndpoints.FileMetadata.GetAll);
        }

        public Task<ServiceResponse<FileMetadataDto>?> GetByIdAsync(int id)
        {
            var url = ApiEndpoints.FileMetadata.GetById.Replace("{id}", id.ToString());
            return _apiRequestHelper.GetAsync<FileMetadataDto>(url);
        }

        public Task<ServiceResponse<FileMetadataDto>?> GetAccessibleByIdAsync(int id)
        {
            var url = ApiEndpoints.FileMetadata.GetAccessibleById.Replace("{id}", id.ToString());
            return _apiRequestHelper.GetAsync<FileMetadataDto>(url);
        }

        public Task<ServiceResponse<FileMetadataDto>?> CreateAsync(CreateFileDto dto)
        {
            return _apiRequestHelper.PostAsync<CreateFileDto, FileMetadataDto>(ApiEndpoints.FileMetadata.Create, dto);
        }

        public Task<ServiceResponse<FileMetadataDto>?> UpdateAsync(UpdateFileDto dto)
        {
            var url = ApiEndpoints.FileMetadata.Update.Replace("{id}", dto.Id.ToString());
            return _apiRequestHelper.PutAsync<UpdateFileDto, FileMetadataDto>(url, dto);
        }

        public Task<ServiceResponse<object>?> DeleteAsync(int id)
        {
            var url = ApiEndpoints.FileMetadata.Delete.Replace("{id}", id.ToString());
            return _apiRequestHelper.DeleteAsync<object>(url);
        }

        public Task<ServiceResponse<string>?> UploadAsync(IFormFile file)
        {
            return _fileRequestHelper.PostFileAsync<string>(ApiEndpoints.FileStorage.Upload, file);
        }

        public Task<ServiceResponse<byte[]>?> DownloadAsync(int fileId, string fileName)
        {
            var encodedFileName = Uri.EscapeDataString(fileName);
            var url = $"{ApiEndpoints.FileStorage.Download}?fileId={fileId}&fileName={encodedFileName}";
            return _fileRequestHelper.GetFileAsync(url);
        }
    }
}

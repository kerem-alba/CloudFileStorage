using CloudFileStorage.Common.Constants;
using CloudFileStorage.Common.Models;
using CloudFileStorage.UI.Helpers;
using CloudFileStorage.UI.Models.DTOs;
using CloudFileStorage.UI.Services.Interfaces;

namespace CloudFileStorage.UI.Services
{
    public class FileShareService : IFileShareService
    {
        private readonly ApiRequestHelper _api;

        public FileShareService(ApiRequestHelper api)
        {
            _api = api;
        }

        public Task<ServiceResponse<List<FileMetadataDto>>?> GetSharedWithMeAsync(string token)
        {
            return _api.GetAsync<List<FileMetadataDto>>(ApiEndpoints.FileShares.SharedWithMe, token);
        }

        public Task<ServiceResponse<bool>?> ShareFileAsync(CreateFileShareMetadataDto dto, string token)
        {
            return _api.PostAsync<CreateFileShareMetadataDto, bool>(ApiEndpoints.FileShares.ShareFile, dto, token);
        }

        public Task<ServiceResponse<bool>?> UpdateShareAsync(int id, UpdateFileShareMetadataDto dto, string token)
        {
            var url = ApiEndpoints.FileShares.Update.Replace("{id}", id.ToString());
            return _api.PutAsync<UpdateFileShareMetadataDto, bool>(url, dto, token);
        }

        public Task<ServiceResponse<bool>?> DeleteShareAsync(int id, string token)
        {
            var url = ApiEndpoints.FileShares.Delete.Replace("{id}", id.ToString());
            return _api.DeleteAsync<bool>(url, token);
        }
    }
}

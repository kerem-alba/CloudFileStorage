using CloudFileStorage.Common.Constants;
using CloudFileStorage.Common.Models;
using CloudFileStorage.UI.Helpers;
using CloudFileStorage.UI.Models.DTOs;
using CloudFileStorage.UI.Services.Interfaces;

namespace CloudFileStorage.UI.Services
{
    public class FileShareService : IFileShareService
    {
        private readonly ApiRequestHelper _apiRequestHelper;

        public FileShareService(ApiRequestHelper apiRequestHelper)
        {
            _apiRequestHelper = apiRequestHelper;
        }

        public Task<ServiceResponse<List<FileMetadataDto>>?> GetSharedWithMeAsync()
        {
            return _apiRequestHelper.GetAsync<List<FileMetadataDto>>(ApiEndpoints.FileShares.SharedWithMe);
        }

        public Task<ServiceResponse<bool>?> ShareFileAsync(CreateFileShareMetadataDto dto)
        {
            return _apiRequestHelper.PostAsync<CreateFileShareMetadataDto, bool>(ApiEndpoints.FileShares.ShareFile, dto);
        }

        public Task<ServiceResponse<bool>?> UpdateShareAsync(int id, UpdateFileShareMetadataDto dto)
        {
            var url = ApiEndpoints.FileShares.Update.Replace("{id}", id.ToString());
            return _apiRequestHelper.PutAsync<UpdateFileShareMetadataDto, bool>(url, dto);
        }

        public Task<ServiceResponse<bool>?> DeleteShareAsync(int id)
        {
            var url = ApiEndpoints.FileShares.Delete.Replace("{id}", id.ToString());
            return _apiRequestHelper.DeleteAsync<bool>(url);
        }
    }
}

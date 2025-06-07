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

        private Task<ServiceResponse<string>?> ShareFileAsync(CreateFileShareMetadataDto dto)
        {
            return _apiRequestHelper.PostAsync<CreateFileShareMetadataDto, string>(ApiEndpoints.FileShares.ShareFile, dto);
        }

        public async Task<ServiceResponse<bool>?> ShareFileWithMultipleUsersAsync(int fileMetadataId, List<UserShareSelection> selectedUsers)
        {
            selectedUsers = selectedUsers
                .Where(u => u.UserId > 0)
                .ToList();

            foreach (var userSelection in selectedUsers)
            {
                var dto = new CreateFileShareMetadataDto
                {
                    FileMetadataId = fileMetadataId,
                    UserId = userSelection.UserId,
                    Permission = userSelection.Permission
                };

                var result = await ShareFileAsync(dto);

                if (result == null || !result.Success)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = result?.Message ?? "File share failed",
                        StatusCode = result?.StatusCode ?? 500
                    };
                }
            }

            return new ServiceResponse<bool>
            {
                Success = true,
                Data = true,
                Message = "File shared successfully with all selected users"
            };
        }
    }
}
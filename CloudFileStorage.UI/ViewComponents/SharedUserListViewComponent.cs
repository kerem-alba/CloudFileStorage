using CloudFileStorage.UI.Models.DTOs;
using CloudFileStorage.UI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CloudFileStorage.UI.ViewComponents
{
    public class SharedUserListViewComponent : ViewComponent
    {
        private readonly IFileShareService _fileShareService;

        public SharedUserListViewComponent(IFileShareService fileShareService)
        {
            _fileShareService = fileShareService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int fileMetadataId)
        {
            var result = await _fileShareService.GetFileSharesByFileIdAsync(fileMetadataId);
            var users = result?.Success == true && result.Data != null ? result.Data : new List<FileShareDto>();
            return View(users);
        }
    }
}

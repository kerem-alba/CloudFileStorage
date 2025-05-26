using CloudFileStorage.UI.Models.DTOs;
using CloudFileStorage.UI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CloudFileStorage.UI.ViewComponents
{
    public class FileTableViewComponent : ViewComponent
    {
        private readonly IFileService _fileService;

        public FileTableViewComponent(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _fileService.GetAllAsync();

            var files = result?.Data;
            return View(files);
        }
    }
}

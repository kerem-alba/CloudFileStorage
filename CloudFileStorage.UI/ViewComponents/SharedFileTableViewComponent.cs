using Microsoft.AspNetCore.Mvc;
using CloudFileStorage.UI.Models.DTOs;

namespace CloudFileStorage.UI.ViewComponents
{
    public class SharedFileTableViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<FileMetadataDto> model)
        {
            return View(model);
        }
    }
}

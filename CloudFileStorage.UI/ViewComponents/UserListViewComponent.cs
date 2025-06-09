using CloudFileStorage.UI.Services.Interfaces;
using CloudFileStorage.UI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CloudFileStorage.UI.ViewComponents
{
    public class UserListViewComponent : ViewComponent
    {
        private readonly IUserService _userService;

        public UserListViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? fileMetadataId = null)
        {
            var result = await _userService.GetAllUsersAsync();
            var users = result?.Success == true && result.Data != null ? result.Data : new List<UserDto>();

            Console.WriteLine($"UserList: Found {users.Count} users");

            if (fileMetadataId.HasValue)
            {
                ViewBag.FileMetadataId = fileMetadataId.Value;
            }

            return View(users);
        }
    }
}
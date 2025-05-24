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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var token = HttpContext.Session.GetString("token");
            var result = await _userService.GetAllUsersAsync(token);
            var users = result.Success && result.Data != null ? result.Data : new List<UserDto>();

            return View(users);
        }
    }
}

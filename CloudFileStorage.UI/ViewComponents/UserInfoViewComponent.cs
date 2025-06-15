using CloudFileStorage.UI.Helpers;
using Microsoft.AspNetCore.Mvc;
using CloudFileStorage.UI.Services.Interfaces;


namespace CloudFileStorage.UI.ViewComponents
{
    public class UserInfoViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public UserInfoViewComponent(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");

            string? userName = null;

            if (!string.IsNullOrEmpty(token))
            {
                var userId = JwtHelper.GetUserIdFromToken(token);
                if (userId.HasValue)
                {
                    var result = await _userService.GetUserNameByIdAsync(userId.Value);
                    userName = result?.Data;
                }
            }
            return View("Default", userName);
        }

    }

}

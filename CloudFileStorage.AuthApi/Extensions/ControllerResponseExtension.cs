using CloudFileStorage.AuthApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace CloudFileStorage.AuthApi.Extensions
{
    public static class ControllerResponseExtension
    {
        public static IActionResult HandleResponse<T>(this ControllerBase controller, ServiceResponse<T> response)
        {
            return controller.StatusCode(response.StatusCode, response);
        }

    }
}

using CloudFileStorage.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace CloudFileStorage.Common.Extensions
{
    public static class ControllerResponseExtension
    {
        public static IActionResult HandleResponse<T>(this ControllerBase controller, ServiceResponse<T> response)
        {
            return controller.StatusCode(response.StatusCode, response);
        }
    }
}

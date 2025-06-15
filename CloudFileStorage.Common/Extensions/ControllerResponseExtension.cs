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

        public static IActionResult HandleResponse<T>(this ControllerBase controller, ServiceResponse<T> response, string createdAtAction, object routeValues)
        {
            if (response.Success && response.StatusCode == 201)
            {
                return controller.CreatedAtAction(createdAtAction, routeValues, response);
            }
            return controller.StatusCode(response.StatusCode, response);
        }
    }
}

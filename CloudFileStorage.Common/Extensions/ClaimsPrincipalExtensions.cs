using System;
using System.Security.Claims;

namespace CloudFileStorage.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userIdStr = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? user.FindFirst("sub")?.Value
                         ?? user.FindFirst("id")?.Value;

            return string.IsNullOrEmpty(userIdStr) ? 0 : int.Parse(userIdStr);
        }

    }
}

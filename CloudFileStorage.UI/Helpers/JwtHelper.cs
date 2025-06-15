using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CloudFileStorage.UI.Helpers;

public static class JwtHelper
{
    public static int? GetUserIdFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var idClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        return idClaim != null ? int.Parse(idClaim.Value) : null;
    }
    public static bool IsTokenExpired(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        return jwtToken.ValidTo < DateTime.UtcNow;
    }
}
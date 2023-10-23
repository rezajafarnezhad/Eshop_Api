using System.Security.Claims;

namespace Common.AspNet;

public static class ClaimUtils
{
    public static long GetUserId(this ClaimsPrincipal principal)
    {
        if(principal is null)
            throw new ArgumentNullException(nameof(principal));

        return Convert.ToInt32( principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}
using System.Security.Claims;

namespace api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string? GetUsername(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;

        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
            => int.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}
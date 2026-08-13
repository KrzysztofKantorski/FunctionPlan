using System.Security.Claims;

namespace API.Extensions
{
    public static class UserClaims
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            var userIdStr = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdStr, out int UserId))
            {
                throw new UnauthorizedAccessException("Incorrect or missing token");
            }

            return UserId;
        }
    }
}

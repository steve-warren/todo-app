using System.Security.Claims;

namespace WarrenSoftware.TodoApp.Core.Infrastructure
{
    public static class ClaimsExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user) => int.Parse(user.FindFirstValue("id"));
    }
}
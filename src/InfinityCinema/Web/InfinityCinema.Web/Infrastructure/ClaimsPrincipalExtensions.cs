namespace InfinityCinema.Web.Infrastructure
{
    using System.Security.Claims;

    public class ClaimsPrincipalExtensions
    {
        public static string GetId(ClaimsPrincipal user)
        {
            string id = user.FindFirst(ClaimTypes.NameIdentifier).Value;

            return id;
        }
    }
}

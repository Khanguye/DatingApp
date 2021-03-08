using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user){
            return user.FindFirst(ClaimTypes.Name)?.Value; // new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
              
        }

        public static int GetUserId(this ClaimsPrincipal user){
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value); //   new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString())
        }
    }
}
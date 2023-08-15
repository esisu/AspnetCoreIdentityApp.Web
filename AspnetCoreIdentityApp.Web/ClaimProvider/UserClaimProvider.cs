using System.Security.Claims;
using AspnetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace AspnetCoreIdentityApp.Web.ClaimProvider
{
    public class UserClaimProvider : IClaimsTransformation
    {

        private readonly UserManager<AppUser> _userManager;

        public UserClaimProvider(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var identityPrincipal = principal.Identity as ClaimsIdentity;

            var currentUser = await _userManager.FindByNameAsync(identityPrincipal.Name);

            if (currentUser == null)
            {
                return principal;
            }

            if (String.IsNullOrEmpty(currentUser.City))
            {
                return principal;
            }

            if (principal.HasClaim(x => x.Type != "city"))
            {
                Claim cityClaim = new Claim("city", currentUser.City);
                identityPrincipal.AddClaim(cityClaim);
            }


            return principal;

        }
    }
}

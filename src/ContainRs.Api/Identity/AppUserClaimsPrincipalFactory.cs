using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace ContainRs.Api.Identity;

public class AppUserClaimsPrincipalFactory(
    UserManager<AppUser> userManager
    , IOptions<IdentityOptions> optionsAccessor) 
    : UserClaimsPrincipalFactory<AppUser>(userManager, optionsAccessor)
{
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        (await UserManager.GetRolesAsync(user))
            .ToList()
            .ForEach(role => identity.AddClaim(new Claim(ClaimTypes.Role, role)));

        return identity;
    }
}

using ComtainRs.Contracts;

namespace ContainRs.Api.Extensions;

public class GetUserClaimAsyncImpl : IGetUserClaim
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetUserClaimAsyncImpl(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<string> GetUserClaimAsync(string claimType)
    {
        var context = _httpContextAccessor.HttpContext;
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var claimValue = context.User.Claims
            .Where(c => c.Type.Equals(claimType))
            .Select(c => c.Value)
            .FirstOrDefault();

        return Task.FromResult(claimValue ?? string.Empty);
    }
}

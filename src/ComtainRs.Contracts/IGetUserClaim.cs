namespace ComtainRs.Contracts;

public interface IGetUserClaim
{
    Task<string> GetUserClaimAsync(string claimType);
}

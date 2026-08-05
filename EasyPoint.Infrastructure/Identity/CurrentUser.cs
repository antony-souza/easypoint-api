using System.Security.Claims;
using EasyPoint.Application.Common.Authentication;
using Microsoft.AspNetCore.Http;

namespace EasyPoint.Infrastructure.Identity;

public sealed class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    private ClaimsPrincipal User => httpContextAccessor.HttpContext?.User
        ?? throw new UnauthorizedAccessException("There is no current HTTP request.");

    public Guid UserId => GetGuidClaim(ClaimTypes.NameIdentifier);
    public Guid StoreId => GetGuidClaim("store_id");

    private Guid GetGuidClaim(string claimType)
    {
        var value = GetRequiredClaim(claimType);

        return Guid.TryParse(value, out var id)
            ? id
            : throw new UnauthorizedAccessException($"The {claimType} claim is invalid.");
    }

    private string GetRequiredClaim(string claimType)
    {
        return User.FindFirstValue(claimType)
            ?? throw new UnauthorizedAccessException($"The {claimType} claim is required.");
    }
}

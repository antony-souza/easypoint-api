using System.Security.Claims;
using EasyPoint.Application.Common.Authentication;
using EasyPoint.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EasyPoint.Infrastructure.Identity;

public sealed class CurrentUser(
    IHttpContextAccessor httpContextAccessor,
    EasyPointDbContext context) : ICurrentUser
{
    private ClaimsPrincipal User => httpContextAccessor.HttpContext?.User
        ?? throw new UnauthorizedAccessException("There is no current HTTP request.");

    public Guid UserId => GetGuidClaim(ClaimTypes.NameIdentifier);
    public Guid OrganizationId => GetGuidClaim("organization_id");

    public Task<bool> HasStoreAccessAsync(
        Guid storeId,
        CancellationToken cancellationToken = default)
    {
        return context.StoreUsers.AnyAsync(
            member =>
                member.UserId == UserId &&
                member.OrganizationId == OrganizationId &&
                member.StoreId == storeId &&
                member.Store.OrganizationId == OrganizationId,
            cancellationToken);
    }

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

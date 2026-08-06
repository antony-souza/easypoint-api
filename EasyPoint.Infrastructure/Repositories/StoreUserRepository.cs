using EasyPoint.Domain.Entities.StoreUsers;
using EasyPoint.Domain.Repositories;
using EasyPoint.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyPoint.Infrastructure.Repositories;

public sealed class StoreUserRepository(EasyPointDbContext context)
    : Repository<StoreUser>(context), IStoreUserRepository
{
    public Task<StoreUser?> GetByStoreAndUserAsync(
        Guid storeId,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return context.StoreUsers.SingleOrDefaultAsync(
            member =>
                member.StoreId == storeId &&
                member.UserId == userId,
            cancellationToken);
    }

    public Task<bool> UserBelongsToOrganizationAsync(
        Guid userId,
        Guid organizationId,
        CancellationToken cancellationToken = default)
    {
        return context.Users.AnyAsync(
            user =>
                user.Id == userId &&
                user.OrganizationId == organizationId,
            cancellationToken);
    }
}

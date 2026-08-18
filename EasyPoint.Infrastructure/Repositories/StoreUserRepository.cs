using EasyPoint.Domain.Entities.StoreUsers;
using EasyPoint.Domain.ReadModels.StoreUsers;
using EasyPoint.Domain.Repositories;
using EasyPoint.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyPoint.Infrastructure.Repositories;

public sealed class StoreUserRepository(EasyPointDbContext context)
    : Repository<StoreUser>(context), IStoreUserRepository
{
    public async Task<(IReadOnlyList<StoreUserListItem> StoreUsers, int TotalItems)> GetPagedByStoreAsync(
        Guid storeId,
        int skip,
        int take,
        CancellationToken cancellationToken = default)
    {
        var query = context.StoreUsers
            .AsNoTracking()
            .Where(storeUser => storeUser.StoreId == storeId)
            .OrderBy(storeUser => storeUser.User.Name);

        var totalItems = await query.CountAsync(cancellationToken);

        var storeUsers = await query
            .Skip(skip)
            .Take(take)
            .Select(storeUser => new StoreUserListItem(
                storeUser.Id,
                storeUser.UserId,
                storeUser.User.Name))
            .ToListAsync(cancellationToken);

        return (storeUsers, totalItems);
    }

    public Task<StoreUser?> GetByStoreAndUserAsync(
        Guid storeId,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return context.StoreUsers.SingleOrDefaultAsync(
            storeUser =>
                storeUser.StoreId == storeId &&
                storeUser.UserId == userId,
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
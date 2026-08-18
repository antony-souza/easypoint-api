using EasyPoint.Domain.Entities.StoreUsers;
using EasyPoint.Domain.ReadModels.StoreUsers;

namespace EasyPoint.Domain.Repositories;

public interface IStoreUserRepository : IRepository<StoreUser>
{
    Task<(IReadOnlyList<StoreUserListItem> StoreUsers, int TotalItems)> GetPagedByStoreAsync(
        Guid storeId,
        int skip,
        int take,
        CancellationToken cancellationToken = default);

    Task<StoreUser?> GetByStoreAndUserAsync(
        Guid storeId,
        Guid userId,
        CancellationToken cancellationToken = default);

    Task<bool> UserBelongsToOrganizationAsync(
        Guid userId,
        Guid organizationId,
        CancellationToken cancellationToken = default);
}

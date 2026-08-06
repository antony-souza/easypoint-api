using EasyPoint.Domain.Entities.StoreUsers;

namespace EasyPoint.Domain.Repositories;

public interface IStoreUserRepository : IRepository<StoreUser>
{
    Task<StoreUser?> GetByStoreAndUserAsync(
        Guid storeId,
        Guid userId,
        CancellationToken cancellationToken = default);

    Task<bool> UserBelongsToOrganizationAsync(
        Guid userId,
        Guid organizationId,
        CancellationToken cancellationToken = default);
}

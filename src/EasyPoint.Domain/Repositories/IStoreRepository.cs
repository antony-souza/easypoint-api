using EasyPoint.Domain.Entities.Stores;

namespace EasyPoint.Domain.Repositories;

public interface IStoreRepository : IRepository<Store>
{
    Task<Store?> GetByCnpjAsync(
        string cnpj,
        CancellationToken cancellationToken = default);
}

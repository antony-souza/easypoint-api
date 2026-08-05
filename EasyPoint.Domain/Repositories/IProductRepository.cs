using EasyPoint.Domain.Entities.Products;

namespace EasyPoint.Domain.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetByBarCodeAsync(
        string barCode,
        CancellationToken cancellationToken = default);
}

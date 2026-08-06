using EasyPoint.Domain.Entities.Products;

namespace EasyPoint.Domain.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<(IReadOnlyList<Product> Products, int TotalItems)> GetPagedByOrganizationAsync(
        Guid organizationId,
        int skip,
        int take,
        CancellationToken cancellationToken = default);

    Task<Product?> GetByBarCodeAsync(
        string barCode,
        CancellationToken cancellationToken = default);
}

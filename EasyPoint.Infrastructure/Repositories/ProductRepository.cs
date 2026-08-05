using EasyPoint.Domain.Entities.Products;
using EasyPoint.Domain.Repositories;
using EasyPoint.Infrastructure.Data.Context;

namespace EasyPoint.Infrastructure.Repositories;

public class ProductRepository(EasyPointDbContext context)
    : Repository<Product>(context), IProductRepository
{
    public Task<Product?> GetByBarCodeAsync(
        string barCode,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
using EasyPoint.Application.Modules.Catalog.Products;
using EasyPoint.Domain.Modules.Catalog.Entities;
using EasyPoint.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyPoint.Infrastructure.Persistence.Repositories.Catalogs;

public class ProductRepository : IProductRepository
{
    private readonly EasyPointDbContext _dbContext;

    public ProductRepository(EasyPointDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Product>> GetAllProductsByStore(Guid storeId)
    {
        var products = await _dbContext.Products
            .Where(product => product.StoreId == storeId)
            .ToListAsync();

        return products;
    }

}

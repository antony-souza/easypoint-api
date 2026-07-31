using EasyPoint.Application.Common.Abstractions;
using EasyPoint.Domain.Modules.Catalog.Entities;

namespace EasyPoint.Application.Modules.Catalog.Products;

public class ListAllProductsUseCase : IUseCase<Guid, List<Product>>
{
    //test
    public List<Product> Handler(Guid id)
    {
        var products = new List<Product>
        {
            new Product
            {
                Id = id,
                Name = "Product 1",
                BarCode = 123456,
                CategoryId = Guid.NewGuid(),
                StoreId = Guid.NewGuid()
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 2",
                BarCode = 789012,
                CategoryId = Guid.NewGuid(),
                StoreId = Guid.NewGuid()
            }
        };

        return products;
    }
}
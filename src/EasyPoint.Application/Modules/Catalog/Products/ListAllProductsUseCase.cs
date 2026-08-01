using EasyPoint.Application.Common.Abstractions;
using EasyPoint.Domain.Modules.Catalog.Entities;

namespace EasyPoint.Application.Modules.Catalog.Products;

public class ListAllProductsUseCase : IUseCase<Guid, Task<List<Product>>>
{
    private readonly IProductRepository _productRepository;
    
    public ListAllProductsUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> Handler(Guid storeId)
    {
        var products = await _productRepository.GetAllProductsByStore(storeId);

        return products;
    }
}

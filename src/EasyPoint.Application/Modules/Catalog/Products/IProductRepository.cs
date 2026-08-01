using EasyPoint.Application.Common.Abstractions;
using EasyPoint.Domain.Modules.Catalog.Entities;

namespace EasyPoint.Application.Modules.Catalog.Products;

public interface IProductRepository : IRepository
{
    Task<List<Product>> GetAllProductsByStore(Guid storeId);
}
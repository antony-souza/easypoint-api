using EasyPoint.Domain.Entities.Products;
using EasyPoint.Domain.Entities.Stores;
using EasyPoint.Domain.Common.Entities;

namespace EasyPoint.Domain.Entities.ProductPrices;

public class ProductPrice : Entity
{
    public decimal Price { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public Guid StoreId { get; set; }
    public Store Store { get; set; } = null!;
}

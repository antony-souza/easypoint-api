using EasyPoint.Domain.Entities.Products;
using EasyPoint.Domain.Entities.Stores;

namespace EasyPoint.Domain.Entities.ProductPrices;

public class ProductPrice
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = new Product();
    public Guid StoreId { get; set; }
    public Store Store { get; set; } = new Store();
}

using EasyPoint.Domain.Modules.Catalog.Entities;

namespace EasyPoint.Domain.Modules.Stores.Entities;

public class Store
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<ProductPrice> ProductPrices { get; set; } = new List<ProductPrice>();
}

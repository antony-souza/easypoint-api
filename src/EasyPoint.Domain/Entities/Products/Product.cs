
using EasyPoint.Domain.Entities.Categories;
using EasyPoint.Domain.Entities.Stores;

namespace EasyPoint.Domain.Entities.Products;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string BarCode { get; set; } = String.Empty;
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = new Category();
    public Guid StoreId { get; set; }
    public Store Store { get; set; } = new Store();
}

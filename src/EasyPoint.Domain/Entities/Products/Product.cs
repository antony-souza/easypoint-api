
using EasyPoint.Domain.Entities.Categories;
using EasyPoint.Domain.Entities.Stores;
using EasyPoint.Domain.Common.Entities;

namespace EasyPoint.Domain.Entities.Products;

public class Product : Entity
{
    public string Name { get; set; } = string.Empty;
    public string BarCode { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public Guid StoreId { get; set; }
    public Store Store { get; set; } = null!;
}

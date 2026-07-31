using EasyPoint.Domain.Modules.Stores.Entities;

namespace EasyPoint.Domain.Modules.Catalog.Entities;


public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int BarCode { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = new Category();
    public Guid StoreId { get; set; }
    public Store Store { get; set; } = new Store();
}
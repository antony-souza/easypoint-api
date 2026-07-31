using EasyPoint.Domain.Modules.Stores.Entities;

namespace EasyPoint.Domain.Modules.Catalog.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid StoreId { get; set; }
    public Store Store { get; set; } = new Store();
}
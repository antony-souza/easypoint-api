using EasyPoint.Domain.Entities.Stores;

namespace EasyPoint.Domain.Entities.Categories;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid StoreId { get; set; }
    public Store Store { get; set; } = null!;
}

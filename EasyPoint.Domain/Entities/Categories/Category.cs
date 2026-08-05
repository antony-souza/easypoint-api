using EasyPoint.Domain.Entities.Stores;
using EasyPoint.Domain.Common.Entities;

namespace EasyPoint.Domain.Entities.Categories;

public class Category : Entity
{
    public string Name { get; set; } = string.Empty;
    public Guid StoreId { get; set; }
    public Store Store { get; set; } = null!;
}

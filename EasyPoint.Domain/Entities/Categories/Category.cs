using EasyPoint.Domain.Common.Entities;
using EasyPoint.Domain.Entities.Organizations;
using EasyPoint.Domain.Entities.Products;

namespace EasyPoint.Domain.Entities.Categories;

public class Category : Entity
{
    public string Name { get; set; } = string.Empty;
    public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; } = null!;
    public ICollection<Product> Products { get; set; } = new List<Product>();
}

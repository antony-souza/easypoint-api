using EasyPoint.Domain.Common.Entities;
using EasyPoint.Domain.Entities.Categories;
using EasyPoint.Domain.Entities.Products;
using EasyPoint.Domain.Entities.Stores;

namespace EasyPoint.Domain.Entities.Organizations;

public class Organization : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public ICollection<Store> Stores { get; set; } = new List<Store>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<Product> Products { get; set; } = new List<Product>();
}

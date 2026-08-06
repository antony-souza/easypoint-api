using EasyPoint.Domain.Common.Entities;
using EasyPoint.Domain.Entities.Categories;
using EasyPoint.Domain.Entities.Products;
using EasyPoint.Domain.Entities.Stores;

namespace EasyPoint.Domain.Entities.Organization;

public class Organization : Entity
{
    string Name { get; set; } = string.Empty;
    string Cnpj { get; set; } = string.Empty;
    ICollection<OrganizationEmployee> OrganizationEmployees { get; set; } = new List<OrganizationEmployee>();
    public ICollection<Store> Stores { get; set; } = new List<Store>();
    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}
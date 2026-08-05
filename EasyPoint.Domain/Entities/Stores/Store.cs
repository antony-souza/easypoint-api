using EasyPoint.Domain.Entities.Categories;
using EasyPoint.Domain.Entities.CashRegisters;
using EasyPoint.Domain.Entities.ProductPrices;
using EasyPoint.Domain.Entities.Products;
using EasyPoint.Domain.Common.Entities;

namespace EasyPoint.Domain.Entities.Stores;

public class Store : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<ProductPrice> ProductPrices { get; set; } = new List<ProductPrice>();
    public ICollection<CashRegister> CashRegisters { get; set; } = new List<CashRegister>();
}

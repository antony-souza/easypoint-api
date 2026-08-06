using EasyPoint.Domain.Common.Entities;
using EasyPoint.Domain.Entities.CashRegisters;
using EasyPoint.Domain.Entities.Organizations;
using EasyPoint.Domain.Entities.ProductPrices;
using EasyPoint.Domain.Entities.StoreUsers;

namespace EasyPoint.Domain.Entities.Stores;

public class Store : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; } = null!;
    public ICollection<ProductPrice> ProductPrices { get; set; } = new List<ProductPrice>();
    public ICollection<CashRegister> CashRegisters { get; set; } = new List<CashRegister>();
    public ICollection<StoreUser> StoreUsers { get; set; } = new List<StoreUser>();
}

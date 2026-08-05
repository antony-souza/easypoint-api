using EasyPoint.Domain.Common.Entities;
using EasyPoint.Domain.Entities.Stores;

namespace EasyPoint.Domain.Entities.CashRegisters;

public sealed class CashRegister : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public Guid StoreId { get; set; }
    public Store Store { get; set; } = null!;
}

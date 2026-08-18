using EasyPoint.Domain.Common.Entities;

namespace EasyPoint.Domain.Entities.CashSessions;

public class CashSessions : Entity
{
    public Guid UserId { get; set; }
    public Guid CashRegisterId { get; set; }

    public CashRegister CashRegisters { get; set; } = null!;
}
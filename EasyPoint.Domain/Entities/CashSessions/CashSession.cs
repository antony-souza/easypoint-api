using EasyPoint.Domain.Common.Entities;
using EasyPoint.Domain.Entities.CashRegisters;
using EasyPoint.Domain.Entities.Users;

namespace EasyPoint.Domain.Entities.CashSessions;

public class CashSession : Entity
{
    public bool Active { get; set; } = false;
    public Guid UserId { get; set; }
    public Guid CashRegisterId { get; set; }

    public DateTime OpenedAt { get; set; }
    public decimal OpeningAmount { get; set; }

    public DateTime? ClosedAt { get; set; }
    public decimal? ClosingAmount { get; set; }

    public bool Canceled { get; set; } = false;

    public CashRegister CashRegister { get; set; } = null!;
    public AppUser User { get; set; } = null!;
}

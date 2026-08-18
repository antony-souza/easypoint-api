using EasyPoint.Domain.Entities.CashSessions;
using EasyPoint.Domain.Entities.Organizations;
using EasyPoint.Domain.Entities.StoreUsers;
using Microsoft.AspNetCore.Identity;

namespace EasyPoint.Domain.Entities.Users;

public sealed class AppUser : IdentityUser<Guid>
{
    public string Name { get; set; } = string.Empty;
    public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; } = null!;
    public ICollection<StoreUser> StoreUsers { get; set; } = new List<StoreUser>();
    public ICollection<CashSession> CashSessions { get; set; } = new List<CashSession>();
}

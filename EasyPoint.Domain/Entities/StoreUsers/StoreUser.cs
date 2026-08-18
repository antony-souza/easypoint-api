using EasyPoint.Domain.Common.Entities;
using EasyPoint.Domain.Entities.Organizations;
using EasyPoint.Domain.Entities.Stores;
using EasyPoint.Domain.Entities.Users;

namespace EasyPoint.Domain.Entities.StoreUsers;

public class StoreUser : Entity
{
    public Guid OrganizationId { get; set; }
    public Guid StoreId { get; set; }
    public Guid UserId { get; set; }
    public Organization Organization { get; set; } = null!;
    public Store Store { get; set; } = null!;
    public AppUser User { get; set; } = null!;
}

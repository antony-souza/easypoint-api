using EasyPoint.Domain.Common.Entities;
using EasyPoint.Domain.Entities.Organizations;

namespace EasyPoint.Domain.Entities.OrganizationEmployees;

public class OrganizationEmployee : Entity
{
    Guid OrganizationId { get; set; }
    Guid UserId { get; set; }
    public Organization Organization { get; set; } = null!;
}
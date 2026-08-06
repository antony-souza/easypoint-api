using EasyPoint.Domain.Common.Entities;
using EasyPoint.Domain.Entities.Stores;

namespace EasyPoint.Domain.Entities.StoreEmployees;

public class StoreUsers : Entity
{
    public Guid StoreId { get; set; }
    public Store Store { get; set; } = new Store();
    public Guid EmployeeId { get; set; }
}
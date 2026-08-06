using EasyPoint.Domain.Entities.Organizations;

namespace EasyPoint.Domain.Repositories;

public interface IOrganizationRepository : IRepository<Organization>
{
    public Task<Organization?> GetByCnpjAsync(string cnpj, CancellationToken cancellationToken = default);
}
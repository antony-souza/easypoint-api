using EasyPoint.Domain.Entities.Organizations;
using EasyPoint.Domain.Repositories;
using EasyPoint.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyPoint.Infrastructure.Repositories;

public class OrganizationRepository(EasyPointDbContext context)
    : Repository<Organization>(context), IOrganizationRepository
{
    public Task<Organization?> GetByCnpjAsync(
        string cnpj,
        CancellationToken cancellationToken = default)
    {
        return context.Organizations.SingleOrDefaultAsync(org => org.Cnpj == cnpj, cancellationToken);
    }
}
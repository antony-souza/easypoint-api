using EasyPoint.Domain.Entities.Products;
using EasyPoint.Domain.Repositories;
using EasyPoint.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EasyPoint.Infrastructure.Repositories;

public sealed class ProductRepository(EasyPointDbContext context)
    : Repository<Product>(context), IProductRepository
{
    public async Task<(
        IReadOnlyList<Product> Products,
        int TotalItems)> GetPagedByOrganizationAsync(
        Guid organizationId,
        int skip,
        int take,
        CancellationToken cancellationToken = default)
    {
        var query = context.Products
            .AsNoTracking()
            .Where(product => product.OrganizationId == organizationId)
            .OrderBy(product => product.Name)
            .ThenBy(product => product.Id)
            .Include(product => product.Category)
            .Include(product => product.Organization);

        var totalItems = await query.CountAsync(cancellationToken);
        var products = await query
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);

        return (products, totalItems);
    }

    public Task<Product?> GetByBarCodeAsync(
        string barCode,
        CancellationToken cancellationToken = default)
    {
        return context.Products.SingleOrDefaultAsync(
            product => product.BarCode == barCode,
            cancellationToken);
    }
}

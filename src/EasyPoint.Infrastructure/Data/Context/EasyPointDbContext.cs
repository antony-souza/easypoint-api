using EasyPoint.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace EasyPoint.Infrastructure.Data.Context;

public class EasyPointDbContext(DbContextOptions<EasyPointDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);
    }
}

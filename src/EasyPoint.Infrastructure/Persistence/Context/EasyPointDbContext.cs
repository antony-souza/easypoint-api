using EasyPoint.Domain.Modules.Catalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyPoint.Infrastructure.Persistence.Context;

public class EasyPointDbContext : DbContext
{
    public EasyPointDbContext(DbContextOptions<EasyPointDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(
            ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(EasyPointDbContext).Assembly);
    }

    public DbSet<Product> Products { get; set; } = null!;
}

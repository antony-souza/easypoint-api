using EasyPoint.Domain.Entities.Products;
using EasyPoint.Domain.Entities.Stores;
using Microsoft.EntityFrameworkCore;

namespace EasyPoint.Infrastructure.Data.Context;

public class EasyPointDbContext(DbContextOptions<EasyPointDbContext> options) : DbContext(options)
{
    public DbSet<Store> Stores { get; set; } = null!;
    public DbSet<Store> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Product> ProductPrice { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(EasyPointDbContext).Assembly);
    }
}
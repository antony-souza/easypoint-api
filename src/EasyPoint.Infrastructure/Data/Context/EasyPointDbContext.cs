using EasyPoint.Domain.Entities.Categories;
using EasyPoint.Domain.Entities.ProductPrices;
using EasyPoint.Domain.Entities.Products;
using EasyPoint.Domain.Entities.Stores;
using Microsoft.EntityFrameworkCore;

namespace EasyPoint.Infrastructure.Data.Contexts;

public class EasyPointDbContext(DbContextOptions<EasyPointDbContext> options) : DbContext(options)
{
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductPrice> ProductPrices => Set<ProductPrice>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(EasyPointDbContext).Assembly);
    }
}

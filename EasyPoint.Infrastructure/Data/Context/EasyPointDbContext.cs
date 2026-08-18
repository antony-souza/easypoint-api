using EasyPoint.Domain.Common.Entities;
using EasyPoint.Domain.Entities.Categories;
using EasyPoint.Domain.Entities.CashRegisters;
using EasyPoint.Domain.Entities.CashSessions;
using EasyPoint.Domain.Entities.Organizations;
using EasyPoint.Domain.Entities.ProductPrices;
using EasyPoint.Domain.Entities.Products;
using EasyPoint.Domain.Entities.StockMovements;
using EasyPoint.Domain.Entities.Stocks;
using EasyPoint.Domain.Entities.Stores;
using EasyPoint.Domain.Entities.StoreUsers;
using EasyPoint.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyPoint.Infrastructure.Data.Context;

public class EasyPointDbContext(DbContextOptions<EasyPointDbContext> options)
    : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductPrice> ProductPrices => Set<ProductPrice>();
    public DbSet<CashRegister> CashRegisters => Set<CashRegister>();
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<StoreUser> StoreUsers => Set<StoreUser>();
    public DbSet<Stock> Stocks => Set<Stock>();
    public DbSet<StockMovement> StockMovements => Set<StockMovement>();
    public DbSet<CashSession> CashSessions => Set<CashSession>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(EasyPointDbContext).Assembly);
    }

    public override int SaveChanges()
    {
        ApplyAuditInformation();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditInformation();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditInformation()
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<IEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = now;
                entry.Entity.UpdatedAt = now;
            }

            if (entry.State == EntityState.Modified)
                entry.Entity.UpdatedAt = now;

            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.DeletedAt = now;
                entry.Entity.UpdatedAt = now;
            }
        }
    }
}

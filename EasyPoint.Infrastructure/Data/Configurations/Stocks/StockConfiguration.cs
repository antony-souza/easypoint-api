using EasyPoint.Domain.Entities.Stocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyPoint.Infrastructure.Data.Configurations.Stocks;

public sealed class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.ToTable("Stocks");

        builder.HasKey(stock => stock.Id);

        builder.Property(stock => stock.Id)
            .ValueGeneratedOnAdd();

        builder.Property(stock => stock.Quantity)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(stock => stock.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(stock => stock.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasQueryFilter(stock => stock.DeletedAt == null);

        builder.HasIndex(stock => new
        {
            stock.StoreId,
            stock.ProductId
        })
        .IsUnique();

        builder.HasOne(stock => stock.Store)
            .WithMany()
            .HasForeignKey(stock => stock.StoreId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(stock => stock.Product)
            .WithMany()
            .HasForeignKey(stock => stock.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

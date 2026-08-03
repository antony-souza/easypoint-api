using EasyPoint.Domain.Entities.ProductPrices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyPoint.Infrastructure.Data.Configurations.ProductPrices;

public sealed class ProductPriceConfiguration : IEntityTypeConfiguration<ProductPrice>
{
    public void Configure(EntityTypeBuilder<ProductPrice> builder)
    {
        builder.ToTable("ProductPrices");

        builder.HasKey(productPrice => productPrice.Id);

        builder.Property(productPrice => productPrice.Id)
            .ValueGeneratedOnAdd();

        builder.Property(productPrice => productPrice.Price)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(productPrice => productPrice.CreatedAt)
            .IsRequired();

        builder.HasIndex(productPrice => new
        {
            productPrice.ProductId,
            productPrice.StoreId,
            productPrice.CreatedAt
        });

        builder.HasOne(productPrice => productPrice.Product)
            .WithMany()
            .HasForeignKey(productPrice => productPrice.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(productPrice => productPrice.Store)
            .WithMany(store => store.ProductPrices)
            .HasForeignKey(productPrice => productPrice.StoreId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

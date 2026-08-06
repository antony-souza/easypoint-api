using EasyPoint.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyPoint.Infrastructure.Data.Configurations.Products;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(product => product.Id);

        builder.Property(product => product.Id)
            .ValueGeneratedOnAdd();

        builder.Property(product => product.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(product => product.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(product => product.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(product => product.BarCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(product => new
        {
            product.OrganizationId,
            product.BarCode
        })
        .IsUnique();

        builder.HasOne(product => product.Organization)
            .WithMany(organization => organization.Products)
            .HasForeignKey(product => product.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(product => product.Category)
            .WithMany(category => category.Products)
            .HasForeignKey(product => new
            {
                product.CategoryId,
                product.OrganizationId
            })
            .HasPrincipalKey(category => new
            {
                category.Id,
                category.OrganizationId
            })
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(product =>
            product.DeletedAt == null &&
            product.Organization.DeletedAt == null &&
            product.Category.DeletedAt == null &&
            product.Category.Organization.DeletedAt == null);
    }
}

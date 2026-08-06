using EasyPoint.Domain.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyPoint.Infrastructure.Data.Configurations.Categories;

public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(category => category.Id);

        builder.HasAlternateKey(category => new
        {
            category.Id,
            category.OrganizationId
        });

        builder.Property(category => category.Id)
            .ValueGeneratedOnAdd();

        builder.Property(category => category.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(category => category.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(category => category.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(category => new
        {
            category.OrganizationId,
            category.Name
        })
        .IsUnique();

        builder.HasOne(category => category.Organization)
            .WithMany(organization => organization.Categories)
            .HasForeignKey(category => category.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(category =>
            category.DeletedAt == null && category.Organization.DeletedAt == null);
    }
}

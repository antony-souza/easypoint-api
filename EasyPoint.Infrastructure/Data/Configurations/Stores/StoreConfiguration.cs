using EasyPoint.Domain.Entities.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyPoint.Infrastructure.Data.Configurations.Stores;

public sealed class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToTable("Stores");

        builder.HasKey(store => store.Id);

        builder.Property(store => store.Id)
            .ValueGeneratedOnAdd();

        builder.Property(store => store.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(store => store.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasQueryFilter(store =>
            store.DeletedAt == null && store.Organization.DeletedAt == null);

        builder.Property(store => store.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(store => store.Cnpj)
            .IsRequired()
            .HasMaxLength(18);

        builder.HasIndex(store => store.Cnpj).IsUnique();

        builder.HasOne(store => store.Organization)
            .WithMany(organization => organization.Stores)
            .HasForeignKey(store => store.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

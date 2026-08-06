using EasyPoint.Domain.Entities.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyPoint.Infrastructure.Data.Configurations.Organizations;

public sealed class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable("Organizations");

        builder.HasKey(organization => organization.Id);

        builder.Property(organization => organization.Id)
            .ValueGeneratedOnAdd();

        builder.Property(organization => organization.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(organization => organization.Cnpj)
            .IsRequired()
            .HasMaxLength(18);

        builder.HasIndex(organization => organization.Cnpj)
            .IsUnique();

        builder.HasQueryFilter(organization => organization.DeletedAt == null);
    }
}

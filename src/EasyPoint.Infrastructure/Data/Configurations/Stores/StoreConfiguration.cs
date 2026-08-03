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

        builder.Property(store => store.Name)
            .IsRequired()
            .HasMaxLength(150);
    }
}

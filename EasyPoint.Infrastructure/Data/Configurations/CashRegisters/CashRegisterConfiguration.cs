using EasyPoint.Domain.Entities.CashRegisters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyPoint.Infrastructure.Data.Configurations.CashRegisters;

public sealed class CashRegisterConfiguration : IEntityTypeConfiguration<CashRegister>
{
    public void Configure(EntityTypeBuilder<CashRegister> builder)
    {
        builder.ToTable("CashRegisters");

        builder.HasKey(cashRegister => cashRegister.Id);

        builder.Property(cashRegister => cashRegister.Id)
            .ValueGeneratedOnAdd();

        builder.Property(cashRegister => cashRegister.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(cashRegister => cashRegister.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasQueryFilter(cashRegister => cashRegister.DeletedAt == null);

        builder.Property(cashRegister => cashRegister.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(cashRegister => cashRegister.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(cashRegister => cashRegister.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(cashRegister => new
        {
            cashRegister.StoreId,
            cashRegister.Code
        })
        .IsUnique();

        builder.HasOne(cashRegister => cashRegister.Store)
            .WithMany(store => store.CashRegisters)
            .HasForeignKey(cashRegister => cashRegister.StoreId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

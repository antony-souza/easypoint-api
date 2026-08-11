using EasyPoint.Domain.Entities.StockMovements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyPoint.Infrastructure.Data.Configurations.StockMovements;

public sealed class StockMovementConfiguration
    : IEntityTypeConfiguration<StockMovement>
{
    public void Configure(EntityTypeBuilder<StockMovement> builder)
    {
        builder.ToTable("StockMovements");

        builder.HasKey(movement => movement.Id);

        builder.Property(movement => movement.Id)
            .ValueGeneratedOnAdd();

        builder.Property(movement => movement.Type)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(movement => movement.Quantity)
            .IsRequired();

        builder.Property(movement => movement.BeforeQuantity)
            .IsRequired();

        builder.Property(movement => movement.AfterQuantity)
            .IsRequired();

        builder.Property(movement => movement.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(movement => movement.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasQueryFilter(movement => movement.DeletedAt == null);

        builder.HasIndex(movement => new
        {
            movement.StockId,
            movement.CreatedAt
        });

        builder.HasOne(movement => movement.Stock)
            .WithMany()
            .HasForeignKey(movement => movement.StockId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

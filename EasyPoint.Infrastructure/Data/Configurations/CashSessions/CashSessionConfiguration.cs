using EasyPoint.Domain.Entities.CashSessions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyPoint.Infrastructure.Data.Configurations.CashSessions;

public sealed class CashSessionConfiguration
    : IEntityTypeConfiguration<CashSession>
{
    public void Configure(EntityTypeBuilder<CashSession> builder)
    {
        builder.ToTable("CashSessions");

        builder.HasKey(session => session.Id);

        builder.Property(session => session.Id)
            .ValueGeneratedOnAdd();

        builder.Property(session => session.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(session => session.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(session => session.Active)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(session => session.UserId)
            .IsRequired();

        builder.Property(session => session.CashRegisterId)
            .IsRequired();

        builder.Property(session => session.OpenedAt)
            .IsRequired();

        builder.Property(session => session.OpeningAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(session => session.ClosingAmount)
            .HasPrecision(18, 2);

        builder.Property(session => session.Canceled)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasQueryFilter(session =>
            session.DeletedAt == null);

        builder.HasOne(session => session.User)
            .WithMany(user => user.CashSessions)
            .HasForeignKey(session => session.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(session => session.CashRegister)
            .WithMany(cashRegister => cashRegister.CashSessions)
            .HasForeignKey(session => session.CashRegisterId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(session => session.CashRegisterId)
            .IsUnique()
            .HasFilter("\"Active\" = TRUE AND \"Canceled\" = FALSE AND \"DeletedAt\" IS NULL");
    }
}

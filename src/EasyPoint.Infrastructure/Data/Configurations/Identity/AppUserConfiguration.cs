using EasyPoint.Domain.Entities.Stores;
using EasyPoint.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyPoint.Infrastructure.Data.Configurations.Identity;

public sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(user => user.StoreId).IsRequired();

        builder.Property(user => user.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(user => user.UserName).IsRequired();
        builder.Property(user => user.Email).IsRequired();

        builder.HasIndex(user => user.NormalizedUserName)
            .HasDatabaseName("UserNameIndex")
            .IsUnique(false);

        builder.HasIndex(user => new { user.StoreId, user.NormalizedUserName })
            .HasDatabaseName("IX_AspNetUsers_StoreId_NormalizedUserName")
            .IsUnique();

        builder.HasIndex(user => new { user.StoreId, user.NormalizedEmail })
            .HasDatabaseName("IX_AspNetUsers_StoreId_NormalizedEmail")
            .IsUnique();

        builder.HasOne<Store>()
            .WithMany()
            .HasForeignKey(user => user.StoreId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

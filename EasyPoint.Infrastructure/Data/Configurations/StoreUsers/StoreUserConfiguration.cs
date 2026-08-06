using EasyPoint.Domain.Entities.StoreUsers;
using EasyPoint.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyPoint.Infrastructure.Data.Configurations.StoreUsers;

public sealed class StoreUserConfiguration : IEntityTypeConfiguration<StoreUser>
{
    public void Configure(EntityTypeBuilder<StoreUser> builder)
    {
        builder.ToTable("StoreUsers");

        builder.HasKey(member => member.Id);

        builder.Property(member => member.OrganizationId)
            .IsRequired();

        builder.HasOne(member => member.Organization)
            .WithMany()
            .HasForeignKey(member => member.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(member => member.Store)
            .WithMany(store => store.StoreUsers)
            .HasForeignKey(member => member.StoreId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<AppUser>()
            .WithMany(user => user.StoreUsers)
            .HasForeignKey(member => member.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(member => member.OrganizationId);

        builder.HasIndex(member => new
        {
            member.StoreId,
            member.UserId
        })
        .IsUnique();

        builder.HasQueryFilter(member =>
            member.DeletedAt == null &&
            member.Store.DeletedAt == null &&
            member.Organization.DeletedAt == null);
    }
}

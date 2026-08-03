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

        builder.Property(category => category.Id)
            .ValueGeneratedOnAdd();

        builder.Property(category => category.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(category => category.Store)
            .WithMany(store => store.Categories)
            .HasForeignKey(category => category.StoreId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

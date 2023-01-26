using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entities;

namespace WebApi.Data.Configurations;

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
           .IsRequired()
           .ValueGeneratedOnAdd();

        builder.Property(p => p.Title)
          .IsRequired()
          .HasMaxLength(100);
    }
}

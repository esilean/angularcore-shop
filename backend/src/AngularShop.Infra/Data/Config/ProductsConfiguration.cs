using AngularShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngularShop.Infra.Data.Config
{
    public class ProductsConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id)
                                .IsRequired();
            builder.Property(x => x.Name)
                                .IsRequired()
                                .HasMaxLength(100);
            builder.Property(x => x.Description)
                                .IsRequired();
            builder.Property(x => x.Price)
                                .IsRequired()
                                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.PictureUrl)
                                .IsRequired();

            builder.HasOne(b => b.ProductBrand)
                                .WithMany()
                                .HasForeignKey(f => f.ProductBrandId);
            builder.HasOne(b => b.ProductType)
                                .WithMany()
                                .HasForeignKey(f => f.ProductTypeId);                                
        }
    }
}
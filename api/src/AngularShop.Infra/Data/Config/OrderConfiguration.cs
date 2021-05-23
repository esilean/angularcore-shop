using System;
using AngularShop.Core.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngularShop.Infra.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShipToAddress, a =>
            {
                a.WithOwner();
                a.Property(x => x.FirstName).IsRequired();
                a.Property(x => x.LastName).IsRequired();
                a.Property(x => x.Street).IsRequired();
                a.Property(x => x.City).IsRequired();
                a.Property(x => x.State).IsRequired();
                a.Property(x => x.ZipCode).IsRequired();
            });

            builder.Property(s => s.Status)
                .HasConversion(
                    o => o.ToString(),
                    o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)
                );

            builder.HasMany(o => o.Items)
                .WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
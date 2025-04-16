using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Code).IsRequired();
            builder.HasIndex(o => o.Code).IsUnique();

            builder.Property(o => o.Status).IsRequired().HasConversion(
                   s => s.ToString(),
                   s => (OrderStatus)Enum.Parse(typeof(OrderStatus), s));

            builder.Property(o => o.ShippingOption).IsRequired().HasConversion(
                   so => so.ToString(),
                   so => (ShippingOption)Enum.Parse(typeof(ShippingOption), so));

            builder.Property(o => o.OrderDate).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(o => o.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(o => o.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()");

            //Relation Field Configuration
            builder.HasOne(o => o.PaymentDetail).WithOne(pd => pd.Order).HasForeignKey<PaymentDetail>(pd => pd.OrderId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(o => o.Shipments).WithOne(s => s.Order).HasForeignKey(s => s.OrderId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
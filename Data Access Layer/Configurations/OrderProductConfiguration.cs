using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.Configurations
{
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(op => new
            {
                op.OrderId,
                op.ProductId
            });

            builder.Property(op => op.Quantity).IsRequired();
            builder.Property(op => op.UnitPrice).IsRequired();

            builder.HasOne(op => op.Product).WithMany(p => p.OrderProducts)
            .HasForeignKey(op => op.ProductId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(op => op.Order).WithMany(p => p.OrderProducts)
            .HasForeignKey(op => op.OrderId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
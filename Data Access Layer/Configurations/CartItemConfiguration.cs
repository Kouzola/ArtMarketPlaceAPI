using Domain_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Quantity)
            .IsRequired()
            .HasDefaultValue(1);

            builder.HasOne(ci => ci.Cart).WithMany(c => c.Items).HasForeignKey(ci => ci.CartId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ci => ci.Product).WithMany().HasForeignKey(ci => ci.ProductId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}

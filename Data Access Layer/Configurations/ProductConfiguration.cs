using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Description).IsRequired();

            builder.Property(p => p.Reference).IsRequired();
            builder.HasIndex(p => p.Reference).IsUnique();

            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.Stock).IsRequired();
            builder.Property(p => p.ReservedStock).IsRequired();
            builder.Property(p => p.Available).IsRequired();

            builder.Property(p => p.Image).IsRequired();
            builder.HasIndex(p => p.Image).IsUnique();

            builder.Property(p => p.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(p => p.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()");

            //Relation Field Configuration
            builder.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(p => p.Customizations).WithOne(c => c.Product).HasForeignKey(c => c.ProductId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.Reviews).WithOne(r => r.Product).HasForeignKey(r => r.ProductId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
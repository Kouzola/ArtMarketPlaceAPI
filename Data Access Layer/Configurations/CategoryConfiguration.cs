using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.HasIndex(c => c.Name).IsUnique();

            builder.Property(c => c.Description).IsRequired();

            builder.Property(c => c.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(c => c.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.Configurations
{
    public class CustomizationConfiguration : IEntityTypeConfiguration<Customization>
    {
        public void Configure(EntityTypeBuilder<Customization> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).IsRequired();
            builder.Property(c => c.Price).IsRequired();

            builder.Property(c => c.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(c => c.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
        }
    }
}
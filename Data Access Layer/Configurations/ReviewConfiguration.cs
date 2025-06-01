using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Title).IsRequired().HasMaxLength(50);
            builder.Property(r => r.Description).IsRequired();
            builder.Property(r => r.Score).IsRequired();

            builder.Property(r => r.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(r => r.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.CustomerId).IsRequired();
        }
    }
}
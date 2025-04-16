using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.Configurations
{
    public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.TrackingNumber).IsRequired().HasMaxLength(32);

            builder.Property(s => s.Status).IsRequired().HasConversion(
                   ss => ss.ToString(),
                   ss => (ShipmentStatus)Enum.Parse(typeof(ShipmentStatus), ss));

            builder.Property(s => s.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(s => s.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
        }
    }
}
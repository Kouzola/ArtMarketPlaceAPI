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
    public class OrderStatusPerArtisanConfiguration : IEntityTypeConfiguration<OrderStatusPerArtisan>
    {
        public void Configure(EntityTypeBuilder<OrderStatusPerArtisan> builder)
        {
            builder.HasKey(os => new
            {
                os.OrderId,
                os.ArtisanId,
            });

            builder.Property(os => os.Status).IsRequired();

            builder.HasOne(os => os.Order).WithMany(o => o.OrderStatusPerArtisans)
            .HasForeignKey(os => os.OrderId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(os => os.Artisan).WithMany(a => a.OrderStatusPerArtisans)
            .HasForeignKey(os => os.ArtisanId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}

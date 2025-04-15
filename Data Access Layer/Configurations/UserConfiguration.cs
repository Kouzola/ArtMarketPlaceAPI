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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName).IsRequired().HasMaxLength(50);
            builder.HasIndex(u => u.UserName).IsUnique();

            builder.Property(u => u.Password).IsRequired().HasMaxLength(32);

            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);

            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.Role).IsRequired().HasConversion(
                   r => r.ToString(),
                   r => (Role)Enum.Parse(typeof(Role),r));

            builder.Property(u => u.Active).IsRequired().HasDefaultValue(true);

            builder.Property(g => g.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(g => g.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()");

            builder.OwnsOne(u => u.Address, a =>
            {
                a.Property(p => p.Street).HasColumnName("Street");
                a.Property(p => p.City).HasColumnName("City");
                a.Property(p => p.PostalCode).HasColumnName("PostalCode");
                a.Property(p => p.Country).HasColumnName("Country");
            });
            //Relation Field Configuration

            //Customer Role
            builder.HasMany(u => u.Reviews).WithOne(r => r.Customer).HasForeignKey(r => r.CustomerId);
            builder.HasMany(u => u.Inquiries).WithOne(r => r.Customer).HasForeignKey(r => r.CustomerId);
            builder.HasMany(u => u.Orders).WithOne(r => r.Customer).HasForeignKey(r => r.CustomerId);
            //Artisan Role
            builder.HasMany(u => u.Products).WithOne(r => r.Artisan).HasForeignKey(r => r.ArtisanId);
            builder.HasMany(u => u.Inquiries).WithOne(r => r.Artisan).HasForeignKey(r => r.ArtisanId);
            //DeliveryPartner Role
            builder.HasMany(u => u.Shipments).WithOne(r => r.DeliveryPartner).HasForeignKey(r => r.DeliveryPartnerId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.Configurations
{
    public class PaymentDetailConfiguration : IEntityTypeConfiguration<PaymentDetail>
    {
        public void Configure(EntityTypeBuilder<PaymentDetail> builder)
        {
            builder.HasKey(pd => pd.Id);

            builder.Property(pd => pd.PaymentMethod).IsRequired();
            builder.Property(pd => pd.Amount).IsRequired();

            builder.Property(pd => pd.PaymentDate).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(pd => pd.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(pd => pd.UpdatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
        }
    }
}
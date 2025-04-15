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
    public class InquiryConfiguration : IEntityTypeConfiguration<Inquiry>
    {
        public void Configure(EntityTypeBuilder<Inquiry> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Title).IsRequired().HasMaxLength(50);
            builder.Property(i => i.Description).IsRequired();            
            builder.Property(i => i.WantConsultation).IsRequired();

        }
    }
}

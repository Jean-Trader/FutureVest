using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.EntityConfiguration
{
    public class RateReturnConfiguration : IEntityTypeConfiguration<RateReturn>
    {
        public void Configure(EntityTypeBuilder<RateReturn> builder)
        {
            builder.ToTable("RateReturns");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.MinRate).IsRequired();
            builder.Property(e => e.MaxRate).IsRequired();
        }
    }
    
}

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
    public class CountryIndicatorsConfigurations : IEntityTypeConfiguration<CountryIndicator>
    {
        public void Configure(EntityTypeBuilder<CountryIndicator> builder)
        {
            builder.HasKey(ci => ci.Id);
            builder.ToTable("CountryIndicators");


            builder.Property(ci => ci.Year).IsRequired().HasMaxLength(4);
            builder.Property(ci => ci.Value).IsRequired().HasColumnType("decimal(20,4)");



            builder.HasOne<Country>(ci => ci.Country)
                   .WithMany(c => c.Indicators)
                   .HasForeignKey(ci => ci.CountryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<MacroIndicator>(ci => ci.MacroIndicator)
                   .WithMany(mi => mi.Indicators)
                   .HasForeignKey(ci => ci.MacroIndicatorId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

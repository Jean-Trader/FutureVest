using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("Countries");

            builder.Property(c => c.Name).IsRequired().HasMaxLength(40);
            builder.Property(c => c.Code).IsRequired().HasMaxLength(10);

            builder.HasMany<CountryIndicator> (c => c.Indicators)
                   .WithOne(ci => ci.Country)
                   .HasForeignKey(ci => ci.CountryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;
namespace Persistence.EntityConfiguration
{
    public class MacroIndicatorsConfiguration : IEntityTypeConfiguration<MacroIndicator>
    {
        public void Configure(EntityTypeBuilder<MacroIndicator> builder)
        {
            builder.ToTable("MacroIndicators");
            builder.HasKey(e => e.Id);

           
            builder.Property(e => e.Name).IsRequired().HasMaxLength(40);
            builder.Property(e => e.Weight).IsRequired().HasColumnType("decimal(20,2)");
           
            builder.HasMany<CountryIndicator>(mi => mi.Indicators)
                   .WithOne(ci => ci.MacroIndicator)
                   .HasForeignKey(ci => ci.MacroIndicatorId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
    
 }

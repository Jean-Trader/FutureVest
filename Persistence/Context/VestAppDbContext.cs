using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Context
{
    public class VestAppDbContext : DbContext
    {
        DbSet<Country> Countries { get; set; }
        DbSet<MacroIndicator> MacroIndicators { get; set; }
        DbSet<CountryIndicator> CountryIndicators { get; set; }
        DbSet<RateReturn> IndicatorValues { get; set; }

        public VestAppDbContext(DbContextOptions<VestAppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        
    }
}

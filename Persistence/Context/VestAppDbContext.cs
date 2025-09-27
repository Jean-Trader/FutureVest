using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using System.Reflection;

namespace Persistence.Context
{
    public class VestAppDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<MacroIndicator> MacroIndicators { get; set; }
        public DbSet<CountryIndicator> CountryIndicators { get; set; }
        public DbSet<RateReturn> IndicatorValues { get; set; }

        public VestAppDbContext(DbContextOptions<VestAppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        
    }
}

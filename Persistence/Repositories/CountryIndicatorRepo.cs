
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Entities;
using Persistence.Interfaces;

namespace Persistence.Repositories
{
    public class CountryIndicatorRepo : ICommonRepo<CountryIndicator>
    {
        VestAppDbContext _Context;
        public CountryIndicatorRepo(VestAppDbContext context) 
        { 
         _Context = context;
        }

        public IQueryable<CountryIndicator> GetAllQuery()
        {
            return _Context.Set<CountryIndicator>().AsQueryable();
        }

        public async Task<List<CountryIndicator>> GetAllList()
        {
            return await _Context.Set<CountryIndicator>().ToListAsync();
        }

        public async Task<CountryIndicator?> CreateAsync(CountryIndicator country)
        {
            if (country != null)
            {
                await _Context.Set<CountryIndicator>().AddAsync(country);
                await _Context.SaveChangesAsync();
                return country;
            }
            else { return null; }
        }

        public async Task<CountryIndicator?> GetByIdAsync(int id)
        {
            var country = await _Context.Set<CountryIndicator>().FindAsync(id);
            if (country != null)
            {
                return country;
            }
            else { return null; }
        }

        public async Task<CountryIndicator?> UpdateByAsync(CountryIndicator country, int id)
        {
            var entry = await _Context.Set<CountryIndicator>().FindAsync(id);
            if (entry != null)
            {

                _Context.Entry(entry).CurrentValues.SetValues(country);
                await _Context.SaveChangesAsync();
                return entry;

            }
            else
            {
                return null;
            }
        }
        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entry = await _Context.Set<CountryIndicator>().FindAsync(id);
            if (entry != null)
            {
                _Context.Set<CountryIndicator>().Remove(entry);
                await _Context.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }
    }
}


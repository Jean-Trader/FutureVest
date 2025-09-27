using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Entities;
using Persistence.Context;
using Microsoft.EntityFrameworkCore;
namespace Persistence.Repositories
{
    public class CountryRepo : ICommonRepo<Country>
    {
        VestAppDbContext _Context { get; set; }
        public CountryRepo(VestAppDbContext context) 
        { 
            _Context = context;
        }

        public IQueryable<Country> GetAllQuery() 
        { 
          return _Context.Set<Country>().AsQueryable();
        }

        public async Task<List<Country>>GetAllList()
        { 
          return await _Context.Set<Country>().ToListAsync();
        }

        public async Task<Country?> CreateAsync(Country country) 
        {
            if (country != null)
            {
                await _Context.Set<Country>().AddAsync(country);
                await _Context.SaveChangesAsync();
                return country;
            }
            else { return null; }
        }
        
        public async Task<Country?> GetByIdAsync(int id) 
        {
            var country = await _Context.Set<Country>().FindAsync(id);
            if (country != null)
            {
                return country;
            }
            else { return null; }
        }

        public async Task<Country?> UpdateByAsync(Country country, int id) 
        {
            var entry = await _Context.Set<Country>().FindAsync(id);
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
            var entry = await _Context.Set<Country>().FindAsync(id);
            if (entry != null)
            {
                _Context.Set<Country>().Remove(entry);
                await _Context.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }
    }
}

    


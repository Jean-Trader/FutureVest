using Persistence.Entities;
using Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Persistence.Repositories
{
    public class MacroIndicatorRepo : ICommonRepo<MacroIndicator>
    {
        VestAppDbContext _Context;
        public MacroIndicatorRepo(VestAppDbContext context)
        {
            _Context = context;
        }
        public IQueryable<MacroIndicator> GetAllQuery()
        {
            return _Context.Set<MacroIndicator>().AsQueryable();
        }
        public async Task<List<MacroIndicator>> GetAllList()
        {
            return await _Context.Set<MacroIndicator>().ToListAsync();
        }
        public async Task<MacroIndicator?> CreateAsync(MacroIndicator macroIndicator)
        {
            if (macroIndicator != null)
            {
                await _Context.Set<MacroIndicator>().AddAsync(macroIndicator);
                await _Context.SaveChangesAsync();
                return macroIndicator;
            }
            else { return null; }
        }
        public async Task<MacroIndicator?> GetByIdAsync(int id)
        {
            var macroIndicator = await _Context.Set<MacroIndicator>().FindAsync(id);
            if (macroIndicator != null)
            {
                return macroIndicator;
            }
            else { return null; }
        }
        public async Task<MacroIndicator?> UpdateByAsync(MacroIndicator macroIndicator, int id)
        {
            var entry = await _Context.Set<MacroIndicator>().FindAsync(id);
            if (entry != null)
            {
                _Context.Entry(entry).CurrentValues.SetValues(macroIndicator);
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
            var entry = await _Context.Set<MacroIndicator>().FindAsync(id);
            if (entry != null)
            {
                _Context.Set<MacroIndicator>().Remove(entry);
                await _Context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        

    }
}

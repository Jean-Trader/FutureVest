using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Persistence.Context;
using Persistence.Interfaces;

namespace Persistence.Repositories
{
    public class RateReturnRepo : IRateReturnRepo
    {
        VestAppDbContext _Context;
        public RateReturnRepo(VestAppDbContext context)
        {
            _Context = context;
        }
        public async Task<bool> NewAsync(RateReturn newRate)
        {
            var rateReturn = await _Context.Set<RateReturn>().FirstAsync();

            if (rateReturn != null)
            {
                if (newRate != null)
                {
                    _Context.Entry(rateReturn).CurrentValues.SetValues(newRate);
                    await _Context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new ArgumentNullException(nameof(newRate), "The newRate parameter cannot be null.");
                }
            }
            else
            {
                if (newRate == null)
                {
                    throw new ArgumentNullException(nameof(newRate), "The newRate parameter cannot be null.");
                }
                else
                {
                    await _Context.Set<RateReturn>().AddAsync(newRate);
                    await _Context.SaveChangesAsync();
                    return true;
                }
            }
        }

        public async Task<RateReturn?> GetTheAsync()
        {
            var rateReturn = await _Context.Set<RateReturn>().FirstOrDefaultAsync();

            if (rateReturn != null)
            {
                return rateReturn;
            }
            else
            {
                return null;
            }

        }
    }
}

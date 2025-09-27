
using Persistence.Entities;

namespace Persistence.Interfaces
{
    public interface IRateReturnRepo
    {
        Task<bool> NewAsync(RateReturn rateReturn);
        Task<RateReturn?> GetTheAsync();
        

    }
}

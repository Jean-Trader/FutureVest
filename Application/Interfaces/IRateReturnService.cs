
using Application.DTOs.RateReturn;
namespace Application.Interfaces
{
    public interface IRateReturnService
    {
        Task<bool>CreateOrUpdateAsync(RateReturnDto rateReturn);
        Task<RateReturnDto>GetRateReturn();
    }
}

using Application.Interfaces;
using Persistence.Entities;
using Persistence.Interfaces;
using Persistence.Repositories;
using Persistence.Context;
using Application.DTOs.RateReturn;
namespace Application.Services
{
    public class RateReturnService : IRateReturnService
    {
        private RateReturnRepo _rateReturnRepo;

        public RateReturnService(VestAppDbContext context) 
        {
            _rateReturnRepo = new RateReturnRepo(context);
        }

        public async Task<bool> CreateOrUpdateAsync(RateReturnDto rateReturn)
        {
            try
            {
                var existingRateReturn = await _rateReturnRepo.GetTheAsync();

                if (existingRateReturn != null)
                {
                    existingRateReturn.MinRate = rateReturn.MinRate;
                    existingRateReturn.MaxRate = rateReturn.MaxRate;

                    await _rateReturnRepo.NewAsync(existingRateReturn);
                    return true;
                }
                else
                {
                    var newRateReturn = new RateReturn
                    {
                        Id = 1,
                        MinRate = rateReturn.MinRate,
                        MaxRate = rateReturn.MaxRate
                    };
                    await _rateReturnRepo.NewAsync(newRateReturn);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<RateReturnDto> GetRateReturn() 
        { 
        
         var Rate = await _rateReturnRepo.GetTheAsync();

            RateReturnDto rateReturn = new RateReturnDto
            {
                Id = Rate.Id,
                MinRate = Rate.MinRate,
                MaxRate = Rate.MaxRate
            };

            return rateReturn;
        }

    }
}

using Application.Interfaces;
using Persistence.Entities;
using Persistence.Interfaces;
using Persistence.Repositories;
using Persistence.Context;
using Application.DTOs.MacroIndicator;


namespace Application.Services
{
    public class MacroIndicatorServices : IMacroIndicatorService
    {
       private ICommonRepo<MacroIndicator> _macroIndicatorRepo;

        public MacroIndicatorServices(VestAppDbContext context)
        {
            _macroIndicatorRepo = new MacroIndicatorRepo(context);
        }

        public async Task<bool> CreateAsync(MacroIndicatorDto dto)
        {
            try
            {
                var Indicators = await _macroIndicatorRepo.GetAllList();

                var weightSum = Indicators.Sum(i => i.Weight);

                if (weightSum + dto.Weight > 1) 
                { 
                 
                    return false;

                }
                else
                {
                    MacroIndicator macroIndicator = new MacroIndicator
                    {
                        Id = 0,
                        Name = dto.Name,
                        Weight = dto.Weight,
                        HighBetter = dto.HighBetter
                    };
                    var createdEntity = await _macroIndicatorRepo.CreateAsync(macroIndicator);

                    return true;
                }

                   
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                var result = await _macroIndicatorRepo.DeleteByIdAsync(Id);
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<List<MacroIndicatorDto>> GetAllAsync()
        {
            try
            {
                var macroIndicators = await _macroIndicatorRepo.GetAllList();
                if (macroIndicators != null)
                {
                    List<MacroIndicatorDto> macroIndicatorDtos = macroIndicators.Select(mi => new MacroIndicatorDto
                    {
                        Id = mi.Id,
                        Name = mi.Name,
                        Weight = mi.Weight,
                        HighBetter = mi.HighBetter
                    }).ToList();
                    return macroIndicatorDtos;
                }
                return new List<MacroIndicatorDto>();
            }
            catch (Exception)
            {
                return new List<MacroIndicatorDto>();
            }
        }
        public async Task<MacroIndicatorDto?> GetByIdAsync(int id)
        {
            try
            {
                var macroIndicator = await _macroIndicatorRepo.GetByIdAsync(id);
                if (macroIndicator != null)
                {
                    return new MacroIndicatorDto
                    {
                        Id = macroIndicator.Id,
                        Name = macroIndicator.Name,
                        Weight = macroIndicator.Weight,
                        HighBetter = macroIndicator.HighBetter
                    };
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public async Task<bool> UpdateAsync(MacroIndicatorDto dto, int id)
        {
            try
            {
                var existingEntity = await _macroIndicatorRepo.GetByIdAsync(id);
                if (existingEntity != null)
                {
                    MacroIndicator macroIndicator = new MacroIndicator
                    {
                        Id = id,
                        Name = dto.Name,
                        Weight = dto.Weight,
                        HighBetter = dto.HighBetter
                    };
                    var result = await _macroIndicatorRepo.UpdateByAsync(macroIndicator, id);
                    if (result != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}

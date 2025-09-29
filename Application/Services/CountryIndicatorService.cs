using Application.DTOs.Common;
using Application.DTOs.Country;
using Application.DTOs.CountryIndicator;
using Application.DTOs.MacroIndicator;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Entities;
using Persistence.Interfaces;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Services
{

    public class CountryIndicatorService : ICountryIndicatorService 
    {
        private ICommonRepo<CountryIndicator> _repository;

        public CountryIndicatorService(VestAppDbContext context)
        {
            _repository = new CountryIndicatorRepo(context);
        }

        public async Task<List<CountryIndicatorDto>> GetAllAsync()
        {
            try
            {
                var countries = await _repository.GetAllList();

                if (countries != null)
                {
                    List<CountryIndicatorDto> countryDto = countries.Select(c => new CountryIndicatorDto
                    {
                        Id = c.Id,
                        CountryId = c.CountryId,
                        MacroIndicatorId = c.MacroIndicatorId,
                        Value = c.Value,
                        Year = c.Year
                    }).ToList();

                    return countryDto;
                }
                return [];

            }
            catch (Exception)
            {
                return [];
            }
        }

        public async Task<CountryIndicatorDto?> GetByIdAsync(int Id)
        {
            try
            {
                var countryI = await _repository.GetByIdAsync(Id);

                if (countryI != null)
                {
                    CountryIndicatorDto countryIndicatorDto = new CountryIndicatorDto
                    {
                        Id = countryI.Id,
                        CountryId = countryI.CountryId,
                        MacroIndicatorId = countryI.MacroIndicatorId,
                        Value = countryI.Value,
                        Year = countryI.Year

                    };
                    return countryIndicatorDto;
                }
                return null;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public async Task<bool> CreateAsync(CountryIndicatorDto countryDto)
        {
            try
            {
                if (countryDto != null)
                {
                    var indicators = _repository.GetAllQuery();

                    var exists = indicators.Any(i => i.MacroIndicatorId == countryDto.MacroIndicatorId && i.Year == countryDto.Year);

                    if (exists)
                    {
                        return false;
                    }

                    CountryIndicator country = new CountryIndicator
                    {
                        Id = countryDto.Id,
                        CountryId = countryDto.CountryId,
                        MacroIndicatorId = countryDto.MacroIndicatorId,
                        Value = countryDto.Value,
                        Year = countryDto.Year

                    };
                    var result = await _repository.CreateAsync(country);
                    if (result != null)
                    {
                        return true;
                    }
                    return false;

                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> UpdateAsync(CountryIndicatorDto countryIndicatorDto, int id)
        {
            try
            {
                if (countryIndicatorDto != null)
                {
                    CountryIndicator countryIndicator = new CountryIndicator
                    {
                        Id = countryIndicatorDto.Id,
                        CountryId = countryIndicatorDto.CountryId,
                        MacroIndicatorId = countryIndicatorDto.MacroIndicatorId,
                        Value = countryIndicatorDto.Value,
                        Year = countryIndicatorDto.Year

                    };
                    var result = await _repository.UpdateByAsync(countryIndicator, id);
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
        public async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                var result = await _repository.DeleteByIdAsync(Id);
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<CountryIndicatorDto> GetAllQuery()
        {
            try
            {
                var countryIndicator = _repository.GetAllQuery().Include(c => c.Country).Include(c => c.MacroIndicator);

                if (countryIndicator != null)
                {
                    var listCountryDto = countryIndicator.Select(c => new CountryIndicatorDto
                    {
                        Id = c.Id,
                        CountryId = c.CountryId,
                        MacroIndicatorId = c.MacroIndicatorId,
                        Value = c.Value,
                        Year = c.Year,
                        Country = c.Country != null ? new CountryDto
                        {
                            Id = c.Country.Id,
                            Name = c.Country.Name,
                            Code = c.Country.Code
                        } : null,
                        MacroIndicator = c.MacroIndicator != null ? new MacroIndicatorDto
                        {
                            Id = c.MacroIndicator.Id,
                            Name = c.MacroIndicator.Name,
                            Weight = c.MacroIndicator.Weight,
                            HighBetter = c.MacroIndicator.HighBetter
                        } : null
                    }).ToList();

                    return listCountryDto;
                }

                return [];
            }
            catch (Exception)
            {
                return [];
            }
        }
        public List<CountryIndicatorDto> GetFilter(int countryId, int date)
        {

            try
            {
                var countryIndicators = _repository.GetAllQuery().Include(ci => ci.Country).Include(ci => ci.MacroIndicator)
                .Where(ci => ci.CountryId == countryId && ci.Year == date).ToList();



                if (countryIndicators != null && countryIndicators.Count > 0)
                {
                    var listCountryIndicatorDtoFilter = countryIndicators.Select(ci => new CountryIndicatorDto
                    {
                        Id = ci.Id,
                        CountryId = ci.CountryId,
                        MacroIndicatorId = ci.MacroIndicatorId,
                        Value = ci.Value,
                        Year = ci.Year,
                        Country = ci.Country != null ? new CountryDto
                        {
                            Id = ci.Country.Id,
                            Name = ci.Country.Name,
                            Code = ci.Country.Code
                        } : null,
                        MacroIndicator = ci.MacroIndicator != null ? new MacroIndicatorDto
                        {
                            Id = ci.MacroIndicator.Id,
                            Name = ci.MacroIndicator.Name,
                            Weight = ci.MacroIndicator.Weight,
                            HighBetter = ci.MacroIndicator.HighBetter
                        } : null
                    }).ToList();

                    return listCountryIndicatorDtoFilter;
                }
                else
                { 
                 return [];
                }
            }
            catch (Exception)
            {
                return [];

            }
        }
        public List<CountryIndicatorDto> GetFilterForYear(int date)
        {
            try
            {
                
                var countryIndicators = _repository.GetAllQuery().Include(ci => ci.MacroIndicator)
                    .Where(ci => ci.Year == date).ToList();

                if (countryIndicators == null || countryIndicators.Count == 0)
                {
                    return new List<CountryIndicatorDto>();
                }

                var listCountryIndicatorDto = countryIndicators.Select(ci => new CountryIndicatorDto
                {
                    
                    Id = ci.Id,
                    CountryId = ci.CountryId,
                    MacroIndicatorId = ci.MacroIndicatorId,
                    Value = ci.Value,
                    Year = ci.Year,
                }).ToList();

                return listCountryIndicatorDto;
            }
            catch (Exception)
            {
                return new List<CountryIndicatorDto>();
            }
        }
    }
}


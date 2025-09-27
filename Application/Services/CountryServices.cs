using Application.Interfaces;
using Persistence.Entities;
using Persistence.Interfaces;
using Persistence.Repositories;
using Persistence.Context;
using Application.DTOs.Country;
using Application.DTOs.CountryIndicator;
using Microsoft.EntityFrameworkCore;
namespace Application.Services
{
    public class CountryServices : ICountryServices
    {
        ICommonRepo<Country> _repository;
        public CountryServices(VestAppDbContext context)
        {
            _repository = new CountryRepo(context);

        }

        public async Task<List<CountryDto>> GetAllAsync()
        {
            try
            {
                var countries = await _repository.GetAllList();

                if (countries != null)
                {
                    List<CountryDto> countryDto = countries.Select(c => new CountryDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Code = c.Code
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

        public async Task<CountryDto?> GetByIdAsync(int Id)
        {
            try
            {
                var country = await _repository.GetByIdAsync(Id);

                if (country != null)
                {
                    CountryDto countryDto = new CountryDto
                    {
                        Id = country.Id,
                        Name = country.Name,
                        Code = country.Code
                    };
                    return countryDto;
                }
                return null;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public async Task<bool> CreateAsync(CountryDto countryDto)
        {
            try
            {
                if (countryDto != null)
                {

                    Country country = new Country
                    {
                        Id = countryDto.Id,
                        Name = countryDto.Name,
                        Code = countryDto.Code
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
        public async Task<bool> UpdateAsync(CountryDto countryDto, int id)
        {
            try
            {
                if (countryDto != null)
                {
                    Country country = new Country
                    {
                        Id = countryDto.Id,
                        Name = countryDto.Name,
                        Code = countryDto.Code
                    };
                    var result = await _repository.UpdateByAsync(country, id);
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

        public List<CountryDto> GetAllQuery()
        {
            try
            {
                var country = _repository.GetAllQuery();
                var countryWithIndicators = country.Include(c => c.Indicators);

                if (country != null)
                {
                    var listCountryDto = countryWithIndicators.Select(c => new CountryDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Code = c.Code,
                        Indicators = c.Indicators.Where(c => c != null).Select(i => new CountryIndicatorDto
                        {
                            Id = i.Id,
                            CountryId = i.CountryId,
                            MacroIndicatorId = i.MacroIndicatorId,
                            Value = i.Value,
                            Year = i.Year,
                            
                        }).ToList()

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
    }

}                
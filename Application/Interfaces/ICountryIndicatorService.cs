using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.CountryIndicator;
using Application.Interfaces;
namespace Application.Interfaces
{
    public interface ICountryIndicatorService :IDefaultServices<CountryIndicatorDto>, IQueryServices<CountryIndicatorDto>
    { 
        List<CountryIndicatorDto> GetFilter(int countryId, int date);
        List<CountryIndicatorDto> GetFilterForYear(int date);
    }
}

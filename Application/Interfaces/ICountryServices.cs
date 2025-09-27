using Application.DTOs.Country;

namespace Application.Interfaces
{
    public interface ICountryServices : IDefaultServices<CountryDto>, IQueryServices<CountryDto>
    {
    }
}

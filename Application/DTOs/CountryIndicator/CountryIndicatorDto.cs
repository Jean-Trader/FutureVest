using Application.DTOs.Common;
using Application.DTOs.Country;
using Application.DTOs.MacroIndicator;

namespace Application.DTOs.CountryIndicator
{
    public class CountryIndicatorDto: BasicEntityDto<int>
    {
        public required int CountryId { get; set; }
        public required int MacroIndicatorId { get; set; }
        public required int Year { get; set; }
        public required decimal Value { get; set; }
        public CountryDto? Country { get; set; }
        public MacroIndicatorDto? MacroIndicator { get; set; }
    }
}

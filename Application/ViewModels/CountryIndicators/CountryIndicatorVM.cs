using Application.DTOs.MacroIndicator;
using Application.ViewModels.CommonView;
using Application.ViewModels.Country;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.CountryIndicators
{
    public class CountryIndicatorVM : CommonModel<int>
    {
        [Required(ErrorMessage = "Country Required")]
        public required int CountryId { get; set; }
        [Required(ErrorMessage = "Macro Indicator Required")]
        public required int MacroIndicatorId { get; set; }
        [Required(ErrorMessage = "Year Required")]
        [Range(1900, 2100, ErrorMessage = "El año debe ser válido")]
        public required int Year { get; set; }
        [Required(ErrorMessage = "Value Required")]
        [Range(1, 100000000000, ErrorMessage = "Year required")]
        public decimal Value { get; set; }
        public CountryViewModel? Country { get; set; }
        public MacroIndicatorDto? MacroIndicator { get; set; }

    }
}

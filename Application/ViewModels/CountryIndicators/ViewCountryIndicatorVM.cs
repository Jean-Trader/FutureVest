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
    public class ViewCountryIndicatorVM : CommonModel<int>
    {
        public required int CountryId { get; set; }
        public required int MacroIndicatorId { get; set; }
        public required string CountryName { get; set; } 
        public required string MacroIndicatorName { get; set; }
        public required int Year { get; set; }
        public required decimal Value { get; set; }
     

    }
}

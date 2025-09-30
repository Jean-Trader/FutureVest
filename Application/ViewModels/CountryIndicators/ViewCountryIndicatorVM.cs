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
        public int CountryId { get; set; }
        public int MacroIndicatorId { get; set; }
        public string CountryName { get; set; } 
        public string MacroIndicatorName { get; set; }
        public int Year { get; set; }
        public decimal Value { get; set; }
     

    }
}

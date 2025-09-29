using Application.DTOs.CountryIndicator;
using Application.ViewModels.CommonView;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Country
{
    public class CountryViewModel : CommonModel<int>
    {
        [Required(ErrorMessage = "Name is Required" )]
        public required string Name { get; set; }
        [Required(ErrorMessage = "Code id Required")]
        public required string Code { get; set; }
        public ICollection<CountryIndicatorDto>? Indicators { get; set; }
    }
}

using Application.DTOs.Common;
using Application.DTOs.CountryIndicator;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Country
{
    public class CountryDto : BasicEntityDto<int>
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public ICollection<CountryIndicatorDto>? Indicators { get; set; }

    }
}

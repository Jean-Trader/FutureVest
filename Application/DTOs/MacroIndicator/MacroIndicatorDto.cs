using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.CountryIndicator;

namespace Application.DTOs.MacroIndicator
{
    public class MacroIndicatorDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Weight { get; set; }
        public required bool HighBetter { get; set; }
        public ICollection<CountryIndicatorDto>? Indicators { get; set; }
    }
}

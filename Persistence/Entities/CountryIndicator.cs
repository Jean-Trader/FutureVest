using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Entities
{
    public class CountryIndicator
    {
        public required int Id { get; set; }
        public required int CountryId { get; set; }
        public required int MacroIndicatorId { get; set; }
        public required string Year { get; set; }
        public required decimal Value { get; set; }
        public Country? Country { get; set; }
        public MacroIndicator? MacroIndicator { get; set; }


    }
}

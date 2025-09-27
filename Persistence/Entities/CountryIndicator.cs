using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Common;

namespace Persistence.Entities
{
    public class CountryIndicator : BasicEntity<int>
    {
        public required int CountryId { get; set; }
        public required int MacroIndicatorId { get; set; }
        public required int Year { get; set; }
        public required decimal Value { get; set; }
        public Country? Country { get; set; }
        public MacroIndicator? MacroIndicator { get; set; }


    }
}

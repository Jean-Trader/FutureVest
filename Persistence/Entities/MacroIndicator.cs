using Persistence.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Entities
{
    public class MacroIndicator : BasicEntity<int>
    {
        public required string Name { get; set; }
        public required decimal Weight { get; set; }
        public required bool HighBetter { get; set; }
        public ICollection<CountryIndicator>? Indicators { get; set; }
    }
}

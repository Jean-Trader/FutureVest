using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Entities
{
    public class RateReturn
    {
        public required int Id { get; set; }
        public required double MinRate { get; set; }
        public required double MaxRate { get; set; }
    }
}

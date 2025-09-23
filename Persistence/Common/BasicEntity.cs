using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Common
{
    public class BasicEntity<Tkey>
    {
        public required Tkey Id { get; set; }
    }
}

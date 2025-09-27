using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Common
{
    public class BasicEntityDto<Tkey>
    {
        public required Tkey Id { get; set; }
    }
}

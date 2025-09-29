using Application.ViewModels.CommonView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.CountryIndicators
{
    public class AddCountryModel : CommonModel<int>
    {
        public required string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels.CommonView;

namespace Application.ViewModels.Country
{
    public class DeleteCountryVM : CommonModel<int>
    {
        public required string Name;
    }
}

using Application.ViewModels.CommonView;
using System.ComponentModel.DataAnnotations;
namespace Application.ViewModels.RateOfReturns
{
    public class RateReturnVM : CommonModel<int>
    {
        [Required(ErrorMessage = "MinRate Required")]
        public required decimal MinRate { get; set; }
        [Required(ErrorMessage = "MaxRate Required")]
        public required decimal MaxRate { get; set; }
    }
}

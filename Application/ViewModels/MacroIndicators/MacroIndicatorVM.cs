using Application.ViewModels.CountryIndicators;
using System.ComponentModel.DataAnnotations;


namespace Application.ViewModels.MacroIndicators
{
    public class MacroIndicatorVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Value Required")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "Value Required")]
        public required decimal Weight { get; set; }
        [Required(ErrorMessage = "Value Required")]
        public required bool HighBetter { get; set; }
        public ICollection<CountryIndicatorVM>? Indicators { get; set; }
       
    }
}

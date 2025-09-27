using Persistence.Common;


namespace Application.DTOs.RateReturn
{
    public class RateReturnDto : BasicEntity<int>
    {
        public required double MinRate { get; set; }
        public required double MaxRate { get; set; }
    }
}

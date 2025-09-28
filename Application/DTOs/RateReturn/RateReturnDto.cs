using Persistence.Common;


namespace Application.DTOs.RateReturn
{
    public class RateReturnDto : BasicEntity<int>
    {
        public required decimal MinRate { get; set; }
        public required decimal MaxRate { get; set; }
    }
}

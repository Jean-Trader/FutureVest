using Persistence.Common;


namespace Persistence.Entities
{
    public class RateReturn : BasicEntity<int>
    {
        public required decimal MinRate { get; set; }
        public required decimal MaxRate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Ranking
{
    public class CalculatedRankingVM
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string Code { get; set; }
        public double Scoring { get; set; }
        public double StimatedRate { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Ranking
{
    public class CalculatedRankingDto
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public double Scoring {  get; set; }
        public double StimatedRate { get; set; }

    }
}

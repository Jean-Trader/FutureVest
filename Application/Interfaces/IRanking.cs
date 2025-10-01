using Application.DTOs.Country;
using Application.DTOs.Ranking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRanking
    {
        Task<List<CountryDto>> RankingValidationsCountry(int Year);
        Task<List<CalculatedRankingDto>> CalculateRankingForYear(int year);
    }
}

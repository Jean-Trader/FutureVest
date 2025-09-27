using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.DTOs;
using Application.DTOs.Ranking;

namespace Application.Services
{
    public class CalculateRanking
    {    
        IMacroIndicatorService _macroIndicatorService;
        ICountryIndicatorService _countryIndicatorService;
        ICountryServices _countryService;
        IRateReturnService _rateReturnService;
        public CalculateRanking(IMacroIndicatorService macroIndicator,ICountryIndicatorService countryIndicator,
                                ICountryServices country, IRateReturnService rateReturn) 
        {
        
            _macroIndicatorService = macroIndicator; 
            _countryIndicatorService = countryIndicator;
            _countryService = country;
            _rateReturnService = rateReturn;
         
        }

        // Cambia el nombre del método para que no coincida con el nombre de la clase
        public async Task<List<CalculatedRankingDto>> CalculateRankingForYear(int year)
        {
            var macroIndicators = await _macroIndicatorService.GetAllAsync();
            var Scores = new Dictionary<int, decimal>();

            if (macroIndicators != null)
            {
                foreach (var macro in macroIndicators)
                {
                    var I = await _countryIndicatorService.GetAllAsync();

                    if (I != null)
                    {
                        var Indicators = I.Where(i => i.MacroIndicatorId == macro.Id && i.Year == year).ToList();

                        var max = Indicators.Max().Value;
                        var min = Indicators.Min().Value;

                        decimal valueRange = max - min;

                        foreach (var countryIndicator in Indicators)
                        {
                            decimal Zscore = 0;
                            decimal ValueActual = countryIndicator.Value;

                            if (macro.HighBetter)
                            {
                                Zscore = (ValueActual - min) / valueRange;
                            }
                            else
                            {
                                Zscore = (max - ValueActual) / valueRange;
                            }

                            decimal scoreContribution = Zscore * macro.Weight;

                            Scores.TryAdd(countryIndicator.CountryId, 0);
                            Scores[countryIndicator.CountryId] += scoreContribution;
                        }
                    }

                    var countries = await _countryService.GetAllAsync();

                    var rateReturn = await _rateReturnService.GetRateReturn();

                    if (rateReturn == null || !macroIndicators.Any() || !countries.Any())
                    {
                        return new List<CalculatedRankingDto>();
                    }

                    double Rmin = 2;
                    double Rmax = 15;

                    if (rateReturn.MaxRate != 0)
                    {
                        Rmin = rateReturn.MinRate;
                        Rmax = rateReturn.MaxRate;
                    }

                    double rateRange = Rmax - Rmin;

                    var result = new List<CalculatedRankingDto>();

                    foreach (var score in Scores)
                    {
                        var country = countries.FirstOrDefault(c => c.Id == score.Key);
                        if (country != null)
                        {
                            double scoring = (double)score.Value;
                            double estimatedRate = Rmin + (rateRange * scoring);

                            result.Add(new CalculatedRankingDto
                            {
                                CountryId = country.Id,
                                CountryName = country.Name,
                                Scoring = scoring,
                                StimatedRate = estimatedRate
                            });
                        }
                    }

                    return result.OrderByDescending(r => r.Scoring).ToList();
                }
            }

            return new List<CalculatedRankingDto>();
        }

               
        
    }
}

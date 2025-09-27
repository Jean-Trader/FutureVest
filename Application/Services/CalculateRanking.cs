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

        public async Task<List<CalculatedRankingDto>> CalculateRanking()
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
                        var Indicators = I.Where(i => i.MacroIndicatorId == macro.Id);

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
                    var rateSettings = await _rateReturnService.GetRateReturn();

                    if (rateSettings == null || !macroIndicators.Any() || !allCountryValues.Any())
                    {
                        return new List<FinalRankingResult>();
                    }

                    decimal R_min = rateSettings.MinimumRate;
                    decimal R_max = rateSettings.MaximumRate;
                    decimal rateRange = R_max - R_min;
                }
            }

               
        }
    }
}

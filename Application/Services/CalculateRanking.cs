using Application.DTOs.Country;
using Application.DTOs.MacroIndicator;
using Application.DTOs.Ranking;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Application.Services
{
    public class CalculateRanking
    {
        IMacroIndicatorService _macroIndicatorService;
        ICountryIndicatorService _countryIndicatorService;
        ICountryServices _countryService;
        IRateReturnService _rateReturnService;
        public CalculateRanking(IMacroIndicatorService macroIndicator, ICountryIndicatorService countryIndicator,
                                ICountryServices country, IRateReturnService rateReturn)
        {

            _macroIndicatorService = macroIndicator;
            _countryIndicatorService = countryIndicator;
            _countryService = country;
            _rateReturnService = rateReturn;

        }


        public async Task<List<CountryDto>> RankingValidationsCountry(int Year)
        {

            var allCountries = await _countryService.GetAllAsync();
            var macroIndicators = await _macroIndicatorService.GetAllAsync();
            var allCountryIndicatorsForYear = _countryIndicatorService.GetFilterForYear(Year);

            int requiredIndicatorCount = macroIndicators.Count;

            if (requiredIndicatorCount == 0 || !allCountryIndicatorsForYear.Any())
            {
                return new List<CountryDto>();
            }

            var validCountriesList = new List<CountryDto>();


            foreach (var country in allCountries)
            {
                int actualIndicatorCount = 0;
                bool hasAllIndicators = true;


                foreach (var indicator in allCountryIndicatorsForYear)
                {
                    if (indicator.CountryId == country.Id)
                    {
                        actualIndicatorCount++;
                    }
                }


                if (actualIndicatorCount == requiredIndicatorCount)
                {

                    validCountriesList.Add(country);
                }
            }

            return validCountriesList;
        }


        public async Task<List<CalculatedRankingDto>> CalculateRankingForYear(int year)
        {

            var macroIndicators = await _macroIndicatorService.GetAllAsync();

            var validCountries = await RankingValidationsCountry(year);

            var totalWeight = macroIndicators.Sum(m => m.Weight);

            if (totalWeight != 1)
            {
                return [];
            }

            if (!validCountries.Any())
            {
                return new List<CalculatedRankingDto>();
            }

            var Scores = new Dictionary<int, decimal>();

            if (macroIndicators != null)
            {
                var I = await _countryIndicatorService.GetAllAsync();

                foreach (var macro in macroIndicators)
                {
                    if (I != null)
                    {
                        var Indicators = I.Where(i => i.MacroIndicatorId == macro.Id && i.Year == year).ToList();

                        if (!Indicators.Any()) continue;

                        var max = Indicators.Max().Value;
                        var min = Indicators.Min().Value;

                        decimal valueRange = max - min;

                        foreach (var countryIndicator in Indicators)
                        {
                            if (!validCountries.Any(c => c.Id == countryIndicator.CountryId)) continue;

                            decimal Zscore = 0;
                            decimal ValueActual = countryIndicator.Value;

                            if (valueRange == 0)
                            {
                                Zscore = 1;
                            }
                            else if (macro.HighBetter)
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
                }
            }

            var countries = validCountries;

            var rateReturn = await _rateReturnService.GetRateReturn();

            if (rateReturn == null || !countries.Any())
            {
                return new List<CalculatedRankingDto>();
            }

            decimal Rmin = 2;
            decimal Rmax = 15;

            if (rateReturn.MaxRate != 0)
            {
                Rmin = rateReturn.MinRate;
                Rmax = rateReturn.MaxRate;
            }

            decimal rateRange = Rmax - Rmin;

            var result = new List<CalculatedRankingDto>();

            foreach (var score in Scores)
            {
                var country = countries.FirstOrDefault(c => c.Id == score.Key);

                if (country != null)
                {
                    decimal scoring = score.Value;
                    decimal estimatedRate = Rmin + (rateRange * scoring);

                    result.Add(new CalculatedRankingDto
                    {
                        CountryId = country.Id,
                        CountryName = country.Name,
                        Scoring = (double)scoring,
                        StimatedRate = (double)estimatedRate
                    });
                }
            }

            return result.OrderByDescending(r => r.Scoring).ToList();
        }
    }
}

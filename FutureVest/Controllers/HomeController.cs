using Application.Interfaces;
using Application.Services;
using Application.ViewModels.Ranking;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace FutureVest.Controllers
{
    public class HomeController : Controller
    {
        IMacroIndicatorService _macroIndicatorService;
        ICountryIndicatorService _indicatorService;
        IRanking _rankingService;
        public HomeController(IRanking ranking,VestAppDbContext context)
        {
            _rankingService = ranking; 
           _indicatorService = new CountryIndicatorService(context);
           _macroIndicatorService = new MacroIndicatorServices(context);
        }

        public async Task<IActionResult> Index()
        {
            var indicators = await _indicatorService.GetAllAsync(); 

            var availableYears = indicators 
                .Select(i => i.Year)
                .Distinct()
                .OrderByDescending(y => y)
                .ToList();

            ViewBag.AvailableYears = availableYears;
            ViewBag.SelectedYear = availableYears.FirstOrDefault();
            return View();
            
            
        }
        [HttpPost]
        public async Task<IActionResult> Index(int year)
        {

            var rank = await _rankingService.CalculateRankingForYear(year);

            var indicators = await _indicatorService.GetAllAsync();

            var availableYears = indicators
                .Select(i => i.Year)
                .Distinct()
                .OrderByDescending(y => y)
                .ToList();

            ViewBag.AvailableYears = availableYears;
            ViewBag.SelectedYear = availableYears.FirstOrDefault();

            var macroIndicators = await _macroIndicatorService.GetAllAsync();

            var totalWeight = macroIndicators.Sum(m => m.Weight);

            if (totalWeight != 1)
            {
                ViewBag.ErrorMacro = "El peso de los macro indicadores no es igual a 1, agrega otro a la lista <a href='/MacroIndicator/Index'> aquí </a>";
                ViewBag.AvailableYears = availableYears;
                ViewBag.SelectedYear = availableYears.FirstOrDefault();
                return View(new List<CalculatedRankingVM>());
            }

            if (rank.Count < 2) 
            {
                ViewBag.ErrorMessage = "No hay suficientes piases que cumplan con los requisitos para la simulación en este año, <a href='/Countries/Index'>Agrega uno aquí</a>.";
                ViewBag.AvailableYears = availableYears;
                ViewBag.SelectedYear = availableYears.FirstOrDefault();
                return View(new List<CalculatedRankingVM>());
            }
         
            var ranking = rank.Select(R => new CalculatedRankingVM
            {
                CountryId = R.CountryId,
                CountryName = R.CountryName,
                Code = R.Code,
                Scoring = R.Scoring,
                StimatedRate = R.StimatedRate
            }).ToList();
            
            return View(ranking);

        }
    }
}

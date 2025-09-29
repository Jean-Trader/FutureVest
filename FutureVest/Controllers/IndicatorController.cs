using Application.DTOs.Country;
using Application.DTOs.CountryIndicator;
using Application.Interfaces;
using Application.Services;
using Application.ViewModels.CountryIndicators;
using Microsoft.AspNetCore.Mvc;
using Persistence.Context;

namespace FutureVest.Controllers
{
    public class IndicatorController : Controller
    {
        ICountryIndicatorService _indicatorService;
        ICountryServices _countryService;
        IMacroIndicatorService _macroIndicatorService;

        public IndicatorController(VestAppDbContext context)
        {
            _indicatorService = new CountryIndicatorService(context);
            _countryService = new CountryServices(context);
            _macroIndicatorService = new MacroIndicatorServices(context);
        }

        public async Task<IActionResult> Index()
        {
            var indicators = _indicatorService.GetAllQuery();

            var listIndicators = indicators.Select(i => new ViewCountryIndicatorVM
            {
                Id = i.Id,
                CountryId = i.CountryId,
                MacroIndicatorId = i.MacroIndicatorId,
                CountryName = i.Country.Name,
                MacroIndicatorName = i.MacroIndicator.Name,
                Year = i.Year,
                Value = i.Value

            }).ToList();

            return View(listIndicators);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Countries = await _countryService.GetAllAsync();
            ViewBag.MacroIndicators = await _macroIndicatorService.GetAllAsync();

            return View("Save", new CountryIndicatorVM
            {
                CountryId = 0,
                MacroIndicatorId = 0,
                Year = 0,
                Value = 0m
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CountryIndicatorVM vm)
        {
            var Co = await _countryService.GetAllAsync();
            var Ma = await _macroIndicatorService.GetAllAsync();

            var countries = Co.Select(C => new AddCountryModel { Id = C.Id, Name = C.Name }).ToList();
            var macroI = Ma.Select(m => new AddMacroIndicatorVM { Id = m.Id, Name = m.Name }).ToList();

            if (!ModelState.IsValid || vm == null )
            {
                ViewBag.ErrorMessage = "Error al crear indicador";

                return View("Save", vm);
            }
            ViewBag.Countries = countries;
            ViewBag.macroIndicators = macroI;

            var country = await _countryService.GetByIdAsync(vm.CountryId);
            var macroIndicator = await _macroIndicatorService.GetByIdAsync(vm.MacroIndicatorId);

            CountryIndicatorDto dto = new CountryIndicatorDto()
            {
                Id = 0,
                CountryId = vm.CountryId,
                MacroIndicatorId = vm.MacroIndicatorId,
                Year = vm.Year,
                Value = vm.Value,
                Country = country,
                MacroIndicator = macroIndicator
            };

            await _indicatorService.CreateAsync(dto);
            return RedirectToRoute(new { Controller = "Indicator", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _indicatorService.GetByIdAsync(id);

            ViewBag.EditMode = true;
            ViewBag.Countries = await _countryService.GetAllAsync();
            ViewBag.MacroIndicators = await _macroIndicatorService.GetAllAsync();

            if (dto == null)
            {
                ViewBag.ErrorMessage = "No se encontró el indicador";
                return RedirectToRoute(new { Controller = "Indicator", action = "Index" });
            }

            CountryIndicatorVM vm = new CountryIndicatorVM
            {
                Id = dto.Id,
                CountryId = dto.CountryId,
                MacroIndicatorId = dto.MacroIndicatorId,
                Year = dto.Year,
                Value = dto.Value
            };

            return View("Save", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CountryIndicatorVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EditMode = true;
                ViewBag.ErrorMessage = "Error al editar indicador";
                ViewBag.Countries = await _countryService.GetAllAsync();
                ViewBag.MacroIndicators = await _macroIndicatorService.GetAllAsync();
                return View("Save", vm);
            }

            CountryIndicatorDto dto = new CountryIndicatorDto
            {
                Id = vm.Id,
                CountryId = vm.CountryId,
                MacroIndicatorId = vm.MacroIndicatorId,
                Year = vm.Year,
                Value = vm.Value
            };

            await _indicatorService.UpdateAsync(dto, vm.Id);
            return RedirectToRoute(new { Controller = "Indicator", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _indicatorService.GetByIdAsync(id);

            if (dto == null)
            {
                ViewBag.ErrorMessage = "No se encontró el indicador";
                return RedirectToRoute(new { Controller = "Indicator", action = "Index" });
            }

            DeleteCountryIndicatorVM vm = new DeleteCountryIndicatorVM
            {
                Id = dto.Id,
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteCountryIndicatorVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _indicatorService.DeleteAsync(vm.Id);
            return RedirectToRoute(new { controller = "Indicator", action = "Index" });
        }
    }
}
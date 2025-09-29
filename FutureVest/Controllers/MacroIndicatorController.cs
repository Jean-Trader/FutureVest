using Application.DTOs.MacroIndicator;
using Application.Interfaces;
using Application.Services;
using Application.ViewModels.MacroIndicators;
using Microsoft.AspNetCore.Mvc;
using Persistence.Context;

namespace FutureVest.Controllers
{
    public class MacroIndicatorController : Controller
    {
        IMacroIndicatorService _macroIndicatorService;
        public MacroIndicatorController(VestAppDbContext context) 
        {
            _macroIndicatorService = new MacroIndicatorServices(context);
        }

        public async Task<IActionResult> Index()
        {
            var countries = await _macroIndicatorService.GetAllAsync();

            var ListCountries = countries.Select(M => new MacroIndicatorVM
            {
                Id = M.Id,
                Name = M.Name,
                Weight = M.Weight,
                HighBetter = M.HighBetter,
                
            }).ToList();

            return View(ListCountries);
        }

        public IActionResult Create()
        {
            return View("Save", new MacroIndicatorVM { Name = "", Weight = 0m, HighBetter = false });
        }

        [HttpPost]
        public async Task<IActionResult> Create(MacroIndicatorVM M)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Error al crear país";
                return View("Save", M);
            }

            MacroIndicatorDto macroIndicator = new MacroIndicatorDto()
            {
                Id = 0,
                Name = M.Name,
                Weight = M.Weight,
                HighBetter = M.HighBetter,
            };

            await _macroIndicatorService.CreateAsync(macroIndicator);
            return RedirectToRoute(new { Controller = "MacroIndicator", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var MDto = await _macroIndicatorService.GetByIdAsync(id);

            ViewBag.EditMode = true;

            if (MDto == null)
            {
                ViewBag.ErrorMessage = "No se encontró la entidad en la base de datos";
                return RedirectToRoute(new { Controller = "MacroIndicator", action = "Index" });
            }

            MacroIndicatorVM MVM = new MacroIndicatorVM
            {
                Id = MDto.Id,
                Name = MDto.Name,
                Weight = MDto.Weight,
                HighBetter = MDto.HighBetter,
            };

            return View("Save", MVM);


        }

        [HttpPost]
        public async Task<IActionResult> Edit(MacroIndicatorVM MVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EditMode = false;

                ViewBag.Error = "Error al editar el Macro Indicador";
                return View("Save", MVM);
            }

            MacroIndicatorDto C = new MacroIndicatorDto
            {

                Id = MVM.Id,
                Name = MVM.Name,
                Weight = MVM.Weight,
                HighBetter = MVM.HighBetter,

            };

            await _macroIndicatorService.UpdateAsync(C, MVM.Id);
            return RedirectToRoute(new { Controller = "MacroIndicators", action = "Index" });

        }

        public async Task<IActionResult> Delete(int id)
        {
            var C = await _macroIndicatorService.GetByIdAsync(id);

            if (C == null)
            {
                ViewBag.ErrorMessage = "No se encontró el país";
                return RedirectToRoute(new { Controller = "MacroIndicator", action = "Index" });
            }
            DeleteMacroIndicatorVM Del = new DeleteMacroIndicatorVM
            {
                Id = C.Id,
                Name = C.Name
            };

            return View(Del);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteMacroIndicatorVM del)
        {
            if (!ModelState.IsValid)
            {
                return View(del);
            }

            await _macroIndicatorService.DeleteAsync(del.Id);

            return RedirectToRoute(new { controller = "MacroIndicator", action = "Index" });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.Interfaces;
using Persistence.Context;
using Application.ViewModels.Country;
using Application.DTOs.Country;
namespace FutureVest.Controllers
{
    public class CountriesController : Controller
    {
        ICountryServices _countriesService;
        public CountriesController(VestAppDbContext context)
        {
            _countriesService = new CountryServices(context);
        }

        public async Task<IActionResult> Index()
        {
            var countries = await _countriesService.GetAllAsync();

            var ListCountries = countries.Select(c => new CountryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Code = c.Code,
            }).ToList();

            return View(ListCountries);
        }

        public IActionResult Create() 
        { 
         
            return View("Save", new CountryViewModel { Name = "", Code = ""} );
        
        }

        [HttpPost]
        public async Task<IActionResult> Create(CountryViewModel c) 
        { 
         
           if (!ModelState.IsValid) 
           {
                ViewBag.ErrorMessage = "Error al crear país";
                return View("Save",c);
           }

            CountryDto country = new CountryDto() 
            { 
             Id = 0,
             Code = c.Code,
             Name = c.Name,
            };

            await _countriesService.CreateAsync(country);
            return RedirectToRoute(new {Controller = "Countries", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id) 
        {
            var CDto = await _countriesService.GetByIdAsync(id);

            ViewBag.EditMode = true;

            if (CDto == null) 
            {
                ViewBag.ErrorMessage = "No se encontró la entidad en la base de datos";
                return RedirectToRoute(new { Controller = "Countries", action = "Index" });
            }

            CountryViewModel CVM = new CountryViewModel
            {
                Id = CDto.Id,
                Name = CDto.Name,
                Code = CDto.Code
            };

            return View("Save",CVM);

            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CountryViewModel CVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EditMode = false;

                ViewBag.Error = "Error al editar país";
                return View("Save",CVM);
            }

            CountryDto C = new CountryDto 
            { 
                Id = CVM.Id,
                Code = CVM.Code,
                Name = CVM.Name,
               
            };

            await _countriesService.UpdateAsync(C, CVM.Id);
            return RedirectToRoute(new { Controller = "Countries", action = "Index" });

        }

        public async Task<IActionResult>Delete(int id)
        {
            var C = await _countriesService.GetByIdAsync(id);

            if (C == null)
            {
                ViewBag.ErrorMessage = "No se encontró el país";
                return RedirectToRoute(new { Controller = "Countries", action = "Index" });
            }
            DeleteCountryVM Del = new DeleteCountryVM
            { 
             Id = C.Id,
             Name = C.Name
            };

            return View(Del);
        }
        [HttpPost]
        public IActionResult Delete(DeleteCountryVM del) 
        { 
            if (!ModelState.IsValid)
            {
                return View(del);
            }

            _countriesService.DeleteAsync(del.Id);

            ViewBag.Sucess = "País eliminado correctamente";

            return RedirectToRoute(new { controller = "Countries", action = "Index" });
        }
    }
    
}

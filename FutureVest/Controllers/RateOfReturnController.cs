using Application.DTOs.RateReturn;
using Application.Interfaces;
using Application.Services;
using Application.ViewModels.RateOfReturns;
using Microsoft.AspNetCore.Mvc;
using Persistence.Context;

namespace FutureVest.Controllers
{
    public class RateOfReturnController : Controller
    {
        IRateReturnService _rateReturnService;

        public RateOfReturnController(VestAppDbContext context)
        {
            _rateReturnService = new RateReturnService(context);
        }
        public async Task<IActionResult> Index()
        {

            var Rate = await _rateReturnService.GetRateReturn();

            if (Rate == null)
            {
                return View(new RateReturnVM() 
                {
                    MinRate = 0, 
                    MaxRate = 0
                });
            }

            RateReturnVM rateReturn = new()
            {
                Id = Rate.Id,
                MinRate = Rate.MinRate,
                MaxRate = Rate.MaxRate
            };

            return View(rateReturn);
        }

        [HttpPost]
        public async Task<IActionResult> Index(RateReturnVM RVM) 
        {

            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Complete todos los campos requeridos";
                return View(RVM);
            }
            RateReturnDto dto = new() 
          { 
              Id = RVM.Id,
              MinRate = RVM.MinRate,
              MaxRate = RVM.MaxRate 
          };

            await _rateReturnService.CreateOrUpdateAsync(dto);

            return View(RVM);
        }
    }
}

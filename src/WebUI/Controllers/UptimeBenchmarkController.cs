using Application.Common.Interfaces;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize]
    public class UptimeBenchmarkController : Controller
    {
        private IUptimeBenchmarkRepository _uptimeBenchmarkRepository;

        public UptimeBenchmarkController(IUptimeBenchmarkRepository uptimeBenchmarkRepository)
        {
            _uptimeBenchmarkRepository = uptimeBenchmarkRepository;
        }
        public async Task<IActionResult> Index()
        {
            TestLocation enumTestLocation = new();
            ViewBag.TestEnviroment = EnumToListService.EnumToList(enumTestLocation);
            return View();
        }

        //Parametr defaultowy to 1 czyli perwszy kraj wygenerowany na widoku
        public async Task<IActionResult> UptimeBenchmarkTablePartial([FromQuery]TestLocation location= (TestLocation)1)
        {
            //Jako parametry podajemy lokalizację oraz licencję aby pobrać tylko strony z danej licencji
            var model = await _uptimeBenchmarkRepository
                .GetByLocation(location, UserClaimService.GetLicenseId(User));

            //Jeśli w danej lokalizacji są elementy wyświetlamy je, jeśli nie ma wyświetlamy pusty widok
            if(model.Count>0)
                return PartialView("PartialViews/Dashboard/UptimeBenchmarkTablePartial", model);
            else
                return PartialView("PartialViews/Dashboard/EmptyView");
        }
    }
}

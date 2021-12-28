using Application.Common.Interfaces;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize]
    public class ResponseBenchmarkController : Controller
    {
        private IResponseRepository _responseRepository;

        public ResponseBenchmarkController(IResponseRepository responseRepository)
        {
            _responseRepository = responseRepository;
        }
        public async Task<IActionResult> Index()
        {
            TestLocation enumTestLocation = new();
            ViewBag.TestEnviroment = EnumToListService.EnumToList(enumTestLocation);
            return View();
        }

        //Parametr defaultowy to 1 czyli perwszy kraj wygenerowany na widoku
        public async Task<IActionResult> ResponseTablePartial([FromQuery]TestLocation location= (TestLocation)1)
        {
            //Jako parametry podajemy lokalizację oraz licencję aby pobrać tylko strony z danej licencji
            var model = await _responseRepository
                .GetByLocation(location, UserClaimService.GetLicenseId(User));

            //Jeśli w danej lokalizacji są elementy wyświetlamy je, jeśli nie ma wyświetlamy pusty widok
            if(model.Count>0)
                return PartialView("PartialViews/Dashboard/ResponseBenchmarkTablePartial", model);
            else
                return PartialView("PartialViews/Dashboard/EmptyView");
        }
    }
}

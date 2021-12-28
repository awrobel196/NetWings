using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Infrastructure.Persistance;
using WebUI.Models;
using Application.Common.Interfaces;
using Application.Services;

namespace WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IWebsiteRepository _websiteRepository;

        public HomeController(IWebsiteRepository websiteRepository)
        {
            _websiteRepository = websiteRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
           
        }

        public async Task<IActionResult> BreakDownWebsitePartial()
        {
            //Jako parametry podajemy lokalizację oraz licencję aby pobrać tylko strony z danej licencji
            var model = await _websiteRepository.LatestBreakdownWebsite(UserClaimService.GetLicenseId(User));

            //Jeśli w danej lokalizacji są elementy wyświetlamy je, jeśli nie ma wyświetlamy pusty widok
            if (model.Count > 0)
                return PartialView("PartialViews/BreakdownTablePartial", model);
            else
                return PartialView("PartialViews/EmptyView");
        }

        public async Task<IActionResult> WebsiteForImprovementPartial()
        {
            //Jako parametry podajemy lokalizację oraz licencję aby pobrać tylko strony z danej licencji
            var model = await _websiteRepository.WebsiteForImprovement(UserClaimService.GetLicenseId(User));

            //Jeśli w danej lokalizacji są elementy wyświetlamy je, jeśli nie ma wyświetlamy pusty widok
            if (model.Count > 0)
                return PartialView("PartialViews/WebsiteForImprovementPartial", model);
            else
                return PartialView("PartialViews/EmptyImprovementView");
        }
    }
}

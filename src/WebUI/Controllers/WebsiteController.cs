using Application.Common.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize]
    public class WebsiteController : Controller
    {
        private readonly IWebsiteRepository _websiteRepository;
        private readonly ILicenseRepository _licenseRepository;

        public WebsiteController(IWebsiteRepository websiteRepository, ILicenseRepository licenseRepository)
        {
            _websiteRepository = websiteRepository;
            _licenseRepository = licenseRepository;
        }

      
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> WebsitesTablePartial()
        {
            var model = await _websiteRepository.WebsitesList(UserClaimService.GetLicenseId(User));

            if(model.Count==0)
                return PartialView("ParialViews/Dashboard/EmptyView");

            return PartialView("ParialViews/Dashboard/WebsitesTable", model);
        }

        public async Task<bool> DeleteWebsite(string idWebsite)
        {
            bool status = await _websiteRepository.Delete(new Guid(idWebsite));
            return status;
        }


        [HttpGet]
        public IActionResult Create()
        {
            TestLocation enumTestLocation = new();
            TestEnviroment enumTestEnviroment = new();

            ViewBag.TestLocation = EnumToListService.EnumToList(enumTestLocation);
            ViewBag.TestEnviroment = EnumToListService.EnumToList(enumTestEnviroment);

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Create(string name, string url, int testLoaction, int testEnviroment)
        {
           
            if (await _licenseRepository.AreFreeWebsiteResource(UserClaimService.GetLicenseId(User)))
            {
                //Jeśli użytkownik posiada wolne  licencje tworzymy nową stronę
                await _websiteRepository.Create(new Website()
                {
                    Name = name,
                    Url = url,
                    TestLocation = (TestLocation)testLoaction,
                    TestEnviroment = (TestEnviroment)testEnviroment,
                    IdLicense = UserClaimService.GetLicenseId(User)
                });
                return new JsonResult(true);
            }
            else
            {
                return new JsonResult(false);
            }
        }

        public async Task<IActionResult> Details(string idWebsite)
        {
            var model = await _websiteRepository.WebsiteDetailsBreadcrumbs(new Guid(idWebsite));
            return View("Details", model);
        }


        public async Task<IActionResult> WebsiteMetricsPartial([FromQuery]string idWebsite)
        {

            var model = await _websiteRepository.WebsiteMetrics(new Guid(idWebsite));

            if (model is not null)
                return PartialView("ParialViews/Details/WebsiteMetricsPartial", model);
            else
                return PartialView("ParialViews/Details/EmptyView");
        }

        public async Task<IActionResult> WebsiteUptimePartial([FromQuery] string idWebsite)
        {
            var model = await _websiteRepository.WebsiteUptime(new Guid(idWebsite));
            return PartialView("ParialViews/Details/WebsiteUptimePartial",model);
        }

        public async Task<IActionResult> WebsiteResponsePartial([FromQuery] string idWebsite)
        {
            var model = await _websiteRepository.WebsiteResponse(new Guid(idWebsite));
            return PartialView("ParialViews/Details/WebsiteResponsePartial", model);
        }

        public async Task<IActionResult> WebsiteTimingsPartial([FromQuery] string idWebsite)
        {

            var model = await _websiteRepository.WebsiteTimings(new Guid(idWebsite));

            if (model is not null)
                return PartialView("ParialViews/Details/WebsiteTimingsPartial", model);
            else
                return PartialView("ParialViews/Details/EmptyView");
        }

        public async Task<IActionResult> WebsiteGradePartial([FromQuery] string idWebsite)
        {

            var model = await _websiteRepository.WebsiteGrade(new Guid(idWebsite));

            if (model.Count>=1)
                return PartialView("ParialViews/Details/WebsiteGradePartial", model);
            else
                return PartialView("ParialViews/Details/EmptyView");
        }

        public async Task<IActionResult> WebsiteVulnerablePartial([FromQuery] string idWebsite)
        {

                return PartialView("ParialViews/Details/WebsiteVulnerablePartial");
          
        }


    }
}

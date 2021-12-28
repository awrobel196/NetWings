using Application.Common.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class LicenseController : Controller
    {
        private ILicenseRepository _licenseRepository;
        public LicenseController(ILicenseRepository licenseRepository)
        {
            _licenseRepository = licenseRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LicenseDetailsPartial()
        {
            var model = await _licenseRepository.Details(UserClaimService.GetLicenseId(User));
            return PartialView("ParialViews/LicenseDetails", model);
        }

        public async Task<bool> DeleteUserFromLicense(string idUser)
        {
            bool status = await _licenseRepository.DeleteUser(idUser, UserClaimService.GetUserId(User), 
                UserClaimService.GetLicenseId(User));

           
            return status;
        }
    }
}

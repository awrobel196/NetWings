using Application.Common.Interfaces;
using Application.ViewModels.License;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Behaviours
{
    public class LicenseRepository : ILicenseRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoginRepository _loginRepository;
        public LicenseRepository(ApplicationDbContext context, ILoginRepository loginRepository)
        {
            _context = context;
            _loginRepository = loginRepository;
        }

        public async Task<bool> AreFreeWebsiteResource(Guid IdLicense)
        {
            //Poieramy ile maksymalnie stron może być w danej licencji
            var availableWebsite = _context.License.Where(x => x.IdLicense == IdLicense)
                .Select(x => x.LicenseType.NumberOfWebsites).FirstOrDefault();

            //Pobieramy ile jest aktualnie stron
            var usedWebsites = _context.Website.Count(x => x.IdLicense == IdLicense);

            if (availableWebsite > usedWebsites)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<bool> AreFreeUserResource(Guid IdLicense)
        {
            throw new NotImplementedException();
        }

        public async Task<License> CreateLicense()
        {
            License license = new License();

            license.IdLicenseType = _context.LicenseType.Where(x => x.LicenseName == "FREE")
                .Select(x => x.IdLicenseType).FirstOrDefault();
            license.LicenseExpired = DateTime.Now.AddDays(30);

             _context.Add(license);
            await _context.SaveChangesAsync();

            return license;
        }

        public async Task<bool> DeleteUser(string idUser, Guid currentLoggedUser, Guid idLicense)
        {
            User user = _context.User.FirstOrDefault(x => x.IdUser == new Guid(idUser));
            var numberUsersInLicense = _context.User.Where(x => x.IdLicense == idLicense).Count();


            if (user == null || numberUsersInLicense<2)
            {
                return false;
            }

            _context.Remove(user);
            await _context.SaveChangesAsync();

            if (user.IdUser == currentLoggedUser)
            {
                await _loginRepository.Logout();
            }
            return true;
        }

        public async Task<LicenseDashboardViewModel> Details(Guid IdLicense)
        {
            LicenseDashboardViewModel result = new();
            result = await _context.License.Where(x => x.IdLicense == IdLicense)
                .Include(x => x.LicenseType)
                .Include(x => x.User)
                .Include(x => x.Website).Select(x => new LicenseDashboardViewModel
                {
                    ShortIdLicense = x.IdLicense.ToString().ToLower().Substring(0,18),
                    LicenseExpired = x.LicenseExpired.ToString("D"),
                    CurrentUsers = x.User.Count,
                    MaxUsers = x.LicenseType.NumberOfUsers,
                    CurrentWebsites = x.Website.Count,
                    MaxWebsites = x.LicenseType.NumberOfWebsites,
                    Users = x.User,
                    DaysLeft = (x.LicenseExpired - DateTime.Now).Days
                }).FirstOrDefaultAsync();

            return result;
        }


    }
}

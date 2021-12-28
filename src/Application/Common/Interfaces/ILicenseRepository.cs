using Application.ViewModels.License;
using Domain.Entities;
namespace Application.Common.Interfaces
{
    public interface ILicenseRepository
    {
        Task<License> CreateLicense();
        Task<LicenseDashboardViewModel> Details(Guid IdLicense);
        Task<bool> DeleteUser(string IdUser, Guid currentLoggedUser, Guid idLicense);

        Task<bool> AreFreeWebsiteResource(Guid IdLicense);
        Task<bool> AreFreeUserResource(Guid IdLicense);
    }
}

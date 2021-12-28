using Application.ViewModels.Home;
using Application.ViewModels.Website;
using Application.ViewModels.Website.Details;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IWebsiteRepository
    {
        Task<Website> Details(Guid idWebsite);
        Task<List<WebsitesListViewModel>> WebsitesList(Guid idLicense);
        Task Create(Website website);
        Task<bool> Delete(Guid idWebsite);
        Task<BreadcumbViewModel> WebsiteDetailsBreadcrumbs(Guid idWebsite);
        Task<WebsiteMetricsViewModel> WebsiteMetrics(Guid idWebsite);
        Task<UptimeViewModel> WebsiteUptime(Guid idWebsite);

        Task<ResponseViewModel> WebsiteResponse(Guid idWebsite);
        Task<WebsiteTimingsViewModel> WebsiteTimings(Guid idWebsite);
        Task<List<WebsiteGradeViewModel>> WebsiteGrade(Guid idWebsite);
        Task<List<LatestBreakdownWebsiteViewModel>> LatestBreakdownWebsite(Guid idLicense);
        Task<List<WebsiteForImprovement>> WebsiteForImprovement(Guid idLicense);
    }
}

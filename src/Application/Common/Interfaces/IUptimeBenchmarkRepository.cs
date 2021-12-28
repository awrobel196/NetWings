using Application.ViewModels.UptimeBenchmark;
using Domain.Entities;
using Domain.Enums;

namespace Application.Common.Interfaces
{
    public interface IUptimeBenchmarkRepository
    {
        Task<List<UptimeBenchmarkDashboardViewModel>> GetByLocation(TestLocation location, Guid license);
        Task<Website> Details(Guid idWebsite);
    }
}

using Application.ViewModels.Benchmark;
using Domain.Entities;
using Domain.Enums;

namespace Application.Common.Interfaces
{
    public interface IResponseRepository
    {
        Task<List<BenchmarkDashboardViewModel>> GetByLocation(TestLocation location, Guid license);
        Task<Website> Details(Guid idWebsite);
    }
}

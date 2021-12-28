using Application.Common.Interfaces;
using Application.ViewModels.Benchmark;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Behaviours
{
    public class ResponseRepository : IResponseRepository
    {
        private readonly ApplicationDbContext _context;

        public ResponseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Website> Details(Guid idWebsite)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BenchmarkDashboardViewModel>> GetByLocation(TestLocation location, Guid license)
        {
            List<BenchmarkDashboardViewModel> result = new();
            
            var websiteList = await _context.Website
                .Where(x=>x.IdLicense==license)
                .Where(x=>x.TestLocation==location)
                .Include(x => x.WebsiteResponseScore).ToListAsync();
            

            foreach (var item in websiteList)
            {

                BenchmarkDashboardViewModel b = new();
                b.IdWebsite = item.IdWebsite;
                b.WebsiteName = item.Name;
                b.WebsiteUrl = item.Url;
                b.Size = ((double)(item.WebsiteResponseScore.Select(x => x.page_bytes)
                    .LastOrDefault()) / 1000000).ToString("#0.0#;(#0.0#)");
                b.FirstInteraction = item.WebsiteResponseScore.Select(x => x.time_to_interactive)
                    .LastOrDefault();
                b.TTFB = item.WebsiteResponseScore.Select(x => x.time_to_first_byte)
                    .LastOrDefault();
                b.Grade = item.WebsiteResponseScore.Select(x => x.gtmetrix_grade)
                    .LastOrDefault();
                b.FullLoad = item.WebsiteResponseScore.Select(x => x.fully_loaded_time)
                    .LastOrDefault();

                result.Add(b);
            }

            return result;
        }
    }
}

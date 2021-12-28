using Application.Common.Interfaces;
using Application.ViewModels.UptimeBenchmark;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Infrastructure.Persistance;
using MongoDB.Driver;

namespace Application.Common.Behaviours
{
    public class UptimeBenchmarkRepository : IUptimeBenchmarkRepository
    {
        private readonly ApplicationDbContext _context;
        public UptimeBenchmarkRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Website> Details(Guid idWebsite)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UptimeBenchmarkDashboardViewModel>> GetByLocation(TestLocation location, Guid license)
        {
            Stopwatch s = new Stopwatch();
            s.Start();

            List<UptimeBenchmarkDashboardViewModel> result = new();
           
            var websiteList = _context.Website
                .Where(x=>x.IdLicense==license)
                .Where(x=>x.TestLocation==location)
                .Include(x => x.WebsiteUptimeScore.OrderByDescending(y=>y.TestTime).Take(240)).ToList();

            var i = 0;

            foreach (var item in websiteList)
            {
                UptimeBenchmarkDashboardViewModel u = new();
                u.IdForChar = i;
                u.IdWebsite = item.IdWebsite;
                u.WebsiteName = item.Name;
                u.WebsiteUrl = item.Url;
                u.LatestTestTime = (DateTime.Now - item.WebsiteUptimeScore.Select(x => x.TestTime).LastOrDefault())
                    .ToString("ss");

                u.Status = item.WebsiteUptimeScore.Select(x => x.StatusNumber).LastOrDefault() switch
                {
                    "200" => "U",
                    "404" => "D",
                    _ => "O"
                };
                u.Uptime = ((item.WebsiteUptimeScore.Count(x => x.StatusNumber == "200")
                            * 100) / (double)item.WebsiteUptimeScore.Count).ToString("F2");
                u.UptimeChar = new();

                foreach (var score in item.WebsiteUptimeScore.TakeLast(240))
                {
                    UptimeChar uptimeChar = new UptimeChar();
                    uptimeChar.UptimeStatusChar = Convert.ToInt32(score.StatusNumber);
                    uptimeChar.UptimeTestTimeChar = score.TestTime.ToString("dd.MM.yyyy HH:mm");

                    u.UptimeChar.Add(uptimeChar);
                }

                while (u.UptimeChar.Count < 240)
                {
                    UptimeChar uptimeChar = new UptimeChar();
                    uptimeChar.UptimeStatusChar = 999;
                    uptimeChar.UptimeTestTimeChar = "Brak wyniku";
                    u.UptimeChar.Add(uptimeChar);
                }

               
                result.Add(u);
                i++;
            }
            return result;
        }
    }
}

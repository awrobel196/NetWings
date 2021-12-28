using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Application.Common.Interfaces;
using Application.ViewModels.Home;
using Application.ViewModels.Website;
using Application.ViewModels.Website.Details;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistance;
using Application.Services;
using Application.ViewModels.UptimeBenchmark;
using MongoDB.Driver;

namespace Application.Common.Behaviours
{
    public class WebsiteRepository : IWebsiteRepository
    {
        private readonly ApplicationDbContext _context;

        public WebsiteRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(Website website)
        {
            _context.Add(website);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid idWebsite)
        {
            var result = await _context.Website.Where(x => x.IdWebsite == idWebsite).FirstOrDefaultAsync();
            _context.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<Website> Details(Guid idWebsite)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LatestBreakdownWebsiteViewModel>> LatestBreakdownWebsite(Guid idLicense)
        {

            var result = await _context.WebsiteUptimeScore.Where(x => x.Website.IdLicense == idLicense).Where(x => x.StatusNumber != "200").OrderByDescending(x => x.TestTime).Take(3).Select(
                x => new LatestBreakdownWebsiteViewModel()
                {
                    IdWebsite = x.IdWebsite.ToString(),
                    WebsiteName = x.Website.Name,
                    WebsiteUrl = x.Website.Url,
                    ErrorCode = x.StatusNumber,
                    EventDate = x.TestTime.ToString("dd.MM.yyyy"),
                    EventHour = x.TestTime.ToString("HH:mm"),
                    RequestTime = x.ElapsedTime.ToString()

                }).ToListAsync();

            return result;
        }

        public async Task<BreadcumbViewModel> WebsiteDetailsBreadcrumbs(Guid idWebsite)
        {
            var website = await _context.Website.Where(x => x.IdWebsite == idWebsite)
                .Select(x => new BreadcumbViewModel()
                {
                    IdWebsite = x.IdWebsite.ToString(),
                    Url = x.Url,
                    Name = x.Name,
                    Enviroment = ((TestEnviroment)x.TestEnviroment).ToString(),
                    Location = ((TestLocation)x.TestLocation).ToString()
                }).FirstOrDefaultAsync();

            return website;
        }

        public async Task<List<WebsiteForImprovement>> WebsiteForImprovement(Guid idLicense)
        {


            var result = _context.WebsiteResponseScore.Include(x => x.Website).Where(x => x.Website.IdLicense == idLicense).Where(x => x.gtmetrix_grade == "F" || x.gtmetrix_grade == "E").Where(x => x.TestTime > DateTime.Today.AddDays(-2)).AsEnumerable().GroupBy(x => x.IdWebsite).Take(3).Select(x => new WebsiteForImprovement()
            {
                WebsiteName = x.Select(x => x.Website.Name).Last(),
                FirstInteraction = x.Select(x => x.time_to_interactive).Last(),
                FullLoad = x.Select(x => x.fully_loaded_time).Last(),
                Grade = x.Select(x => x.gtmetrix_grade).Last(),
                IdWebsite = x.Select(x => x.IdWebsite).Last(),
                TTFB = x.Select(x => x.time_to_first_byte).Last(),
                Size = ((double)(x.Select(x => x.page_bytes).Last()) / 1000000).ToString("#0.0#;(#0.0#)"),
                WebsiteUrl = x.Select(x => x.Website.Url).Last(),
                TestTime = x.Select(x => x.TestTime.ToString("HH:mm dd.MM.yyyy")).Last(),
            }).ToList();

            //var result = await _context.WebsiteResponseScore.Where(x => x.Website.IdLicense == idLicense)
            //    .Where(x => x.gtmetrix_grade == "F" || x.gtmetrix_grade == "E").GroupBy(x=>x.Website).OrderBy(x=>x.Key).Select(x =>
            //        new WebsiteForImprovement()
            //        {
            //          WebsiteUrl = x.Website.Name,
            //          WebsiteName = x.Website.Name,
            //          IdWebsite = x.IdWebsite,
            //          Grade = x.gtmetrix_grade,
            //          FirstInteraction = x.time_to_interactive,
            //          FullLoad = x.fully_loaded_time,
            //          TTFB = x.time_to_first_byte,
            //          Size = ((double)(x.page_bytes) / 1000000).ToString("#0.0#;(#0.0#)"),
            //        }).ToListAsync();

            return result;
        }

        public Task<List<WebsiteGradeViewModel>> WebsiteGrade(Guid idWebsite)
        {
            var result = _context.WebsiteResponseScore.Where(x => x.IdWebsite == idWebsite).OrderByDescending(x => x.TestTime)
                .Take(5).Select(x => new WebsiteGradeViewModel()
                {
                    TTFB = x.time_to_first_byte,
                    FullLoad = x.fully_loaded_time,
                    Size = ((double)(x.page_bytes) / 1000000).ToString("#0.0#;(#0.0#)"),
                    FirstInteraction = x.time_to_interactive,
                    Grade = x.gtmetrix_grade,
                    TestDate = x.TestTime.ToString("HH:mm dd.MM.yyyy")
                }).ToListAsync();

            return result;
        }

        public async Task<WebsiteMetricsViewModel> WebsiteMetrics(Guid idWebsite)
        {
            Stopwatch s = new();
            s.Start();
            var metrics = await _context.WebsiteResponseScore.Where(x => x.IdWebsite == idWebsite)
                .OrderBy(x => x.TestTime).Select(x => new WebsiteMetricsViewModel()
                {
                    FirstContentfulPaint = x.first_contentful_paint.ToString(),
                    FirstContentfulPaintGrade = MetricsGrade.FirstContentfulPaintGrade(x.first_contentful_paint),
                    FirstContentfulPaintDescription =
                        MetricsGrade.FirstContentfulPaintDescription(x.first_contentful_paint),

                    SpeedIndex = x.speed_index.ToString(),
                    SpeedIndexGrade = MetricsGrade.SpeedIndexGrade(x.speed_index),
                    SpeedIndexDescription = MetricsGrade.SpeedIndexDescription(x.speed_index),

                    LargestContentfulPaint = x.largest_contentful_paint.ToString(),
                    LargestContentfulPaintGrade = MetricsGrade.LargestContentfulPaintGrade(x.largest_contentful_paint),
                    LargestContentfulPaintDescription =
                        MetricsGrade.LargestContentfulPaintDescription(x.largest_contentful_paint),

                    TimeToInteractive = x.time_to_interactive.ToString(),
                    TimeToInteractiveGrade = MetricsGrade.TimeToInteractiveGrade(x.time_to_interactive),
                    TimeToInteractiveDescription = MetricsGrade.TimeToInteractiveDescription(x.time_to_interactive),

                    TotalBlockingTime = x.total_blocking_time.ToString(),
                    TotalBlockingTimeGrade = MetricsGrade.TotalBlockingTimeGrade(x.total_blocking_time),
                    TotalBlockingTimeDescription = MetricsGrade.TotalBlockingTimeDescription(x.total_blocking_time),

                    FullyLoadedTime = x.fully_loaded_time.ToString(),
                    FullyLoadedTimeGrade = MetricsGrade.FullyLoadedTimeGrade(x.fully_loaded_time),
                    FullyLoadedTimeDescription = MetricsGrade.FullyLoadedTimeDescription(x.fully_loaded_time),
                    LastTestDate = x.TestTime.ToString("dd.MM.yyyy HH:mm")

                }).LastOrDefaultAsync();
            s.Stop();
            return metrics;
        }

        public async Task<ResponseViewModel> WebsiteResponse(Guid idWebsite)
        {
            var result = await _context.Website.Where(x => x.IdWebsite == idWebsite).Select(x => new ResponseViewModel()
            {
                AverageResponse =
                    (x.WebsiteUptimeScore.Select(x => x.ElapsedTime).Sum() /
                     x.WebsiteUptimeScore.Select(x => x.ElapsedTime).Count()).ToString(),
                ResponseList = x.WebsiteUptimeScore.OrderByDescending(x => x.TestTime).Take(60).ToList()
            }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<WebsitesListViewModel>> WebsitesList(Guid idLicense)
        {
            var result = await _context.Website.Where(x => x.IdLicense == idLicense)
                .Select(x => new WebsitesListViewModel()
                {
                    IdWebsite = x.IdWebsite,
                    Enviroment = ((TestEnviroment)x.TestEnviroment).ToString(),
                    Location = ((TestLocation)x.TestLocation).ToString(),
                    LastTestedByBenchmark = x.LastTestedByBenchmark.ToString("HH:mm:ss dd.MM.yyyy"),
                    Name = x.Name,
                    Url = x.Url,
                }).ToListAsync();
            return result;
        }

        public async Task<WebsiteTimingsViewModel> WebsiteTimings(Guid idWebsite)
        {
            var result = await _context.WebsiteResponseScore.Where(x => x.IdWebsite == idWebsite)
                .OrderBy(x => x.TestTime).Select(x => new WebsiteTimingsViewModel()
                {
                    TTFB = x.time_to_first_byte.ToString(),
                    FullyLoadedTime = x.fully_loaded_time.ToString(),
                    BackendDuration = x.backend_duration.ToString(),
                    ConnectionDuration = x.connect_duration.ToString(),
                    DOMContentLoadTime = x.dom_content_loaded_time.ToString(),
                    DOMInteractiveTime = x.dom_interactive_time.ToString(),
                    FirstPain = x.first_paint_time.ToString(),
                    OnloadTime = x.onload_time.ToString(),
                    RedirectDuration = x.redirect_duration.ToString(),

                }).LastOrDefaultAsync();

            return result;
        }

        public async Task<UptimeViewModel> WebsiteUptime(Guid idWebsite)
        {


            var result = await _context.Website.Where(x => x.IdWebsite == idWebsite).Select(x => new UptimeViewModel()
            {
                PercentageUptime = ((x.WebsiteUptimeScore.Count(x => x.StatusNumber == "200")
                                     * 100) / (double)x.WebsiteUptimeScore.Count).ToString("F2"),
                
                UptimeList = x.WebsiteUptimeScore.OrderByDescending(x => x.TestTime)
                    .Take(421).Select(x => new UptimeChar()
                    {
                        UptimeTestTimeChar = x.TestTime.ToString("dd.MM.yyyy HH:mm"),
                        UptimeStatusChar = Convert.ToInt32(x.StatusNumber)
                    }).ToList(),
                
                DownTimeEvents = x.WebsiteUptimeScore.Where(x => x.StatusNumber != "200").OrderByDescending(x => x.TestTime).Take(5).ToList(),
            }).FirstOrDefaultAsync();


            while (result.UptimeList.Count < 421)
            {
                result.UptimeList.Add(new UptimeChar(){UptimeStatusChar = 999});
            }

            return result;
        }
    }
}

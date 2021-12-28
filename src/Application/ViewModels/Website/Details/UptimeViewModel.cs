using System.Collections.Generic;
using Application.ViewModels.UptimeBenchmark;
using Domain.Entities;


namespace Application.ViewModels.Website.Details
{
    public class UptimeViewModel
    {
        public string PercentageUptime { get; set; }
        public List<UptimeChar> UptimeList { get; set; }
        public List<WebsiteUptimeScore> DownTimeEvents { get; set; }
    }
}

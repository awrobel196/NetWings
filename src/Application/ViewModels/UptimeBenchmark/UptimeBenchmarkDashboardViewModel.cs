using System;
using System.Collections.Generic;

namespace Application.ViewModels.UptimeBenchmark
{
    public class UptimeBenchmarkDashboardViewModel
    {
        public Guid IdWebsite { get; set; }
        public int IdForChar { get; set; }
        public string WebsiteName { get; set; }
        public string WebsiteUrl { get; set; }
        public string Status { get; set; }
        public string Uptime { get; set; }
        public string LatestTestTime { get; set; }
        public List<UptimeChar> UptimeChar { get; set; }
    }

    public class UptimeChar
    {
        public int UptimeStatusChar { get; set; }
        public string UptimeTestTimeChar { get; set; }
    }
}

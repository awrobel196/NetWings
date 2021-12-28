using System;
using System.Collections.Generic;

namespace Application.ViewModels.Benchmark
{
    public class BenchmarkDashboardViewModel
    {
        public Guid IdWebsite { get; set; }
        public string WebsiteName { get; set; }
        public string WebsiteUrl { get; set; }
        public int TTFB { get; set; }
        public int FullLoad { get; set; }
        public int FirstInteraction { get; set; }
        public string Size { get; set; }
        public string Grade { get; set; }

    }
}



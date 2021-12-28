using System;

namespace Application.ViewModels.Website
{
    public class WebsitesListViewModel
    {
        public Guid IdWebsite { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Location { get; set; }
        public string Enviroment { get; set; }
        public string LastTestedByBenchmark { get; set; }
    }
}

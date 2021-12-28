namespace Application.ViewModels.Website.Details
{
    public class WebsiteTimingsViewModel
    {
        public string RedirectDuration { get; set; }
        public string ConnectionDuration { get; set; }
        public string BackendDuration { get; set; }
        public string TTFB { get; set; }
        public string FirstPain { get; set; }
        public string DOMInteractiveTime { get; set; }
        public string DOMContentLoadTime { get; set; }
        public string OnloadTime { get; set; }
        public string FullyLoadedTime { get; set; }
    }
}

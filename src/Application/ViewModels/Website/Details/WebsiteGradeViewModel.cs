using System;

namespace Application.ViewModels.Website.Details
{
    public class WebsiteGradeViewModel
    {
        public string TestDate { get; set; }
        public int TTFB { get; set; }
        public int FullLoad { get; set; }
        public int FirstInteraction { get; set; }
        public string Size { get; set; }
        public string Grade { get; set; }
    }
}

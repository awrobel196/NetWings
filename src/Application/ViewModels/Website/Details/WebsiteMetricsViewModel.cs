namespace Application.ViewModels.Website.Details
{
    public class WebsiteMetricsViewModel
    {
        public string FirstContentfulPaint { get; set; }
        public string FirstContentfulPaintGrade { get; set; }
        public string FirstContentfulPaintDescription { get; set; }
        public string TimeToInteractive { get; set; }
        public string TimeToInteractiveGrade { get; set; }
        public string TimeToInteractiveDescription { get; set; }
        public string SpeedIndex { get; set; }
        public string SpeedIndexGrade { get; set; }
        public string SpeedIndexDescription { get; set; }
        public string TotalBlockingTime { get; set; }
        public string TotalBlockingTimeGrade { get; set; }
        public string TotalBlockingTimeDescription { get; set; }
        public string LargestContentfulPaint { get; set; }
        public string LargestContentfulPaintGrade { get; set; }
        public string LargestContentfulPaintDescription { get; set; }
        public string FullyLoadedTime { get; set; }
        public string FullyLoadedTimeGrade { get; set; }
        public string FullyLoadedTimeDescription { get; set; }
        public string LastTestDate { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class WebsiteResponseScore
    {
        [Key] public int IdWebsiteResponseScore { get; set; }
        [ForeignKey("Website")] public Guid IdWebsite { get; set; }
        public int structure_score { get; set; }
        public int speed_index { get; set; }
        public int onload_time { get; set; }
        public int redirect_duration { get; set; }
        public int first_paint_time { get; set; }
        public int dom_content_loaded_duration { get; set; }
        public int dom_content_loaded_time { get; set; }
        public int dom_interactive_time { get; set; }
        public int page_requests { get; set; }
        public int page_bytes { get; set; }
        public string? gtmetrix_grade { get; set; }
        public string? location { get; set; }
        public int html_bytes { get; set; }
        public int first_contentful_paint { get; set; }
        public int performance_score { get; set; }
        public int fully_loaded_time { get; set; }
        public int total_blocking_time { get; set; }
        public int largest_contentful_paint { get; set; }
        public int time_to_interactive { get; set; }
        public int time_to_first_byte { get; set; }
        public int rum_speed_index { get; set; }
        public int backend_duration { get; set; }
        public int onload_duration { get; set; }
        public int connect_duration { get; set; }
        public string? state { get; set; }
        public DateTime TestTime { get; set; }
        public Website? Website { get; set; }

    }
}
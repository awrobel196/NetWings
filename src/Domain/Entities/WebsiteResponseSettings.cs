using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class WebsiteResponseSettings
    {
        [Key] public int IdWebsiteResponseSettings { get; set; }
        [ForeignKey("Website")] public Guid IdWebsite { get; set; }
        public string? Location { get; set; }
        public string? Browser { get; set; }
        public string? Report { get; set; }
        public int Adblock { get; set; }
        public string? SimulateDevice { get; set; }
        public string? UserAgent { get; set; }

        public Website? Website { get; set; }
    }
}
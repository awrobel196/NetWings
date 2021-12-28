using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities
{
    public class Website
    {
        [Key] public Guid IdWebsite { get; set; }
        [ForeignKey("License")] public Guid IdLicense { get; set; }

        [Display(Name = "Adres url strony")]
        [Required]
        [Url(ErrorMessage = "Podano niepoprawny adres url strony")]
        public string? Name { get; set; }
        public string? Url { get; set; }
        public TestLocation TestLocation { get; set; }
        public TestEnviroment TestEnviroment { get; set; }
        public bool IsTestedByBenchmark { get; set; }
        public bool IsVerified { get; set; }
        public string? BenchmarkUrl { get; set; }

        //Pole zawerajace informacje true jesli ostatni uptime nie zwrocil błędy. Jeśli MachineUptimeReader wykryje że pole zmieniło się, wyśle sms
        public bool LatestUptimeResult { get; set; }
        public DateTime LastTestedByBenchmark { get; set; }
        public WebsiteResponseSettings? WebsiteResponseSettings { get; set; }
        public List<WebsiteResponseScore>? WebsiteResponseScore { get; set; }
        public List<WebsiteUptimeScore>? WebsiteUptimeScore { get; set; }
        public License? License { get; set; }
    }
}
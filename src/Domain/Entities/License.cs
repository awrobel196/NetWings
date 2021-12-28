using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class License
    {
        [Key] public Guid IdLicense { get; set; } = new Guid();
        [ForeignKey("LicenseType")] public int IdLicenseType { get; set; }
        public DateTime LicenseExpired { get; set; }
        public LicenseType? LicenseType { get; set; }
        public List<User>? User { get; set; }
        public List<Website>? Website { get; set; }
    }
}
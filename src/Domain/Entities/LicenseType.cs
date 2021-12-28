using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class LicenseType
    {
        [Key] public int IdLicenseType { get; set; }
        
        public double Price { get; set; }
        public string? LicenseName { get; set; }
        public int NumberOfWebsites { get; set; }
        public int NumberOfUsers { get; set; }
        
        public List<License>? License { get; set; }

       
    }
}
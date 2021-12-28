using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common.ExtensionMethods;

namespace Domain.Entities
{
    public class User
    {
        [Key] public Guid IdUser { get; set; }
        [ForeignKey("License")] public Guid IdLicense { get; set; }
        [Display(Name = "Nazwa użytkownika")] [Required(ErrorMessage = "Nazwa nie może być pusta")] public string? Name { get; set; }
        [Display(Name = "Numer telefonu")] [Required(ErrorMessage = "Telefon nie może być pusty")] public string? Phone { get; set; }
        [Display(Name = "Adres email")] [Required(ErrorMessage = "Email nie może być pusty")] public string? Email { get; set; }
        [Column] private string? password { get; set; }
        public License? License { get; set; }

        [NotMapped]
        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Hasło nie może być puste")]
        public string Password
        {
            get => password ?? "brak";
            set => password = value.ToHash();
        }
    }


}

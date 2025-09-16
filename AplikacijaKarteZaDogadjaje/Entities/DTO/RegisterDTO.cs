using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Ime je obavezno")]
        [StringLength(50,ErrorMessage = "Ime moze da sadrzi najvise 50 karaktera")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Prezime je obavezno")]
        [StringLength(50,ErrorMessage = "Prezime moze da sadrzi najvise 50 karaktera")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Email je obavezan")]
        [StringLength(100,ErrorMessage = "Email moze da sadrzi najvise 100 karaktera")]
        [EmailAddress(ErrorMessage = "Nepravilan format email adrese")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Broj telefona je obavezan")]
        [Phone]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "Lozinka je obavezna")]
        [MaxLength(50,ErrorMessage = "Lozinka moze da sadrzi najvise 50 karaktera")]
        [MinLength(8,ErrorMessage = "Lozinka mora da sadrzi najmanje 8 karaktera")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")]
        public string? Password { get; set; }
        [Required(ErrorMessage ="Potvrda lozinke je obavezna")]
        [Compare("Password", ErrorMessage = "Loinke se ne poklapaju")]
        public string? ConfirmPassword { get; set; }
    }
}

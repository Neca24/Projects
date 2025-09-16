using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email je obavezan")]
        [EmailAddress(ErrorMessage = "Adresa nije u ispravnom formatu")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lozinka je obavezna")]
        [StringLength(50, ErrorMessage = "Lozinka ne moze biti duza od 100 karaktera")]
        public string Password { get; set; } = string.Empty;
    }
}

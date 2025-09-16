using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class TicketDTO
    {
        [Required(ErrorMessage = "Cena je obavezna")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Dogadjaj je obavezan")]
        public int EventId { get; set; }
    }
}

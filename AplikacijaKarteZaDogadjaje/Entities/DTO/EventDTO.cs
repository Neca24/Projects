using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class EventDTO
    {
        [Required]
        [StringLength(100,ErrorMessage = "Naziv moze sadrzati najvise 100 karaktera")]
        public string? Name { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EventDate { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Lokacija moze sadrzati najvise 50 karaktera")]
        public string? Location { get; set; }
    }
}

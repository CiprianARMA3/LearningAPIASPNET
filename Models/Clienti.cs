using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Clienti
    {
        public int Id { get; set; }
        [Required]
        public required string Nome { get; set; }
        [Required]
        public required string Cognome { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Telefono { get; set; }
    }
}
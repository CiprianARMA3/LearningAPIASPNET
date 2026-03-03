using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        // RLS: link each cliente to the user who created it
        public string? AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        public AppUser? AppUser { get; set; }
    }
}
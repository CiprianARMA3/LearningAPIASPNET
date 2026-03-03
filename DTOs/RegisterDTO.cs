using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string? Nome { get; set; }
        [Required]
        public string? Cognome { get; set; }
        [Required, EmailAddress]
        public string? Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "La password deve essere di almeno 8 caratteri.")]
        public string? Password { get; set; }
        [Required]
        public string? NumeroTelefono { get; set; }
    }
}
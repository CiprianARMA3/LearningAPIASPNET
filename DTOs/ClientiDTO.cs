using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class ClientiDto
    {
        public int Id { get; internal set; }
        public required string Nome { get; set; }
        public required string Cognome { get; set; }
        public required string Email { get; set; }
        public string? Telefono { get; set; }
    }
}
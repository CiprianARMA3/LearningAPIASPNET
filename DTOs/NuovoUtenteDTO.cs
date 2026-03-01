namespace WebAPI.DTOs
{
    public class NuovoUtenteDTO
    {
        public string Nome { get; set; } = null!;
        public string Cognome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string NumeroTelefono { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
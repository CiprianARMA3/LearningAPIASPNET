using Microsoft.AspNetCore.Identity;

namespace WebAPI.Models
{
    public class AppUser : IdentityUser
    {
        public string? Nome { get; set; }
        public string? Cognome { get; set; }

        public string?   numeroTelefono { get; set; }
        public required string Role { get; set; }
    }
}
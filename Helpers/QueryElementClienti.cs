namespace WebAPI.Helpers
{
    public class QueryElementClienti
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Cognome { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize = 10;
    }
}
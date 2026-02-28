namespace WebAPI.Helpers
{
    public class QueryElementOperazioni
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string? Descrizione { get; set; }
        public decimal Importo { get; set; }
        public DateTime Data { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize = 10;
    }
    public class QueryElementOperazioniByPageSize
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize = 10;
    }
}
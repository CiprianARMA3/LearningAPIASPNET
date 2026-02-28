using WebAPI.Models;

namespace WebAPI.DTOs
{
    public class OperazioniDto
    {
        public int Id { get; internal set; }
        public int IdCliente { get; set; }
        public Clienti? Cliente { get; internal set; }
        public string? Descrizione { get; set; }
        public decimal Importo { get; set; }
        public DateTime Data { get; set; }
    }
    public class OperazioniRequestDto
    {
        public int IdCliente { get; set; }
        public string? Descrizione { get; set; }
        public decimal Importo { get; set; }
        public DateTime Data { get; set; }
    }
}
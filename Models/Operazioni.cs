using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebAPI.Models
{
    public class Operazioni
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        
        [ForeignKey("IdCliente")]
        public Clienti? Cliente { get; set; }
        public string? Descrizione { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Importo { get; set; }
        public DateTime Data { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace SmartBudgetApp.Models
{
    public class Ingresos
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}

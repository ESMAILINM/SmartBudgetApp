using System.ComponentModel.DataAnnotations;

namespace SmartBudgetApp.Models
{
    public class Ingresos
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Descripcion { get; set; } = string.Empty;
        [Required(ErrorMessage = "Monto es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El Monto debe ser mayor a cero")]
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}

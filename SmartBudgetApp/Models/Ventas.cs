using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBudgetApp.Models
{
    public class Ventas
    {
        [Key]
        public int VentaId { get; set; }
        [Required(ErrorMessage = "El departamento es obligatorio.")]

        [ForeignKey("DepartamentoId")]
        public int DepartamentoId { get; set; }
        public Departamento? Departamento { get; set; }

        [Required(ErrorMessage = "El total es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El total debe ser un valor positivo.")]
        public decimal Total { get; set; }

        public DateTime Fecha { get; set; }
    }
}


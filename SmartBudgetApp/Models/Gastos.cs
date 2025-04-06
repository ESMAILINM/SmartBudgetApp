using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SmartBudgetApp.Models
{
    public class Gastos
    {
        [Key]
        public int GastoId { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El monto debe ser positivo.")]
        public decimal Monto { get; set; }

        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        [ForeignKey("Categorias")]
        public int CategoriaId { get; set; }

        public Categorias? Categorias { get; set; }
    }
}

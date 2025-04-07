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

        [Required(ErrorMessage = "Monto es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El Monto debe ser mayor a cero")]
        public decimal Monto { get; set; }

        public DateTime Fecha { get; set; }

        
        [ForeignKey("Categorias")]
        public int? CategoriaId { get; set; }

        public Categorias? Categorias { get; set; }
    }
}

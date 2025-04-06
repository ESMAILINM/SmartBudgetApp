using System.ComponentModel.DataAnnotations;

namespace SmartBudgetApp.Models
{
    public class Facturas
    {
        [Key]
        public int FacturaId { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        public int CategoriaId { get; set; } 

        public Categorias Categoria { get; set; } 

        [Required(ErrorMessage = "Estado es obligatorio")]
        public string Estado { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Total es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor a cero")]
        public decimal Total { get; set; }
    }
}
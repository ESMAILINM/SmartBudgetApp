using System.ComponentModel.DataAnnotations;

namespace SmartBudgetApp.Models
{
    public class Categorias
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public string Nombre { get; set; }

    }

   
}
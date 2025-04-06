using System.ComponentModel.DataAnnotations;

namespace SmartBudgetApp.Models
{
    public class Departamento
    {
        [Key]
        public int DepartamentoId { get; set; }

        [Required(ErrorMessage = "El nombre del departamento es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public string Nombre { get; set; }
    }
}

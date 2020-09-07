using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace UciCardiaco.Web.Models.Almacen.Sintoma
{
    public class ActualizarViewModel
    {
        [Required]
        public int idsintoma { get; set; }
        [Required]
        [Display(Name = "Enfermedad")]
        public int idenfermedad { get; set; }
        public string codigo { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe tener más de 50 caracteres, ni menos de 3 caracteres.")]
        public string nombre { get; set; }
        [Required]
        public decimal valor { get; set; }
        public string descripcion { get; set; }
    }
}

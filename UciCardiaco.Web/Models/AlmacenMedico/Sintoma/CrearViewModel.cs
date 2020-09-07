using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UciCardiaco.Web.Models.Almacen.Sintoma
{
    public class CrearViewModel
    {
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

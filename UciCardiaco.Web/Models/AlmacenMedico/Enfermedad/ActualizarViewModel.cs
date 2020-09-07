using System.ComponentModel.DataAnnotations;

namespace UciCardiaco.Web.Models.Almacen.Enfermedad
{
    public class ActualizarViewModel
    {
        [Required]
        public int idenfermedad { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe tener más de 50 caracteres, ni menos de 3 caracteres.")]
        public string nombre { get; set; }
        [StringLength(256)]
        public string descripcion { get; set; }
       
    }
}

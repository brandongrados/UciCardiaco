using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UciCardiaco.Entidad.Almacen
{
    public class Enfermedad
    {
        public int idenfermedad { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener más de 50 caracteres, ni menos de 3 caracteres.")]
        public string nombre { get; set; }
        [StringLength(256)]
        public string descripcion { get; set; }
        public bool condicion { get; set; }

        public ICollection<Sintoma> sintomas {get;set;}
    }
}

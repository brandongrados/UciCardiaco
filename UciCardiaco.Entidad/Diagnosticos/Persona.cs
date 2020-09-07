using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UciCardiaco.Entidad.Almacen;

namespace UciCardiaco.Entidad.Diagnosticos
{
    public class Persona
    {
        public int idpersona { get; set; }
        [Required]
        public string tipo_persona { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener más de 100 caracteres, ni menos de 3 caracteres.")]
        public string nombre { get; set; }
        public string tipo_documento { get; set; }
        public string num_documento { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }

        public ICollection<Historia> historias { get; set; }
        public ICollection<Diagnostico> diagnosticos { get; set; }

    }
}

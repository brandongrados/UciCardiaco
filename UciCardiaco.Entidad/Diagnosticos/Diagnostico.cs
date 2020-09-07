using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UciCardiaco.Entidad.Usuarios;

namespace UciCardiaco.Entidad.Diagnosticos
{
    public class Diagnostico
    {
        public int id_diagnostico{ get; set; }
        [Required]
        public int idpaciente { get; set; }
        [Required]
        public int idusuario { get; set; }
        [Required]
        public string tipo_diagnostico { get; set; }
        public string serie_diagnostico { get; set; }
        [Required]
        public string num_diagnostico { get; set; }
        [Required]
        public DateTime fecha_hora { get; set; }
        [Required]
        public decimal resultado { get; set; }
        [Required]
        public string estado { get; set; }

        public ICollection<DetalleDiagnostico> detalles { get; set; }
        public Usuario usuario { get; set; }
        public Persona persona { get; set; }
    }
}

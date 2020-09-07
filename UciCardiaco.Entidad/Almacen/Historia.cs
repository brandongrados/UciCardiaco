using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UciCardiaco.Entidad.Usuarios;
using UciCardiaco.Entidad.Diagnosticos;

namespace UciCardiaco.Entidad.Almacen
{
    public class Historia
    {
        public int idhistoria { get; set; }
        [Required]
        public int idenfermera { get; set; }
        [Required]
        public int idusuario { get; set; }
        [Required]
        public string tipo_historia { get; set; }
        public string serie_historia { get; set; }
        [Required]
        public string num_historia { get; set; }
        [Required]
        public DateTime fecha_hora { get; set; }
        [Required]
        public decimal resultado { get; set; }
        [Required]
        public string estado { get; set; }

        public ICollection<DetalleHistoria> detalles { get; set; }
        public Usuario usuario { get; set; }
        public Persona persona { get; set; }
    }
}

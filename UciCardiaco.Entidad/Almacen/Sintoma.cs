using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Generic;
using UciCardiaco.Entidad.Diagnosticos;

namespace UciCardiaco.Entidad.Almacen
{
    public class Sintoma
    {
        public int idsintoma { get; set; }
        [Required]
        [Display(Name = "Enfermedad")]
        public int idenfermedad { get; set; }
        public string codigo { get; set; }
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "El nombre debe tener más de 50 caracteres, ni menos de 3 caracteres.")]
        public string nombre { get; set; }
        [Required]
        public decimal valor { get; set; }
        public string descripcion { get; set; }
        public bool condicion { get; set; }

        [ForeignKey("idenfermedad ")]
        public Enfermedad enfermedad { get; set; }
        public ICollection<DetalleHistoria> DetallesHistorias { get; set; }
        public ICollection<DetalleDiagnostico> DetallesDiagnosticos { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using UciCardiaco.Entidad.Almacen;

namespace UciCardiaco.Entidad.Diagnosticos
{
    public class DetalleDiagnostico
    {
        public int iddetalle_diagnostico { get; set; }
        [Required]
        public int id_diagnostico { get; set; }
        [Required]
        public int idsintoma  { get; set;  }
      
        [Required]
        public decimal valor { get; set; }

        public Diagnostico diagnostico { get; set; }
        public Sintoma sintoma { get; set; }
    }
}

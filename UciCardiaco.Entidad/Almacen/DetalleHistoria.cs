using System.ComponentModel.DataAnnotations;

namespace UciCardiaco.Entidad.Almacen
{
    public class DetalleHistoria
    {
        public int iddetalle_historia { get; set; }
        [Required]
        public int idhistoria { get; set; }
        [Required]
        public int idsintoma { get; set; }
        [Required]
        public decimal valor { get; set; }

        
        public Historia historia { get; set; }
        public Sintoma  sintoma { get; set; }
        
    }
}

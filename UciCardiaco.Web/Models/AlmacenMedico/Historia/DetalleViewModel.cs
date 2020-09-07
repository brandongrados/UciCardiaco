using System.ComponentModel.DataAnnotations;

namespace UciCardiaco.Web.Models.Almacen.Historia
{
    public class DetalleViewModel
    {
        [Required]
        public int idsintoma { get; set; }
        public string sintoma { get; set; }
        [Required]
        public decimal valor { get; set; }

    }
}

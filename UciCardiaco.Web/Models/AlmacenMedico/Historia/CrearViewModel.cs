using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UciCardiaco.Web.Models.Almacen.Historia
{
    public class CrearViewModel
    {
        //Propiedades Maestro
        
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
        public decimal resultado { get; set; }
        
        //Propiedades Detalle

        [Required]
        public List<DetalleViewModel> detalles { get; set; }
        
    }
}

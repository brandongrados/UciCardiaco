using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UciCardiaco.Web.Models.Diagnosticos.Diagnostico
{
    public class CrearViewModel
    {
        //Propiedades Maestro

        [Required]
        public int idpaciente { get; set; }
        [Required]
        public int idusuario { get; set; }
        [Required]
        public string tipo_diagnostico{ get; set; }
        public string serie_diagnostico{ get; set; }
        [Required]
        public string num_diagnostico { get; set; }
        [Required]
        public decimal resultado { get; set; }

        //Propiedades Detalle

        [Required]
        public List<DetalleViewModel> detalles { get; set; }

    }
}

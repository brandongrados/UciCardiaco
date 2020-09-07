using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UciCardiaco.Web.Models.Almacen.Historia
{
    public class HistoriaViewModel
    {
        public int idhistoria { get; set; }
        public int idenfermera { get; set; }
        public string enfermera { get; set; }
        public int idusuario { get; set; }
        public string usuario { get; set; }
        public string tipo_historia { get; set; }
        public string serie_historia { get; set; }
        public string num_historia { get; set; }
        public DateTime fecha_hora { get; set; }
        public decimal resultado { get; set; }
        public string estado { get; set; }

        
    }
}

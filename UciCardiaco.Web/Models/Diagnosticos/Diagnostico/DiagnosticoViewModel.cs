using System;


namespace UciCardiaco.Web.Models.Diagnosticos.Diagnostico
{
    public class DiagnosticoViewModel
    {
        public int id_diagnostico { get; set; }
        public int idpaciente { get; set; }
        public string paciente { get; set; }
        public string num_documento { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public int idusuario { get; set; }
        public string usuario { get; set; }
        public string tipo_diagnostico { get; set; }
        public string serie_diagnostico { get; set; }
        public string num_diagnostico{ get; set; }
        public DateTime fecha_hora { get; set; }
        public decimal resultado { get; set; }
        public string estado { get; set; }
    }
}

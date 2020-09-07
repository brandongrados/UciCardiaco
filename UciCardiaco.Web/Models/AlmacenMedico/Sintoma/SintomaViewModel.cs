using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace UciCardiaco.Web.Models.Almacen.Sintoma
{
    public class SintomaViewModel
    {
        public int idsintoma { get; set; }
        [Required]
        [Display(Name = "Enfermedad")]
        [ForeignKey("idenfermedad ")]
        public int idenfermedad { get; set; }
        public string enfermedad { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public decimal valor { get; set; }
        public string descripcion { get; set; }
        public bool condicion { get; set; }

    }
}


//sirve para restringir los llamados a los servicios web
namespace UciCardiaco.Web.Models.Almacen.Enfermedad
{
    public class EnfermedadViewModel
    {
        public int idenfermedad { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool condicion { get; set; }
    }
}

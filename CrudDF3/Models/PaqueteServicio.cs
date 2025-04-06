
namespace CrudDF3.Models
{
    public partial class PaqueteServicio
    {
        public int IdPaquete { get; set; }
        public int IdServicio { get; set; }

        public virtual PaquetesTuristico? IdPaqueteNavigation { get; set; }
        public virtual Servicio? IdServicioNavigation { get; set; }
    }
}

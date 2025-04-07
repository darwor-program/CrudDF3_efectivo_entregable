namespace CrudDF3.Models
{
    public class PaqueteHabitacion
    {
        public int IdPaquete { get; set; }
        public int IdHabitacion { get; set; }

        public virtual PaquetesTuristico? IdPaqueteNavigation { get; set; }
        public virtual Habitacione? IdHabitacionNavigation { get; set; }
    }
}
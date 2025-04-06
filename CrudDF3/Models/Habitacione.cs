namespace CrudDF3.Models
{
    public partial class Habitacione
    {
        public int IdHabitacion { get; set; }
        public string? TipoHabitacion { get; set; }
        public int? CapacidadHuespedes { get; set; }
        public bool EstadoHabitacion { get; set; }
        public string? DescripcionHabitacion { get; set; }
        public decimal? TarifaHabitacion { get; set; }
        public string? CaracteristicasHabitacion { get; set; }

        public virtual ICollection<PaqueteHabitacion> PaqueteHabitaciones { get; set; } = new List<PaqueteHabitacion>();
    }
}
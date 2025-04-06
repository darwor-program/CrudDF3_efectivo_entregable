using System.ComponentModel.DataAnnotations.Schema;

namespace CrudDF3.Models
{
    public partial class PaquetesTuristico
    {
        public int IdPaquete { get; set; }
        public string? NombrePaquete { get; set; }
        public string? DescripcionPaquete { get; set; }
        public decimal? PrecioPaquete { get; set; }
        public bool DisponibilidadPaquete { get; set; }
        public DateTime? FechaPaquete { get; set; }
        public string? DestinoPaquete { get; set; }
        public bool EstadoPaquete { get; set; }
        public string? TipoViajePaquete { get; set; }

        public virtual ICollection<ReservasPaquete> ReservasPaquetes { get; set; } = new List<ReservasPaquete>();
        public virtual ICollection<PaqueteServicio> PaqueteServicios { get; set; } = new List<PaqueteServicio>();
        public virtual ICollection<PaqueteHabitacion> PaqueteHabitaciones { get; set; } = new List<PaqueteHabitacion>();

        // Propiedades para manejar la selección en las vistas
        [NotMapped]
        public List<int>? SelectedServicios { get; set; }
        [NotMapped]
        public List<int>? SelectedHabitaciones { get; set; }
    }
}
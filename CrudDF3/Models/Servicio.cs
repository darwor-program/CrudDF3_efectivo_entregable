namespace CrudDF3.Models
{
    public partial class Servicio
    {
        public int IdServicio { get; set; }
        public string? NombreServicio { get; set; }
        public string? Descripcion { get; set; }
        public string? Categoria { get; set; }
        public decimal? Costo { get; set; }
        public bool Disponibilidad { get; set; }
        public string? Observacion { get; set; }
        public bool EstadoServicio { get; set; }
        public string? TipoServicio { get; set; }


        // Relación muchos a muchos con PaquetesTuristico
        public virtual ICollection<PaqueteServicio> PaqueteServicios { get; set; } = new List<PaqueteServicio>();
    }
}

using System.ComponentModel.DataAnnotations;

namespace CrudDF3.Models
{
    public class PaqueteSelectionViewModel
    {
        public int IdPaquete { get; set; }

        [Display(Name = "Nombre")]
        public string? NombrePaquete { get; set; }

        [Display(Name = "Descripción")]
        public string? DescripcionPaquete { get; set; }

        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public decimal? PrecioPaquete { get; set; }

        [Display(Name = "Destino")]
        public string? DestinoPaquete { get; set; }

        [Display(Name = "Tipo de Viaje")]
        public string? TipoViajePaquete { get; set; }

        [Display(Name = "Seleccionar")]
        public bool IsSelected { get; set; }
    }
}
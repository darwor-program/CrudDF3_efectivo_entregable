using System;
using System.ComponentModel.DataAnnotations;

namespace CrudDF3.Models.ViewModels
{
    public class ReservaIndexViewModel
    {
        public int IdReserva { get; set; }

        [Display(Name = "Usuario")]
        public string NombreUsuario { get; set; }

        [Display(Name = "Fecha Inicial")]
        [DataType(DataType.Date)]
        public DateTime? FechaInicial { get; set; }

        [Display(Name = "Fecha Final")]
        [DataType(DataType.Date)]
        public DateTime? FechaFinal { get; set; }

        [Display(Name = "Personas")]
        public int? NumeroPersonas { get; set; }

        [Display(Name = "Valor Total")]
        [DataType(DataType.Currency)]
        public decimal? Valor { get; set; }

        [Display(Name = "Anticipo")]
        [DataType(DataType.Currency)]
        public decimal? Anticipo { get; set; }

        [Display(Name = "Fecha Reserva")]
        [DataType(DataType.DateTime)]
        public DateTime? FechaReserva { get; set; }

        // Propiedad para el estado (booleana para el controlador)
        public bool EstadoReserva { get; set; }

        // Propiedad calculada para la vista
        [Display(Name = "Estado")]
        public string EstadoReservaTexto => EstadoReserva ? "Activa" : "Inactiva";

        [Display(Name = "Paquetes")]
        public int PaquetesCount { get; set; }

        // Opcional: Si necesitas mostrar los nombres de los paquetes
        public string NombresPaquetes { get; set; }
    }
}
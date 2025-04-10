using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrudDF3.Models.ViewModels
{
    public class EditReservaViewModel
    {
        public int IdReserva { get; set; }

        [Required(ErrorMessage = "El usuario es requerido")]
        [Display(Name = "Usuario")]
        public int? IdUsuario { get; set; }

        [Required(ErrorMessage = "La fecha inicial es requerida")]
        [Display(Name = "Fecha Inicial")]
        [DataType(DataType.Date)]
        public DateTime? FechaInicial { get; set; }

        [Required(ErrorMessage = "La fecha final es requerida")]
        [Display(Name = "Fecha Final")]
        [DataType(DataType.Date)]
        public DateTime? FechaFinal { get; set; }

        [Required(ErrorMessage = "El número de personas es requerido")]
        [Range(1, 20, ErrorMessage = "Debe ser entre 1 y 20 personas")]
        [Display(Name = "Número de Personas")]
        public int? NumeroPersonas { get; set; }

        [Display(Name = "Valor Total")]
        [DataType(DataType.Currency)]
        public decimal? Valor { get; set; }

        [Display(Name = "Anticipo")]
        [DataType(DataType.Currency)]
        public decimal? Anticipo { get; set; }

        [Display(Name = "Fecha de Reserva")]
        [DataType(DataType.DateTime)]
        public DateTime? FechaReserva { get; set; }

        [Display(Name = "Estado de Reserva")]
        public bool EstadoReserva { get; set; }

        // Lista de paquetes disponibles para selección
        public List<PaqueteSelectionViewModel>? PaquetesDisponibles { get; set; } = new List<PaqueteSelectionViewModel>();

        // Lista de IDs de paquetes seleccionados
        public List<int> PaquetesSeleccionados { get; set; } = new List<int>();

        // SelectList para el dropdown de usuarios
        public SelectList? Usuarios { get; set; }
    }
}
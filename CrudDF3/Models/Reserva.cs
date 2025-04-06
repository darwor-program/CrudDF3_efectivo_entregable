using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudDF3.Models;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public int? IdUsuario { get; set; }

    public DateTime? FechaInicial { get; set; }

    public DateTime? FechaFinal { get; set; }

    public int? NumeroPersonas { get; set; }

    public decimal? Valor { get; set; }

    public decimal? Anticipo { get; set; }

    public DateTime? FechaReserva { get; set; }

    public bool EstadoReserva { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    [NotMapped]
    public List<int> SelectedPaquetes { get; set; } = new List<int>();
    public virtual ICollection<ReservasPaquete> ReservasPaquetes { get; set; } = new List<ReservasPaquete>();

}

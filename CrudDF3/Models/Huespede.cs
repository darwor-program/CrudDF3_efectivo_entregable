using System;
using System.Collections.Generic;

namespace CrudDF3.Models;

public partial class Huespede
{
    public int IdHuesped { get; set; }

    public int? CedulaHuesped { get; set; }

    public string? NombreHuesped { get; set; }

    public string? ApellidoHuesped { get; set; }

    public string? CorreoHuesped { get; set; }

    public DateTime? FechaEntradaHuesped { get; set; }

    public DateTime? FechaSalidaHuesped { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}

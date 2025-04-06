using System;
using System.Collections.Generic;

namespace CrudDF3.Models;

public partial class ReservasPaquete
{
    public int IdReservaPaquete { get; set; }

    public int? IdReserva { get; set; }

    public int IdPaquete { get; set; }

    public virtual PaquetesTuristico? IdPaqueteNavigation { get; set; }

    public virtual Reserva? IdReservaNavigation { get; set; }
}

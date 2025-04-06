using System;
using System.Collections.Generic;

namespace CrudDF3.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public int? CedulaUsuario { get; set; }

    public string? NombreUsuario { get; set; }

    public string? ApellidoUsuario { get; set; }

    public string? CorreoUsuario { get; set; }

    public string? ContraseñaUsuario { get; set; }

    public bool EstadoUsuario { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public int? IdRol { get; set; }

    public virtual Role? IdRolNavigation { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}

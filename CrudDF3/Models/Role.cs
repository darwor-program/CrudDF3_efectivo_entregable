using System;
using System.Collections.Generic;

namespace CrudDF3.Models;

public partial class Role
{
    public int IdRol { get; set; }

    public string? NombreRol { get; set; }

    public string? DescripcionRol { get; set; }

    public bool EstadoRol { get; set; }

    public virtual ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

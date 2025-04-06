using System;
using System.Collections.Generic;

namespace CrudDF3.Models;

public partial class Permiso
{
    public int IdPermiso { get; set; }

    public string? NombrePermiso { get; set; }

    public string? DescripcionPermiso { get; set; }

    public bool EstadoPermiso { get; set; }

    public virtual ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();
}

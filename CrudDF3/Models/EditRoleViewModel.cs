using System.Collections.Generic;

namespace CrudDF3.Models
{
    public class EditRoleViewModel
    {
        public int IdRol { get; set; }
        public string? NombreRol { get; set; }
        public string? DescripcionRol { get; set; }
        public bool EstadoRol { get; set; }

        // Lista de todos los permisos disponibles
        public List<Permiso>? AllPermissions { get; set; }

        // Lista de los permisos seleccionados (ID de permisos)
        public List<int>? SelectedPermissions { get; set; }

    }
}


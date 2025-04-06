using Microsoft.AspNetCore.Mvc.Rendering;

namespace CrudDF3.Models
{
    public class CreateRoleViewModel
    {
        public string? NombreRol { get; set; }
        public string? DescripcionRol { get; set; }
        public bool EstadoRol { get; set; }

        // Lista de permisos seleccionados por el usuario
        public List<int>? SelectedPermissions { get; set; }

        // Lista de permisos disponibles
        public IEnumerable<SelectListItem>? Permissions { get; set; }
    }
}

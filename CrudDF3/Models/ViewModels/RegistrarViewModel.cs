namespace CrudDF3.Models.ViewModels
{
    public class RegistrarViewModel
    {
        public int CedulaUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string CorreoUsuario { get; set;}
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string ContraseñaUsuario { get; set; }
        public int IdRol { get; set; } = 2;

    }
}

using System.ComponentModel.DataAnnotations;

namespace CrudDF3.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string CorreoUsuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ContraseñaUsuario { get; set; }

    }
}

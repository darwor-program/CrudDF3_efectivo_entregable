namespace CrudDF3.Models
{
    public class AuthService
    {
        private readonly CrudDf3Context _context;

        public AuthService(CrudDf3Context context)
        {
            _context = context;
        }

        public Usuario? Login(string correoUsuario, string contraseñaUsuario)
        {
            // ENCRIPTAR LA CONTRASEÑA ANTES DE COMPARAR SI USAS HASHING
            return _context.Usuarios.FirstOrDefault(u => u.CorreoUsuario == correoUsuario && u.ContraseñaUsuario == contraseñaUsuario);
        }
    }
}

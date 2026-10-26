using System.Collections.Generic;
using System.Linq;
using SistemaBoletosAutobus.Entidades;

namespace SistemaBoletosAutobus.Controladores
{
    public static class UsuarioController
    {
        public static List<Usuario> Usuarios = new List<Usuario>();

        static UsuarioController()
        {
            InicializarDatos();
        }

        public static void InicializarDatos()
        {
            Usuarios.Clear();
            Usuarios.Add(new Usuario(1, "admin", "admin123", RolUsuario.Administrador, true));
            Usuarios.Add(new Usuario(2, "cajero1", "cajero123", RolUsuario.Cajero, true));
            Usuarios.Add(new Usuario(3, "cajero2", "cajero456", RolUsuario.Cajero, false));
        }

        public static Usuario Autenticar(string nombreUsuario, string clave)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(clave))
            {
                return null;
            }

            return Usuarios.FirstOrDefault(u =>
                u.Activo &&
                u.NombreUsuario.ToLowerInvariant() == nombreUsuario.ToLowerInvariant() &&
                u.Clave == clave);
        }

        public static bool CambiarEstadoUsuario(int idUsuario, bool activo)
        {
            var usuario = Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);
            if (usuario == null)
            {
                return false;
            }

            usuario.Activo = activo;
            return true;
        }

        public static List<Usuario> ObtenerUsuariosActivos()
        {
            return Usuarios.Where(u => u.Activo).ToList();
        }
    }
}

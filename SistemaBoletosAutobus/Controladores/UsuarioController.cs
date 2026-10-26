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

        public static void Reset()
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

        public static Usuario RegistrarUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(usuario.NombreUsuario) || string.IsNullOrWhiteSpace(usuario.Clave))
            {
                return null;
            }

            if (Usuarios.Any(u => u.NombreUsuario.ToLowerInvariant() == usuario.NombreUsuario.Trim().ToLowerInvariant()))
            {
                return null;
            }

            Usuarios.Add(usuario);
            return usuario;
        }

        public static bool ActualizarUsuario(int idUsuario, string nombreUsuario, string clave, RolUsuario rol, bool activo)
        {
            var usuario = Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);
            if (usuario == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(nombreUsuario))
            {
                usuario.NombreUsuario = nombreUsuario.Trim();
            }

            if (!string.IsNullOrWhiteSpace(clave))
            {
                usuario.Clave = clave;
            }

            usuario.Rol = rol;
            usuario.Activo = activo;
            return true;
        }

        public static bool EliminarUsuario(int idUsuario)
        {
            var usuario = Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);
            if (usuario == null)
            {
                return false;
            }

            Usuarios.Remove(usuario);
            return true;
        }

        public static Usuario Autenticar(string nombreUsuario, string clave)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(clave))
            {
                return null;
            }

            return Usuarios.FirstOrDefault(u =>
                u.Activo &&
                u.NombreUsuario.ToLowerInvariant() == nombreUsuario.Trim().ToLowerInvariant() &&
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

        public static bool TienePermiso(int idUsuario, RolUsuario rolRequerido)
        {
            var usuario = Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);
            return usuario != null && usuario.Activo && usuario.Rol == rolRequerido;
        }

        public static List<Usuario> ObtenerUsuariosActivos()
        {
            return Usuarios.Where(u => u.Activo).ToList();
        }
    }
}

namespace SistemaBoletosAutobus.Entidades
{
    public class Usuario
    {
        private int _idUsuario;
        private string _nombreUsuario;
        private string _clave;
        private RolUsuario _rol;
        private bool _activo;

        public Usuario()
        {
        }

        public Usuario(int idUsuario, string nombreUsuario, string clave, RolUsuario rol, bool activo)
        {
            _idUsuario = idUsuario;
            _nombreUsuario = nombreUsuario;
            _clave = clave;
            _rol = rol;
            _activo = activo;
        }

        public int IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }

        public string NombreUsuario
        {
            get { return _nombreUsuario; }
            set { _nombreUsuario = value; }
        }

        public string Clave
        {
            get { return _clave; }
            set { _clave = value; }
        }

        public RolUsuario Rol
        {
            get { return _rol; }
            set { _rol = value; }
        }

        public bool Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }
    }
}

using System;

namespace SistemaBoletosAutobus.Entidades
{
    public class Pasajero
    {
        private int _idPasajero;
        private string _cedula;
        private string _nombres;
        private string _apellidos;
        private DateTime _fechaNacimiento;
        private bool _tieneDiscapacidad;

        public Pasajero()
        {
        }

        public Pasajero(int idPasajero, string cedula, string nombres, string apellidos, DateTime fechaNacimiento, bool tieneDiscapacidad)
        {
            _idPasajero = idPasajero;
            _cedula = cedula;
            _nombres = nombres;
            _apellidos = apellidos;
            _fechaNacimiento = fechaNacimiento;
            _tieneDiscapacidad = tieneDiscapacidad;
        }

        public int IdPasajero
        {
            get { return _idPasajero; }
            set { _idPasajero = value; }
        }

        public string Cedula
        {
            get { return _cedula; }
            set { _cedula = value; }
        }

        public string Nombres
        {
            get { return _nombres; }
            set { _nombres = value; }
        }

        public string Apellidos
        {
            get { return _apellidos; }
            set { _apellidos = value; }
        }

        public DateTime FechaNacimiento
        {
            get { return _fechaNacimiento; }
            set { _fechaNacimiento = value; }
        }

        public bool TieneDiscapacidad
        {
            get { return _tieneDiscapacidad; }
            set { _tieneDiscapacidad = value; }
        }
    }
}

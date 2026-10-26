using System;

namespace SistemaBoletosAutobus.Entidades
{
    public class Ruta
    {
        private int _idRuta;
        private string _ciudadOrigen;
        private string _ciudadDestino;
        private decimal _precioBase;
        private TimeSpan _duracion;

        public Ruta()
        {
        }

        public Ruta(int idRuta, string ciudadOrigen, string ciudadDestino, decimal precioBase, TimeSpan duracion)
        {
            _idRuta = idRuta;
            _ciudadOrigen = ciudadOrigen;
            _ciudadDestino = ciudadDestino;
            _precioBase = precioBase;
            _duracion = duracion;
        }

        public int IdRuta
        {
            get { return _idRuta; }
            set { _idRuta = value; }
        }

        public string CiudadOrigen
        {
            get { return _ciudadOrigen; }
            set { _ciudadOrigen = value; }
        }

        public string CiudadDestino
        {
            get { return _ciudadDestino; }
            set { _ciudadDestino = value; }
        }

        public decimal PrecioBase
        {
            get { return _precioBase; }
            set { _precioBase = value; }
        }

        public TimeSpan Duracion
        {
            get { return _duracion; }
            set { _duracion = value; }
        }
    }
}

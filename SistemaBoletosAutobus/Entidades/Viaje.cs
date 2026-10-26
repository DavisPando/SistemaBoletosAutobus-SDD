using System;

namespace SistemaBoletosAutobus.Entidades
{
    public class Viaje
    {
        private int _idViaje;
        private int _idRuta;
        private int _idBus;
        private DateTime _fechaHoraSalida;
        private EstadoViaje _estado;

        public Viaje()
        {
        }

        public Viaje(int idViaje, int idRuta, int idBus, DateTime fechaHoraSalida, EstadoViaje estado)
        {
            _idViaje = idViaje;
            _idRuta = idRuta;
            _idBus = idBus;
            _fechaHoraSalida = fechaHoraSalida;
            _estado = estado;
        }

        public int IdViaje
        {
            get { return _idViaje; }
            set { _idViaje = value; }
        }

        public int IdRuta
        {
            get { return _idRuta; }
            set { _idRuta = value; }
        }

        public int IdBus
        {
            get { return _idBus; }
            set { _idBus = value; }
        }

        public DateTime FechaHoraSalida
        {
            get { return _fechaHoraSalida; }
            set { _fechaHoraSalida = value; }
        }

        public EstadoViaje Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
    }
}

using System;

namespace SistemaBoletosAutobus.Entidades
{
    public class Boleto
    {
        private int _idBoleto;
        private int _idViaje;
        private int _idPasajero;
        private int _numeroAsiento;
        private decimal _precioPagado;
        private int _idCajero;
        private DateTime _fechaHoraVenta;
        private EstadoBoleto _estado;
        private string _motivoAnulacion;

        public Boleto()
        {
        }

        public Boleto(int idBoleto, int idViaje, int idPasajero, int numeroAsiento, decimal precioPagado, int idCajero, DateTime fechaHoraVenta, EstadoBoleto estado, string motivoAnulacion)
        {
            _idBoleto = idBoleto;
            _idViaje = idViaje;
            _idPasajero = idPasajero;
            _numeroAsiento = numeroAsiento;
            _precioPagado = precioPagado;
            _idCajero = idCajero;
            _fechaHoraVenta = fechaHoraVenta;
            _estado = estado;
            _motivoAnulacion = motivoAnulacion;
        }

        public int IdBoleto
        {
            get { return _idBoleto; }
            set { _idBoleto = value; }
        }

        public int IdViaje
        {
            get { return _idViaje; }
            set { _idViaje = value; }
        }

        public int IdPasajero
        {
            get { return _idPasajero; }
            set { _idPasajero = value; }
        }

        public int NumeroAsiento
        {
            get { return _numeroAsiento; }
            set { _numeroAsiento = value; }
        }

        public decimal PrecioPagado
        {
            get { return _precioPagado; }
            set { _precioPagado = value; }
        }

        public int IdCajero
        {
            get { return _idCajero; }
            set { _idCajero = value; }
        }

        public DateTime FechaHoraVenta
        {
            get { return _fechaHoraVenta; }
            set { _fechaHoraVenta = value; }
        }

        public EstadoBoleto Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public string MotivoAnulacion
        {
            get { return _motivoAnulacion; }
            set { _motivoAnulacion = value; }
        }
    }
}

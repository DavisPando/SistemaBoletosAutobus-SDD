namespace SistemaBoletosAutobus.Entidades
{
    public class Bus
    {
        private int _idBus;
        private string _numeroDisco;
        private string _placa;
        private int _capacidad;

        public Bus()
        {
        }

        public Bus(int idBus, string numeroDisco, string placa, int capacidad)
        {
            _idBus = idBus;
            _numeroDisco = numeroDisco;
            _placa = placa;
            _capacidad = capacidad;
        }

        public int IdBus
        {
            get { return _idBus; }
            set { _idBus = value; }
        }

        public string NumeroDisco
        {
            get { return _numeroDisco; }
            set { _numeroDisco = value; }
        }

        public string Placa
        {
            get { return _placa; }
            set { _placa = value; }
        }

        public int Capacidad
        {
            get { return _capacidad; }
            set { _capacidad = value; }
        }
    }
}

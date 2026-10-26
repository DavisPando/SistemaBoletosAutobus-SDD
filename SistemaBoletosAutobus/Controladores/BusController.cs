using System.Collections.Generic;
using System.Linq;
using SistemaBoletosAutobus.Entidades;

namespace SistemaBoletosAutobus.Controladores
{
    public static class BusController
    {
        public static List<Bus> Buses = new List<Bus>();

        static BusController()
        {
            InicializarDatos();
        }

        public static void Reset()
        {
            InicializarDatos();
        }

        public static void InicializarDatos()
        {
            Buses.Clear();
            Buses.Add(new Bus(1, "001", "ABC-1234", 40));
            Buses.Add(new Bus(2, "002", "XYZ-9876", 30));
        }

        public static bool EsPlacaValida(string placa)
        {
            return !string.IsNullOrWhiteSpace(placa) && System.Text.RegularExpressions.Regex.IsMatch(placa, @"^[A-Z]{3}-\d{4}$");
        }

        public static bool EsCapacidadValida(int capacidad)
        {
            return capacidad >= 10 && capacidad <= 60;
        }

        public static bool RegistrarBus(Bus bus)
        {
            if (bus == null || !EsPlacaValida(bus.Placa) || !EsCapacidadValida(bus.Capacidad))
            {
                return false;
            }

            if (Buses.Any(b => b.Placa.Trim().ToUpperInvariant() == bus.Placa.Trim().ToUpperInvariant()))
            {
                return false;
            }

            Buses.Add(bus);
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using SistemaBoletosAutobus.Entidades;

namespace SistemaBoletosAutobus.Controladores
{
    public static class ViajeController
    {
        public static List<Viaje> Viajes = new List<Viaje>();

        static ViajeController()
        {
            InicializarDatos();
        }

        public static void Reset()
        {
            InicializarDatos();
        }

        public static void InicializarDatos()
        {
            Viajes.Clear();
            Viajes.Add(new Viaje(1, 1, 1, DateTime.Today.AddDays(2).AddHours(8), EstadoViaje.Programado));
            Viajes.Add(new Viaje(2, 2, 2, DateTime.Today.AddDays(5).AddHours(10), EstadoViaje.Programado));
        }

        public static bool ExisteConflictoDeHorario(int idBus, DateTime fechaHoraSalida)
        {
            return Viajes.Any(v => v.IdBus == idBus && v.Estado == EstadoViaje.Programado && v.FechaHoraSalida == fechaHoraSalida);
        }

        public static bool ProgramarViaje(Viaje viaje)
        {
            if (viaje == null)
            {
                return false;
            }

            if (viaje.IdBus <= 0 || viaje.IdRuta <= 0)
            {
                return false;
            }

            if (Viajes.Any(v => v.IdBus == viaje.IdBus && v.FechaHoraSalida == viaje.FechaHoraSalida))
            {
                return false;
            }

            if (viaje.Estado != EstadoViaje.Programado)
            {
                return false;
            }

            Viajes.Add(viaje);
            return true;
        }
    }
}

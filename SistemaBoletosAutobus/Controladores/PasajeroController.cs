using System;
using System.Collections.Generic;
using System.Linq;
using SistemaBoletosAutobus.Entidades;
using SistemaBoletosAutobus.Utilidades;

namespace SistemaBoletosAutobus.Controladores
{
    public static class PasajeroController
    {
        public static List<Pasajero> Pasajeros = new List<Pasajero>();

        static PasajeroController()
        {
            InicializarDatos();
        }

        public static void Reset()
        {
            InicializarDatos();
        }

        public static void InicializarDatos()
        {
            Pasajeros.Clear();
        }

        public static Pasajero RegistrarPasajero(Pasajero pasajero)
        {
            if (pasajero == null)
            {
                return null;
            }

            if (!ValidacionUtil.EsCedulaEcuatoriana(pasajero.Cedula))
            {
                return null;
            }

            if (Pasajeros.Any(p => p.Cedula == pasajero.Cedula))
            {
                return null;
            }

            Pasajeros.Add(pasajero);
            return pasajero;
        }

        public static Pasajero BuscarPorCedula(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
            {
                return null;
            }

            return Pasajeros.FirstOrDefault(p => p.Cedula == cedula.Trim());
        }

        public static bool ActualizarPasajero(int idPasajero, string nombres, string apellidos, DateTime fechaNacimiento, bool tieneDiscapacidad)
        {
            var pasajero = Pasajeros.FirstOrDefault(p => p.IdPasajero == idPasajero);
            if (pasajero == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(nombres))
            {
                pasajero.Nombres = nombres.Trim();
            }

            if (!string.IsNullOrWhiteSpace(apellidos))
            {
                pasajero.Apellidos = apellidos.Trim();
            }

            pasajero.FechaNacimiento = fechaNacimiento;
            pasajero.TieneDiscapacidad = tieneDiscapacidad;
            return true;
        }

        public static bool EliminarPasajero(int idPasajero)
        {
            var pasajero = Pasajeros.FirstOrDefault(p => p.IdPasajero == idPasajero);
            if (pasajero == null)
            {
                return false;
            }

            Pasajeros.Remove(pasajero);
            return true;
        }

        public static int ObtenerEdadEnFecha(DateTime fechaNacimiento, DateTime fechaReferencia)
        {
            var edad = fechaReferencia.Year - fechaNacimiento.Year;
            if (fechaReferencia.Month < fechaNacimiento.Month || (fechaReferencia.Month == fechaNacimiento.Month && fechaReferencia.Day < fechaNacimiento.Day))
            {
                edad--;
            }

            return edad;
        }
    }
}

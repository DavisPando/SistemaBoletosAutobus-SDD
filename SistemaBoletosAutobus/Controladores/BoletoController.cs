using System;
using System.Collections.Generic;
using System.Linq;
using SistemaBoletosAutobus.Entidades;

namespace SistemaBoletosAutobus.Controladores
{
    public static class BoletoController
    {
        public static List<Boleto> Boletos = new List<Boleto>();

        static BoletoController()
        {
            InicializarDatos();
        }

        public static void Reset()
        {
            InicializarDatos();
        }

        public static void InicializarDatos()
        {
            Boletos.Clear();
        }

        public static decimal CalcularPrecio(Pasajero pasajero, decimal precioBase)
        {
            if (pasajero == null)
            {
                return precioBase;
            }

            var edad = PasajeroController.ObtenerEdadEnFecha(pasajero.FechaNacimiento, DateTime.Today);
            var esElegible = edad >= 65 || pasajero.TieneDiscapacidad;
            if (esElegible)
            {
                return precioBase * 0.5m;
            }

            return precioBase;
        }

        public static bool TieneAsientoDisponible(int idViaje, int numeroAsiento)
        {
            return !Boletos.Any(b => b.IdViaje == idViaje && b.NumeroAsiento == numeroAsiento && b.Estado == EstadoBoleto.Vendido);
        }

        public static bool VenderBoleto(Boleto boleto)
        {
            if (boleto == null)
            {
                return false;
            }

            var viaje = ViajeController.Viajes.FirstOrDefault(v => v.IdViaje == boleto.IdViaje);
            if (viaje == null)
            {
                return false;
            }

            var pasajero = PasajeroController.Pasajeros.FirstOrDefault(p => p.IdPasajero == boleto.IdPasajero);
            if (pasajero == null)
            {
                return false;
            }

            var cajero = UsuarioController.Usuarios.FirstOrDefault(u => u.IdUsuario == boleto.IdCajero && u.Activo && u.Rol == RolUsuario.Cajero);
            if (cajero == null)
            {
                return false;
            }

            var bus = BusController.Buses.FirstOrDefault(b => b.IdBus == viaje.IdBus);
            if (bus == null)
            {
                return false;
            }

            if (boleto.NumeroAsiento <= 0 || boleto.NumeroAsiento > bus.Capacidad)
            {
                return false;
            }

            if (!TieneAsientoDisponible(boleto.IdViaje, boleto.NumeroAsiento))
            {
                return false;
            }

            if (viaje.Estado != EstadoViaje.Programado || viaje.FechaHoraSalida <= DateTime.Now)
            {
                return false;
            }

            boleto.PrecioPagado = CalcularPrecio(pasajero, RutaController.Rutas.FirstOrDefault(r => r.IdRuta == viaje.IdRuta)?.PrecioBase ?? 0m);
            if (boleto.PrecioPagado <= 0m)
            {
                return false;
            }

            boleto.Estado = EstadoBoleto.Vendido;
            boleto.FechaHoraVenta = DateTime.Now;
            Boletos.Add(boleto);
            return true;
        }

        public static List<Boleto> ObtenerBoletosPorCajeroYFecha(int idCajero, DateTime fecha)
        {
            return Boletos
                .Where(b => b.IdCajero == idCajero && b.Estado == EstadoBoleto.Vendido && b.FechaHoraVenta.Date == fecha.Date)
                .ToList();
        }

        public static bool AnularBoleto(int idBoleto, string motivo)
        {
            var boleto = Boletos.FirstOrDefault(b => b.IdBoleto == idBoleto && b.Estado == EstadoBoleto.Vendido);
            if (boleto == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(motivo))
            {
                return false;
            }

            boleto.Estado = EstadoBoleto.Anulado;
            boleto.MotivoAnulacion = motivo.Trim();
            return true;
        }
    }
}

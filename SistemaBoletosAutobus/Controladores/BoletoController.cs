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

            if (!TieneAsientoDisponible(boleto.IdViaje, boleto.NumeroAsiento))
            {
                return false;
            }

            boleto.Estado = EstadoBoleto.Vendido;
            boleto.FechaHoraVenta = DateTime.Now;
            Boletos.Add(boleto);
            return true;
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

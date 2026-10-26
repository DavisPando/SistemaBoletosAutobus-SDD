using System;
using System.Collections.Generic;
using System.Linq;
using SistemaBoletosAutobus.Entidades;

namespace SistemaBoletosAutobus.Controladores
{
    public static class CajaController
    {
        public static void Reset()
        {
            BoletoController.Reset();
        }

        public static List<Boleto> ObtenerVentasDelDiaPorCajero(int idCajero, DateTime fecha)
        {
            return BoletoController.Boletos
                .Where(b => b.IdCajero == idCajero && b.Estado == EstadoBoleto.Vendido && b.FechaHoraVenta.Date == fecha.Date)
                .ToList();
        }

        public static decimal ObtenerTotalCajaPorCajero(int idCajero, DateTime fecha)
        {
            return ObtenerVentasDelDiaPorCajero(idCajero, fecha).Sum(b => b.PrecioPagado);
        }

        public static decimal ObtenerTotalCajaActualPorCajero(int idCajero)
        {
            return ObtenerTotalCajaPorCajero(idCajero, DateTime.Today);
        }
    }
}

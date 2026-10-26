using System;
using System.Collections.Generic;
using System.Linq;
using SistemaBoletosAutobus.Entidades;

namespace SistemaBoletosAutobus.Controladores
{
    public static class ReporteController
    {
        public static decimal ObtenerTotalCajaPorCajero(int idCajero, DateTime fecha)
        {
            return BoletoController.Boletos
                .Where(b => b.IdCajero == idCajero && b.Estado == EstadoBoleto.Vendido && b.FechaHoraVenta.Date == fecha.Date)
                .Sum(b => b.PrecioPagado);
        }

        public static List<ReporteRuta> ObtenerReportePorRutaYFecha(DateTime fecha)
        {
            var viajes = ViajeController.Viajes
                .Where(v => v.FechaHoraSalida.Date == fecha.Date)
                .ToList();

            var resultados = new List<ReporteRuta>();
            foreach (var viaje in viajes)
            {
                var ruta = RutaController.Rutas.FirstOrDefault(r => r.IdRuta == viaje.IdRuta);
                if (ruta == null)
                {
                    continue;
                }

                var total = BoletoController.Boletos
                    .Where(b => b.IdViaje == viaje.IdViaje && b.Estado == EstadoBoleto.Vendido)
                    .Sum(b => b.PrecioPagado);

                resultados.Add(new ReporteRuta(ruta.CiudadOrigen, ruta.CiudadDestino, total));
            }

            return resultados;
        }
    }

    public class ReporteRuta
    {
        public ReporteRuta(string origen, string destino, decimal total)
        {
            Origen = origen;
            Destino = destino;
            Total = total;
        }

        public string Origen { get; set; }

        public string Destino { get; set; }

        public decimal Total { get; set; }
    }
}

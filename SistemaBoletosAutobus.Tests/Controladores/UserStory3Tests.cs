using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaBoletosAutobus.Controladores;
using SistemaBoletosAutobus.Entidades;

namespace SistemaBoletosAutobus.Tests.Controladores
{
    [TestClass]
    public class UserStory3Tests
    {
        [TestInitialize]
        public void Setup()
        {
            UsuarioController.Reset();
            PasajeroController.Reset();
            BusController.Reset();
            RutaController.Reset();
            ViajeController.Reset();
            BoletoController.Reset();
            ReporteController.Reset();
            CajaController.Reset();
        }

        [TestMethod]
        public void BoletoController_AnularBoleto_ConMotivoValido_ActualizaEstadoYLiberaAsiento()
        {
            var pasajero = new Pasajero(1, "1701234567", "Ana", "Paredes", new DateTime(1990, 5, 15), false);
            PasajeroController.RegistrarPasajero(pasajero);

            var viaje = new Viaje(1, 1, 1, DateTime.Today.AddDays(2).AddHours(8), EstadoViaje.Programado);
            ViajeController.Viajes.Add(viaje);

            var boleto = new Boleto(1, 1, 1, 2, 20m, 2, DateTime.Now, EstadoBoleto.Vendido, null);
            BoletoController.Boletos.Add(boleto);

            var resultado = BoletoController.AnularBoleto(1, "Solicita devolución");

            Assert.IsTrue(resultado);
            Assert.AreEqual(EstadoBoleto.Anulado, boleto.Estado);
            Assert.AreEqual("Solicita devolución", boleto.MotivoAnulacion);
            Assert.IsTrue(BoletoController.TieneAsientoDisponible(1, 2));
        }

        [TestMethod]
        public void BoletoController_AnularBoleto_ConMotivoVacio_NoModificaBoleto()
        {
            var pasajero = new Pasajero(1, "1701234567", "Ana", "Paredes", new DateTime(1990, 5, 15), false);
            PasajeroController.RegistrarPasajero(pasajero);

            var viaje = new Viaje(1, 1, 1, DateTime.Today.AddDays(2).AddHours(8), EstadoViaje.Programado);
            ViajeController.Viajes.Add(viaje);

            var boleto = new Boleto(1, 1, 1, 2, 20m, 2, DateTime.Now, EstadoBoleto.Vendido, null);
            BoletoController.Boletos.Add(boleto);

            var resultado = BoletoController.AnularBoleto(1, "   ");

            Assert.IsFalse(resultado);
            Assert.AreEqual(EstadoBoleto.Vendido, boleto.Estado);
            Assert.IsNull(boleto.MotivoAnulacion);
        }

        [TestMethod]
        public void CajaController_ObtenerTotalCajaPorCajero_UsaSoloVentasDelCajeroYFecha()
        {
            var fecha = DateTime.Today;
            BoletoController.Boletos.Add(new Boleto(1, 1, 1, 1, 20m, 2, fecha.AddHours(8), EstadoBoleto.Vendido, null));
            BoletoController.Boletos.Add(new Boleto(2, 1, 1, 2, 15m, 2, fecha.AddHours(9), EstadoBoleto.Vendido, null));
            BoletoController.Boletos.Add(new Boleto(3, 1, 1, 3, 12m, 1, fecha.AddHours(10), EstadoBoleto.Vendido, null));

            var total = CajaController.ObtenerTotalCajaPorCajero(2, fecha);

            Assert.AreEqual(35m, total);
        }

        [TestMethod]
        public void CajaController_ObtenerTotalCajaPorCajero_ExcluyeOtrasFechasYCajeros()
        {
            var hoy = DateTime.Today;
            var ayer = hoy.AddDays(-1);

            BoletoController.Boletos.Add(new Boleto(1, 1, 1, 1, 20m, 2, hoy.AddHours(8), EstadoBoleto.Vendido, null));
            BoletoController.Boletos.Add(new Boleto(2, 1, 1, 2, 10m, 2, ayer.AddHours(8), EstadoBoleto.Vendido, null));
            BoletoController.Boletos.Add(new Boleto(3, 1, 1, 3, 50m, 1, hoy.AddHours(9), EstadoBoleto.Vendido, null));

            var total = CajaController.ObtenerTotalCajaPorCajero(2, hoy);

            Assert.AreEqual(20m, total);
        }
    }
}

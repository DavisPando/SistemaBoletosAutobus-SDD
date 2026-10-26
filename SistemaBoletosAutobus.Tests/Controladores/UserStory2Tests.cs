using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaBoletosAutobus.Controladores;
using SistemaBoletosAutobus.Entidades;
using SistemaBoletosAutobus.Utilidades;

namespace SistemaBoletosAutobus.Tests.Controladores
{
    [TestClass]
    public class UserStory2Tests
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
        }

        [TestMethod]
        public void PasajeroController_RegistrarPasajero_CedulaValida_Acepta()
        {
            var pasajero = new Pasajero(10, "1701234567", "Ana", "Paredes", new DateTime(1990, 5, 15), false);

            var resultado = PasajeroController.RegistrarPasajero(pasajero);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(1, PasajeroController.Pasajeros.Count);
        }

        [TestMethod]
        public void PasajeroController_RegistrarPasajero_CedulaDuplicada_Rechaza()
        {
            var pasajero = new Pasajero(10, "1701234567", "Ana", "Paredes", new DateTime(1990, 5, 15), false);
            PasajeroController.RegistrarPasajero(pasajero);

            var duplicado = new Pasajero(11, "1701234567", "Luis", "Paredes", new DateTime(1985, 6, 20), false);
            var resultado = PasajeroController.RegistrarPasajero(duplicado);

            Assert.IsNull(resultado);
        }

        [TestMethod]
        public void PasajeroController_BuscarPorCedula_RetornaPasajeroCorrecto()
        {
            var pasajero = new Pasajero(10, "1701234567", "Ana", "Paredes", new DateTime(1990, 5, 15), false);
            PasajeroController.RegistrarPasajero(pasajero);

            var encontrado = PasajeroController.BuscarPorCedula("1701234567");

            Assert.IsNotNull(encontrado);
            Assert.AreEqual("Ana", encontrado.Nombres);
        }

        [TestMethod]
        public void PasajeroController_ActualizarPasajero_ModificaDatos()
        {
            var pasajero = new Pasajero(10, "1701234567", "Ana", "Paredes", new DateTime(1990, 5, 15), false);
            PasajeroController.RegistrarPasajero(pasajero);

            var resultado = PasajeroController.ActualizarPasajero(10, "Ana Maria", "Paredes", new DateTime(1988, 8, 12), true);

            Assert.IsTrue(resultado);
            Assert.AreEqual("Ana Maria", PasajeroController.Pasajeros[0].Nombres);
            Assert.IsTrue(PasajeroController.Pasajeros[0].TieneDiscapacidad);
        }

        [TestMethod]
        public void BoletoController_CalcularPrecio_PasajeroMayorDe65_AplicaDescuento50()
        {
            var pasajero = new Pasajero(1, "1701234567", "Ana", "Paredes", new DateTime(1957, 5, 15), false);
            var precio = BoletoController.CalcularPrecio(pasajero, 20m);

            Assert.AreEqual(10m, precio);
        }

        [TestMethod]
        public void BoletoController_CalcularPrecio_PasajeroDiscapacitado_AplicaDescuento50()
        {
            var pasajero = new Pasajero(1, "1701234567", "Ana", "Paredes", new DateTime(1990, 5, 15), true);
            var precio = BoletoController.CalcularPrecio(pasajero, 20m);

            Assert.AreEqual(10m, precio);
        }

        [TestMethod]
        public void BoletoController_CalcularPrecio_PasajeroSinDescuento_NoAplicaDescuento()
        {
            var pasajero = new Pasajero(1, "1701234567", "Ana", "Paredes", new DateTime(1990, 5, 15), false);
            var precio = BoletoController.CalcularPrecio(pasajero, 20m);

            Assert.AreEqual(20m, precio);
        }

        [TestMethod]
        public void BoletoController_VenderBoleto_AsientoDuplicado_RechazaVenta()
        {
            var pasajero = new Pasajero(1, "1701234567", "Ana", "Paredes", new DateTime(1990, 5, 15), false);
            PasajeroController.RegistrarPasajero(pasajero);
            var viaje = new Viaje(1, 1, 1, DateTime.Today.AddDays(3).AddHours(8), EstadoViaje.Programado);
            ViajeController.Viajes.Add(viaje);

            var boleto1 = new Boleto(1, 1, 1, 1, 20m, 2, DateTime.Now, EstadoBoleto.Vendido, null);
            BoletoController.Boletos.Add(boleto1);

            var boleto2 = new Boleto(2, 1, 1, 1, 20m, 2, DateTime.Now, EstadoBoleto.Vendido, null);
            var resultado = BoletoController.VenderBoleto(boleto2);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void TicketPdf_GenerarPdf_ArchivoCreadoConCabeceraPdf()
        {
            var ruta = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".pdf");
            var boleto = new Boleto(99, 1, 1, 4, 10m, 2, DateTime.Now, EstadoBoleto.Vendido, null);

            var resultado = TicketPdf.GenerarPdf(boleto, ruta);

            Assert.IsTrue(resultado);
            Assert.IsTrue(File.Exists(ruta));
            Assert.IsTrue(File.ReadAllText(ruta).StartsWith("%PDF-"));

            File.Delete(ruta);
        }
    }
}

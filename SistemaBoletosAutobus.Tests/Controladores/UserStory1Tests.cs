using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaBoletosAutobus.Controladores;
using SistemaBoletosAutobus.Entidades;

namespace SistemaBoletosAutobus.Tests.Controladores
{
    [TestClass]
    public class UserStory1Tests
    {
        [TestInitialize]
        public void Setup()
        {
            UsuarioController.Reset();
            BusController.Reset();
            RutaController.Reset();
            ViajeController.Reset();
            BoletoController.Reset();
            ReporteController.Reset();
        }

        [TestMethod]
        public void UsuarioController_Autenticar_UsuarioActivoConClaveCorrecta_RetornaUsuario()
        {
            var usuario = UsuarioController.Autenticar("admin", "admin123");

            Assert.IsNotNull(usuario);
            Assert.AreEqual(RolUsuario.Administrador, usuario.Rol);
            Assert.IsTrue(usuario.Activo);
        }

        [TestMethod]
        public void UsuarioController_Autenticar_UsuarioInactivo_NoAutentica()
        {
            UsuarioController.CambiarEstadoUsuario(1, false);

            var usuario = UsuarioController.Autenticar("admin", "admin123");

            Assert.IsNull(usuario);
        }

        [TestMethod]
        public void BusController_RegistrarBus_PlacaYCapacidadValidas_Acepta()
        {
            var bus = new Bus(10, "010", "XYZ-1234", 35);

            var resultado = BusController.RegistrarBus(bus);

            Assert.IsTrue(resultado);
            Assert.AreEqual(3, BusController.Buses.Count);
        }

        [TestMethod]
        public void BusController_RegistrarBus_PlacaDuplicada_ORechaza()
        {
            var bus = new Bus(10, "010", "ABC-1234", 25);

            var resultado = BusController.RegistrarBus(bus);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void RutaController_RegistrarRuta_OrigenDestinoDistintosYPrecioPositivo_Acepta()
        {
            var ruta = new Ruta(10, "Ambato", "Riobamba", 25.50m, TimeSpan.FromHours(3));

            var resultado = RutaController.RegistrarRuta(ruta);

            Assert.IsTrue(resultado);
            Assert.AreEqual(3, RutaController.Rutas.Count);
        }

        [TestMethod]
        public void RutaController_RegistrarRuta_OrigenIgualDestino_ORechaza()
        {
            var ruta = new Ruta(10, "Quito", "Quito", 15m, TimeSpan.FromHours(2));

            var resultado = RutaController.RegistrarRuta(ruta);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void ViajeController_ProgramarViaje_MismoBusYFechaHora_SeRechaza()
        {
            var viaje = new Viaje(10, 1, 1, DateTime.Today.AddDays(2).AddHours(8), EstadoViaje.Programado);

            var resultado = ViajeController.ProgramarViaje(viaje);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void ReporteController_ObtenerReportePorRutaYFecha_AgrupaVentaPorRutaYFecha()
        {
            var fecha = DateTime.Today.AddDays(2);
            BoletoController.Boletos.Add(new Boleto(1, 1, 1, 1, 20m, 2, fecha.AddHours(9), EstadoBoleto.Vendido, null));
            BoletoController.Boletos.Add(new Boleto(2, 1, 1, 2, 15m, 2, fecha.AddHours(10), EstadoBoleto.Vendido, null));

            var resultado = ReporteController.ObtenerReportePorRutaYFecha(fecha);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(1, resultado.Count);
            Assert.AreEqual("Quito", resultado[0].Origen);
            Assert.AreEqual("Guayaquil", resultado[0].Destino);
            Assert.AreEqual(35m, resultado[0].Total);
        }
    }
}

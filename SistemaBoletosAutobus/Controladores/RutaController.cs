using System.Collections.Generic;
using System.Linq;
using SistemaBoletosAutobus.Entidades;

namespace SistemaBoletosAutobus.Controladores
{
    public static class RutaController
    {
        public static List<Ruta> Rutas = new List<Ruta>();

        static RutaController()
        {
            InicializarDatos();
        }

        public static void Reset()
        {
            InicializarDatos();
        }

        public static void InicializarDatos()
        {
            Rutas.Clear();
            Rutas.Add(new Ruta(1, "Quito", "Guayaquil", 20m, new System.TimeSpan(6, 30, 0)));
            Rutas.Add(new Ruta(2, "Cuenca", "Loja", 18m, new System.TimeSpan(4, 15, 0)));
        }

        public static bool EsRutaValida(Ruta ruta)
        {
            if (ruta == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(ruta.CiudadOrigen) || string.IsNullOrWhiteSpace(ruta.CiudadDestino))
            {
                return false;
            }

            if (ruta.CiudadOrigen.Trim().ToLowerInvariant() == ruta.CiudadDestino.Trim().ToLowerInvariant())
            {
                return false;
            }

            return ruta.PrecioBase > 0m;
        }

        public static bool RegistrarRuta(Ruta ruta)
        {
            if (!EsRutaValida(ruta))
            {
                return false;
            }

            Rutas.Add(ruta);
            return true;
        }
    }
}

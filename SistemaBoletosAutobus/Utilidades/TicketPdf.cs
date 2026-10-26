using System;
using System.IO;
using SistemaBoletosAutobus.Entidades;

namespace SistemaBoletosAutobus.Utilidades
{
    public static class TicketPdf
    {
        public static bool GenerarPdf(Boleto boleto, string rutaArchivo)
        {
            if (boleto == null || string.IsNullOrWhiteSpace(rutaArchivo))
            {
                return false;
            }

            var directorio = Path.GetDirectoryName(rutaArchivo);
            if (!string.IsNullOrEmpty(directorio) && !Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            var contenido = string.Format(
                "%PDF-1.4\n1 0 obj\n<< /Type /Catalog /Pages 2 0 R >>\nendobj\n2 0 obj\n<< /Type /Pages /Kids [3 0 R] /Count 1 >>\nendobj\n3 0 obj\n<< /Type /Page /Parent 2 0 R /MediaBox [0 0 300 220] /Contents 4 0 R /Resources << /Font << /F1 5 0 R >> >> >>\nendobj\n4 0 obj\n<< /Length {0} >>\nstream\nBT\n/F1 12 Tf\n50 180 Td\n(Boleto #{1}) Tj\n0 -18 Td\n(Ficha: {2}) Tj\n0 -18 Td\n(Pasajero: {3}) Tj\n0 -18 Td\n(Asiento: {4}) Tj\n0 -18 Td\n(Precio: {5}) Tj\nET\nendstream\nendobj\n5 0 obj\n<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>\nendobj\nxref\n0 6\n0000000000 65535 f \n0000000010 00000 n \n0000000068 00000 n \n0000000125 00000 n \n0000000348 00000 n \n0000000691 00000 n \ntrailer\n<< /Root 1 0 R /Size 6 >>\nstartxref\n{6}\n%%EOF",
                265,
                boleto.IdBoleto,
                boleto.IdViaje,
                boleto.IdPasajero,
                boleto.NumeroAsiento,
                boleto.PrecioPagado,
                0);

            File.WriteAllText(rutaArchivo, contenido);
            return File.Exists(rutaArchivo) && new FileInfo(rutaArchivo).Length > 0;
        }
    }
}

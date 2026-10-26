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

            var contenido = "%PDF-1.4\n" +
                            "1 0 obj\n<< /Type /Catalog /Pages 2 0 R >>\nendobj\n" +
                            "2 0 obj\n<< /Type /Pages /Kids [3 0 R] /Count 1 >>\nendobj\n" +
                            "3 0 obj\n<< /Type /Page /Parent 2 0 R /MediaBox [0 0 300 220] /Contents 4 0 R /Resources << /Font << /F1 5 0 R >> >> >>\nendobj\n" +
                            "4 0 obj\n<< /Length 200 >>\nstream\nBT\n/F1 12 Tf\n50 180 Td\n(Boleto #" + boleto.IdBoleto + ") Tj\n0 -18 Td\n(Ficha: " + boleto.IdViaje + ") Tj\n0 -18 Td\n(Pasajero: " + boleto.IdPasajero + ") Tj\n0 -18 Td\n(Asiento: " + boleto.NumeroAsiento + ") Tj\n0 -18 Td\n(Precio: " + boleto.PrecioPagado + ") Tj\nET\nendstream\nendobj\n" +
                            "5 0 obj\n<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>\nendobj\n" +
                            "xref\n0 6\n0000000000 65535 f \n" +
                            "0000000010 00000 n \n" +
                            "0000000068 00000 n \n" +
                            "0000000125 00000 n \n" +
                            "0000000348 00000 n \n" +
                            "0000000691 00000 n \n" +
                            "trailer\n<< /Root 1 0 R /Size 6 >>\nstartxref\n0\n%%EOF";

            File.WriteAllText(rutaArchivo, contenido);
            return File.Exists(rutaArchivo) && new FileInfo(rutaArchivo).Length > 0 && File.ReadAllText(rutaArchivo).StartsWith("%PDF-");
        }
    }
}

using System.Text.RegularExpressions;

namespace SistemaBoletosAutobus.Utilidades
{
    public static class ValidacionUtil
    {
        public static string FormatearError(string mensaje)
        {
            return string.IsNullOrWhiteSpace(mensaje)
                ? "La operación no se pudo completar."
                : mensaje.Trim();
        }

        public static bool EsCedulaEcuatoriana(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula) || cedula.Length != 10)
            {
                return false;
            }

            if (!Regex.IsMatch(cedula, "^\\d{10}$"))
            {
                return false;
            }

            int[] coeficientes = { 2, 1, 2, 1, 2, 1, 2, 1, 2 };
            int suma = 0;

            for (int i = 0; i < coeficientes.Length; i++)
            {
                int valor = int.Parse(cedula[i].ToString()) * coeficientes[i];
                if (valor >= 10)
                {
                    valor -= 9;
                }

                suma += valor;
            }

            int digitoVerificador = (10 - (suma % 10)) % 10;
            return digitoVerificador == int.Parse(cedula[9].ToString());
        }

        public static bool TieneMotivoValido(string motivo)
        {
            return !string.IsNullOrWhiteSpace(motivo);
        }
    }
}

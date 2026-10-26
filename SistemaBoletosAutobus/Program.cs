using System;
using System.Windows.Forms;
using SistemaBoletosAutobus.Formularios;

namespace SistemaBoletosAutobus
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}

using System;
using System.Windows.Forms;

namespace SistemaBoletosAutobus.Formularios
{
    public class FrmMiCaja : Form
    {
        public FrmMiCaja()
        {
            Text = "Mi Caja";
            StartPosition = FormStartPosition.CenterParent;
            Width = 460;
            Height = 260;
        }

        public FrmMiCaja(int idCajero) : this()
        {
            IdCajero = idCajero;
        }

        public int IdCajero { get; set; }
    }
}

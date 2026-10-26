using System;
using System.Windows.Forms;

namespace SistemaBoletosAutobus.Formularios
{
    public class FrmAnulacionBoleto : Form
    {
        public FrmAnulacionBoleto()
        {
            Text = "Anulación de boleto";
            StartPosition = FormStartPosition.CenterParent;
            Width = 420;
            Height = 220;
        }

        public FrmAnulacionBoleto(int idBoleto) : this()
        {
            IdBoleto = idBoleto;
        }

        public int IdBoleto { get; set; }
    }
}

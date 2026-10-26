using System.Windows.Forms;
using SistemaBoletosAutobus.Utilidades;

namespace SistemaBoletosAutobus.Formularios
{
    public class FrmBase : Form
    {
        protected void MostrarError(string mensaje)
        {
            MessageBox.Show(ValidacionUtil.FormatearError(mensaje), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

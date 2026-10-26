using System.Windows.Forms;

namespace SistemaBoletosAutobus.Formularios
{
    public partial class ViajeForm : Form
    {
        public ViajeForm()
        {
            InitializeComponent();
            Text = "Viajes";
            Width = 640;
            Height = 420;
            StartPosition = FormStartPosition.CenterScreen;
        }
    }
}

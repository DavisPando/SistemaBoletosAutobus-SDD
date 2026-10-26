using System.Drawing;
using System.Windows.Forms;

namespace SistemaBoletosAutobus.Formularios
{
    public class MainForm : Form
    {
        public MainForm()
        {
            Text = "Sistema de boletos de autobús";
            StartPosition = FormStartPosition.CenterScreen;
            Width = 860;
            Height = 620;
            BackColor = Color.White;

            var titulo = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Location = new Point(30, 30),
                Text = "Configuración base inicial"
            };

            var descripcion = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(760, 0),
                Location = new Point(30, 80),
                Text = "La solución y la estructura base ya están creadas para continuar con la capa de entidades, controladores y utilidades del proyecto."
            };

            Controls.Add(titulo);
            Controls.Add(descripcion);
        }
    }
}

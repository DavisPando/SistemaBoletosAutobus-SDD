using System;
using System.Windows.Forms;
using SistemaBoletosAutobus.Controladores;

namespace SistemaBoletosAutobus.Formularios
{
    public partial class AdminLoginForm : Form
    {
        public AdminLoginForm()
        {
            InitializeComponent();
            btnIngresar.Click += Ingresar_Click;
            AcceptButton = btnIngresar;
        }

        private void Ingresar_Click(object sender, EventArgs e)
        {
            var usuario = UsuarioController.Autenticar(txtUsuario.Text, txtClave.Text);
            if (usuario == null)
            {
                MessageBox.Show("El usuario o la clave son incorrectos, o la cuenta está desactivada.", "Error de inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtClave.Clear();
                txtClave.Focus();
                return;
            }

            Hide();
            using (var menu = new MainForm(usuario))
            {
                menu.ShowDialog(this);
            }
            txtClave.Clear();
            txtUsuario.Focus();
            Show();
        }
    }
}

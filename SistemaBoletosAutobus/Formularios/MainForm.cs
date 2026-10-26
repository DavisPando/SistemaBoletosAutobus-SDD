using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaBoletosAutobus.Controladores;
using SistemaBoletosAutobus.Entidades;

namespace SistemaBoletosAutobus.Formularios
{
    public partial class MainForm : Form
    {
        private readonly Usuario _usuario;

        public MainForm(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException("usuario");
            }

            _usuario = usuario;
            InitializeComponent();
            lblBienvenida.Text = "Sesión: " + usuario.NombreUsuario + " | Rol: " + usuario.Rol;
            AgregarBoton("Venta", Venta_Click);
            AgregarBoton("Pasajeros", Pasajeros_Click);
            AgregarBoton("Anulación", Anulacion_Click);
            AgregarBoton("Mi Caja", MiCaja_Click);

            if (usuario.Rol == RolUsuario.Administrador)
            {
                AgregarBoton("Usuarios", delegate { AbrirFormulario(new UsuarioForm()); });
                AgregarBoton("Buses", delegate { AbrirFormulario(new BusForm()); });
                AgregarBoton("Rutas", delegate { AbrirFormulario(new RutaForm()); });
                AgregarBoton("Viajes", delegate { AbrirFormulario(new ViajeForm()); });
                AgregarBoton("Reportes", delegate { AbrirFormulario(new ReporteForm()); });
            }

            AgregarBoton("Cerrar sesión", CerrarSesion_Click);
        }

        private void AgregarBoton(string texto, EventHandler accion)
        {
            var boton = new Button
            {
                Text = texto,
                Width = 220,
                Height = 52,
                Margin = new Padding(12),
                Font = new Font("Segoe UI", 10F)
            };
            boton.Click += accion;
            panelOpciones.Controls.Add(boton);
        }

        private void AbrirFormulario(Form formulario)
        {
            using (formulario)
            {
                formulario.ShowDialog(this);
            }
        }

        private void Venta_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new VentaForm(_usuario.IdUsuario));
        }

        private void Pasajeros_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new PasajeroForm());
        }

        private void Anulacion_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmAnulacionBoleto());
        }

        private void MiCaja_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmMiCaja(_usuario.IdUsuario));
        }

        private void CerrarSesion_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
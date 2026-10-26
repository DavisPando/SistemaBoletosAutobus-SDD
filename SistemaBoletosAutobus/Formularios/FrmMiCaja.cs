using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SistemaBoletosAutobus.Controladores;
using SistemaBoletosAutobus.Entidades;

namespace SistemaBoletosAutobus.Formularios
{
    public class FrmMiCaja : Form
    {
        private Label lblCajero = new Label();
        private DateTimePicker dtpFecha = new DateTimePicker();
        private Label lblTotal = new Label();
        private DataGridView dgvVentas = new DataGridView();

        public FrmMiCaja()
        {
            Text = "Mi Caja";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(640, 480);
            Font = new Font("Segoe UI", 9.5F);

            lblCajero.Location = new Point(20, 20);
            lblCajero.AutoSize = true;
            lblCajero.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            Controls.Add(lblCajero);

            Controls.Add(new Label { Text = "Fecha:", Location = new Point(20, 58), AutoSize = true });
            dtpFecha.Location = new Point(80, 55);
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Width = 140;
            Controls.Add(dtpFecha);

            Button btnConsultar = new Button { Text = "Consultar", Location = new Point(235, 53), Size = new Size(110, 28) };
            btnConsultar.Click += delegate { Consultar(); };
            Controls.Add(btnConsultar);

            lblTotal.Location = new Point(20, 95);
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            Controls.Add(lblTotal);

            dgvVentas.Location = new Point(20, 130);
            dgvVentas.Size = new Size(580, 290);
            dgvVentas.ReadOnly = true;
            dgvVentas.AllowUserToAddRows = false;
            dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Controls.Add(dgvVentas);
        }

        public FrmMiCaja(int idCajero) : this()
        {
            IdCajero = idCajero;
            Usuario usuario = UsuarioController.Usuarios.FirstOrDefault(u => u.IdUsuario == idCajero);
            lblCajero.Text = "Cajero: " + (usuario == null ? "Desconocido" : usuario.NombreUsuario);
            Consultar();
        }

        public int IdCajero { get; set; }

        private void Consultar()
        {
            DateTime fecha = dtpFecha.Value.Date;

            dgvVentas.DataSource = CajaController.ObtenerVentasDelDiaPorCajero(IdCajero, fecha)
                .Select(b => new
                {
                    Boleto = b.IdBoleto,
                    Viaje = b.IdViaje,
                    Asiento = b.NumeroAsiento,
                    Precio = b.PrecioPagado.ToString("0.00"),
                    Hora = b.FechaHoraVenta.ToString("HH:mm")
                })
                .ToList();

            decimal total = CajaController.ObtenerTotalCajaPorCajero(IdCajero, fecha);
            lblTotal.Text = "Total vendido: $" + total.ToString("0.00");
        }
    }
}

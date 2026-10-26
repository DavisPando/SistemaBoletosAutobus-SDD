using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SistemaBoletosAutobus.Controladores;
using SistemaBoletosAutobus.Entidades;

namespace SistemaBoletosAutobus.Formularios
{
    public class FrmAnulacionBoleto : Form
    {
        private TextBox txtBoleto = new TextBox();
        private Label lblDatos = new Label();
        private TextBox txtMotivo = new TextBox();
        private Boleto boletoActual = null;

        public FrmAnulacionBoleto()
        {
            Text = "Anulación de boleto";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(520, 360);
            Font = new Font("Segoe UI", 9.5F);

            Controls.Add(new Label { Text = "N° de boleto:", Location = new Point(20, 23), AutoSize = true });
            txtBoleto.Location = new Point(130, 20);
            txtBoleto.Width = 150;
            Controls.Add(txtBoleto);

            Button btnBuscar = new Button { Text = "Buscar", Location = new Point(295, 18), Size = new Size(100, 28) };
            btnBuscar.Click += Buscar_Click;
            Controls.Add(btnBuscar);

            lblDatos.Location = new Point(20, 60);
            lblDatos.Size = new Size(460, 80);
            lblDatos.Text = "Datos del boleto: (sin buscar)";
            Controls.Add(lblDatos);

            Controls.Add(new Label { Text = "Motivo de anulación:", Location = new Point(20, 150), AutoSize = true });
            txtMotivo.Location = new Point(20, 175);
            txtMotivo.Size = new Size(460, 80);
            txtMotivo.Multiline = true;
            Controls.Add(txtMotivo);

            Button btnAnular = new Button { Text = "Anular boleto", Location = new Point(330, 268), Size = new Size(150, 32) };
            btnAnular.Click += Anular_Click;
            Controls.Add(btnAnular);
        }

        public FrmAnulacionBoleto(int idBoleto) : this()
        {
            IdBoleto = idBoleto;
            txtBoleto.Text = idBoleto.ToString();
        }

        public int IdBoleto { get; set; }

        private void Buscar_Click(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(txtBoleto.Text.Trim(), out id))
            {
                MessageBox.Show("Ingrese un número de boleto válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            boletoActual = BoletoController.Boletos.FirstOrDefault(b => b.IdBoleto == id);
            if (boletoActual == null)
            {
                lblDatos.Text = "Datos del boleto: (sin buscar)";
                MessageBox.Show("No existe un boleto con ese número.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Pasajero pasajero = PasajeroController.Pasajeros.FirstOrDefault(p => p.IdPasajero == boletoActual.IdPasajero);
            string nombre = pasajero == null ? "Desconocido" : pasajero.Nombres + " " + pasajero.Apellidos;

            lblDatos.Text = "Boleto #" + boletoActual.IdBoleto +
                            "   Viaje: " + boletoActual.IdViaje +
                            "   Asiento: " + boletoActual.NumeroAsiento +
                            "\nPasajero: " + nombre +
                            "\nPrecio: $" + boletoActual.PrecioPagado.ToString("0.00") +
                            "   Estado: " + boletoActual.Estado;
        }

        private void Anular_Click(object sender, EventArgs e)
        {
            if (boletoActual == null)
            {
                MessageBox.Show("Primero busque el boleto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (boletoActual.Estado == EstadoBoleto.Anulado)
            {
                MessageBox.Show("Este boleto ya está anulado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMotivo.Text))
            {
                MessageBox.Show("El motivo de anulación es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("¿Desea anular el boleto #" + boletoActual.IdBoleto + "?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            if (!BoletoController.AnularBoleto(boletoActual.IdBoleto, txtMotivo.Text))
            {
                MessageBox.Show("No se pudo anular el boleto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Boleto anulado. El asiento quedó libre.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            boletoActual = null;
            txtBoleto.Clear();
            txtMotivo.Clear();
            lblDatos.Text = "Datos del boleto: (sin buscar)";
        }
    }
}

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SistemaBoletosAutobus.Controladores;
using SistemaBoletosAutobus.Entidades;
using SistemaBoletosAutobus.Utilidades;

namespace SistemaBoletosAutobus.Formularios
{
	public class VentaForm : Form
	{
		private ComboBox cmbViaje = new ComboBox();
		private TextBox txtCedula = new TextBox();
		private Label lblPasajero = new Label();
		private ComboBox cmbAsiento = new ComboBox();
		private Label lblPrecio = new Label();
		private DataGridView dgvBoletos = new DataGridView();
		private Pasajero pasajeroActual = null;
		private Boleto ultimoBoleto = null;
		private int idCajero;

		public VentaForm(int idCajero)
		{
			this.idCajero = idCajero;
			Text = "Punto de venta de boletos";
			StartPosition = FormStartPosition.CenterParent;
			Size = new Size(780, 560);
			Font = new Font("Segoe UI", 9.5F);

			Controls.Add(new Label { Text = "Viaje:", Location = new Point(20, 23), AutoSize = true });
			cmbViaje.Location = new Point(140, 20);
			cmbViaje.Width = 400;
			cmbViaje.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbViaje.SelectedIndexChanged += delegate { CargarAsientos(); CargarBoletos(); ActualizarPrecio(); };
			Controls.Add(cmbViaje);

			Controls.Add(new Label { Text = "Cédula pasajero:", Location = new Point(20, 63), AutoSize = true });
			txtCedula.Location = new Point(140, 60);
			txtCedula.Width = 180;
			Controls.Add(txtCedula);
			AgregarBoton("Buscar", 330, 58, Buscar_Click);

			lblPasajero.Location = new Point(140, 95);
			lblPasajero.AutoSize = true;
			lblPasajero.Text = "Pasajero: (sin seleccionar)";
			Controls.Add(lblPasajero);

			Controls.Add(new Label { Text = "Asiento libre:", Location = new Point(20, 133), AutoSize = true });
			cmbAsiento.Location = new Point(140, 130);
			cmbAsiento.Width = 100;
			cmbAsiento.DropDownStyle = ComboBoxStyle.DropDownList;
			Controls.Add(cmbAsiento);

			lblPrecio.Location = new Point(140, 170);
			lblPrecio.AutoSize = true;
			lblPrecio.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			lblPrecio.Text = "Precio a pagar: $0.00";
			Controls.Add(lblPrecio);

			AgregarBoton("Vender", 580, 20, Vender_Click);
			AgregarBoton("Imprimir ticket PDF", 580, 58, Imprimir_Click);

			dgvBoletos.Location = new Point(20, 215);
			dgvBoletos.Size = new Size(720, 280);
			dgvBoletos.ReadOnly = true;
			dgvBoletos.AllowUserToAddRows = false;
			dgvBoletos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvBoletos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			Controls.Add(dgvBoletos);

			CargarViajes();
		}

		private void AgregarBoton(string texto, int x, int y, EventHandler accion)
		{
			Button boton = new Button { Text = texto, Location = new Point(x, y), Size = new Size(160, 30) };
			boton.Click += accion;
			Controls.Add(boton);
		}

		private void CargarViajes()
		{
			var viajes = ViajeController.Viajes
				.Where(v => v.Estado == EstadoViaje.Programado && v.FechaHoraSalida > DateTime.Now)
				.Select(v => new
				{
					Id = v.IdViaje,
					Texto = "Viaje " + v.IdViaje + " | " + NombreRuta(v.IdRuta) + " | " + v.FechaHoraSalida.ToString("dd/MM/yyyy HH:mm")
				})
				.ToList();

            cmbViaje.DisplayMember = "Texto";
            cmbViaje.ValueMember = "Id";
            cmbViaje.DataSource = viajes;
            CargarAsientos();
            CargarBoletos();
        }

		private string NombreRuta(int idRuta)
		{
			Ruta ruta = RutaController.Rutas.FirstOrDefault(r => r.IdRuta == idRuta);
			return ruta == null ? "Ruta desconocida" : ruta.CiudadOrigen + " - " + ruta.CiudadDestino;
		}

		private Viaje ViajeSeleccionado()
		{
			if (cmbViaje.SelectedValue == null || !(cmbViaje.SelectedValue is int))
			{
				return null;
			}

			int id = (int)cmbViaje.SelectedValue;
			return ViajeController.Viajes.FirstOrDefault(v => v.IdViaje == id);
		}

		private void CargarAsientos()
		{
			cmbAsiento.Items.Clear();
			Viaje viaje = ViajeSeleccionado();
			if (viaje == null)
			{
				return;
			}

			Bus bus = BusController.Buses.FirstOrDefault(b => b.IdBus == viaje.IdBus);
			if (bus == null)
			{
				return;
			}

			for (int asiento = 1; asiento <= bus.Capacidad; asiento++)
			{
				if (BoletoController.TieneAsientoDisponible(viaje.IdViaje, asiento))
				{
					cmbAsiento.Items.Add(asiento);
				}
			}

			if (cmbAsiento.Items.Count > 0)
			{
				cmbAsiento.SelectedIndex = 0;
			}
		}

		private void CargarBoletos()
		{
			Viaje viaje = ViajeSeleccionado();
			if (viaje == null)
			{
				dgvBoletos.DataSource = null;
				return;
			}

			dgvBoletos.DataSource = BoletoController.Boletos
				.Where(b => b.IdViaje == viaje.IdViaje)
				.Select(b => new
				{
					Boleto = b.IdBoleto,
					Asiento = b.NumeroAsiento,
					Pasajero = b.IdPasajero,
					Precio = b.PrecioPagado.ToString("0.00"),
					Estado = b.Estado.ToString(),
					Venta = b.FechaHoraVenta.ToString("dd/MM/yyyy HH:mm")
				})
				.ToList();
		}

		private void ActualizarPrecio()
		{
			Viaje viaje = ViajeSeleccionado();
			if (viaje == null || pasajeroActual == null)
			{
				lblPrecio.Text = "Precio a pagar: $0.00";
				return;
			}

			Ruta ruta = RutaController.Rutas.FirstOrDefault(r => r.IdRuta == viaje.IdRuta);
			decimal precioBase = ruta == null ? 0m : ruta.PrecioBase;
			decimal precio = BoletoController.CalcularPrecio(pasajeroActual, precioBase);
			string descuento = precio < precioBase ? " (50% de descuento)" : "";
			lblPrecio.Text = "Precio a pagar: $" + precio.ToString("0.00") + descuento;
		}

		private void Buscar_Click(object sender, EventArgs e)
		{
			pasajeroActual = PasajeroController.BuscarPorCedula(txtCedula.Text);
			if (pasajeroActual == null)
			{
				lblPasajero.Text = "Pasajero: (sin seleccionar)";
				MessageBox.Show("No existe un pasajero con esa cédula. Regístrelo primero en Pasajeros.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				lblPasajero.Text = "Pasajero: " + pasajeroActual.Nombres + " " + pasajeroActual.Apellidos;
			}

			ActualizarPrecio();
		}

		private void Vender_Click(object sender, EventArgs e)
		{
			Viaje viaje = ViajeSeleccionado();
			if (viaje == null)
			{
				MessageBox.Show("Seleccione un viaje.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (pasajeroActual == null)
			{
				MessageBox.Show("Busque y seleccione un pasajero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (cmbAsiento.SelectedItem == null)
			{
				MessageBox.Show("No hay asientos libres en este viaje.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			int nuevoId = BoletoController.Boletos.Count == 0 ? 1 : BoletoController.Boletos.Max(b => b.IdBoleto) + 1;
			Boleto boleto = new Boleto(nuevoId, viaje.IdViaje, pasajeroActual.IdPasajero, (int)cmbAsiento.SelectedItem, 0m, idCajero, DateTime.Now, EstadoBoleto.Vendido, null);

			if (!BoletoController.VenderBoleto(boleto))
			{
				MessageBox.Show("No se pudo vender el boleto. Verifique que el usuario sea Cajero, que el viaje esté programado y que el asiento esté libre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			ultimoBoleto = boleto;
			MessageBox.Show("Boleto #" + boleto.IdBoleto + " vendido. Total: $" + boleto.PrecioPagado.ToString("0.00"), "Venta exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
			CargarAsientos();
			CargarBoletos();
		}

		private void Imprimir_Click(object sender, EventArgs e)
		{
			if (ultimoBoleto == null)
			{
				MessageBox.Show("Primero venda un boleto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			using (SaveFileDialog dialogo = new SaveFileDialog())
			{
				dialogo.Filter = "Archivo PDF (*.pdf)|*.pdf";
				dialogo.FileName = "Boleto_" + ultimoBoleto.IdBoleto + ".pdf";
				if (dialogo.ShowDialog(this) != DialogResult.OK)
				{
					return;
				}

				if (TicketPdf.GenerarPdf(ultimoBoleto, dialogo.FileName))
				{
					MessageBox.Show("Ticket generado correctamente.", "PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					MessageBox.Show("No se pudo generar el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
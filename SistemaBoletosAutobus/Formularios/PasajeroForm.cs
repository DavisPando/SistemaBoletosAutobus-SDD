using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SistemaBoletosAutobus.Controladores;
using SistemaBoletosAutobus.Entidades;

namespace SistemaBoletosAutobus.Formularios
{
	public class PasajeroForm : Form
	{
		private TextBox txtCedula = new TextBox();
		private TextBox txtNombres = new TextBox();
		private TextBox txtApellidos = new TextBox();
		private DateTimePicker dtpNacimiento = new DateTimePicker();
		private CheckBox chkDiscapacidad = new CheckBox();
		private TextBox txtBuscar = new TextBox();
		private DataGridView dgvPasajeros = new DataGridView();
		private int idSeleccionado = 0;

		public PasajeroForm()
		{
			Text = "Gestión de pasajeros";
			StartPosition = FormStartPosition.CenterParent;
			Size = new Size(760, 520);
			Font = new Font("Segoe UI", 9.5F);

			AgregarCampo("Cédula:", txtCedula, 20);
			AgregarCampo("Nombres:", txtNombres, 55);
			AgregarCampo("Apellidos:", txtApellidos, 90);
			AgregarCampo("Fecha nacimiento:", dtpNacimiento, 125);
			dtpNacimiento.Format = DateTimePickerFormat.Short;

			chkDiscapacidad.Text = "Tiene discapacidad";
			chkDiscapacidad.Location = new Point(150, 160);
			chkDiscapacidad.AutoSize = true;
			Controls.Add(chkDiscapacidad);

			AgregarBoton("Guardar", 420, 20, Guardar_Click);
			AgregarBoton("Editar", 420, 55, Editar_Click);
			AgregarBoton("Eliminar", 420, 90, Eliminar_Click);
			AgregarBoton("Limpiar", 420, 125, delegate { Limpiar(); });

			Label lblBuscar = new Label { Text = "Buscar por cédula:", Location = new Point(20, 200), AutoSize = true };
			Controls.Add(lblBuscar);
			txtBuscar.Location = new Point(150, 197);
			txtBuscar.Width = 200;
			Controls.Add(txtBuscar);
			AgregarBoton("Buscar", 360, 195, Buscar_Click);

			dgvPasajeros.Location = new Point(20, 235);
			dgvPasajeros.Size = new Size(700, 220);
			dgvPasajeros.ReadOnly = true;
			dgvPasajeros.AllowUserToAddRows = false;
			dgvPasajeros.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvPasajeros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvPasajeros.CellClick += DgvPasajeros_CellClick;
			Controls.Add(dgvPasajeros);

			CargarTabla();
		}

		private void AgregarCampo(string texto, Control control, int y)
		{
			Controls.Add(new Label { Text = texto, Location = new Point(20, y + 3), AutoSize = true });
			control.Location = new Point(150, y);
			control.Width = 240;
			Controls.Add(control);
		}

		private void AgregarBoton(string texto, int x, int y, EventHandler accion)
		{
			Button boton = new Button { Text = texto, Location = new Point(x, y), Size = new Size(110, 28) };
			boton.Click += accion;
			Controls.Add(boton);
		}

		private void CargarTabla()
		{
			dgvPasajeros.DataSource = PasajeroController.Pasajeros
				.Select(p => new
				{
					Id = p.IdPasajero,
					Cedula = p.Cedula,
					Nombres = p.Nombres,
					Apellidos = p.Apellidos,
					Nacimiento = p.FechaNacimiento.ToShortDateString(),
					Discapacidad = p.TieneDiscapacidad ? "Sí" : "No"
				})
				.ToList();
		}

		private void Guardar_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtNombres.Text) || string.IsNullOrWhiteSpace(txtApellidos.Text))
			{
				MessageBox.Show("Ingrese nombres y apellidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			int nuevoId = PasajeroController.Pasajeros.Count == 0 ? 1 : PasajeroController.Pasajeros.Max(p => p.IdPasajero) + 1;
			Pasajero pasajero = new Pasajero(nuevoId, txtCedula.Text.Trim(), txtNombres.Text.Trim(), txtApellidos.Text.Trim(), dtpNacimiento.Value.Date, chkDiscapacidad.Checked);

			if (PasajeroController.RegistrarPasajero(pasajero) == null)
			{
				MessageBox.Show("No se pudo registrar: la cédula no es válida o ya existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			MessageBox.Show("Pasajero registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
			Limpiar();
			CargarTabla();
		}

		private void Editar_Click(object sender, EventArgs e)
		{
			if (idSeleccionado == 0)
			{
				MessageBox.Show("Seleccione un pasajero de la tabla.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!PasajeroController.ActualizarPasajero(idSeleccionado, txtNombres.Text, txtApellidos.Text, dtpNacimiento.Value.Date, chkDiscapacidad.Checked))
			{
				MessageBox.Show("No se pudo actualizar el pasajero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			MessageBox.Show("Pasajero actualizado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
			Limpiar();
			CargarTabla();
		}

		private void Eliminar_Click(object sender, EventArgs e)
		{
			if (idSeleccionado == 0)
			{
				MessageBox.Show("Seleccione un pasajero de la tabla.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (MessageBox.Show("¿Desea eliminar este pasajero?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				return;
			}

			if (!PasajeroController.EliminarPasajero(idSeleccionado))
			{
				MessageBox.Show("No se pudo eliminar el pasajero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Limpiar();
			CargarTabla();
		}

		private void Buscar_Click(object sender, EventArgs e)
		{
			Pasajero pasajero = PasajeroController.BuscarPorCedula(txtBuscar.Text);
			if (pasajero == null)
			{
				MessageBox.Show("No se encontró un pasajero con esa cédula.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			MostrarPasajero(pasajero);
		}

		private void DgvPasajeros_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
			{
				return;
			}

			int id = Convert.ToInt32(dgvPasajeros.Rows[e.RowIndex].Cells["Id"].Value);
			Pasajero pasajero = PasajeroController.Pasajeros.FirstOrDefault(p => p.IdPasajero == id);
			if (pasajero != null)
			{
				MostrarPasajero(pasajero);
			}
		}

		private void MostrarPasajero(Pasajero pasajero)
		{
			idSeleccionado = pasajero.IdPasajero;
			txtCedula.Text = pasajero.Cedula;
			txtCedula.Enabled = false;
			txtNombres.Text = pasajero.Nombres;
			txtApellidos.Text = pasajero.Apellidos;
			dtpNacimiento.Value = pasajero.FechaNacimiento;
			chkDiscapacidad.Checked = pasajero.TieneDiscapacidad;
		}

		private void Limpiar()
		{
			idSeleccionado = 0;
			txtCedula.Clear();
			txtCedula.Enabled = true;
			txtNombres.Clear();
			txtApellidos.Clear();
			txtBuscar.Clear();
			dtpNacimiento.Value = DateTime.Today.AddYears(-25);
			chkDiscapacidad.Checked = false;
		}
	}
}
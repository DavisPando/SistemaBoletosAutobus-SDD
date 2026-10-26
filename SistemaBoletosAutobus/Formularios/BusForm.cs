using System;
using System.Linq;
using System.Windows.Forms;
using SistemaBoletosAutobus.Controladores;
using SistemaBoletosAutobus.Entidades;

namespace SistemaBoletosAutobus.Formularios
{
    public partial class BusForm : Form
    {
        public BusForm()
        {
            InitializeComponent();
            Text = "Buses";
            Width = 640;
            Height = 420;
            StartPosition = FormStartPosition.CenterScreen;

            btnGuardar.Click += Guardar_Click;
            btnEditar.Click += Editar_Click;
            btnEliminar.Click += Eliminar_Click;
            btnLimpiar.Click += Limpiar_Click;
            dgvBuses.SelectionChanged += Buses_SelectionChanged;

            ActualizarListado();
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            int capacidad;
            if (!ValidarFormulario(out capacidad))
            {
                return;
            }

            var nuevoId = BusController.Buses.Count == 0
                ? 1
                : BusController.Buses.Max(busExistente => busExistente.IdBus) + 1;
            var bus = new Bus(
                nuevoId,
                txtNumeroDisco.Text.Trim(),
                txtPlaca.Text.Trim().ToUpperInvariant(),
                capacidad);

            if (!BusController.RegistrarBus(bus))
            {
                MostrarError("No se pudo registrar el bus. Verifique los datos y que la placa no esté registrada.");
                return;
            }

            ActualizarListado();
            LimpiarFormulario();
            MessageBox.Show(this, "El bus se registró correctamente.", "Buses", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Editar_Click(object sender, EventArgs e)
        {
            var bus = ObtenerBusSeleccionado();
            if (bus == null)
            {
                MostrarError("Seleccione un bus para editar.");
                return;
            }

            int capacidad;
            if (!ValidarFormulario(out capacidad))
            {
                return;
            }

            if (!BusController.ActualizarBus(
                bus.IdBus,
                txtNumeroDisco.Text.Trim(),
                txtPlaca.Text.Trim().ToUpperInvariant(),
                capacidad))
            {
                MostrarError("No se pudo actualizar el bus. Verifique los datos y que la placa no esté registrada.");
                return;
            }

            ActualizarListado();
            LimpiarFormulario();
            MessageBox.Show(this, "El bus se actualizó correctamente.", "Buses", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            var bus = ObtenerBusSeleccionado();
            if (bus == null)
            {
                MostrarError("Seleccione un bus para eliminar.");
                return;
            }

            var confirmacion = MessageBox.Show(
                this,
                "¿Está seguro de que desea eliminar el bus seleccionado?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (confirmacion != DialogResult.Yes)
            {
                return;
            }

            if (!BusController.EliminarBus(bus.IdBus))
            {
                MostrarError("No se pudo eliminar el bus. Puede estar asociado a uno o más viajes.");
                return;
            }

            ActualizarListado();
            LimpiarFormulario();
            MessageBox.Show(this, "El bus se eliminó correctamente.", "Buses", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Limpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void Buses_SelectionChanged(object sender, EventArgs e)
        {
            var bus = ObtenerBusSeleccionado();
            if (bus == null)
            {
                return;
            }

            txtNumeroDisco.Text = bus.NumeroDisco;
            txtPlaca.Text = bus.Placa;
            txtCapacidad.Text = bus.Capacidad.ToString();
        }

        private bool ValidarFormulario(out int capacidad)
        {
            capacidad = 0;
            if (string.IsNullOrWhiteSpace(txtNumeroDisco.Text))
            {
                MostrarError("Ingrese el número de disco del bus.");
                txtNumeroDisco.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPlaca.Text))
            {
                MostrarError("Ingrese la placa del bus.");
                txtPlaca.Focus();
                return false;
            }

            if (!BusController.EsPlacaValida(txtPlaca.Text.Trim().ToUpperInvariant()))
            {
                MostrarError("La placa debe tener el formato ABC-1234.");
                txtPlaca.Focus();
                return false;
            }

            if (!int.TryParse(txtCapacidad.Text.Trim(), out capacidad))
            {
                MostrarError("La capacidad debe ser un número entero.");
                txtCapacidad.Focus();
                return false;
            }

            if (!BusController.EsCapacidadValida(capacidad))
            {
                MostrarError("La capacidad debe estar entre 10 y 60 pasajeros.");
                txtCapacidad.Focus();
                return false;
            }

            return true;
        }

        private Bus ObtenerBusSeleccionado()
        {
            if (dgvBuses.SelectedRows.Count == 0)
            {
                return null;
            }

            return dgvBuses.SelectedRows[0].DataBoundItem as Bus;
        }

        private void ActualizarListado()
        {
            dgvBuses.DataSource = null;
            dgvBuses.DataSource = BusController.Buses
                .OrderBy(bus => bus.IdBus)
                .ToList();
            dgvBuses.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            txtNumeroDisco.Clear();
            txtPlaca.Clear();
            txtCapacidad.Clear();
            dgvBuses.ClearSelection();
            txtNumeroDisco.Focus();
        }

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(this, mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using SistemaBoletosAutobus.Controladores;
using SistemaBoletosAutobus.Entidades;

namespace SistemaBoletosAutobus.Formularios
{
    public partial class RutaForm : Form
    {
        public RutaForm()
        {
            InitializeComponent();
            Text = "Rutas";
            Width = 640;
            Height = 420;
            StartPosition = FormStartPosition.CenterScreen;

            btnGuardar.Click += Guardar_Click;
            btnEditar.Click += Editar_Click;
            btnEliminar.Click += Eliminar_Click;
            btnLimpiar.Click += Limpiar_Click;
            dgvRutas.SelectionChanged += Rutas_SelectionChanged;

            ActualizarListado();
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            decimal precio;
            TimeSpan duracion;
            if (!ValidarFormulario(out precio, out duracion))
            {
                return;
            }

            var nuevoId = RutaController.Rutas.Count == 0
                ? 1
                : RutaController.Rutas.Max(rutaExistente => rutaExistente.IdRuta) + 1;
            var ruta = new Ruta(
                nuevoId,
                txtCiudadOrigen.Text.Trim(),
                txtCiudadDestino.Text.Trim(),
                precio,
                duracion);

            if (!RutaController.RegistrarRuta(ruta))
            {
                MostrarError("No se pudo registrar la ruta. Verifique que los datos sean válidos y que el origen y destino sean diferentes.");
                return;
            }

            ActualizarListado();
            LimpiarFormulario();
            MessageBox.Show(this, "La ruta se registró correctamente.", "Rutas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Editar_Click(object sender, EventArgs e)
        {
            var ruta = ObtenerRutaSeleccionada();
            if (ruta == null)
            {
                MostrarError("Seleccione una ruta para editar.");
                return;
            }

            decimal precio;
            TimeSpan duracion;
            if (!ValidarFormulario(out precio, out duracion))
            {
                return;
            }

            if (!RutaController.ActualizarRuta(
                ruta.IdRuta,
                txtCiudadOrigen.Text.Trim(),
                txtCiudadDestino.Text.Trim(),
                precio,
                duracion))
            {
                MostrarError("No se pudo actualizar la ruta. Verifique los datos y que el origen y destino sean diferentes.");
                return;
            }

            ActualizarListado();
            LimpiarFormulario();
            MessageBox.Show(this, "La ruta se actualizó correctamente.", "Rutas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            var ruta = ObtenerRutaSeleccionada();
            if (ruta == null)
            {
                MostrarError("Seleccione una ruta para eliminar.");
                return;
            }

            var confirmacion = MessageBox.Show(
                this,
                "¿Está seguro de que desea eliminar la ruta seleccionada?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (confirmacion != DialogResult.Yes)
            {
                return;
            }

            if (!RutaController.EliminarRuta(ruta.IdRuta))
            {
                MostrarError("No se pudo eliminar la ruta. Puede estar asociada a uno o más viajes.");
                return;
            }

            ActualizarListado();
            LimpiarFormulario();
            MessageBox.Show(this, "La ruta se eliminó correctamente.", "Rutas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Limpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void Rutas_SelectionChanged(object sender, EventArgs e)
        {
            var ruta = ObtenerRutaSeleccionada();
            if (ruta == null)
            {
                return;
            }

            txtCiudadOrigen.Text = ruta.CiudadOrigen;
            txtCiudadDestino.Text = ruta.CiudadDestino;
            txtPrecioBase.Text = ruta.PrecioBase.ToString(CultureInfo.CurrentCulture);
            txtDuracion.Text = ruta.Duracion.ToString();
        }

        private bool ValidarFormulario(out decimal precio, out TimeSpan duracion)
        {
            precio = 0m;
            duracion = TimeSpan.Zero;

            if (string.IsNullOrWhiteSpace(txtCiudadOrigen.Text))
            {
                MostrarError("Ingrese la ciudad de origen.");
                txtCiudadOrigen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCiudadDestino.Text))
            {
                MostrarError("Ingrese la ciudad de destino.");
                txtCiudadDestino.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrecioBase.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out precio) || precio <= 0m)
            {
                MostrarError("El precio base debe ser un número mayor que cero.");
                txtPrecioBase.Focus();
                return false;
            }

            if (!TimeSpan.TryParse(txtDuracion.Text.Trim(), CultureInfo.CurrentCulture, out duracion) || duracion <= TimeSpan.Zero)
            {
                MostrarError("La duración debe ser válida y mayor que cero. Use el formato horas:minutos, por ejemplo 6:30.");
                txtDuracion.Focus();
                return false;
            }

            if (string.Equals(txtCiudadOrigen.Text.Trim(), txtCiudadDestino.Text.Trim(), StringComparison.CurrentCultureIgnoreCase))
            {
                MostrarError("La ciudad de origen debe ser diferente de la ciudad de destino.");
                txtCiudadDestino.Focus();
                return false;
            }

            return true;
        }

        private Ruta ObtenerRutaSeleccionada()
        {
            if (dgvRutas.SelectedRows.Count == 0)
            {
                return null;
            }

            return dgvRutas.SelectedRows[0].DataBoundItem as Ruta;
        }

        private void ActualizarListado()
        {
            dgvRutas.DataSource = null;
            dgvRutas.DataSource = RutaController.Rutas
                .OrderBy(ruta => ruta.IdRuta)
                .ToList();
            dgvRutas.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            txtCiudadOrigen.Clear();
            txtCiudadDestino.Clear();
            txtPrecioBase.Clear();
            txtDuracion.Clear();
            dgvRutas.ClearSelection();
            txtCiudadOrigen.Focus();
        }

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(this, mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

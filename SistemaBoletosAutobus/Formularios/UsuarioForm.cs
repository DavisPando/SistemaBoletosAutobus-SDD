using System;
using System.Linq;
using System.Windows.Forms;
using SistemaBoletosAutobus.Controladores;
using SistemaBoletosAutobus.Entidades;

namespace SistemaBoletosAutobus.Formularios
{
    public partial class UsuarioForm : Form
    {
        public UsuarioForm()
        {
            InitializeComponent();

            cmbRol.DataSource = Enum.GetValues(typeof(RolUsuario));
            btnGuardar.Click += Guardar_Click;
            btnEditar.Click += Editar_Click;
            btnEliminar.Click += Eliminar_Click;
            btnLimpiar.Click += Limpiar_Click;
            dgvUsuarios.SelectionChanged += Usuarios_SelectionChanged;

            ActualizarListado();
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario(true))
            {
                return;
            }

            var nuevoId = UsuarioController.Usuarios.Count == 0
                ? 1
                : UsuarioController.Usuarios.Max(usuarioExistente => usuarioExistente.IdUsuario) + 1;
            var usuario = new Usuario(
                nuevoId,
                txtNombreUsuario.Text.Trim(),
                txtClave.Text,
                (RolUsuario)cmbRol.SelectedItem,
                chkActivo.Checked);

            if (UsuarioController.RegistrarUsuario(usuario) == null)
            {
                MostrarError("No se pudo registrar el usuario. Verifique los datos y que el nombre no esté registrado.");
                return;
            }

            ActualizarListado();
            LimpiarFormulario();
            MessageBox.Show(this, "El usuario se registró correctamente.", "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Editar_Click(object sender, EventArgs e)
        {
            var usuario = ObtenerUsuarioSeleccionado();
            if (usuario == null)
            {
                MostrarError("Seleccione un usuario para editar.");
                return;
            }

            if (!ValidarFormulario(false))
            {
                return;
            }

            if (!UsuarioController.ActualizarUsuario(
                usuario.IdUsuario,
                txtNombreUsuario.Text.Trim(),
                txtClave.Text,
                (RolUsuario)cmbRol.SelectedItem,
                chkActivo.Checked))
            {
                MostrarError("No se pudo actualizar el usuario seleccionado.");
                return;
            }

            ActualizarListado();
            LimpiarFormulario();
            MessageBox.Show(this, "El usuario se actualizó correctamente.", "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            var usuario = ObtenerUsuarioSeleccionado();
            if (usuario == null)
            {
                MostrarError("Seleccione un usuario para eliminar.");
                return;
            }

            var confirmacion = MessageBox.Show(
                this,
                "¿Está seguro de que desea eliminar el usuario seleccionado?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (confirmacion != DialogResult.Yes)
            {
                return;
            }

            if (!UsuarioController.EliminarUsuario(usuario.IdUsuario))
            {
                MostrarError("No se pudo eliminar el usuario seleccionado.");
                return;
            }

            ActualizarListado();
            LimpiarFormulario();
            MessageBox.Show(this, "El usuario se eliminó correctamente.", "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Limpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void Usuarios_SelectionChanged(object sender, EventArgs e)
        {
            var usuario = ObtenerUsuarioSeleccionado();
            if (usuario == null)
            {
                return;
            }

            txtNombreUsuario.Text = usuario.NombreUsuario;
            txtClave.Clear();
            cmbRol.SelectedItem = usuario.Rol;
            chkActivo.Checked = usuario.Activo;
        }

        private bool ValidarFormulario(bool requiereClave)
        {
            if (string.IsNullOrWhiteSpace(txtNombreUsuario.Text))
            {
                MostrarError("Ingrese el nombre de usuario.");
                txtNombreUsuario.Focus();
                return false;
            }

            if (requiereClave && string.IsNullOrWhiteSpace(txtClave.Text))
            {
                MostrarError("Ingrese la clave del usuario.");
                txtClave.Focus();
                return false;
            }

            if (cmbRol.SelectedItem == null)
            {
                MostrarError("Seleccione un rol para el usuario.");
                cmbRol.Focus();
                return false;
            }

            return true;
        }

        private Usuario ObtenerUsuarioSeleccionado()
        {
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                return null;
            }

            return dgvUsuarios.SelectedRows[0].DataBoundItem as Usuario;
        }

        private void ActualizarListado()
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = UsuarioController.Usuarios
                .OrderBy(usuario => usuario.IdUsuario)
                .ToList();
            dgvUsuarios.ClearSelection();
        }

        private void LimpiarFormulario()
        {
            txtNombreUsuario.Clear();
            txtClave.Clear();
            if (cmbRol.Items.Count > 0)
            {
                cmbRol.SelectedIndex = 0;
            }
            chkActivo.Checked = true;
            dgvUsuarios.ClearSelection();
            txtNombreUsuario.Focus();
        }

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(this, mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

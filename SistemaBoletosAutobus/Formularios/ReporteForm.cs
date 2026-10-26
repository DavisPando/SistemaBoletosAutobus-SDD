using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SistemaBoletosAutobus.Controladores;
using SistemaBoletosAutobus.Entidades;

namespace SistemaBoletosAutobus.Formularios
{
    public partial class ReporteForm : Form
    {
        public ReporteForm()
        {
            InitializeComponent();
            Text = "Reportes";
            Width = 680;
            Height = 460;
            StartPosition = FormStartPosition.CenterScreen;

            cmbRuta.DataSource = RutaController.Rutas
                .OrderBy(ruta => ruta.CiudadOrigen)
                .ThenBy(ruta => ruta.CiudadDestino)
                .ToList();
            cmbRuta.Format += Ruta_Format;
            btnGenerar.Click += Generar_Click;
        }

        private void Ruta_Format(object sender, ListControlConvertEventArgs e)
        {
            var ruta = e.ListItem as Ruta;
            if (ruta != null)
            {
                e.Value = ruta.CiudadOrigen + " - " + ruta.CiudadDestino;
            }
        }

        private void Generar_Click(object sender, EventArgs e)
        {
            var ruta = cmbRuta.SelectedItem as Ruta;
            if (ruta == null)
            {
                MostrarError("Seleccione una ruta para generar el reporte.");
                return;
            }

            var fechaDesde = dtpDesde.Value.Date;
            var fechaHasta = dtpHasta.Value.Date;
            if (fechaDesde > fechaHasta)
            {
                MostrarError("La fecha inicial no puede ser posterior a la fecha final.");
                return;
            }

            decimal totalVentas = 0m;
            for (var fecha = fechaDesde; fecha <= fechaHasta; fecha = fecha.AddDays(1))
            {
                totalVentas += ReporteController.ObtenerReportePorRutaYFecha(fecha)
                    .Where(reporte =>
                        string.Equals(reporte.Origen, ruta.CiudadOrigen, StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(reporte.Destino, ruta.CiudadDestino, StringComparison.OrdinalIgnoreCase))
                    .Sum(reporte => reporte.Total);
            }

            dgvReporte.DataSource = null;
            dgvReporte.DataSource = new List<ReporteRuta>
            {
                new ReporteRuta(ruta.CiudadOrigen, ruta.CiudadDestino, totalVentas)
            };
            dgvReporte.ClearSelection();
        }

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(this, mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

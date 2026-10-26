namespace SistemaBoletosAutobus.Formularios
{
    partial class RutaForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblCiudadOrigen;
        private System.Windows.Forms.TextBox txtCiudadOrigen;
        private System.Windows.Forms.Label lblCiudadDestino;
        private System.Windows.Forms.TextBox txtCiudadDestino;
        private System.Windows.Forms.Label lblPrecioBase;
        private System.Windows.Forms.TextBox txtPrecioBase;
        private System.Windows.Forms.Label lblDuracion;
        private System.Windows.Forms.TextBox txtDuracion;
        private System.Windows.Forms.DataGridView dgvRutas;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblCiudadOrigen = new System.Windows.Forms.Label();
            this.txtCiudadOrigen = new System.Windows.Forms.TextBox();
            this.lblCiudadDestino = new System.Windows.Forms.Label();
            this.txtCiudadDestino = new System.Windows.Forms.TextBox();
            this.lblPrecioBase = new System.Windows.Forms.Label();
            this.txtPrecioBase = new System.Windows.Forms.TextBox();
            this.lblDuracion = new System.Windows.Forms.Label();
            this.txtDuracion = new System.Windows.Forms.TextBox();
            this.dgvRutas = new System.Windows.Forms.DataGridView();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRutas)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCiudadOrigen
            // 
            this.lblCiudadOrigen.AutoSize = true;
            this.lblCiudadOrigen.Location = new System.Drawing.Point(18, 20);
            this.lblCiudadOrigen.Name = "lblCiudadOrigen";
            this.lblCiudadOrigen.Size = new System.Drawing.Size(83, 15);
            this.lblCiudadOrigen.TabIndex = 0;
            this.lblCiudadOrigen.Text = "Ciudad origen:";
            // 
            // txtCiudadOrigen
            // 
            this.txtCiudadOrigen.Location = new System.Drawing.Point(107, 17);
            this.txtCiudadOrigen.Name = "txtCiudadOrigen";
            this.txtCiudadOrigen.Size = new System.Drawing.Size(180, 23);
            this.txtCiudadOrigen.TabIndex = 1;
            // 
            // lblCiudadDestino
            // 
            this.lblCiudadDestino.AutoSize = true;
            this.lblCiudadDestino.Location = new System.Drawing.Point(315, 20);
            this.lblCiudadDestino.Name = "lblCiudadDestino";
            this.lblCiudadDestino.Size = new System.Drawing.Size(88, 15);
            this.lblCiudadDestino.TabIndex = 2;
            this.lblCiudadDestino.Text = "Ciudad destino:";
            // 
            // txtCiudadDestino
            // 
            this.txtCiudadDestino.Location = new System.Drawing.Point(409, 17);
            this.txtCiudadDestino.Name = "txtCiudadDestino";
            this.txtCiudadDestino.Size = new System.Drawing.Size(180, 23);
            this.txtCiudadDestino.TabIndex = 3;
            // 
            // lblPrecioBase
            // 
            this.lblPrecioBase.AutoSize = true;
            this.lblPrecioBase.Location = new System.Drawing.Point(18, 62);
            this.lblPrecioBase.Name = "lblPrecioBase";
            this.lblPrecioBase.Size = new System.Drawing.Size(69, 15);
            this.lblPrecioBase.TabIndex = 4;
            this.lblPrecioBase.Text = "Precio base:";
            // 
            // txtPrecioBase
            // 
            this.txtPrecioBase.Location = new System.Drawing.Point(93, 59);
            this.txtPrecioBase.Name = "txtPrecioBase";
            this.txtPrecioBase.Size = new System.Drawing.Size(120, 23);
            this.txtPrecioBase.TabIndex = 5;
            // 
            // lblDuracion
            // 
            this.lblDuracion.AutoSize = true;
            this.lblDuracion.Location = new System.Drawing.Point(245, 62);
            this.lblDuracion.Name = "lblDuracion";
            this.lblDuracion.Size = new System.Drawing.Size(57, 15);
            this.lblDuracion.TabIndex = 6;
            this.lblDuracion.Text = "Duración:";
            // 
            // txtDuracion
            // 
            this.txtDuracion.Location = new System.Drawing.Point(308, 59);
            this.txtDuracion.Name = "txtDuracion";
            this.txtDuracion.Size = new System.Drawing.Size(120, 23);
            this.txtDuracion.TabIndex = 7;
            // 
            // dgvRutas
            // 
            this.dgvRutas.AllowUserToAddRows = false;
            this.dgvRutas.AllowUserToDeleteRows = false;
            this.dgvRutas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRutas.AutoGenerateColumns = false;
            this.dgvRutas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRutas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "IdRuta", HeaderText = "ID", Name = "colIdRuta", Width = 60 },
                new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "CiudadOrigen", HeaderText = "Ciudad origen", Name = "colOrigen", Width = 170 },
                new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "CiudadDestino", HeaderText = "Ciudad destino", Name = "colDestino", Width = 170 },
                new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "PrecioBase", HeaderText = "Precio base", Name = "colPrecioBase", Width = 110 },
                new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Duracion", HeaderText = "Duración", Name = "colDuracion", Width = 120 }
            });
            this.dgvRutas.Location = new System.Drawing.Point(18, 145);
            this.dgvRutas.MultiSelect = false;
            this.dgvRutas.Name = "dgvRutas";
            this.dgvRutas.ReadOnly = true;
            this.dgvRutas.RowHeadersVisible = false;
            this.dgvRutas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRutas.Size = new System.Drawing.Size(720, 330);
            this.dgvRutas.TabIndex = 12;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(18, 101);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(100, 30);
            this.btnGuardar.TabIndex = 8;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(128, 101);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(100, 30);
            this.btnEditar.TabIndex = 9;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(238, 101);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(100, 30);
            this.btnEliminar.TabIndex = 10;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(348, 101);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(100, 30);
            this.btnLimpiar.TabIndex = 11;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // RutaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 493);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.dgvRutas);
            this.Controls.Add(this.txtDuracion);
            this.Controls.Add(this.lblDuracion);
            this.Controls.Add(this.txtPrecioBase);
            this.Controls.Add(this.lblPrecioBase);
            this.Controls.Add(this.txtCiudadDestino);
            this.Controls.Add(this.lblCiudadDestino);
            this.Controls.Add(this.txtCiudadOrigen);
            this.Controls.Add(this.lblCiudadOrigen);
            this.MinimumSize = new System.Drawing.Size(650, 420);
            this.Name = "RutaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gestión de rutas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRutas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

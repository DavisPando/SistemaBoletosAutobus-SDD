namespace SistemaBoletosAutobus.Formularios
{
    partial class BusForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblNumeroDisco;
        private System.Windows.Forms.TextBox txtNumeroDisco;
        private System.Windows.Forms.Label lblPlaca;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Label lblCapacidad;
        private System.Windows.Forms.TextBox txtCapacidad;
        private System.Windows.Forms.DataGridView dgvBuses;
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
            this.lblNumeroDisco = new System.Windows.Forms.Label();
            this.txtNumeroDisco = new System.Windows.Forms.TextBox();
            this.lblPlaca = new System.Windows.Forms.Label();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.lblCapacidad = new System.Windows.Forms.Label();
            this.txtCapacidad = new System.Windows.Forms.TextBox();
            this.dgvBuses = new System.Windows.Forms.DataGridView();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuses)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNumeroDisco
            // 
            this.lblNumeroDisco.AutoSize = true;
            this.lblNumeroDisco.Location = new System.Drawing.Point(18, 20);
            this.lblNumeroDisco.Name = "lblNumeroDisco";
            this.lblNumeroDisco.Size = new System.Drawing.Size(91, 15);
            this.lblNumeroDisco.TabIndex = 0;
            this.lblNumeroDisco.Text = "Número de disco:";
            // 
            // txtNumeroDisco
            // 
            this.txtNumeroDisco.Location = new System.Drawing.Point(115, 17);
            this.txtNumeroDisco.Name = "txtNumeroDisco";
            this.txtNumeroDisco.Size = new System.Drawing.Size(180, 23);
            this.txtNumeroDisco.TabIndex = 1;
            // 
            // lblPlaca
            // 
            this.lblPlaca.AutoSize = true;
            this.lblPlaca.Location = new System.Drawing.Point(320, 20);
            this.lblPlaca.Name = "lblPlaca";
            this.lblPlaca.Size = new System.Drawing.Size(38, 15);
            this.lblPlaca.TabIndex = 2;
            this.lblPlaca.Text = "Placa:";
            // 
            // txtPlaca
            // 
            this.txtPlaca.Location = new System.Drawing.Point(364, 17);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(180, 23);
            this.txtPlaca.TabIndex = 3;
            // 
            // lblCapacidad
            // 
            this.lblCapacidad.AutoSize = true;
            this.lblCapacidad.Location = new System.Drawing.Point(18, 62);
            this.lblCapacidad.Name = "lblCapacidad";
            this.lblCapacidad.Size = new System.Drawing.Size(65, 15);
            this.lblCapacidad.TabIndex = 4;
            this.lblCapacidad.Text = "Capacidad:";
            // 
            // txtCapacidad
            // 
            this.txtCapacidad.Location = new System.Drawing.Point(89, 59);
            this.txtCapacidad.Name = "txtCapacidad";
            this.txtCapacidad.Size = new System.Drawing.Size(100, 23);
            this.txtCapacidad.TabIndex = 5;
            // 
            // dgvBuses
            // 
            this.dgvBuses.AllowUserToAddRows = false;
            this.dgvBuses.AllowUserToDeleteRows = false;
            this.dgvBuses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBuses.AutoGenerateColumns = false;
            this.dgvBuses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBuses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "IdBus", HeaderText = "ID", Name = "colIdBus", Width = 70 },
                new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "NumeroDisco", HeaderText = "Número de disco", Name = "colNumeroDisco", Width = 180 },
                new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Placa", HeaderText = "Placa", Name = "colPlaca", Width = 180 },
                new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Capacidad", HeaderText = "Capacidad", Name = "colCapacidad", Width = 100 }
            });
            this.dgvBuses.Location = new System.Drawing.Point(18, 145);
            this.dgvBuses.MultiSelect = false;
            this.dgvBuses.Name = "dgvBuses";
            this.dgvBuses.ReadOnly = true;
            this.dgvBuses.RowHeadersVisible = false;
            this.dgvBuses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBuses.Size = new System.Drawing.Size(720, 330);
            this.dgvBuses.TabIndex = 10;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(18, 101);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(100, 30);
            this.btnGuardar.TabIndex = 6;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(128, 101);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(100, 30);
            this.btnEditar.TabIndex = 7;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(238, 101);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(100, 30);
            this.btnEliminar.TabIndex = 8;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(348, 101);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(100, 30);
            this.btnLimpiar.TabIndex = 9;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // BusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 493);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.dgvBuses);
            this.Controls.Add(this.txtCapacidad);
            this.Controls.Add(this.lblCapacidad);
            this.Controls.Add(this.txtPlaca);
            this.Controls.Add(this.lblPlaca);
            this.Controls.Add(this.txtNumeroDisco);
            this.Controls.Add(this.lblNumeroDisco);
            this.MinimumSize = new System.Drawing.Size(650, 420);
            this.Name = "BusForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gestión de buses";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuses)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

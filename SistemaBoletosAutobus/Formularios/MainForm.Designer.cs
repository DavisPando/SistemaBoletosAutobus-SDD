namespace SistemaBoletosAutobus.Formularios
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.FlowLayoutPanel panelOpciones;

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
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.panelOpciones = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // lblBienvenida
            // 
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBienvenida.Location = new System.Drawing.Point(20, 20);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(93, 21);
            this.lblBienvenida.TabIndex = 0;
            this.lblBienvenida.Text = "Sesión:";
            // 
            // panelOpciones
            // 
            this.panelOpciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOpciones.AutoScroll = true;
            this.panelOpciones.Location = new System.Drawing.Point(20, 60);
            this.panelOpciones.Name = "panelOpciones";
            this.panelOpciones.Padding = new System.Windows.Forms.Padding(8);
            this.panelOpciones.Size = new System.Drawing.Size(500, 360);
            this.panelOpciones.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 440);
            this.Controls.Add(this.panelOpciones);
            this.Controls.Add(this.lblBienvenida);
            this.MinimumSize = new System.Drawing.Size(420, 300);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sistema de boletos de autobús";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

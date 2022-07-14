namespace XuliaRun
{
    partial class XuliaRun
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblProceso = new System.Windows.Forms.Label();
            this.lblFichero = new System.Windows.Forms.Label();
            this.tmrEsperaFinProceso = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblProceso
            // 
            this.lblProceso.AutoSize = true;
            this.lblProceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProceso.Location = new System.Drawing.Point(98, 44);
            this.lblProceso.Name = "lblProceso";
            this.lblProceso.Size = new System.Drawing.Size(326, 42);
            this.lblProceso.TabIndex = 0;
            this.lblProceso.Text = "Actualizando Xulia";
            // 
            // lblFichero
            // 
            this.lblFichero.AutoSize = true;
            this.lblFichero.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFichero.Location = new System.Drawing.Point(98, 113);
            this.lblFichero.Name = "lblFichero";
            this.lblFichero.Size = new System.Drawing.Size(326, 42);
            this.lblFichero.TabIndex = 1;
            this.lblFichero.Text = "Actualizando Xulia";
            // 
            // tmrEsperaFinProceso
            // 
            this.tmrEsperaFinProceso.Interval = 10000;
            this.tmrEsperaFinProceso.Tick += new System.EventHandler(this.tmrEsperaFinProceso_Tick);
            // 
            // XuliaRun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 219);
            this.Controls.Add(this.lblFichero);
            this.Controls.Add(this.lblProceso);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "XuliaRun";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xulia Update";
            this.Activated += new System.EventHandler(this.XuliaRun_Activated);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProceso;
        private System.Windows.Forms.Label lblFichero;
        private System.Windows.Forms.Timer tmrEsperaFinProceso;
    }
}


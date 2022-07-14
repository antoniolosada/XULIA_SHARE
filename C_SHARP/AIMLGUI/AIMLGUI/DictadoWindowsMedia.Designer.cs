namespace XULIA
{
    partial class DictadoWindowsMedia
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tbSalida = new System.Windows.Forms.TextBox();
            this.btnCopiar = new System.Windows.Forms.Button();
            this.btnCopiarSalir = new System.Windows.Forms.Button();
            this.tmrSalida = new System.Windows.Forms.Timer(this.components);
            this.tbSalidaDirecta = new System.Windows.Forms.TextBox();
            this.tmrOkXulia = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tbSalida
            // 
            this.tbSalida.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSalida.Location = new System.Drawing.Point(13, 39);
            this.tbSalida.Multiline = true;
            this.tbSalida.Name = "tbSalida";
            this.tbSalida.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbSalida.Size = new System.Drawing.Size(833, 325);
            this.tbSalida.TabIndex = 0;
            // 
            // btnCopiar
            // 
            this.btnCopiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopiar.Location = new System.Drawing.Point(189, 385);
            this.btnCopiar.Name = "btnCopiar";
            this.btnCopiar.Size = new System.Drawing.Size(206, 36);
            this.btnCopiar.TabIndex = 1;
            this.btnCopiar.Text = "Copiar";
            this.btnCopiar.UseVisualStyleBackColor = true;
            this.btnCopiar.Click += new System.EventHandler(this.btnCopiar_Click);
            // 
            // btnCopiarSalir
            // 
            this.btnCopiarSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopiarSalir.Location = new System.Drawing.Point(494, 385);
            this.btnCopiarSalir.Name = "btnCopiarSalir";
            this.btnCopiarSalir.Size = new System.Drawing.Size(206, 36);
            this.btnCopiarSalir.TabIndex = 2;
            this.btnCopiarSalir.Text = "Copiar y Salir";
            this.btnCopiarSalir.UseVisualStyleBackColor = true;
            // 
            // tmrSalida
            // 
            this.tmrSalida.Enabled = true;
            this.tmrSalida.Tick += new System.EventHandler(this.tmrSalida_Tick);
            // 
            // tbSalidaDirecta
            // 
            this.tbSalidaDirecta.Location = new System.Drawing.Point(13, 13);
            this.tbSalidaDirecta.Name = "tbSalidaDirecta";
            this.tbSalidaDirecta.Size = new System.Drawing.Size(833, 20);
            this.tbSalidaDirecta.TabIndex = 3;
            // 
            // tmrOkXulia
            // 
            this.tmrOkXulia.Enabled = true;
            this.tmrOkXulia.Interval = 10000;
            this.tmrOkXulia.Tick += new System.EventHandler(this.tmrOkXulia_Tick);
            // 
            // DictadoWindowsMedia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 450);
            this.Controls.Add(this.tbSalidaDirecta);
            this.Controls.Add(this.btnCopiarSalir);
            this.Controls.Add(this.btnCopiar);
            this.Controls.Add(this.tbSalida);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DictadoWindowsMedia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DictadoWindowsMedia";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.DictadoWindowsMedia_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbSalida;
        private System.Windows.Forms.Button btnCopiar;
        private System.Windows.Forms.Button btnCopiarSalir;
        private System.Windows.Forms.Timer tmrSalida;
        private System.Windows.Forms.TextBox tbSalidaDirecta;
        private System.Windows.Forms.Timer tmrOkXulia;
    }
}
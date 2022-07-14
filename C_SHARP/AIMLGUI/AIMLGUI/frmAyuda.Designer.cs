namespace AIMLGUI
{
    partial class frmAyuda
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
            this.lstGramaticas = new System.Windows.Forms.ListBox();
            this.lstElementos = new System.Windows.Forms.ListBox();
            this.lstGramaticasDesactivas = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabAyuda = new System.Windows.Forms.TabPage();
            this.lstAyuda = new System.Windows.Forms.ListBox();
            this.tabInstrucciones = new System.Windows.Forms.TabPage();
            this.lstComandos = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tabAyuda.SuspendLayout();
            this.tabInstrucciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstGramaticas
            // 
            this.lstGramaticas.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstGramaticas.FormattingEnabled = true;
            this.lstGramaticas.ItemHeight = 24;
            this.lstGramaticas.Location = new System.Drawing.Point(12, 37);
            this.lstGramaticas.Name = "lstGramaticas";
            this.lstGramaticas.Size = new System.Drawing.Size(213, 340);
            this.lstGramaticas.TabIndex = 0;
            this.lstGramaticas.SelectedIndexChanged += new System.EventHandler(this.lstGramaticas_SelectedIndexChanged);
            // 
            // lstElementos
            // 
            this.lstElementos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstElementos.FormattingEnabled = true;
            this.lstElementos.ItemHeight = 24;
            this.lstElementos.Location = new System.Drawing.Point(231, 37);
            this.lstElementos.Name = "lstElementos";
            this.lstElementos.Size = new System.Drawing.Size(234, 700);
            this.lstElementos.TabIndex = 2;
            this.lstElementos.SelectedIndexChanged += new System.EventHandler(this.lstElementos_SelectedIndexChanged);
            // 
            // lstGramaticasDesactivas
            // 
            this.lstGramaticasDesactivas.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstGramaticasDesactivas.FormattingEnabled = true;
            this.lstGramaticasDesactivas.ItemHeight = 24;
            this.lstGramaticasDesactivas.Location = new System.Drawing.Point(12, 421);
            this.lstGramaticasDesactivas.Name = "lstGramaticasDesactivas";
            this.lstGramaticasDesactivas.Size = new System.Drawing.Size(213, 340);
            this.lstGramaticasDesactivas.TabIndex = 1;
            this.lstGramaticasDesactivas.SelectedIndexChanged += new System.EventHandler(this.lstGramaticasDesactivas_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Gramáticas Activas";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(12, 398);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Gramáticas Desactivas";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabAyuda);
            this.tabControl1.Controls.Add(this.tabInstrucciones);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(471, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(762, 749);
            this.tabControl1.TabIndex = 7;
            // 
            // tabAyuda
            // 
            this.tabAyuda.Controls.Add(this.lstAyuda);
            this.tabAyuda.Location = new System.Drawing.Point(4, 25);
            this.tabAyuda.Name = "tabAyuda";
            this.tabAyuda.Padding = new System.Windows.Forms.Padding(3);
            this.tabAyuda.Size = new System.Drawing.Size(754, 720);
            this.tabAyuda.TabIndex = 1;
            this.tabAyuda.Text = "Ayuda";
            this.tabAyuda.UseVisualStyleBackColor = true;
            // 
            // lstAyuda
            // 
            this.lstAyuda.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstAyuda.FormattingEnabled = true;
            this.lstAyuda.ItemHeight = 24;
            this.lstAyuda.Location = new System.Drawing.Point(7, 3);
            this.lstAyuda.Name = "lstAyuda";
            this.lstAyuda.Size = new System.Drawing.Size(741, 700);
            this.lstAyuda.TabIndex = 5;
            this.lstAyuda.TabStop = false;
            // 
            // tabInstrucciones
            // 
            this.tabInstrucciones.Controls.Add(this.lstComandos);
            this.tabInstrucciones.Location = new System.Drawing.Point(4, 25);
            this.tabInstrucciones.Name = "tabInstrucciones";
            this.tabInstrucciones.Padding = new System.Windows.Forms.Padding(3);
            this.tabInstrucciones.Size = new System.Drawing.Size(754, 720);
            this.tabInstrucciones.TabIndex = 0;
            this.tabInstrucciones.Text = "Instrucciones";
            this.tabInstrucciones.UseVisualStyleBackColor = true;
            // 
            // lstComandos
            // 
            this.lstComandos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstComandos.FormattingEnabled = true;
            this.lstComandos.ItemHeight = 24;
            this.lstComandos.Location = new System.Drawing.Point(6, 6);
            this.lstComandos.Name = "lstComandos";
            this.lstComandos.Size = new System.Drawing.Size(741, 700);
            this.lstComandos.TabIndex = 4;
            this.lstComandos.TabStop = false;
            // 
            // frmAyuda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 766);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstGramaticasDesactivas);
            this.Controls.Add(this.lstElementos);
            this.Controls.Add(this.lstGramaticas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1250, 800);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1250, 800);
            this.Name = "frmAyuda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayuda";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAyuda_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.frmAyuda_SizeChanged);
            this.tabControl1.ResumeLayout(false);
            this.tabAyuda.ResumeLayout(false);
            this.tabInstrucciones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstGramaticas;
        private System.Windows.Forms.ListBox lstElementos;
        private System.Windows.Forms.ListBox lstGramaticasDesactivas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabInstrucciones;
        private System.Windows.Forms.ListBox lstComandos;
        private System.Windows.Forms.TabPage tabAyuda;
        private System.Windows.Forms.ListBox lstAyuda;
    }
}
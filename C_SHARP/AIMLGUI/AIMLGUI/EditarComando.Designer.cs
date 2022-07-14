namespace XULIA
{
    partial class EditarComando
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
            this.tbClave = new System.Windows.Forms.TextBox();
            this.tbComandoVoz = new System.Windows.Forms.TextBox();
            this.tbPrecision = new System.Windows.Forms.TextBox();
            this.tbAyuda = new System.Windows.Forms.TextBox();
            this.lstMacro = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Grabar = new System.Windows.Forms.Button();
            this.Cancelar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cbComandos = new System.Windows.Forms.ComboBox();
            this.tbPar1 = new System.Windows.Forms.TextBox();
            this.tbPar2 = new System.Windows.Forms.TextBox();
            this.cmdAgregarComando = new System.Windows.Forms.Button();
            this.tbPar3 = new System.Windows.Forms.TextBox();
            this.lblPar1 = new System.Windows.Forms.Label();
            this.lblPar2 = new System.Windows.Forms.Label();
            this.lblPar3 = new System.Windows.Forms.Label();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.cmdSubir = new System.Windows.Forms.Button();
            this.cmdBajar = new System.Windows.Forms.Button();
            this.cmdBorrar = new System.Windows.Forms.Button();
            this.tbModificador = new System.Windows.Forms.TextBox();
            this.cmdLimpiar = new System.Windows.Forms.Button();
            this.cmdBorrarComando = new System.Windows.Forms.Button();
            this.cmdPegarCoordenadas = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbClave
            // 
            this.tbClave.Enabled = false;
            this.tbClave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbClave.Location = new System.Drawing.Point(175, 10);
            this.tbClave.Margin = new System.Windows.Forms.Padding(4);
            this.tbClave.Name = "tbClave";
            this.tbClave.Size = new System.Drawing.Size(328, 26);
            this.tbClave.TabIndex = 0;
            // 
            // tbComandoVoz
            // 
            this.tbComandoVoz.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbComandoVoz.Location = new System.Drawing.Point(175, 43);
            this.tbComandoVoz.Margin = new System.Windows.Forms.Padding(4);
            this.tbComandoVoz.Name = "tbComandoVoz";
            this.tbComandoVoz.Size = new System.Drawing.Size(538, 26);
            this.tbComandoVoz.TabIndex = 1;
            // 
            // tbPrecision
            // 
            this.tbPrecision.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPrecision.Location = new System.Drawing.Point(176, 77);
            this.tbPrecision.Margin = new System.Windows.Forms.Padding(4);
            this.tbPrecision.Name = "tbPrecision";
            this.tbPrecision.Size = new System.Drawing.Size(85, 26);
            this.tbPrecision.TabIndex = 2;
            // 
            // tbAyuda
            // 
            this.tbAyuda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAyuda.Location = new System.Drawing.Point(175, 111);
            this.tbAyuda.Margin = new System.Windows.Forms.Padding(4);
            this.tbAyuda.Name = "tbAyuda";
            this.tbAyuda.Size = new System.Drawing.Size(641, 22);
            this.tbAyuda.TabIndex = 3;
            // 
            // lstMacro
            // 
            this.lstMacro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstMacro.FormattingEnabled = true;
            this.lstMacro.ItemHeight = 16;
            this.lstMacro.Location = new System.Drawing.Point(175, 145);
            this.lstMacro.Margin = new System.Windows.Forms.Padding(4);
            this.lstMacro.Name = "lstMacro";
            this.lstMacro.Size = new System.Drawing.Size(641, 308);
            this.lstMacro.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(103, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Clave";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 43);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Comando de voz";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(78, 77);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Precisión";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(95, 115);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Ayuda";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(5, 145);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Macro de ejecución";
            // 
            // Grabar
            // 
            this.Grabar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Grabar.Location = new System.Drawing.Point(721, 3);
            this.Grabar.Margin = new System.Windows.Forms.Padding(4);
            this.Grabar.Name = "Grabar";
            this.Grabar.Size = new System.Drawing.Size(92, 46);
            this.Grabar.TabIndex = 10;
            this.Grabar.Text = "&Grabar";
            this.Grabar.UseVisualStyleBackColor = true;
            this.Grabar.Click += new System.EventHandler(this.Grabar_Click);
            // 
            // Cancelar
            // 
            this.Cancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancelar.Location = new System.Drawing.Point(721, 51);
            this.Cancelar.Margin = new System.Windows.Forms.Padding(4);
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.Size = new System.Drawing.Size(92, 26);
            this.Cancelar.TabIndex = 11;
            this.Cancelar.Text = "&Cancelar";
            this.Cancelar.UseVisualStyleBackColor = true;
            this.Cancelar.Click += new System.EventHandler(this.Cancelar_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(271, 81);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "%";
            // 
            // cbComandos
            // 
            this.cbComandos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbComandos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbComandos.FormattingEnabled = true;
            this.cbComandos.Location = new System.Drawing.Point(176, 461);
            this.cbComandos.Margin = new System.Windows.Forms.Padding(4);
            this.cbComandos.Name = "cbComandos";
            this.cbComandos.Size = new System.Drawing.Size(640, 24);
            this.cbComandos.Sorted = true;
            this.cbComandos.TabIndex = 13;
            this.cbComandos.SelectedIndexChanged += new System.EventHandler(this.cbComandos_SelectedIndexChanged);
            // 
            // tbPar1
            // 
            this.tbPar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPar1.Location = new System.Drawing.Point(451, 530);
            this.tbPar1.Margin = new System.Windows.Forms.Padding(4);
            this.tbPar1.Name = "tbPar1";
            this.tbPar1.Size = new System.Drawing.Size(365, 22);
            this.tbPar1.TabIndex = 15;
            // 
            // tbPar2
            // 
            this.tbPar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPar2.Location = new System.Drawing.Point(451, 560);
            this.tbPar2.Margin = new System.Windows.Forms.Padding(4);
            this.tbPar2.Name = "tbPar2";
            this.tbPar2.Size = new System.Drawing.Size(365, 22);
            this.tbPar2.TabIndex = 16;
            // 
            // cmdAgregarComando
            // 
            this.cmdAgregarComando.Location = new System.Drawing.Point(23, 457);
            this.cmdAgregarComando.Margin = new System.Windows.Forms.Padding(4);
            this.cmdAgregarComando.Name = "cmdAgregarComando";
            this.cmdAgregarComando.Size = new System.Drawing.Size(145, 30);
            this.cmdAgregarComando.TabIndex = 17;
            this.cmdAgregarComando.Text = "&Insertar Comando";
            this.cmdAgregarComando.UseVisualStyleBackColor = true;
            this.cmdAgregarComando.Click += new System.EventHandler(this.cmdAgregarComando_Click);
            // 
            // tbPar3
            // 
            this.tbPar3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPar3.Location = new System.Drawing.Point(451, 590);
            this.tbPar3.Margin = new System.Windows.Forms.Padding(4);
            this.tbPar3.Name = "tbPar3";
            this.tbPar3.Size = new System.Drawing.Size(365, 22);
            this.tbPar3.TabIndex = 18;
            // 
            // lblPar1
            // 
            this.lblPar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPar1.Location = new System.Drawing.Point(27, 528);
            this.lblPar1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPar1.Name = "lblPar1";
            this.lblPar1.Size = new System.Drawing.Size(416, 26);
            this.lblPar1.TabIndex = 19;
            this.lblPar1.Text = "Macro de ejecución";
            this.lblPar1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPar2
            // 
            this.lblPar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPar2.Location = new System.Drawing.Point(23, 558);
            this.lblPar2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPar2.Name = "lblPar2";
            this.lblPar2.Size = new System.Drawing.Size(420, 26);
            this.lblPar2.TabIndex = 20;
            this.lblPar2.Text = "Macro de ejecución";
            this.lblPar2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPar3
            // 
            this.lblPar3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPar3.Location = new System.Drawing.Point(23, 586);
            this.lblPar3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPar3.Name = "lblPar3";
            this.lblPar3.Size = new System.Drawing.Size(420, 26);
            this.lblPar3.TabIndex = 21;
            this.lblPar3.Text = "Macro de ejecución";
            this.lblPar3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAyuda
            // 
            this.lblAyuda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAyuda.Location = new System.Drawing.Point(172, 510);
            this.lblAyuda.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(919, 16);
            this.lblAyuda.TabIndex = 22;
            this.lblAyuda.Text = "Macro de ejecución";
            // 
            // cmdSubir
            // 
            this.cmdSubir.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdSubir.Location = new System.Drawing.Point(57, 176);
            this.cmdSubir.Margin = new System.Windows.Forms.Padding(4);
            this.cmdSubir.Name = "cmdSubir";
            this.cmdSubir.Size = new System.Drawing.Size(92, 46);
            this.cmdSubir.TabIndex = 23;
            this.cmdSubir.Text = "&Subir";
            this.cmdSubir.UseVisualStyleBackColor = true;
            this.cmdSubir.Click += new System.EventHandler(this.cmdSubir_Click);
            // 
            // cmdBajar
            // 
            this.cmdBajar.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdBajar.Location = new System.Drawing.Point(57, 230);
            this.cmdBajar.Margin = new System.Windows.Forms.Padding(4);
            this.cmdBajar.Name = "cmdBajar";
            this.cmdBajar.Size = new System.Drawing.Size(92, 46);
            this.cmdBajar.TabIndex = 24;
            this.cmdBajar.Text = "&Bajar";
            this.cmdBajar.UseVisualStyleBackColor = true;
            this.cmdBajar.Click += new System.EventHandler(this.cmdBajar_Click);
            // 
            // cmdBorrar
            // 
            this.cmdBorrar.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdBorrar.Location = new System.Drawing.Point(57, 283);
            this.cmdBorrar.Margin = new System.Windows.Forms.Padding(4);
            this.cmdBorrar.Name = "cmdBorrar";
            this.cmdBorrar.Size = new System.Drawing.Size(92, 46);
            this.cmdBorrar.TabIndex = 25;
            this.cmdBorrar.Text = "&Borrar";
            this.cmdBorrar.UseVisualStyleBackColor = true;
            this.cmdBorrar.Click += new System.EventHandler(this.cmdBorrar_Click);
            // 
            // tbModificador
            // 
            this.tbModificador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbModificador.Location = new System.Drawing.Point(175, 484);
            this.tbModificador.Margin = new System.Windows.Forms.Padding(4);
            this.tbModificador.Name = "tbModificador";
            this.tbModificador.Size = new System.Drawing.Size(641, 22);
            this.tbModificador.TabIndex = 26;
            this.tbModificador.TextChanged += new System.EventHandler(this.tbModificador_TextChanged);
            this.tbModificador.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbModificador_KeyPress);
            // 
            // cmdLimpiar
            // 
            this.cmdLimpiar.Location = new System.Drawing.Point(175, 530);
            this.cmdLimpiar.Margin = new System.Windows.Forms.Padding(4);
            this.cmdLimpiar.Name = "cmdLimpiar";
            this.cmdLimpiar.Size = new System.Drawing.Size(92, 30);
            this.cmdLimpiar.TabIndex = 27;
            this.cmdLimpiar.Text = "&Limpiar";
            this.cmdLimpiar.UseVisualStyleBackColor = true;
            this.cmdLimpiar.Click += new System.EventHandler(this.cmdLimpiar_Click);
            // 
            // cmdBorrarComando
            // 
            this.cmdBorrarComando.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmdBorrarComando.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBorrarComando.ForeColor = System.Drawing.Color.White;
            this.cmdBorrarComando.Location = new System.Drawing.Point(721, 81);
            this.cmdBorrarComando.Margin = new System.Windows.Forms.Padding(4);
            this.cmdBorrarComando.Name = "cmdBorrarComando";
            this.cmdBorrarComando.Size = new System.Drawing.Size(92, 26);
            this.cmdBorrarComando.TabIndex = 28;
            this.cmdBorrarComando.Text = "Borrar";
            this.cmdBorrarComando.UseVisualStyleBackColor = false;
            this.cmdBorrarComando.Click += new System.EventHandler(this.cmdBorrarComando_Click);
            // 
            // cmdPegarCoordenadas
            // 
            this.cmdPegarCoordenadas.Location = new System.Drawing.Point(175, 568);
            this.cmdPegarCoordenadas.Margin = new System.Windows.Forms.Padding(4);
            this.cmdPegarCoordenadas.Name = "cmdPegarCoordenadas";
            this.cmdPegarCoordenadas.Size = new System.Drawing.Size(92, 44);
            this.cmdPegarCoordenadas.TabIndex = 29;
            this.cmdPegarCoordenadas.Text = "&Pegar Coordenadas";
            this.cmdPegarCoordenadas.UseVisualStyleBackColor = true;
            this.cmdPegarCoordenadas.Click += new System.EventHandler(this.cmdPegarCoordenadas_Click);
            // 
            // EditarComando
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 621);
            this.ControlBox = false;
            this.Controls.Add(this.cmdPegarCoordenadas);
            this.Controls.Add(this.cmdBorrarComando);
            this.Controls.Add(this.cmdLimpiar);
            this.Controls.Add(this.tbModificador);
            this.Controls.Add(this.cmdBorrar);
            this.Controls.Add(this.cmdBajar);
            this.Controls.Add(this.cmdSubir);
            this.Controls.Add(this.lblAyuda);
            this.Controls.Add(this.lblPar3);
            this.Controls.Add(this.lblPar2);
            this.Controls.Add(this.lblPar1);
            this.Controls.Add(this.tbPar3);
            this.Controls.Add(this.cmdAgregarComando);
            this.Controls.Add(this.tbPar2);
            this.Controls.Add(this.tbPar1);
            this.Controls.Add(this.cbComandos);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Cancelar);
            this.Controls.Add(this.Grabar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstMacro);
            this.Controls.Add(this.tbAyuda);
            this.Controls.Add(this.tbPrecision);
            this.Controls.Add(this.tbComandoVoz);
            this.Controls.Add(this.tbClave);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditarComando";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditarComando";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditarComando_FormClosed);
            this.Load += new System.EventHandler(this.EditarComando_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbClave;
        private System.Windows.Forms.TextBox tbComandoVoz;
        private System.Windows.Forms.TextBox tbPrecision;
        private System.Windows.Forms.TextBox tbAyuda;
        private System.Windows.Forms.ListBox lstMacro;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Grabar;
        private System.Windows.Forms.Button Cancelar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbComandos;
        private System.Windows.Forms.TextBox tbPar1;
        private System.Windows.Forms.TextBox tbPar2;
        private System.Windows.Forms.Button cmdAgregarComando;
        private System.Windows.Forms.TextBox tbPar3;
        private System.Windows.Forms.Label lblPar1;
        private System.Windows.Forms.Label lblPar2;
        private System.Windows.Forms.Label lblPar3;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.Button cmdSubir;
        private System.Windows.Forms.Button cmdBajar;
        private System.Windows.Forms.Button cmdBorrar;
        private System.Windows.Forms.TextBox tbModificador;
        private System.Windows.Forms.Button cmdLimpiar;
        private System.Windows.Forms.Button cmdBorrarComando;
        private System.Windows.Forms.Button cmdPegarCoordenadas;
    }
}
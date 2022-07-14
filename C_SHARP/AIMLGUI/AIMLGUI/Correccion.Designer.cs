namespace XULIA
{
    partial class Correccion
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
            this.cmdGrabar = new System.Windows.Forms.Button();
            this.cmdCancelar = new System.Windows.Forms.Button();
            this.tbPos = new System.Windows.Forms.TextBox();
            this.tbEdit = new System.Windows.Forms.TextBox();
            this.cmdLimpiar = new System.Windows.Forms.Button();
            this.cmdGrabarPalabra = new System.Windows.Forms.Button();
            this.cmdUnaMayuscula = new System.Windows.Forms.Button();
            this.cmdTodasMayusculas = new System.Windows.Forms.Button();
            this.cmdMinusculas = new System.Windows.Forms.Button();
            this.cmdCorregirMayusculas = new System.Windows.Forms.Button();
            this.chkSinDoblesEspacios = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cmdGrabar
            // 
            this.cmdGrabar.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdGrabar.Location = new System.Drawing.Point(5, 3);
            this.cmdGrabar.Name = "cmdGrabar";
            this.cmdGrabar.Size = new System.Drawing.Size(108, 26);
            this.cmdGrabar.TabIndex = 3;
            this.cmdGrabar.Text = "grabar &Frase";
            this.cmdGrabar.UseVisualStyleBackColor = true;
            this.cmdGrabar.Click += new System.EventHandler(this.cmdGrabar_Click);
            // 
            // cmdCancelar
            // 
            this.cmdCancelar.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdCancelar.Location = new System.Drawing.Point(237, 3);
            this.cmdCancelar.Name = "cmdCancelar";
            this.cmdCancelar.Size = new System.Drawing.Size(75, 26);
            this.cmdCancelar.TabIndex = 5;
            this.cmdCancelar.Text = "&Cancelar";
            this.cmdCancelar.UseVisualStyleBackColor = true;
            this.cmdCancelar.Click += new System.EventHandler(this.cmdCancelar_Click);
            // 
            // tbPos
            // 
            this.tbPos.BackColor = System.Drawing.Color.Aqua;
            this.tbPos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPos.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tbPos.Location = new System.Drawing.Point(5, 32);
            this.tbPos.Name = "tbPos";
            this.tbPos.Size = new System.Drawing.Size(43, 26);
            this.tbPos.TabIndex = 1;
            this.tbPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbPos.TextChanged += new System.EventHandler(this.tbPos_TextChanged);
            this.tbPos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPos_KeyPress_1);
            // 
            // tbEdit
            // 
            this.tbEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbEdit.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tbEdit.Location = new System.Drawing.Point(54, 32);
            this.tbEdit.Name = "tbEdit";
            this.tbEdit.Size = new System.Drawing.Size(510, 26);
            this.tbEdit.TabIndex = 2;
            this.tbEdit.TextChanged += new System.EventHandler(this.tbEdit_TextChanged);
            this.tbEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbEdit_KeyPress);
            // 
            // cmdLimpiar
            // 
            this.cmdLimpiar.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdLimpiar.Location = new System.Drawing.Point(314, 3);
            this.cmdLimpiar.Name = "cmdLimpiar";
            this.cmdLimpiar.Size = new System.Drawing.Size(75, 26);
            this.cmdLimpiar.TabIndex = 6;
            this.cmdLimpiar.Text = "&Limpiar";
            this.cmdLimpiar.UseVisualStyleBackColor = true;
            this.cmdLimpiar.Click += new System.EventHandler(this.cmdLimpiar_Click);
            // 
            // cmdGrabarPalabra
            // 
            this.cmdGrabarPalabra.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdGrabarPalabra.Location = new System.Drawing.Point(114, 3);
            this.cmdGrabarPalabra.Name = "cmdGrabarPalabra";
            this.cmdGrabarPalabra.Size = new System.Drawing.Size(122, 26);
            this.cmdGrabarPalabra.TabIndex = 4;
            this.cmdGrabarPalabra.Text = "grabar &Palabra";
            this.cmdGrabarPalabra.UseVisualStyleBackColor = true;
            this.cmdGrabarPalabra.Click += new System.EventHandler(this.cmdGrabarPalabra_Click);
            // 
            // cmdUnaMayuscula
            // 
            this.cmdUnaMayuscula.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdUnaMayuscula.Location = new System.Drawing.Point(390, 3);
            this.cmdUnaMayuscula.Name = "cmdUnaMayuscula";
            this.cmdUnaMayuscula.Size = new System.Drawing.Size(47, 26);
            this.cmdUnaMayuscula.TabIndex = 7;
            this.cmdUnaMayuscula.Text = "&May";
            this.cmdUnaMayuscula.UseVisualStyleBackColor = true;
            this.cmdUnaMayuscula.Click += new System.EventHandler(this.cmdUnaMayuscula_Click);
            // 
            // cmdTodasMayusculas
            // 
            this.cmdTodasMayusculas.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdTodasMayusculas.Location = new System.Drawing.Point(439, 3);
            this.cmdTodasMayusculas.Name = "cmdTodasMayusculas";
            this.cmdTodasMayusculas.Size = new System.Drawing.Size(47, 26);
            this.cmdTodasMayusculas.TabIndex = 8;
            this.cmdTodasMayusculas.Text = "M&AY";
            this.cmdTodasMayusculas.UseVisualStyleBackColor = true;
            this.cmdTodasMayusculas.Click += new System.EventHandler(this.cmdTodasMayusculas_Click);
            // 
            // cmdMinusculas
            // 
            this.cmdMinusculas.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdMinusculas.Location = new System.Drawing.Point(488, 3);
            this.cmdMinusculas.Name = "cmdMinusculas";
            this.cmdMinusculas.Size = new System.Drawing.Size(47, 26);
            this.cmdMinusculas.TabIndex = 9;
            this.cmdMinusculas.Text = "m&in";
            this.cmdMinusculas.UseVisualStyleBackColor = true;
            this.cmdMinusculas.Click += new System.EventHandler(this.cmdMinusculas_Click);
            // 
            // cmdCorregirMayusculas
            // 
            this.cmdCorregirMayusculas.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdCorregirMayusculas.Location = new System.Drawing.Point(538, 3);
            this.cmdCorregirMayusculas.Name = "cmdCorregirMayusculas";
            this.cmdCorregirMayusculas.Size = new System.Drawing.Size(109, 26);
            this.cmdCorregirMayusculas.TabIndex = 10;
            this.cmdCorregirMayusculas.Text = "C&orregir May";
            this.cmdCorregirMayusculas.UseVisualStyleBackColor = true;
            this.cmdCorregirMayusculas.Click += new System.EventHandler(this.cmdCorregirMayusculas_Click);
            // 
            // chkSinDoblesEspacios
            // 
            this.chkSinDoblesEspacios.AutoSize = true;
            this.chkSinDoblesEspacios.Location = new System.Drawing.Point(653, 10);
            this.chkSinDoblesEspacios.Name = "chkSinDoblesEspacios";
            this.chkSinDoblesEspacios.Size = new System.Drawing.Size(120, 17);
            this.chkSinDoblesEspacios.TabIndex = 11;
            this.chkSinDoblesEspacios.Text = "Sin dobles espacios";
            this.chkSinDoblesEspacios.UseVisualStyleBackColor = true;
            // 
            // Correccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 131);
            this.ControlBox = false;
            this.Controls.Add(this.chkSinDoblesEspacios);
            this.Controls.Add(this.cmdCorregirMayusculas);
            this.Controls.Add(this.cmdMinusculas);
            this.Controls.Add(this.cmdTodasMayusculas);
            this.Controls.Add(this.cmdUnaMayuscula);
            this.Controls.Add(this.cmdGrabarPalabra);
            this.Controls.Add(this.cmdLimpiar);
            this.Controls.Add(this.tbPos);
            this.Controls.Add(this.tbEdit);
            this.Controls.Add(this.cmdCancelar);
            this.Controls.Add(this.cmdGrabar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Correccion";
            this.Text = "Corrección Xulia";
            this.Activated += new System.EventHandler(this.Correccion_Activated);
            this.Deactivate += new System.EventHandler(this.Correccion_Deactivate);
            this.Load += new System.EventHandler(this.Correccion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdGrabar;
        private System.Windows.Forms.Button cmdCancelar;
        private System.Windows.Forms.TextBox tbPos;
        private System.Windows.Forms.TextBox tbEdit;
        private System.Windows.Forms.Button cmdLimpiar;
        private System.Windows.Forms.Button cmdGrabarPalabra;
        private System.Windows.Forms.Button cmdUnaMayuscula;
        private System.Windows.Forms.Button cmdTodasMayusculas;
        private System.Windows.Forms.Button cmdMinusculas;
        private System.Windows.Forms.Button cmdCorregirMayusculas;
        private System.Windows.Forms.CheckBox chkSinDoblesEspacios;



    }
}
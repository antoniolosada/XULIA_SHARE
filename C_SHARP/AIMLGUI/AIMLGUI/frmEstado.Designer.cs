namespace AIMLGUI
{
    partial class frmEstado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEstado));
            this.lblSeleccion = new System.Windows.Forms.Label();
            this.lblAcento = new System.Windows.Forms.Label();
            this.lblMAY = new System.Windows.Forms.Label();
            this.lblShift = new System.Windows.Forms.Label();
            this.lblControl = new System.Windows.Forms.Label();
            this.lblALT = new System.Windows.Forms.Label();
            this.tbComando = new System.Windows.Forms.TextBox();
            this.tbClase = new System.Windows.Forms.TextBox();
            this.tbTitulo = new System.Windows.Forms.TextBox();
            this.tbNumero = new System.Windows.Forms.TextBox();
            this.tmrOcultar = new System.Windows.Forms.Timer(this.components);
            this.tbModo = new System.Windows.Forms.TextBox();
            this.pbActivo = new System.Windows.Forms.PictureBox();
            this.picEspera = new System.Windows.Forms.PictureBox();
            this.lblAviso = new System.Windows.Forms.Label();
            this.lblCodTeclaPresionado = new System.Windows.Forms.Label();
            this.tmrDesactivar = new System.Windows.Forms.Timer(this.components);
            this.tmrDesactivacionAutomatica = new System.Windows.Forms.Timer(this.components);
            this.pbarPrecision = new XULIA.MyVerticalProgessBarC();
            this.tmrResetRecVoz = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbActivo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEspera)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSeleccion
            // 
            this.lblSeleccion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSeleccion.AutoSize = true;
            this.lblSeleccion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblSeleccion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSeleccion.Location = new System.Drawing.Point(277, 25);
            this.lblSeleccion.Name = "lblSeleccion";
            this.lblSeleccion.Size = new System.Drawing.Size(16, 15);
            this.lblSeleccion.TabIndex = 121;
            this.lblSeleccion.Text = "S";
            this.lblSeleccion.Visible = false;
            // 
            // lblAcento
            // 
            this.lblAcento.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAcento.AutoSize = true;
            this.lblAcento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblAcento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAcento.Location = new System.Drawing.Point(263, 25);
            this.lblAcento.Name = "lblAcento";
            this.lblAcento.Size = new System.Drawing.Size(16, 15);
            this.lblAcento.TabIndex = 120;
            this.lblAcento.Text = "A";
            this.lblAcento.Visible = false;
            // 
            // lblMAY
            // 
            this.lblMAY.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMAY.AutoSize = true;
            this.lblMAY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblMAY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMAY.Location = new System.Drawing.Point(292, 25);
            this.lblMAY.Name = "lblMAY";
            this.lblMAY.Size = new System.Drawing.Size(18, 15);
            this.lblMAY.TabIndex = 119;
            this.lblMAY.Text = "M";
            this.lblMAY.Visible = false;
            // 
            // lblShift
            // 
            this.lblShift.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblShift.AutoSize = true;
            this.lblShift.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblShift.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblShift.Location = new System.Drawing.Point(291, 9);
            this.lblShift.Name = "lblShift";
            this.lblShift.Size = new System.Drawing.Size(16, 15);
            this.lblShift.TabIndex = 118;
            this.lblShift.Text = "S";
            this.lblShift.Visible = false;
            // 
            // lblControl
            // 
            this.lblControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblControl.AutoSize = true;
            this.lblControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblControl.Location = new System.Drawing.Point(277, 9);
            this.lblControl.Name = "lblControl";
            this.lblControl.Size = new System.Drawing.Size(16, 15);
            this.lblControl.TabIndex = 117;
            this.lblControl.Text = "C";
            this.lblControl.Visible = false;
            // 
            // lblALT
            // 
            this.lblALT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblALT.AutoSize = true;
            this.lblALT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblALT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblALT.Location = new System.Drawing.Point(262, 9);
            this.lblALT.Name = "lblALT";
            this.lblALT.Size = new System.Drawing.Size(16, 15);
            this.lblALT.TabIndex = 116;
            this.lblALT.Text = "A";
            this.lblALT.Visible = false;
            // 
            // tbComando
            // 
            this.tbComando.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbComando.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbComando.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbComando.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbComando.ForeColor = System.Drawing.Color.White;
            this.tbComando.Location = new System.Drawing.Point(72, 6);
            this.tbComando.Name = "tbComando";
            this.tbComando.Size = new System.Drawing.Size(164, 18);
            this.tbComando.TabIndex = 123;
            this.tbComando.Text = "Starting ...";
            // 
            // tbClase
            // 
            this.tbClase.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbClase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbClase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbClase.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbClase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tbClase.Location = new System.Drawing.Point(72, 24);
            this.tbClase.Name = "tbClase";
            this.tbClase.Size = new System.Drawing.Size(93, 18);
            this.tbClase.TabIndex = 124;
            this.tbClase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbTitulo
            // 
            this.tbTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbTitulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.tbTitulo.Location = new System.Drawing.Point(72, 61);
            this.tbTitulo.Name = "tbTitulo";
            this.tbTitulo.Size = new System.Drawing.Size(17, 20);
            this.tbTitulo.TabIndex = 125;
            // 
            // tbNumero
            // 
            this.tbNumero.BackColor = System.Drawing.Color.LightGray;
            this.tbNumero.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbNumero.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tbNumero.ForeColor = System.Drawing.Color.Black;
            this.tbNumero.Location = new System.Drawing.Point(314, 25);
            this.tbNumero.Name = "tbNumero";
            this.tbNumero.Size = new System.Drawing.Size(19, 19);
            this.tbNumero.TabIndex = 126;
            this.tbNumero.Text = "0";
            this.tbNumero.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tmrOcultar
            // 
            this.tmrOcultar.Interval = 2000;
            this.tmrOcultar.Tick += new System.EventHandler(this.tmrOcultar_Tick);
            // 
            // tbModo
            // 
            this.tbModo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbModo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbModo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbModo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbModo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tbModo.Location = new System.Drawing.Point(164, 24);
            this.tbModo.Name = "tbModo";
            this.tbModo.Size = new System.Drawing.Size(72, 18);
            this.tbModo.TabIndex = 127;
            this.tbModo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pbActivo
            // 
            this.pbActivo.BackgroundImage = global::XULIA.Properties.Resources.aspa1;
            this.pbActivo.Location = new System.Drawing.Point(17, 12);
            this.pbActivo.Name = "pbActivo";
            this.pbActivo.Size = new System.Drawing.Size(22, 24);
            this.pbActivo.TabIndex = 128;
            this.pbActivo.TabStop = false;
            this.pbActivo.Visible = false;
            this.pbActivo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbActivo_MouseDown);
            // 
            // picEspera
            // 
            this.picEspera.BackgroundImage = global::XULIA.Properties.Resources.reloj;
            this.picEspera.InitialImage = ((System.Drawing.Image)(resources.GetObject("picEspera.InitialImage")));
            this.picEspera.Location = new System.Drawing.Point(17, 12);
            this.picEspera.Name = "picEspera";
            this.picEspera.Size = new System.Drawing.Size(24, 23);
            this.picEspera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picEspera.TabIndex = 130;
            this.picEspera.TabStop = false;
            this.picEspera.Visible = false;
            this.picEspera.Click += new System.EventHandler(this.picEspera_Click);
            // 
            // lblAviso
            // 
            this.lblAviso.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAviso.AutoSize = true;
            this.lblAviso.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblAviso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAviso.Location = new System.Drawing.Point(244, 25);
            this.lblAviso.Name = "lblAviso";
            this.lblAviso.Size = new System.Drawing.Size(16, 15);
            this.lblAviso.TabIndex = 133;
            this.lblAviso.Text = "A";
            this.lblAviso.Visible = false;
            // 
            // lblCodTeclaPresionado
            // 
            this.lblCodTeclaPresionado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCodTeclaPresionado.AutoSize = true;
            this.lblCodTeclaPresionado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblCodTeclaPresionado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCodTeclaPresionado.Location = new System.Drawing.Point(244, 9);
            this.lblCodTeclaPresionado.Name = "lblCodTeclaPresionado";
            this.lblCodTeclaPresionado.Size = new System.Drawing.Size(16, 15);
            this.lblCodTeclaPresionado.TabIndex = 132;
            this.lblCodTeclaPresionado.Text = "P";
            this.lblCodTeclaPresionado.Visible = false;
            // 
            // tmrDesactivar
            // 
            this.tmrDesactivar.Interval = 4;
            this.tmrDesactivar.Tick += new System.EventHandler(this.tmrDesactivar_Tick);
            // 
            // tmrDesactivacionAutomatica
            // 
            this.tmrDesactivacionAutomatica.Interval = 50000;
            this.tmrDesactivacionAutomatica.Tick += new System.EventHandler(this.tmrDesactivacionAutomatica_Tick);
            // 
            // pbarPrecision
            // 
            this.pbarPrecision.BackColor = System.Drawing.Color.Maroon;
            this.pbarPrecision.Location = new System.Drawing.Point(54, 3);
            this.pbarPrecision.Name = "pbarPrecision";
            this.pbarPrecision.Size = new System.Drawing.Size(10, 42);
            this.pbarPrecision.TabIndex = 131;
            // 
            // tmrResetRecVoz
            // 
            this.tmrResetRecVoz.Interval = 10000;
            this.tmrResetRecVoz.Tick += new System.EventHandler(this.tmrResetRecVoz_Tick);
            // 
            // frmEstado
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Red;
            this.BackgroundImage = global::XULIA.Properties.Resources.fondo_estado_abajo_7_90;
            this.ClientSize = new System.Drawing.Size(389, 44);
            this.Controls.Add(this.lblAviso);
            this.Controls.Add(this.lblCodTeclaPresionado);
            this.Controls.Add(this.pbarPrecision);
            this.Controls.Add(this.picEspera);
            this.Controls.Add(this.pbActivo);
            this.Controls.Add(this.tbModo);
            this.Controls.Add(this.tbNumero);
            this.Controls.Add(this.tbTitulo);
            this.Controls.Add(this.tbClase);
            this.Controls.Add(this.tbComando);
            this.Controls.Add(this.lblSeleccion);
            this.Controls.Add(this.lblAcento);
            this.Controls.Add(this.lblMAY);
            this.Controls.Add(this.lblShift);
            this.Controls.Add(this.lblControl);
            this.Controls.Add(this.lblALT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(389, 44);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(389, 44);
            this.Name = "frmEstado";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "XULIA - Xestión Unificada do Linguaxe con Intelixencia Artificial";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(255)))), ((int)(((byte)(64)))));
            this.Activated += new System.EventHandler(this.frmEstado_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEstado_FormClosing);
            this.Load += new System.EventHandler(this.frmEstado_Load);
            this.SizeChanged += new System.EventHandler(this.frmEstado_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmEstado_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmEstado_KeyPress);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmEstado_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbActivo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEspera)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSeleccion;
        private System.Windows.Forms.Label lblAcento;
        private System.Windows.Forms.Label lblMAY;
        private System.Windows.Forms.Label lblShift;
        private System.Windows.Forms.Label lblControl;
        private System.Windows.Forms.Label lblALT;
        private System.Windows.Forms.TextBox tbComando;
        private System.Windows.Forms.TextBox tbClase;
        private System.Windows.Forms.TextBox tbTitulo;
        private System.Windows.Forms.TextBox tbNumero;
        private System.Windows.Forms.Timer tmrOcultar;
        private System.Windows.Forms.TextBox tbModo;
        private System.Windows.Forms.PictureBox pbActivo;
        private System.Windows.Forms.PictureBox picEspera;
        private XULIA.MyVerticalProgessBarC pbarPrecision;
        private System.Windows.Forms.Label lblAviso;
        private System.Windows.Forms.Label lblCodTeclaPresionado;
        private System.Windows.Forms.Timer tmrDesactivar;
        private System.Windows.Forms.Timer tmrDesactivacionAutomatica;
        private System.Windows.Forms.Timer tmrResetRecVoz;
    }
}
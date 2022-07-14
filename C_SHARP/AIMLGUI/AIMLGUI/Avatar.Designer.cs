namespace XULIA
{
    partial class Avatar
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
            this.lblContador = new System.Windows.Forms.Label();
            this.tmrAvatar = new System.Windows.Forms.Timer(this.components);
            this.tbTexto = new System.Windows.Forms.TextBox();
            this.pbPensar = new System.Windows.Forms.PictureBox();
            this.imgAvatar = new System.Windows.Forms.PictureBox();
            this.tbRec = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbPensar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // lblContador
            // 
            this.lblContador.AutoSize = true;
            this.lblContador.Location = new System.Drawing.Point(697, 440);
            this.lblContador.Name = "lblContador";
            this.lblContador.Size = new System.Drawing.Size(0, 13);
            this.lblContador.TabIndex = 3;
            // 
            // tmrAvatar
            // 
            this.tmrAvatar.Interval = 200;
            this.tmrAvatar.Tick += new System.EventHandler(this.tmrAvatar_Tick);
            // 
            // tbTexto
            // 
            this.tbTexto.Location = new System.Drawing.Point(13, 547);
            this.tbTexto.Multiline = true;
            this.tbTexto.Name = "tbTexto";
            this.tbTexto.Size = new System.Drawing.Size(759, 137);
            this.tbTexto.TabIndex = 4;
            // 
            // pbPensar
            // 
            this.pbPensar.BackColor = System.Drawing.Color.Red;
            this.pbPensar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPensar.Image = global::XULIA.Properties.Resources.usar_memoria_peque1;
            this.pbPensar.Location = new System.Drawing.Point(0, 37);
            this.pbPensar.Name = "pbPensar";
            this.pbPensar.Size = new System.Drawing.Size(173, 147);
            this.pbPensar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPensar.TabIndex = 5;
            this.pbPensar.TabStop = false;
            this.pbPensar.Visible = false;
            // 
            // imgAvatar
            // 
            this.imgAvatar.Image = global::XULIA.Properties.Resources.Xulia_boca_cerrada;
            this.imgAvatar.Location = new System.Drawing.Point(0, 0);
            this.imgAvatar.Name = "imgAvatar";
            this.imgAvatar.Size = new System.Drawing.Size(939, 659);
            this.imgAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgAvatar.TabIndex = 2;
            this.imgAvatar.TabStop = false;
            // 
            // tbRec
            // 
            this.tbRec.BackColor = System.Drawing.Color.Black;
            this.tbRec.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbRec.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbRec.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRec.ForeColor = System.Drawing.Color.White;
            this.tbRec.Location = new System.Drawing.Point(0, 0);
            this.tbRec.Name = "tbRec";
            this.tbRec.Size = new System.Drawing.Size(939, 31);
            this.tbRec.TabIndex = 6;
            this.tbRec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Avatar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(939, 660);
            this.Controls.Add(this.tbRec);
            this.Controls.Add(this.pbPensar);
            this.Controls.Add(this.imgAvatar);
            this.Controls.Add(this.lblContador);
            this.Controls.Add(this.tbTexto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Avatar";
            this.Text = "XULIA";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Avatar_FormClosing);
            this.Load += new System.EventHandler(this.Avatar_Load);
            this.SizeChanged += new System.EventHandler(this.Avatar_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pbPensar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgAvatar;
        private System.Windows.Forms.Label lblContador;
        private System.Windows.Forms.Timer tmrAvatar;
        private System.Windows.Forms.TextBox tbTexto;
        private System.Windows.Forms.PictureBox pbPensar;
        private System.Windows.Forms.TextBox tbRec;
    }
}
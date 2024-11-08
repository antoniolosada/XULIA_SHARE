namespace XULIA
{
    partial class frmGPT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGPT));
            this.tbSalidaGPT = new System.Windows.Forms.TextBox();
            this.tbGPT = new System.Windows.Forms.TextBox();
            this.cmdGPT = new System.Windows.Forms.Button();
            this.tmrOcultarFormGPT = new System.Windows.Forms.Timer(this.components);
            this.pbPensando = new System.Windows.Forms.PictureBox();
            this.cmdCerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbPensando)).BeginInit();
            this.SuspendLayout();
            // 
            // tbSalidaGPT
            // 
            this.tbSalidaGPT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSalidaGPT.Location = new System.Drawing.Point(12, 12);
            this.tbSalidaGPT.Multiline = true;
            this.tbSalidaGPT.Name = "tbSalidaGPT";
            this.tbSalidaGPT.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbSalidaGPT.Size = new System.Drawing.Size(637, 614);
            this.tbSalidaGPT.TabIndex = 0;
            // 
            // tbGPT
            // 
            this.tbGPT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbGPT.Location = new System.Drawing.Point(12, 629);
            this.tbGPT.Multiline = true;
            this.tbGPT.Name = "tbGPT";
            this.tbGPT.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbGPT.Size = new System.Drawing.Size(591, 193);
            this.tbGPT.TabIndex = 1;
            this.tbGPT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbGPT_KeyPress);
            // 
            // cmdGPT
            // 
            this.cmdGPT.Image = ((System.Drawing.Image)(resources.GetObject("cmdGPT.Image")));
            this.cmdGPT.Location = new System.Drawing.Point(605, 675);
            this.cmdGPT.Name = "cmdGPT";
            this.cmdGPT.Size = new System.Drawing.Size(54, 50);
            this.cmdGPT.TabIndex = 2;
            this.cmdGPT.UseVisualStyleBackColor = true;
            this.cmdGPT.Click += new System.EventHandler(this.cmdGPT_Click);
            // 
            // pbPensando
            // 
            this.pbPensando.BackColor = System.Drawing.Color.Transparent;
            this.pbPensando.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbPensando.Image = ((System.Drawing.Image)(resources.GetObject("pbPensando.Image")));
            this.pbPensando.Location = new System.Drawing.Point(120, 80);
            this.pbPensando.Name = "pbPensando";
            this.pbPensando.Size = new System.Drawing.Size(393, 357);
            this.pbPensando.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPensando.TabIndex = 3;
            this.pbPensando.TabStop = false;
            this.pbPensando.Visible = false;
            // 
            // cmdCerrar
            // 
            this.cmdCerrar.Image = global::XULIA.Properties.Resources.aspa;
            this.cmdCerrar.Location = new System.Drawing.Point(617, 777);
            this.cmdCerrar.Name = "cmdCerrar";
            this.cmdCerrar.Size = new System.Drawing.Size(32, 33);
            this.cmdCerrar.TabIndex = 4;
            this.cmdCerrar.UseVisualStyleBackColor = true;
            this.cmdCerrar.Click += new System.EventHandler(this.cmdCerrar_Click);
            // 
            // frmGPT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(661, 837);
            this.ControlBox = false;
            this.Controls.Add(this.cmdCerrar);
            this.Controls.Add(this.pbPensando);
            this.Controls.Add(this.cmdGPT);
            this.Controls.Add(this.tbGPT);
            this.Controls.Add(this.tbSalidaGPT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmGPT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmGPT";
            this.Load += new System.EventHandler(this.frmGPT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbPensando)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbSalidaGPT;
        private System.Windows.Forms.TextBox tbGPT;
        private System.Windows.Forms.Button cmdGPT;
        private System.Windows.Forms.Timer tmrOcultarFormGPT;
        private System.Windows.Forms.PictureBox pbPensando;
        private System.Windows.Forms.Button cmdCerrar;
    }
}
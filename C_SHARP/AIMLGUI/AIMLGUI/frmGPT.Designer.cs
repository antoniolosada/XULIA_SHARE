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
            this.tbGPT = new System.Windows.Forms.TextBox();
            this.cmdGPT = new System.Windows.Forms.Button();
            this.tmrOcultarFormGPT = new System.Windows.Forms.Timer(this.components);
            this.pbPensando = new System.Windows.Forms.PictureBox();
            this.cmdCerrar = new System.Windows.Forms.Button();
            this.tbSalidaGPT = new System.Windows.Forms.RichTextBox();
            this.picEjecFuncion = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbPensando)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEjecFuncion)).BeginInit();
            this.SuspendLayout();
            // 
            // tbGPT
            // 
            this.tbGPT.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tbGPT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbGPT.Location = new System.Drawing.Point(42, 844);
            this.tbGPT.Multiline = true;
            this.tbGPT.Name = "tbGPT";
            this.tbGPT.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbGPT.Size = new System.Drawing.Size(560, 72);
            this.tbGPT.TabIndex = 1;
            this.tbGPT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbGPT_KeyPress);
            // 
            // cmdGPT
            // 
            this.cmdGPT.Image = ((System.Drawing.Image)(resources.GetObject("cmdGPT.Image")));
            this.cmdGPT.Location = new System.Drawing.Point(604, 854);
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
            this.pbPensando.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPensando.Image = ((System.Drawing.Image)(resources.GetObject("pbPensando.Image")));
            this.pbPensando.Location = new System.Drawing.Point(221, 277);
            this.pbPensando.Name = "pbPensando";
            this.pbPensando.Size = new System.Drawing.Size(202, 212);
            this.pbPensando.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPensando.TabIndex = 3;
            this.pbPensando.TabStop = false;
            this.pbPensando.Visible = false;
            // 
            // cmdCerrar
            // 
            this.cmdCerrar.Image = global::XULIA.Properties.Resources.aspa;
            this.cmdCerrar.Location = new System.Drawing.Point(4, 863);
            this.cmdCerrar.Name = "cmdCerrar";
            this.cmdCerrar.Size = new System.Drawing.Size(32, 33);
            this.cmdCerrar.TabIndex = 4;
            this.cmdCerrar.UseVisualStyleBackColor = true;
            this.cmdCerrar.Click += new System.EventHandler(this.cmdCerrar_Click);
            // 
            // tbSalidaGPT
            // 
            this.tbSalidaGPT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tbSalidaGPT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbSalidaGPT.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSalidaGPT.Location = new System.Drawing.Point(5, -1);
            this.tbSalidaGPT.Name = "tbSalidaGPT";
            this.tbSalidaGPT.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.tbSalidaGPT.Size = new System.Drawing.Size(654, 839);
            this.tbSalidaGPT.TabIndex = 5;
            this.tbSalidaGPT.Text = "";
            // 
            // picEjecFuncion
            // 
            this.picEjecFuncion.Image = ((System.Drawing.Image)(resources.GetObject("picEjecFuncion.Image")));
            this.picEjecFuncion.Location = new System.Drawing.Point(200, 277);
            this.picEjecFuncion.Name = "picEjecFuncion";
            this.picEjecFuncion.Size = new System.Drawing.Size(255, 232);
            this.picEjecFuncion.TabIndex = 6;
            this.picEjecFuncion.TabStop = false;
            this.picEjecFuncion.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(524, 936);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 33);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmGPT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(661, 928);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.picEjecFuncion);
            this.Controls.Add(this.pbPensando);
            this.Controls.Add(this.tbSalidaGPT);
            this.Controls.Add(this.cmdCerrar);
            this.Controls.Add(this.cmdGPT);
            this.Controls.Add(this.tbGPT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmGPT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmGPT";
            this.Load += new System.EventHandler(this.frmGPT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbPensando)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEjecFuncion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbGPT;
        private System.Windows.Forms.Button cmdGPT;
        private System.Windows.Forms.Timer tmrOcultarFormGPT;
        private System.Windows.Forms.PictureBox pbPensando;
        private System.Windows.Forms.Button cmdCerrar;
        private System.Windows.Forms.RichTextBox tbSalidaGPT;
        private System.Windows.Forms.PictureBox picEjecFuncion;
        private System.Windows.Forms.Button button1;
    }
}
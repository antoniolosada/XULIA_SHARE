namespace XULIA
{
    partial class NavegadorWeb
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
            this.webBox = new System.Windows.Forms.GroupBox();
            this.rtbRespuesta = new System.Windows.Forms.RichTextBox();
            this.picXulia = new System.Windows.Forms.PictureBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.btIrA = new System.Windows.Forms.Button();
            this.tbDireccionWeb = new System.Windows.Forms.TextBox();
            this.cmdVerDoc = new System.Windows.Forms.Button();
            this.cmdAtras = new System.Windows.Forms.Button();
            this.cmdAdelante = new System.Windows.Forms.Button();
            this.webBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picXulia)).BeginInit();
            this.SuspendLayout();
            // 
            // webBox
            // 
            this.webBox.Controls.Add(this.rtbRespuesta);
            this.webBox.Controls.Add(this.picXulia);
            this.webBox.Controls.Add(this.webBrowser1);
            this.webBox.Location = new System.Drawing.Point(2, 25);
            this.webBox.Name = "webBox";
            this.webBox.Size = new System.Drawing.Size(343, 472);
            this.webBox.TabIndex = 0;
            this.webBox.TabStop = false;
            // 
            // rtbRespuesta
            // 
            this.rtbRespuesta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.rtbRespuesta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbRespuesta.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbRespuesta.Location = new System.Drawing.Point(0, 0);
            this.rtbRespuesta.Name = "rtbRespuesta";
            this.rtbRespuesta.Size = new System.Drawing.Size(337, 472);
            this.rtbRespuesta.TabIndex = 7;
            this.rtbRespuesta.Text = "";
            this.rtbRespuesta.Visible = false;
            // 
            // picXulia
            // 
            this.picXulia.BackColor = System.Drawing.Color.Black;
            this.picXulia.BackgroundImage = global::XULIA.Properties.Resources.xulia_consulta;
            this.picXulia.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picXulia.Location = new System.Drawing.Point(0, -23);
            this.picXulia.Name = "picXulia";
            this.picXulia.Size = new System.Drawing.Size(337, 576);
            this.picXulia.TabIndex = 4;
            this.picXulia.TabStop = false;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 16);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(337, 453);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // btIrA
            // 
            this.btIrA.Location = new System.Drawing.Point(52, 2);
            this.btIrA.Name = "btIrA";
            this.btIrA.Size = new System.Drawing.Size(40, 23);
            this.btIrA.TabIndex = 1;
            this.btIrA.Text = "Ir a";
            this.btIrA.UseVisualStyleBackColor = true;
            this.btIrA.Click += new System.EventHandler(this.btIrA_Click);
            // 
            // tbDireccionWeb
            // 
            this.tbDireccionWeb.Location = new System.Drawing.Point(98, 4);
            this.tbDireccionWeb.Name = "tbDireccionWeb";
            this.tbDireccionWeb.Size = new System.Drawing.Size(247, 20);
            this.tbDireccionWeb.TabIndex = 2;
            // 
            // cmdVerDoc
            // 
            this.cmdVerDoc.Location = new System.Drawing.Point(408, 12);
            this.cmdVerDoc.Name = "cmdVerDoc";
            this.cmdVerDoc.Size = new System.Drawing.Size(75, 23);
            this.cmdVerDoc.TabIndex = 3;
            this.cmdVerDoc.Text = "Ver Doc";
            this.cmdVerDoc.UseVisualStyleBackColor = true;
            this.cmdVerDoc.Click += new System.EventHandler(this.cmdVerDoc_Click);
            // 
            // cmdAtras
            // 
            this.cmdAtras.Location = new System.Drawing.Point(2, 2);
            this.cmdAtras.Name = "cmdAtras";
            this.cmdAtras.Size = new System.Drawing.Size(24, 23);
            this.cmdAtras.TabIndex = 5;
            this.cmdAtras.Text = "<";
            this.cmdAtras.UseVisualStyleBackColor = true;
            this.cmdAtras.Click += new System.EventHandler(this.cmdAtras_Click);
            // 
            // cmdAdelante
            // 
            this.cmdAdelante.Location = new System.Drawing.Point(27, 2);
            this.cmdAdelante.Name = "cmdAdelante";
            this.cmdAdelante.Size = new System.Drawing.Size(22, 23);
            this.cmdAdelante.TabIndex = 6;
            this.cmdAdelante.Text = ">";
            this.cmdAdelante.UseVisualStyleBackColor = true;
            this.cmdAdelante.Click += new System.EventHandler(this.cmdAdelante_Click);
            // 
            // NavegadorWeb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 497);
            this.Controls.Add(this.cmdAdelante);
            this.Controls.Add(this.cmdAtras);
            this.Controls.Add(this.cmdVerDoc);
            this.Controls.Add(this.tbDireccionWeb);
            this.Controls.Add(this.btIrA);
            this.Controls.Add(this.webBox);
            this.Name = "NavegadorWeb";
            this.Text = "-----  SEARCH IN THE WEB  -----";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NavegadorWeb_FormClosing);
            this.Load += new System.EventHandler(this.NavegadorWeb_Load);
            this.Resize += new System.EventHandler(this.NavegadorWeb_Resize);
            this.webBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picXulia)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox webBox;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button btIrA;
        private System.Windows.Forms.TextBox tbDireccionWeb;
        private System.Windows.Forms.Button cmdVerDoc;
        private System.Windows.Forms.PictureBox picXulia;
        private System.Windows.Forms.Button cmdAtras;
        private System.Windows.Forms.Button cmdAdelante;
        private System.Windows.Forms.RichTextBox rtbRespuesta;
    }
}
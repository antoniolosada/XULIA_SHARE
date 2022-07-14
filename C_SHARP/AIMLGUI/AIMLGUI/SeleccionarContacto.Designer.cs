namespace XULIA
{
    partial class SeleccionarContacto
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
            this.lstContactos = new System.Windows.Forms.ListBox();
            this.cmdAceptar = new System.Windows.Forms.Button();
            this.cmdCamcelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstContactos
            // 
            this.lstContactos.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstContactos.FormattingEnabled = true;
            this.lstContactos.ItemHeight = 17;
            this.lstContactos.Location = new System.Drawing.Point(2, 2);
            this.lstContactos.Name = "lstContactos";
            this.lstContactos.Size = new System.Drawing.Size(546, 225);
            this.lstContactos.TabIndex = 0;
            // 
            // cmdAceptar
            // 
            this.cmdAceptar.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAceptar.Location = new System.Drawing.Point(66, 233);
            this.cmdAceptar.Name = "cmdAceptar";
            this.cmdAceptar.Size = new System.Drawing.Size(172, 38);
            this.cmdAceptar.TabIndex = 1;
            this.cmdAceptar.Text = "Aceptar";
            this.cmdAceptar.UseVisualStyleBackColor = true;
            this.cmdAceptar.Click += new System.EventHandler(this.cmdAceptar_Click);
            // 
            // cmdCamcelar
            // 
            this.cmdCamcelar.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCamcelar.Location = new System.Drawing.Point(323, 233);
            this.cmdCamcelar.Name = "cmdCamcelar";
            this.cmdCamcelar.Size = new System.Drawing.Size(172, 38);
            this.cmdCamcelar.TabIndex = 2;
            this.cmdCamcelar.Text = "Cancelar";
            this.cmdCamcelar.UseVisualStyleBackColor = true;
            this.cmdCamcelar.Click += new System.EventHandler(this.cmdCamcelar_Click);
            // 
            // SeleccionarContacto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 283);
            this.Controls.Add(this.cmdCamcelar);
            this.Controls.Add(this.cmdAceptar);
            this.Controls.Add(this.lstContactos);
            this.Name = "SeleccionarContacto";
            this.Text = "SeleccionarContacto";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstContactos;
        private System.Windows.Forms.Button cmdAceptar;
        private System.Windows.Forms.Button cmdCamcelar;
    }
}
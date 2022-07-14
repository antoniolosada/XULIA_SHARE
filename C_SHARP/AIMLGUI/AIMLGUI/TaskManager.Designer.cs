namespace XULIA
{
    partial class TaskManager
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
            this.cmdVerAplicaciones = new System.Windows.Forms.Button();
            this.lstAplicaciones = new System.Windows.Forms.ListBox();
            this.cmdActivar = new System.Windows.Forms.Button();
            this.cmdRenumerar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdVerAplicaciones
            // 
            this.cmdVerAplicaciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.cmdVerAplicaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdVerAplicaciones.Location = new System.Drawing.Point(543, 12);
            this.cmdVerAplicaciones.Name = "cmdVerAplicaciones";
            this.cmdVerAplicaciones.Size = new System.Drawing.Size(96, 35);
            this.cmdVerAplicaciones.TabIndex = 2;
            this.cmdVerAplicaciones.Text = "&RECARGAR";
            this.cmdVerAplicaciones.UseVisualStyleBackColor = false;
            this.cmdVerAplicaciones.Click += new System.EventHandler(this.cmdVerAplicaciones_Click);
            // 
            // lstAplicaciones
            // 
            this.lstAplicaciones.BackColor = System.Drawing.Color.Black;
            this.lstAplicaciones.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lstAplicaciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lstAplicaciones.FormattingEnabled = true;
            this.lstAplicaciones.ItemHeight = 18;
            this.lstAplicaciones.Location = new System.Drawing.Point(-1, 53);
            this.lstAplicaciones.Name = "lstAplicaciones";
            this.lstAplicaciones.Size = new System.Drawing.Size(652, 598);
            this.lstAplicaciones.Sorted = true;
            this.lstAplicaciones.TabIndex = 1;
            this.lstAplicaciones.SelectedIndexChanged += new System.EventHandler(this.lstAplicaciones_SelectedIndexChanged);
            this.lstAplicaciones.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstAplicaciones_KeyPress);
            // 
            // cmdActivar
            // 
            this.cmdActivar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.cmdActivar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdActivar.Location = new System.Drawing.Point(442, 12);
            this.cmdActivar.Name = "cmdActivar";
            this.cmdActivar.Size = new System.Drawing.Size(95, 35);
            this.cmdActivar.TabIndex = 1;
            this.cmdActivar.Text = "&ACTIVAR";
            this.cmdActivar.UseVisualStyleBackColor = false;
            this.cmdActivar.Click += new System.EventHandler(this.cmdActivar_Click);
            // 
            // cmdRenumerar
            // 
            this.cmdRenumerar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.cmdRenumerar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRenumerar.Location = new System.Drawing.Point(12, 12);
            this.cmdRenumerar.Name = "cmdRenumerar";
            this.cmdRenumerar.Size = new System.Drawing.Size(114, 35);
            this.cmdRenumerar.TabIndex = 0;
            this.cmdRenumerar.Text = "RE&NUMERAR";
            this.cmdRenumerar.UseVisualStyleBackColor = false;
            this.cmdRenumerar.Click += new System.EventHandler(this.cmdRenumerar_Click);
            // 
            // TaskManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::XULIA.Properties.Resources.blog_matrix;
            this.ClientSize = new System.Drawing.Size(651, 643);
            this.Controls.Add(this.cmdRenumerar);
            this.Controls.Add(this.cmdActivar);
            this.Controls.Add(this.lstAplicaciones);
            this.Controls.Add(this.cmdVerAplicaciones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TaskManager";
            this.Text = "WindowsManager";
            this.Activated += new System.EventHandler(this.TaskManager_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TaskManager_FormClosing);
            this.Load += new System.EventHandler(this.TaskManager_Load);
            this.SizeChanged += new System.EventHandler(this.TaskManager_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdVerAplicaciones;
        private System.Windows.Forms.ListBox lstAplicaciones;
        private System.Windows.Forms.Button cmdActivar;
        private System.Windows.Forms.Button cmdRenumerar;
    }
}
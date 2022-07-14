namespace XULIA
{
    partial class frmEditarModo
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
            this.lstSeleccion = new System.Windows.Forms.ListBox();
            this.cmdAgregar = new System.Windows.Forms.Button();
            this.cmdAgregarTodo = new System.Windows.Forms.Button();
            this.cmdEliminar = new System.Windows.Forms.Button();
            this.cmdQuitar = new System.Windows.Forms.Button();
            this.cmdGrabar = new System.Windows.Forms.Button();
            this.cmdSalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstGramaticas
            // 
            this.lstGramaticas.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lstGramaticas.FormattingEnabled = true;
            this.lstGramaticas.ItemHeight = 18;
            this.lstGramaticas.Location = new System.Drawing.Point(13, 13);
            this.lstGramaticas.Name = "lstGramaticas";
            this.lstGramaticas.Size = new System.Drawing.Size(215, 364);
            this.lstGramaticas.TabIndex = 0;
            // 
            // lstSeleccion
            // 
            this.lstSeleccion.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lstSeleccion.FormattingEnabled = true;
            this.lstSeleccion.ItemHeight = 18;
            this.lstSeleccion.Location = new System.Drawing.Point(341, 12);
            this.lstSeleccion.Name = "lstSeleccion";
            this.lstSeleccion.Size = new System.Drawing.Size(215, 364);
            this.lstSeleccion.TabIndex = 1;
            // 
            // cmdAgregar
            // 
            this.cmdAgregar.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAgregar.Location = new System.Drawing.Point(250, 72);
            this.cmdAgregar.Name = "cmdAgregar";
            this.cmdAgregar.Size = new System.Drawing.Size(75, 39);
            this.cmdAgregar.TabIndex = 2;
            this.cmdAgregar.Text = ">";
            this.cmdAgregar.UseVisualStyleBackColor = true;
            this.cmdAgregar.Click += new System.EventHandler(this.cmdAgregar_Click);
            // 
            // cmdAgregarTodo
            // 
            this.cmdAgregarTodo.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAgregarTodo.Location = new System.Drawing.Point(250, 162);
            this.cmdAgregarTodo.Name = "cmdAgregarTodo";
            this.cmdAgregarTodo.Size = new System.Drawing.Size(75, 39);
            this.cmdAgregarTodo.TabIndex = 3;
            this.cmdAgregarTodo.Text = ">>";
            this.cmdAgregarTodo.UseVisualStyleBackColor = true;
            this.cmdAgregarTodo.Click += new System.EventHandler(this.cmdAgregarTodo_Click);
            // 
            // cmdEliminar
            // 
            this.cmdEliminar.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdEliminar.Location = new System.Drawing.Point(250, 217);
            this.cmdEliminar.Name = "cmdEliminar";
            this.cmdEliminar.Size = new System.Drawing.Size(75, 39);
            this.cmdEliminar.TabIndex = 4;
            this.cmdEliminar.Text = "X";
            this.cmdEliminar.UseVisualStyleBackColor = true;
            this.cmdEliminar.Click += new System.EventHandler(this.cmdEliminar_Click);
            // 
            // cmdQuitar
            // 
            this.cmdQuitar.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdQuitar.Location = new System.Drawing.Point(250, 117);
            this.cmdQuitar.Name = "cmdQuitar";
            this.cmdQuitar.Size = new System.Drawing.Size(75, 39);
            this.cmdQuitar.TabIndex = 5;
            this.cmdQuitar.Text = "<";
            this.cmdQuitar.UseVisualStyleBackColor = true;
            this.cmdQuitar.Click += new System.EventHandler(this.cmdQuitar_Click);
            // 
            // cmdGrabar
            // 
            this.cmdGrabar.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGrabar.Location = new System.Drawing.Point(250, 271);
            this.cmdGrabar.Name = "cmdGrabar";
            this.cmdGrabar.Size = new System.Drawing.Size(75, 39);
            this.cmdGrabar.TabIndex = 6;
            this.cmdGrabar.Text = "OK";
            this.cmdGrabar.UseVisualStyleBackColor = true;
            this.cmdGrabar.Click += new System.EventHandler(this.cmdGrabar_Click);
            // 
            // cmdSalir
            // 
            this.cmdSalir.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSalir.Location = new System.Drawing.Point(250, 325);
            this.cmdSalir.Name = "cmdSalir";
            this.cmdSalir.Size = new System.Drawing.Size(75, 39);
            this.cmdSalir.TabIndex = 7;
            this.cmdSalir.Text = "Salir";
            this.cmdSalir.UseVisualStyleBackColor = true;
            this.cmdSalir.Click += new System.EventHandler(this.cmdSalir_Click);
            // 
            // frmEditarModo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 394);
            this.Controls.Add(this.cmdSalir);
            this.Controls.Add(this.cmdGrabar);
            this.Controls.Add(this.cmdQuitar);
            this.Controls.Add(this.cmdEliminar);
            this.Controls.Add(this.cmdAgregarTodo);
            this.Controls.Add(this.cmdAgregar);
            this.Controls.Add(this.lstSeleccion);
            this.Controls.Add(this.lstGramaticas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditarModo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditarModo";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstGramaticas;
        private System.Windows.Forms.ListBox lstSeleccion;
        private System.Windows.Forms.Button cmdAgregar;
        private System.Windows.Forms.Button cmdAgregarTodo;
        private System.Windows.Forms.Button cmdEliminar;
        private System.Windows.Forms.Button cmdQuitar;
        private System.Windows.Forms.Button cmdGrabar;
        private System.Windows.Forms.Button cmdSalir;
    }
}
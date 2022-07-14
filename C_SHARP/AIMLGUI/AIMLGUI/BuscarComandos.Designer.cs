namespace XULIA
{
    partial class BuscarComandos
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabComandos = new System.Windows.Forms.TabPage();
            this.cmdBuscarComando = new System.Windows.Forms.Button();
            this.tbComando = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgComandos = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gramatica = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabGramaticas = new System.Windows.Forms.TabPage();
            this.lstGramaticas = new System.Windows.Forms.ListBox();
            this.dgGramaticas = new System.Windows.Forms.DataGridView();
            this.Comando = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Aydua = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Macro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabModo = new System.Windows.Forms.TabPage();
            this.lstGramaticaModo = new System.Windows.Forms.ListBox();
            this.lstModos = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tabComandos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgComandos)).BeginInit();
            this.tabGramaticas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgGramaticas)).BeginInit();
            this.tabModo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabComandos);
            this.tabControl1.Controls.Add(this.tabGramaticas);
            this.tabControl1.Controls.Add(this.tabModo);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(11, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1054, 756);
            this.tabControl1.TabIndex = 11;
            // 
            // tabComandos
            // 
            this.tabComandos.Controls.Add(this.cmdBuscarComando);
            this.tabComandos.Controls.Add(this.tbComando);
            this.tabComandos.Controls.Add(this.label2);
            this.tabComandos.Controls.Add(this.dgComandos);
            this.tabComandos.Location = new System.Drawing.Point(4, 25);
            this.tabComandos.Name = "tabComandos";
            this.tabComandos.Padding = new System.Windows.Forms.Padding(3);
            this.tabComandos.Size = new System.Drawing.Size(1046, 727);
            this.tabComandos.TabIndex = 0;
            this.tabComandos.Text = "Por Comando";
            this.tabComandos.UseVisualStyleBackColor = true;
            // 
            // cmdBuscarComando
            // 
            this.cmdBuscarComando.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdBuscarComando.Location = new System.Drawing.Point(655, 6);
            this.cmdBuscarComando.Name = "cmdBuscarComando";
            this.cmdBuscarComando.Size = new System.Drawing.Size(118, 26);
            this.cmdBuscarComando.TabIndex = 3;
            this.cmdBuscarComando.Text = "&Buscar";
            this.cmdBuscarComando.UseVisualStyleBackColor = true;
            this.cmdBuscarComando.Click += new System.EventHandler(this.cmdBuscarComando_Click);
            // 
            // tbComando
            // 
            this.tbComando.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tbComando.Location = new System.Drawing.Point(174, 6);
            this.tbComando.Name = "tbComando";
            this.tbComando.Size = new System.Drawing.Size(475, 25);
            this.tbComando.TabIndex = 0;
            this.tbComando.TextChanged += new System.EventHandler(this.tbComando_TextChanged);
            this.tbComando.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbComando_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(10, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Buscar Comando";
            // 
            // dgComandos
            // 
            this.dgComandos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgComandos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.Gramatica,
            this.dataGridViewTextBoxColumn3});
            this.dgComandos.Location = new System.Drawing.Point(8, 38);
            this.dgComandos.Name = "dgComandos";
            this.dgComandos.Size = new System.Drawing.Size(1033, 683);
            this.dgComandos.TabIndex = 4;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Comando";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 260;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Ayuda";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 600;
            // 
            // Gramatica
            // 
            this.Gramatica.HeaderText = "Gramática";
            this.Gramatica.Name = "Gramatica";
            this.Gramatica.Width = 180;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Macro";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 400;
            // 
            // tabGramaticas
            // 
            this.tabGramaticas.Controls.Add(this.lstGramaticas);
            this.tabGramaticas.Controls.Add(this.dgGramaticas);
            this.tabGramaticas.Location = new System.Drawing.Point(4, 25);
            this.tabGramaticas.Name = "tabGramaticas";
            this.tabGramaticas.Padding = new System.Windows.Forms.Padding(3);
            this.tabGramaticas.Size = new System.Drawing.Size(1046, 727);
            this.tabGramaticas.TabIndex = 1;
            this.tabGramaticas.Text = "Por Gramáticas";
            this.tabGramaticas.UseVisualStyleBackColor = true;
            // 
            // lstGramaticas
            // 
            this.lstGramaticas.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstGramaticas.FormattingEnabled = true;
            this.lstGramaticas.ItemHeight = 24;
            this.lstGramaticas.Location = new System.Drawing.Point(6, 7);
            this.lstGramaticas.Name = "lstGramaticas";
            this.lstGramaticas.Size = new System.Drawing.Size(213, 700);
            this.lstGramaticas.TabIndex = 0;
            this.lstGramaticas.SelectedIndexChanged += new System.EventHandler(this.lstGramaticas_SelectedIndexChanged);
            // 
            // dgGramaticas
            // 
            this.dgGramaticas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgGramaticas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Comando,
            this.Aydua,
            this.dataGridViewTextBoxColumn4,
            this.Macro});
            this.dgGramaticas.Location = new System.Drawing.Point(225, 7);
            this.dgGramaticas.Name = "dgGramaticas";
            this.dgGramaticas.Size = new System.Drawing.Size(815, 714);
            this.dgGramaticas.TabIndex = 1;
            // 
            // Comando
            // 
            this.Comando.HeaderText = "Comando";
            this.Comando.Name = "Comando";
            this.Comando.Width = 260;
            // 
            // Aydua
            // 
            this.Aydua.HeaderText = "Ayuda";
            this.Aydua.Name = "Aydua";
            this.Aydua.Width = 600;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Gramática";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 180;
            // 
            // Macro
            // 
            this.Macro.HeaderText = "Macro";
            this.Macro.Name = "Macro";
            this.Macro.Width = 400;
            // 
            // tabModo
            // 
            this.tabModo.Controls.Add(this.lstGramaticaModo);
            this.tabModo.Controls.Add(this.lstModos);
            this.tabModo.Location = new System.Drawing.Point(4, 25);
            this.tabModo.Name = "tabModo";
            this.tabModo.Size = new System.Drawing.Size(1046, 727);
            this.tabModo.TabIndex = 2;
            this.tabModo.Text = "Por Modo";
            this.tabModo.UseVisualStyleBackColor = true;
            // 
            // lstGramaticaModo
            // 
            this.lstGramaticaModo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstGramaticaModo.FormattingEnabled = true;
            this.lstGramaticaModo.ItemHeight = 24;
            this.lstGramaticaModo.Location = new System.Drawing.Point(397, 7);
            this.lstGramaticaModo.Name = "lstGramaticaModo";
            this.lstGramaticaModo.Size = new System.Drawing.Size(635, 700);
            this.lstGramaticaModo.TabIndex = 3;
            // 
            // lstModos
            // 
            this.lstModos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstModos.FormattingEnabled = true;
            this.lstModos.ItemHeight = 24;
            this.lstModos.Location = new System.Drawing.Point(6, 7);
            this.lstModos.Name = "lstModos";
            this.lstModos.Size = new System.Drawing.Size(385, 700);
            this.lstModos.TabIndex = 2;
            this.lstModos.SelectedIndexChanged += new System.EventHandler(this.lstModos_SelectedIndexChanged);
            // 
            // BuscarComandos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 761);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BuscarComandos";
            this.Text = "Buscar Comandos Xulia";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BuscarComandos_FormClosing);
            this.Load += new System.EventHandler(this.BuscarComandos_Load);
            this.SizeChanged += new System.EventHandler(this.BuscarComandos_SizeChanged);
            this.tabControl1.ResumeLayout(false);
            this.tabComandos.ResumeLayout(false);
            this.tabComandos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgComandos)).EndInit();
            this.tabGramaticas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgGramaticas)).EndInit();
            this.tabModo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabGramaticas;
        private System.Windows.Forms.DataGridView dgGramaticas;
        private System.Windows.Forms.TabPage tabComandos;
        private System.Windows.Forms.DataGridView dgComandos;
        private System.Windows.Forms.ListBox lstGramaticas;
        private System.Windows.Forms.Button cmdBuscarComando;
        private System.Windows.Forms.TextBox tbComando;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabModo;
        private System.Windows.Forms.ListBox lstGramaticaModo;
        private System.Windows.Forms.ListBox lstModos;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gramatica;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comando;
        private System.Windows.Forms.DataGridViewTextBoxColumn Aydua;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Macro;
    }
}
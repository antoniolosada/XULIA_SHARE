namespace XULIA
{
    partial class Configuracion
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
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.cmdRegarcarConfig = new System.Windows.Forms.Button();
            this.cmdCfgRecVoz = new System.Windows.Forms.Button();
            this.cmdEditarVar = new System.Windows.Forms.Button();
            this.tbVar = new System.Windows.Forms.TextBox();
            this.dgVar = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prueba = new System.Windows.Forms.Button();
            this.cmdGrabar = new System.Windows.Forms.Button();
            this.tbValor = new System.Windows.Forms.TextBox();
            this.tabModos = new System.Windows.Forms.TabPage();
            this.lblError = new System.Windows.Forms.Label();
            this.cmdActivarEditarModo = new System.Windows.Forms.Button();
            this.cmdActivarModos = new System.Windows.Forms.Button();
            this.cmdBorrar = new System.Windows.Forms.Button();
            this.cmdModificar = new System.Windows.Forms.Button();
            this.dgModos = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbValorGramatica = new System.Windows.Forms.TextBox();
            this.tbVarGramatica = new System.Windows.Forms.TextBox();
            this.tabGramaticas = new System.Windows.Forms.TabPage();
            this.cmdEditarComandoGramatica = new System.Windows.Forms.Button();
            this.cmdActivarSeleccionGramaticas = new System.Windows.Forms.Button();
            this.cmdActivarGridGramaticas = new System.Windows.Forms.Button();
            this.cmdEliminarSeccion = new System.Windows.Forms.Button();
            this.cmdNuevoComando = new System.Windows.Forms.Button();
            this.dgGramaticas = new System.Windows.Forms.DataGridView();
            this.Clave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ComandoVoz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ComandoAyuda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Macro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.cbGramaticas = new System.Windows.Forms.ComboBox();
            this.tabDcitadoGoogle = new System.Windows.Forms.TabPage();
            this.cmdActivarEdicionElementoLista = new System.Windows.Forms.Button();
            this.cmdActivarSeleccionLista = new System.Windows.Forms.Button();
            this.cmdActivarGridLista = new System.Windows.Forms.Button();
            this.cmdBorrarElementoLista = new System.Windows.Forms.Button();
            this.tbTexto = new System.Windows.Forms.ComboBox();
            this.cmdModificarTexto = new System.Windows.Forms.Button();
            this.tbTextoSustituto = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dgListaSustitucion = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.cbListaSustitucion = new System.Windows.Forms.ComboBox();
            this.tabDirecciones = new System.Windows.Forms.TabPage();
            this.tbVarDir = new System.Windows.Forms.TextBox();
            this.dgVarDir = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdModificarDir = new System.Windows.Forms.Button();
            this.tbValorDir = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVar)).BeginInit();
            this.tabModos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgModos)).BeginInit();
            this.tabGramaticas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgGramaticas)).BeginInit();
            this.tabDcitadoGoogle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgListaSustitucion)).BeginInit();
            this.tabDirecciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVarDir)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabGeneral);
            this.tabControl1.Controls.Add(this.tabModos);
            this.tabControl1.Controls.Add(this.tabGramaticas);
            this.tabControl1.Controls.Add(this.tabDcitadoGoogle);
            this.tabControl1.Controls.Add(this.tabDirecciones);
            this.tabControl1.Location = new System.Drawing.Point(1, -1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(907, 680);
            this.tabControl1.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.cmdRegarcarConfig);
            this.tabGeneral.Controls.Add(this.cmdCfgRecVoz);
            this.tabGeneral.Controls.Add(this.cmdEditarVar);
            this.tabGeneral.Controls.Add(this.tbVar);
            this.tabGeneral.Controls.Add(this.dgVar);
            this.tabGeneral.Controls.Add(this.Prueba);
            this.tabGeneral.Controls.Add(this.cmdGrabar);
            this.tabGeneral.Controls.Add(this.tbValor);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(899, 654);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // cmdRegarcarConfig
            // 
            this.cmdRegarcarConfig.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdRegarcarConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRegarcarConfig.Location = new System.Drawing.Point(708, 619);
            this.cmdRegarcarConfig.Name = "cmdRegarcarConfig";
            this.cmdRegarcarConfig.Size = new System.Drawing.Size(185, 31);
            this.cmdRegarcarConfig.TabIndex = 6;
            this.cmdRegarcarConfig.Text = "Recargar Configuración";
            this.cmdRegarcarConfig.UseVisualStyleBackColor = false;
            this.cmdRegarcarConfig.Click += new System.EventHandler(this.cmdRegarcarConfig_Click);
            // 
            // cmdCfgRecVoz
            // 
            this.cmdCfgRecVoz.BackColor = System.Drawing.Color.Yellow;
            this.cmdCfgRecVoz.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCfgRecVoz.Location = new System.Drawing.Point(108, 620);
            this.cmdCfgRecVoz.Name = "cmdCfgRecVoz";
            this.cmdCfgRecVoz.Size = new System.Drawing.Size(280, 28);
            this.cmdCfgRecVoz.TabIndex = 5;
            this.cmdCfgRecVoz.Text = "Config. Rec de Voz y entrenamiento";
            this.cmdCfgRecVoz.UseVisualStyleBackColor = false;
            this.cmdCfgRecVoz.Click += new System.EventHandler(this.cmdCfgRecVoz_Click);
            // 
            // cmdEditarVar
            // 
            this.cmdEditarVar.Location = new System.Drawing.Point(6, 623);
            this.cmdEditarVar.Name = "cmdEditarVar";
            this.cmdEditarVar.Size = new System.Drawing.Size(96, 23);
            this.cmdEditarVar.TabIndex = 4;
            this.cmdEditarVar.Text = "activar &Editar";
            this.cmdEditarVar.UseVisualStyleBackColor = true;
            this.cmdEditarVar.Click += new System.EventHandler(this.cmdEditarVar_Click);
            // 
            // tbVar
            // 
            this.tbVar.Enabled = false;
            this.tbVar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVar.Location = new System.Drawing.Point(3, 22);
            this.tbVar.Name = "tbVar";
            this.tbVar.Size = new System.Drawing.Size(319, 26);
            this.tbVar.TabIndex = 1;
            // 
            // dgVar
            // 
            this.dgVar.AllowUserToAddRows = false;
            this.dgVar.AllowUserToDeleteRows = false;
            this.dgVar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dgVar.Location = new System.Drawing.Point(3, 54);
            this.dgVar.Name = "dgVar";
            this.dgVar.ReadOnly = true;
            this.dgVar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgVar.Size = new System.Drawing.Size(891, 559);
            this.dgVar.TabIndex = 7;
            this.dgVar.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgVar_CellContentClick);
            this.dgVar.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgVar_RowEnter);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Variable";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Valor";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 600;
            // 
            // Prueba
            // 
            this.Prueba.Location = new System.Drawing.Point(235, 478);
            this.Prueba.Name = "Prueba";
            this.Prueba.Size = new System.Drawing.Size(75, 23);
            this.Prueba.TabIndex = 5;
            this.Prueba.Text = "PRUEBA";
            this.Prueba.UseVisualStyleBackColor = true;
            this.Prueba.Click += new System.EventHandler(this.Prueba_Click);
            // 
            // cmdGrabar
            // 
            this.cmdGrabar.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGrabar.Location = new System.Drawing.Point(809, 21);
            this.cmdGrabar.Name = "cmdGrabar";
            this.cmdGrabar.Size = new System.Drawing.Size(84, 27);
            this.cmdGrabar.TabIndex = 3;
            this.cmdGrabar.Text = "&Modificar";
            this.cmdGrabar.UseVisualStyleBackColor = true;
            this.cmdGrabar.Click += new System.EventHandler(this.cmdGrabar_Click);
            // 
            // tbValor
            // 
            this.tbValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbValor.Location = new System.Drawing.Point(328, 22);
            this.tbValor.Name = "tbValor";
            this.tbValor.Size = new System.Drawing.Size(474, 26);
            this.tbValor.TabIndex = 2;
            // 
            // tabModos
            // 
            this.tabModos.Controls.Add(this.lblError);
            this.tabModos.Controls.Add(this.cmdActivarEditarModo);
            this.tabModos.Controls.Add(this.cmdActivarModos);
            this.tabModos.Controls.Add(this.cmdBorrar);
            this.tabModos.Controls.Add(this.cmdModificar);
            this.tabModos.Controls.Add(this.dgModos);
            this.tabModos.Controls.Add(this.tbValorGramatica);
            this.tabModos.Controls.Add(this.tbVarGramatica);
            this.tabModos.Location = new System.Drawing.Point(4, 22);
            this.tabModos.Name = "tabModos";
            this.tabModos.Padding = new System.Windows.Forms.Padding(3);
            this.tabModos.Size = new System.Drawing.Size(899, 654);
            this.tabModos.TabIndex = 1;
            this.tabModos.Text = "Modos";
            this.tabModos.UseVisualStyleBackColor = true;
            // 
            // lblError
            // 
            this.lblError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblError.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(3, 50);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(890, 22);
            this.lblError.TabIndex = 8;
            // 
            // cmdActivarEditarModo
            // 
            this.cmdActivarEditarModo.Location = new System.Drawing.Point(109, 622);
            this.cmdActivarEditarModo.Name = "cmdActivarEditarModo";
            this.cmdActivarEditarModo.Size = new System.Drawing.Size(96, 23);
            this.cmdActivarEditarModo.TabIndex = 7;
            this.cmdActivarEditarModo.Text = "activar &Editar";
            this.cmdActivarEditarModo.UseVisualStyleBackColor = true;
            this.cmdActivarEditarModo.Click += new System.EventHandler(this.cmdActivarEditarModo_Click);
            // 
            // cmdActivarModos
            // 
            this.cmdActivarModos.Location = new System.Drawing.Point(7, 622);
            this.cmdActivarModos.Name = "cmdActivarModos";
            this.cmdActivarModos.Size = new System.Drawing.Size(96, 23);
            this.cmdActivarModos.TabIndex = 6;
            this.cmdActivarModos.Text = "activar &Grid";
            this.cmdActivarModos.UseVisualStyleBackColor = true;
            this.cmdActivarModos.Click += new System.EventHandler(this.cmdActivarModos_Click);
            // 
            // cmdBorrar
            // 
            this.cmdBorrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmdBorrar.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdBorrar.ForeColor = System.Drawing.Color.White;
            this.cmdBorrar.Location = new System.Drawing.Point(834, 21);
            this.cmdBorrar.Name = "cmdBorrar";
            this.cmdBorrar.Size = new System.Drawing.Size(60, 26);
            this.cmdBorrar.TabIndex = 4;
            this.cmdBorrar.Text = "&Borrar";
            this.cmdBorrar.UseVisualStyleBackColor = false;
            this.cmdBorrar.Click += new System.EventHandler(this.cmdBorrar_Click);
            // 
            // cmdModificar
            // 
            this.cmdModificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdModificar.Location = new System.Drawing.Point(737, 21);
            this.cmdModificar.Name = "cmdModificar";
            this.cmdModificar.Size = new System.Drawing.Size(91, 26);
            this.cmdModificar.TabIndex = 3;
            this.cmdModificar.Text = "&Modificar";
            this.cmdModificar.UseVisualStyleBackColor = true;
            this.cmdModificar.Click += new System.EventHandler(this.cmdModificar_Click);
            // 
            // dgModos
            // 
            this.dgModos.AllowUserToAddRows = false;
            this.dgModos.AllowUserToDeleteRows = false;
            this.dgModos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgModos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dgModos.Location = new System.Drawing.Point(3, 75);
            this.dgModos.Name = "dgModos";
            this.dgModos.ReadOnly = true;
            this.dgModos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgModos.Size = new System.Drawing.Size(891, 541);
            this.dgModos.TabIndex = 5;
            this.dgModos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgModos_CellContentClick);
            this.dgModos.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgModos_RowEnter);
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Modo";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 248;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Gramáticas";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 600;
            // 
            // tbValorGramatica
            // 
            this.tbValorGramatica.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbValorGramatica.Location = new System.Drawing.Point(297, 21);
            this.tbValorGramatica.Name = "tbValorGramatica";
            this.tbValorGramatica.Size = new System.Drawing.Size(434, 26);
            this.tbValorGramatica.TabIndex = 2;
            // 
            // tbVarGramatica
            // 
            this.tbVarGramatica.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVarGramatica.Location = new System.Drawing.Point(3, 21);
            this.tbVarGramatica.Name = "tbVarGramatica";
            this.tbVarGramatica.Size = new System.Drawing.Size(288, 26);
            this.tbVarGramatica.TabIndex = 1;
            // 
            // tabGramaticas
            // 
            this.tabGramaticas.Controls.Add(this.cmdEditarComandoGramatica);
            this.tabGramaticas.Controls.Add(this.cmdActivarSeleccionGramaticas);
            this.tabGramaticas.Controls.Add(this.cmdActivarGridGramaticas);
            this.tabGramaticas.Controls.Add(this.cmdEliminarSeccion);
            this.tabGramaticas.Controls.Add(this.cmdNuevoComando);
            this.tabGramaticas.Controls.Add(this.dgGramaticas);
            this.tabGramaticas.Controls.Add(this.label1);
            this.tabGramaticas.Controls.Add(this.cbGramaticas);
            this.tabGramaticas.Location = new System.Drawing.Point(4, 22);
            this.tabGramaticas.Name = "tabGramaticas";
            this.tabGramaticas.Size = new System.Drawing.Size(899, 654);
            this.tabGramaticas.TabIndex = 4;
            this.tabGramaticas.Text = "Gramaticas";
            this.tabGramaticas.UseVisualStyleBackColor = true;
            // 
            // cmdEditarComandoGramatica
            // 
            this.cmdEditarComandoGramatica.Location = new System.Drawing.Point(269, 627);
            this.cmdEditarComandoGramatica.Name = "cmdEditarComandoGramatica";
            this.cmdEditarComandoGramatica.Size = new System.Drawing.Size(96, 23);
            this.cmdEditarComandoGramatica.TabIndex = 6;
            this.cmdEditarComandoGramatica.Text = "&Editar comando";
            this.cmdEditarComandoGramatica.UseVisualStyleBackColor = true;
            this.cmdEditarComandoGramatica.Click += new System.EventHandler(this.cmdEditarComandoGramatica_Click);
            // 
            // cmdActivarSeleccionGramaticas
            // 
            this.cmdActivarSeleccionGramaticas.Location = new System.Drawing.Point(110, 627);
            this.cmdActivarSeleccionGramaticas.Name = "cmdActivarSeleccionGramaticas";
            this.cmdActivarSeleccionGramaticas.Size = new System.Drawing.Size(153, 23);
            this.cmdActivarSeleccionGramaticas.TabIndex = 5;
            this.cmdActivarSeleccionGramaticas.Text = "activar &Selección gramáticas";
            this.cmdActivarSeleccionGramaticas.UseVisualStyleBackColor = true;
            this.cmdActivarSeleccionGramaticas.Click += new System.EventHandler(this.cmdActivarSeleccionGramaticas_Click);
            // 
            // cmdActivarGridGramaticas
            // 
            this.cmdActivarGridGramaticas.Location = new System.Drawing.Point(8, 627);
            this.cmdActivarGridGramaticas.Name = "cmdActivarGridGramaticas";
            this.cmdActivarGridGramaticas.Size = new System.Drawing.Size(96, 23);
            this.cmdActivarGridGramaticas.TabIndex = 4;
            this.cmdActivarGridGramaticas.Text = "activar &Grid";
            this.cmdActivarGridGramaticas.UseVisualStyleBackColor = true;
            this.cmdActivarGridGramaticas.Click += new System.EventHandler(this.cmdActivarGridGramaticas_Click);
            // 
            // cmdEliminarSeccion
            // 
            this.cmdEliminarSeccion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmdEliminarSeccion.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdEliminarSeccion.ForeColor = System.Drawing.Color.White;
            this.cmdEliminarSeccion.Location = new System.Drawing.Point(548, 2);
            this.cmdEliminarSeccion.Name = "cmdEliminarSeccion";
            this.cmdEliminarSeccion.Size = new System.Drawing.Size(142, 28);
            this.cmdEliminarSeccion.TabIndex = 7;
            this.cmdEliminarSeccion.Text = "Eliminar Gramática";
            this.cmdEliminarSeccion.UseVisualStyleBackColor = false;
            this.cmdEliminarSeccion.Click += new System.EventHandler(this.cmdEliminarSeccion_Click);
            // 
            // cmdNuevoComando
            // 
            this.cmdNuevoComando.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdNuevoComando.Location = new System.Drawing.Point(767, 4);
            this.cmdNuevoComando.Name = "cmdNuevoComando";
            this.cmdNuevoComando.Size = new System.Drawing.Size(129, 26);
            this.cmdNuevoComando.TabIndex = 2;
            this.cmdNuevoComando.Text = "&NuevoComando";
            this.cmdNuevoComando.UseVisualStyleBackColor = true;
            this.cmdNuevoComando.Click += new System.EventHandler(this.cmdNuevoComando_Click);
            // 
            // dgGramaticas
            // 
            this.dgGramaticas.AllowUserToAddRows = false;
            this.dgGramaticas.AllowUserToDeleteRows = false;
            this.dgGramaticas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgGramaticas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Clave,
            this.ComandoVoz,
            this.Precision,
            this.ComandoAyuda,
            this.Macro});
            this.dgGramaticas.Location = new System.Drawing.Point(7, 34);
            this.dgGramaticas.Name = "dgGramaticas";
            this.dgGramaticas.ReadOnly = true;
            this.dgGramaticas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgGramaticas.Size = new System.Drawing.Size(889, 587);
            this.dgGramaticas.TabIndex = 3;
            this.dgGramaticas.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgGramaticas_CellContentDoubleClick);
            this.dgGramaticas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgGramaticas_CellContentDoubleClick);
            this.dgGramaticas.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgGramaticas_RowEnter);
            this.dgGramaticas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgGramaticas_KeyPress);
            // 
            // Clave
            // 
            this.Clave.HeaderText = "Clave";
            this.Clave.Name = "Clave";
            this.Clave.ReadOnly = true;
            // 
            // ComandoVoz
            // 
            this.ComandoVoz.HeaderText = "ComandoVoz";
            this.ComandoVoz.Name = "ComandoVoz";
            this.ComandoVoz.ReadOnly = true;
            this.ComandoVoz.Width = 150;
            // 
            // Precision
            // 
            this.Precision.HeaderText = "Precisión";
            this.Precision.Name = "Precision";
            this.Precision.ReadOnly = true;
            this.Precision.Width = 70;
            // 
            // ComandoAyuda
            // 
            this.ComandoAyuda.HeaderText = "Ayuda";
            this.ComandoAyuda.Name = "ComandoAyuda";
            this.ComandoAyuda.ReadOnly = true;
            this.ComandoAyuda.Width = 600;
            // 
            // Macro
            // 
            this.Macro.HeaderText = "Macro";
            this.Macro.Name = "Macro";
            this.Macro.ReadOnly = true;
            this.Macro.Width = 600;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(50, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Gramática";
            // 
            // cbGramaticas
            // 
            this.cbGramaticas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGramaticas.FormattingEnabled = true;
            this.cbGramaticas.Location = new System.Drawing.Point(142, 4);
            this.cbGramaticas.Name = "cbGramaticas";
            this.cbGramaticas.Size = new System.Drawing.Size(400, 24);
            this.cbGramaticas.TabIndex = 1;
            this.cbGramaticas.SelectedIndexChanged += new System.EventHandler(this.cbGramaticas_SelectedIndexChanged);
            this.cbGramaticas.Leave += new System.EventHandler(this.cbGramaticas_Leave);
            // 
            // tabDcitadoGoogle
            // 
            this.tabDcitadoGoogle.Controls.Add(this.cmdActivarEdicionElementoLista);
            this.tabDcitadoGoogle.Controls.Add(this.cmdActivarSeleccionLista);
            this.tabDcitadoGoogle.Controls.Add(this.cmdActivarGridLista);
            this.tabDcitadoGoogle.Controls.Add(this.cmdBorrarElementoLista);
            this.tabDcitadoGoogle.Controls.Add(this.tbTexto);
            this.tabDcitadoGoogle.Controls.Add(this.cmdModificarTexto);
            this.tabDcitadoGoogle.Controls.Add(this.tbTextoSustituto);
            this.tabDcitadoGoogle.Controls.Add(this.button1);
            this.tabDcitadoGoogle.Controls.Add(this.dgListaSustitucion);
            this.tabDcitadoGoogle.Controls.Add(this.label2);
            this.tabDcitadoGoogle.Controls.Add(this.cbListaSustitucion);
            this.tabDcitadoGoogle.Location = new System.Drawing.Point(4, 22);
            this.tabDcitadoGoogle.Name = "tabDcitadoGoogle";
            this.tabDcitadoGoogle.Size = new System.Drawing.Size(899, 654);
            this.tabDcitadoGoogle.TabIndex = 3;
            this.tabDcitadoGoogle.Text = "Dictado Google";
            this.tabDcitadoGoogle.UseVisualStyleBackColor = true;
            // 
            // cmdActivarEdicionElementoLista
            // 
            this.cmdActivarEdicionElementoLista.Location = new System.Drawing.Point(268, 623);
            this.cmdActivarEdicionElementoLista.Name = "cmdActivarEdicionElementoLista";
            this.cmdActivarEdicionElementoLista.Size = new System.Drawing.Size(153, 23);
            this.cmdActivarEdicionElementoLista.TabIndex = 9;
            this.cmdActivarEdicionElementoLista.Text = "activar &Edición";
            this.cmdActivarEdicionElementoLista.UseVisualStyleBackColor = true;
            this.cmdActivarEdicionElementoLista.Click += new System.EventHandler(this.cmdActivarEdicionElementoLista_Click);
            // 
            // cmdActivarSeleccionLista
            // 
            this.cmdActivarSeleccionLista.Location = new System.Drawing.Point(109, 623);
            this.cmdActivarSeleccionLista.Name = "cmdActivarSeleccionLista";
            this.cmdActivarSeleccionLista.Size = new System.Drawing.Size(153, 23);
            this.cmdActivarSeleccionLista.TabIndex = 8;
            this.cmdActivarSeleccionLista.Text = "activar selección &Lista";
            this.cmdActivarSeleccionLista.UseVisualStyleBackColor = true;
            this.cmdActivarSeleccionLista.Click += new System.EventHandler(this.cmdActivarSeleccionLista_Click);
            // 
            // cmdActivarGridLista
            // 
            this.cmdActivarGridLista.Location = new System.Drawing.Point(7, 623);
            this.cmdActivarGridLista.Name = "cmdActivarGridLista";
            this.cmdActivarGridLista.Size = new System.Drawing.Size(96, 23);
            this.cmdActivarGridLista.TabIndex = 7;
            this.cmdActivarGridLista.Text = "activar &Grid";
            this.cmdActivarGridLista.UseVisualStyleBackColor = true;
            this.cmdActivarGridLista.Click += new System.EventHandler(this.cmdActivarGridLista_Click);
            // 
            // cmdBorrarElementoLista
            // 
            this.cmdBorrarElementoLista.BackColor = System.Drawing.Color.DarkRed;
            this.cmdBorrarElementoLista.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdBorrarElementoLista.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.cmdBorrarElementoLista.Location = new System.Drawing.Point(827, 9);
            this.cmdBorrarElementoLista.Name = "cmdBorrarElementoLista";
            this.cmdBorrarElementoLista.Size = new System.Drawing.Size(69, 24);
            this.cmdBorrarElementoLista.TabIndex = 5;
            this.cmdBorrarElementoLista.Text = "&Borrar";
            this.cmdBorrarElementoLista.UseVisualStyleBackColor = false;
            this.cmdBorrarElementoLista.Click += new System.EventHandler(this.cmdBorrarElementoLista_Click);
            // 
            // tbTexto
            // 
            this.tbTexto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTexto.FormattingEnabled = true;
            this.tbTexto.Items.AddRange(new object[] {
            "·CodigoIdioma",
            "·SalirDictado",
            "·Literal"});
            this.tbTexto.Location = new System.Drawing.Point(7, 39);
            this.tbTexto.Name = "tbTexto";
            this.tbTexto.Size = new System.Drawing.Size(360, 26);
            this.tbTexto.TabIndex = 2;
            // 
            // cmdModificarTexto
            // 
            this.cmdModificarTexto.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdModificarTexto.Location = new System.Drawing.Point(827, 37);
            this.cmdModificarTexto.Name = "cmdModificarTexto";
            this.cmdModificarTexto.Size = new System.Drawing.Size(69, 23);
            this.cmdModificarTexto.TabIndex = 4;
            this.cmdModificarTexto.Text = "&Modificar";
            this.cmdModificarTexto.UseVisualStyleBackColor = true;
            this.cmdModificarTexto.Click += new System.EventHandler(this.cmdModificarTexto_Click);
            // 
            // tbTextoSustituto
            // 
            this.tbTextoSustituto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTextoSustituto.Location = new System.Drawing.Point(373, 39);
            this.tbTextoSustituto.Name = "tbTextoSustituto";
            this.tbTextoSustituto.Size = new System.Drawing.Size(448, 26);
            this.tbTextoSustituto.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkRed;
            this.button1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(637, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 25);
            this.button1.TabIndex = 10;
            this.button1.Text = "Eliminar Lista";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgListaSustitucion
            // 
            this.dgListaSustitucion.AllowUserToAddRows = false;
            this.dgListaSustitucion.AllowUserToDeleteRows = false;
            this.dgListaSustitucion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgListaSustitucion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.dgListaSustitucion.Location = new System.Drawing.Point(7, 66);
            this.dgListaSustitucion.Name = "dgListaSustitucion";
            this.dgListaSustitucion.ReadOnly = true;
            this.dgListaSustitucion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgListaSustitucion.Size = new System.Drawing.Size(889, 551);
            this.dgListaSustitucion.TabIndex = 6;
            this.dgListaSustitucion.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgListaSustitucion_CellContentClick);
            this.dgListaSustitucion.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgListaSustitucion_RowEnter);
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Texto a buscar";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 400;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Texto a escribir";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 430;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(48, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Lista de sustitución";
            // 
            // cbListaSustitucion
            // 
            this.cbListaSustitucion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbListaSustitucion.FormattingEnabled = true;
            this.cbListaSustitucion.Location = new System.Drawing.Point(208, 9);
            this.cbListaSustitucion.Name = "cbListaSustitucion";
            this.cbListaSustitucion.Size = new System.Drawing.Size(400, 24);
            this.cbListaSustitucion.TabIndex = 1;
            this.cbListaSustitucion.SelectedIndexChanged += new System.EventHandler(this.cbListaSustitucion_SelectedIndexChanged);
            this.cbListaSustitucion.Leave += new System.EventHandler(this.cbListaSustitucion_Leave);
            // 
            // tabDirecciones
            // 
            this.tabDirecciones.Controls.Add(this.tbVarDir);
            this.tabDirecciones.Controls.Add(this.dgVarDir);
            this.tabDirecciones.Controls.Add(this.cmdModificarDir);
            this.tabDirecciones.Controls.Add(this.tbValorDir);
            this.tabDirecciones.Location = new System.Drawing.Point(4, 22);
            this.tabDirecciones.Name = "tabDirecciones";
            this.tabDirecciones.Size = new System.Drawing.Size(899, 654);
            this.tabDirecciones.TabIndex = 5;
            this.tabDirecciones.Text = "Direcciones";
            this.tabDirecciones.UseVisualStyleBackColor = true;
            // 
            // tbVarDir
            // 
            this.tbVarDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVarDir.Location = new System.Drawing.Point(4, 29);
            this.tbVarDir.Name = "tbVarDir";
            this.tbVarDir.Size = new System.Drawing.Size(319, 26);
            this.tbVarDir.TabIndex = 8;
            // 
            // dgVarDir
            // 
            this.dgVarDir.AllowUserToAddRows = false;
            this.dgVarDir.AllowUserToDeleteRows = false;
            this.dgVarDir.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVarDir.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.dgVarDir.Location = new System.Drawing.Point(4, 61);
            this.dgVarDir.Name = "dgVarDir";
            this.dgVarDir.ReadOnly = true;
            this.dgVarDir.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgVarDir.Size = new System.Drawing.Size(891, 581);
            this.dgVarDir.TabIndex = 11;
            this.dgVarDir.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgVarDir_RowEnter);
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Variable";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 200;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Valor";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 600;
            // 
            // cmdModificarDir
            // 
            this.cmdModificarDir.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdModificarDir.Location = new System.Drawing.Point(810, 28);
            this.cmdModificarDir.Name = "cmdModificarDir";
            this.cmdModificarDir.Size = new System.Drawing.Size(84, 27);
            this.cmdModificarDir.TabIndex = 10;
            this.cmdModificarDir.Text = "&Modificar";
            this.cmdModificarDir.UseVisualStyleBackColor = true;
            this.cmdModificarDir.Click += new System.EventHandler(this.cmdModificarDir_Click);
            // 
            // tbValorDir
            // 
            this.tbValorDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbValorDir.Location = new System.Drawing.Point(329, 29);
            this.tbValorDir.Name = "tbValorDir";
            this.tbValorDir.Size = new System.Drawing.Size(474, 26);
            this.tbValorDir.TabIndex = 9;
            // 
            // Configuracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 682);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Configuracion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuracion Xulia";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Configuracion_FormClosing);
            this.Load += new System.EventHandler(this.Configuracion_Load);
            this.SizeChanged += new System.EventHandler(this.Configuracion_SizeChanged);
            this.tabControl1.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVar)).EndInit();
            this.tabModos.ResumeLayout(false);
            this.tabModos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgModos)).EndInit();
            this.tabGramaticas.ResumeLayout(false);
            this.tabGramaticas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgGramaticas)).EndInit();
            this.tabDcitadoGoogle.ResumeLayout(false);
            this.tabDcitadoGoogle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgListaSustitucion)).EndInit();
            this.tabDirecciones.ResumeLayout(false);
            this.tabDirecciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVarDir)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabModos;
        private System.Windows.Forms.TabPage tabDcitadoGoogle;
        private System.Windows.Forms.Button cmdGrabar;
        private System.Windows.Forms.TextBox tbValor;
        private System.Windows.Forms.TextBox tbValorGramatica;
        private System.Windows.Forms.TextBox tbVarGramatica;
        private System.Windows.Forms.TabPage tabGramaticas;
        private System.Windows.Forms.Button Prueba;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbGramaticas;
        private System.Windows.Forms.DataGridView dgGramaticas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Clave;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComandoVoz;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precision;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComandoAyuda;
        private System.Windows.Forms.DataGridViewTextBoxColumn Macro;
        private System.Windows.Forms.DataGridView dgVar;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridView dgModos;
        private System.Windows.Forms.Button cmdModificar;
        private System.Windows.Forms.DataGridView dgListaSustitucion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbListaSustitucion;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Button cmdEliminarSeccion;
        private System.Windows.Forms.Button cmdNuevoComando;
        private System.Windows.Forms.Button cmdModificarTexto;
        private System.Windows.Forms.TextBox tbTextoSustituto;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbVar;
        private System.Windows.Forms.ComboBox tbTexto;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Button cmdBorrarElementoLista;
        private System.Windows.Forms.Button cmdBorrar;
        private System.Windows.Forms.Button cmdActivarEditarModo;
        private System.Windows.Forms.Button cmdActivarModos;
        private System.Windows.Forms.Button cmdEditarVar;
        private System.Windows.Forms.Button cmdActivarSeleccionGramaticas;
        private System.Windows.Forms.Button cmdActivarGridGramaticas;
        private System.Windows.Forms.Button cmdActivarEdicionElementoLista;
        private System.Windows.Forms.Button cmdActivarSeleccionLista;
        private System.Windows.Forms.Button cmdActivarGridLista;
        private System.Windows.Forms.Button cmdEditarComandoGramatica;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button cmdCfgRecVoz;
        private System.Windows.Forms.Button cmdRegarcarConfig;
        private System.Windows.Forms.TabPage tabDirecciones;
        private System.Windows.Forms.TextBox tbVarDir;
        private System.Windows.Forms.DataGridView dgVarDir;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.Button cmdModificarDir;
        private System.Windows.Forms.TextBox tbValorDir;
    }
}
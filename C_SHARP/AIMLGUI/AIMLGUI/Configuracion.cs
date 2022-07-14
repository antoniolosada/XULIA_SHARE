using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AIMLGUI;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace XULIA
{
    public partial class Configuracion : Form
    {
        const string SECCION_DIRECTORIO = "Directorio";
        public ProcesamientoComandos ControlVoz = null;
        const int COMANDO_NUEVO = -2;
        private ConfigXml mCfg;
        string Idioma;
        int FilaSeleccionada = -1;
        int dgListaSustitucion_RowIndex = -1;
        int dgModos_RowIndex = -1;
        int dgVar_RowIndex = -1;
        int dgVarDir_RowIndex = -1;
        Confirmacion frmConfirmacion;
        public struct CommandHelp
        {
            public string Comando;
            public string Valor;
        };
        public struct sDireccion
        {
            public string direccion;
            public string comando;
        };

        List<CommandHelp> ListaComandos = new List<CommandHelp>();
        public Configuracion()
        {
            InitializeComponent();
        }

        private void Configuracion_Load(object sender, EventArgs e)
        {
            CargarConfiguracion();
        }
        public void CargarConfiguracion()
        {
            dgVar.Rows.Clear();
            dgModos.Rows.Clear();
            dgListaSustitucion.Rows.Clear();
            dgGramaticas.Rows.Clear();
            cbListaSustitucion.Items.Clear();
            cbGramaticas.Items.Clear();

            string fic;
            // Usar el path del ejecutable
            fic = Application.StartupPath + @"\XULIA.exe.config";
            // La clase debemos instanciarla indicando el path a usar
            // y opcionalmente si se guarda cada vez que se asigne un valor.
            mCfg = new ConfigXml(fic, true);

            //REcuperamos la información de idioma
            Idioma = mCfg.GetValue("appSettings", "IdiomaGramaticas");
            //Recuperamos la información de configuración
            Hashtable h = mCfg.Claves("appSettings");
            string[] aVar = new string[2];

            //ICollection ckeys;
            //ckeys = h.Keys;
            foreach (DictionaryEntry entry in h)
            {
                string var = entry.Key.ToString();
                if ((var.Substring(0, 1) == "·") || (var.Substring(0, 1) == "$"))
                { //Modos de operación
                    if (Strings.InStr(var, "·SUSTITUCIONES·") > 0)
                    {
                        cbListaSustitucion.Items.Add(entry.Value.ToString());
                    }
                    else if (Strings.Right(var, Idioma.Length) == Idioma)
                    {
                        aVar[0] = entry.Key.ToString();
                        aVar[1] = entry.Value.ToString();
                        dgModos.Rows.Add(aVar);
                    }
                }
                else
                { //Variables
                    if (Strings.Right(var, Idioma.Length) == Idioma)
                    {
                        aVar[0] = entry.Key.ToString();
                        aVar[1] = entry.Value.ToString();
                        dgVar.Rows.Add(aVar);
                    }
                }
            }
            StringCollection secciones = mCfg.Secciones();

            //Cargamos el combobox de gramáticas
            foreach (string s in secciones)
            {
                if ((Strings.InStr(s, "Valores") == 0) &&
                    (Strings.InStr(s, "Precision") == 0) &&
                    (Strings.InStr(s, "Ayuda") == 0) &&
                    (Strings.Right(s, Idioma.Length) == Idioma))
                {
                    string gram = s.Substring(0, s.Length - Idioma.Length);
                    cbGramaticas.Items.Add(gram);
                }

            }
            CargarComandos();
            CargarDirectorio(SECCION_DIRECTORIO);
        }

        private void Prueba_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration((Application.StartupPath + "\\XULIA.exe"));
            AppSettingsSection aps = config.AppSettings;

            //aps.Settings["Cultura"].Value = "hola";
            config.Save(ConfigurationSaveMode.Modified);

            SetValue("hola", "clave", "valor");
        }

        public static void SetValue(string seccion, string clave, string valor)
        {
            System.Configuration.Configuration config =
            //ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationManager.OpenExeConfiguration((Application.StartupPath + "\\XULIA.exe"));

            //Borramos la configuración actual
            config.AppSettings.Settings.Remove(seccion + "." + clave);

            config.Save(ConfigurationSaveMode.Modified);
            //Force a reload of the changed section.
            ConfigurationManager.RefreshSection("appSettings");
            //Grabamos la configuración nueva
            config.AppSettings.Settings.Add(seccion + "." + clave, valor);

            // Save the configuration file.
            config.Save(ConfigurationSaveMode.Modified);
            //Force a reload of the changed section.
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void cbGramaticas_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGramatica();
        }

        public void CargarGramatica()
        {
            if (cbGramaticas.SelectedIndex != -1)
            {
                //Recuperamos la información de configuración
                string Seccion = cbGramaticas.Items[cbGramaticas.SelectedIndex].ToString() + Idioma;
                string SeccionValores = "";
                Hashtable h = mCfg.Claves(Seccion);
                Hashtable hAyuda = new Hashtable();
                Hashtable hPrecision = new Hashtable();
                Hashtable hValores = new Hashtable();
                bool SecAyuda = false;
                bool SecPrecision = false;
                float Precision = 0;

                //ICollection ckeys;
                //ckeys = h.Keys;
                dgGramaticas.Rows.Clear();
                if (h.Count == 0)
                    return;
                SeccionValores = h["SeccionValores"].ToString();
                hValores = mCfg.Claves(SeccionValores);
                if (h.Contains("SeccionAyuda"))
                {
                    SecAyuda = true;
                    hAyuda = mCfg.Claves(h["SeccionAyuda"].ToString());
                }
                if (h.Contains("SeccionPrecision"))
                {
                    SecPrecision = true;
                    hPrecision = mCfg.Claves(h["SeccionPrecision"].ToString());
                }
                if (h.Contains("ValorPrecision"))
                    Precision = Convert.ToInt16(h["ValorPrecision"].ToString()) / 100;

                foreach (DictionaryEntry entry in h)
                {
                    string sPrecision;
                    string sClave = "";
                    string sComandoVoz = "";
                    string sAyuda = "";
                    string sMacro = "";
                    string[] row = new string[5];

                    sPrecision = "";
                    if ((entry.Key.ToString() != "SeccionValores") && (entry.Key.ToString() != "SeccionAyuda") && (entry.Key.ToString() != "SeccionPrecision") && (entry.Key.ToString() != "GramaticaExtendida"))
                    {
                        sClave = entry.Key.ToString();
                        sComandoVoz = entry.Value.ToString();
                        sAyuda = mCfg.RecuperarValorSeccion(hAyuda, sClave, SecAyuda);
                        sPrecision = mCfg.RecuperarValorSeccion(hPrecision, sClave, SecPrecision);
                        if (SeccionValores != "")
                            sMacro = hValores[entry.Key.ToString()].ToString();
                        row[0] = sClave;
                        row[1] = sComandoVoz;
                        row[2] = sPrecision;
                        row[3] = sAyuda;
                        row[4] = sMacro;
                        if (sClave != "")
                            dgGramaticas.Rows.Add(row);
                    }
                }
            }
            else
                dgGramaticas.Rows.Clear();
        }

        private void dgAyuda_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgGramaticas_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            FilaSeleccionada = e.RowIndex;
        }


        private void dgVar_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgVar_RowIndex = e.RowIndex;
            tbVar.Text = dgVar.Rows[e.RowIndex].Cells[0].Value.ToString();
            tbValor.Text = dgVar.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void dgVar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmdGrabar_Click(object sender, EventArgs e)
        {
            if ((tbVar.Text != "") && (tbValor.Text != ""))
            {
                mCfg.SetKeyValue("appSettings", tbVar.Text, tbValor.Text);
                mCfg.Save();
                tbVar.Text = "";
                tbValor.Text = "";
                CargarDirectorio(SECCION_DIRECTORIO);
            }
        }

        private void dgModos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgModos.Rows[dgModos_RowIndex].Cells[0].Value = tbVarGramatica.Text;
            dgModos.Rows[dgModos_RowIndex].Cells[1].Value = tbValorGramatica.Text;
        }

        private void dgModos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            tbVarGramatica.Text = dgModos.Rows[e.RowIndex].Cells[0].Value.ToString();
            tbValorGramatica.Text = dgModos.Rows[e.RowIndex].Cells[1].Value.ToString();
            dgModos_RowIndex = e.RowIndex;
        }
        public void AsignarModoSeleccionado(string valor)
        {
            if (dgModos.Rows[dgModos_RowIndex].Cells[0].Value.ToString() == tbVarGramatica.Text)
                dgModos.Rows[dgModos_RowIndex].Cells[1].Value = valor;
            tbVarGramatica.Text = "";
            tbValorGramatica.Text = "";
        }
        private void cmdModificar_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (tbVarGramatica.Text != "")
            {
                if ((tbVarGramatica.Text.Substring(0, 1) != "·") && (tbVarGramatica.Text.Substring(0, 1) != "$"))
                {
                    lblError.Text = "Los modos de gramáticas deben comenzar por '·' y las gramáticas de aplicación por '$'";
                    return;
                }
                if ((Strings.InStr(tbVarGramatica.Text, "·SUSTITUCIONES·") > 0))
                {
                    //Las listas de sustitución
                    Cursor = Cursors.WaitCursor;
                    mCfg.SetKeyValue("appSettings", tbVarGramatica.Text, tbValorGramatica.Text);
                    mCfg.Save();
                    CargarConfiguracion();
                    Cursor = Cursors.Default;
                }
                else
                {
                    //Para la modificación de gramáticas abrimos el editor
                    frmEditarModo frm = new frmEditarModo();
                    List<string> Gramaticas = new List<string>();
                    List<string> GramSelec = tbValorGramatica.Text.Split(',').ToList<string>();
                    foreach (string s in cbGramaticas.Items)
                        Gramaticas.Add(s);
                    frm.ModificarModo(tbVarGramatica.Text, Gramaticas, GramSelec, this.Handle, mCfg, this);
                }
            }
        }

        void CargarComandos()
        {
            List<string> Comandos = new List<string>();

            //Recuperamos la información de Comandos
            Hashtable h = mCfg.Claves("CommandHelp");
            CommandHelp comando = new CommandHelp();

            foreach (DictionaryEntry entry in h)
            {
                comando.Comando = entry.Key.ToString();
                comando.Valor = entry.Value.ToString();

                ListaComandos.Add(comando);
            }

        }


        private void dgGramaticas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'e')
            {
                LlamadaEditarComando(FilaSeleccionada);
            }
        }

        void LlamadaEditarComando(int row)
        {
            if (row >= 0)
            {
                string Clave = dgGramaticas.Rows[row].Cells[0].Value.ToString();
                string Comandovoz = dgGramaticas.Rows[row].Cells[1].Value.ToString();
                string Precision = dgGramaticas.Rows[row].Cells[2].Value.ToString();
                string Ayuda = dgGramaticas.Rows[row].Cells[3].Value.ToString();
                string Macro = dgGramaticas.Rows[row].Cells[4].Value.ToString();
                EditarComando frmEdit = new EditarComando();

                frmEdit.Editar(cbGramaticas.Text, Clave, Comandovoz, Precision, Ayuda, Macro, ListaComandos, mCfg, Idioma, this);
            }
            else if (row == COMANDO_NUEVO)
            {
                EditarComando frmEdit = new EditarComando();
                frmEdit.Editar(cbGramaticas.Text, "", "", "", "", "", ListaComandos, mCfg, Idioma, this);
            }
        }

        private void dgGramaticas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //LlamadaEditarComando(e.RowIndex);
            //CargarGramatica();
        }

        private void dgGramaticas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbListaSustitucion_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecargarListaSubstitucion();
        }

        void RecargarListaSubstitucion()
        {
            if (cbListaSustitucion.SelectedIndex >= 0)
            {
                //Recuperamos la información de configuración
                string Lista = cbListaSustitucion.Items[cbListaSustitucion.SelectedIndex].ToString();
                Hashtable h = mCfg.Claves(Lista);

                //ICollection ckeys;
                //ckeys = h.Keys;
                dgListaSustitucion.Rows.Clear();

                foreach (DictionaryEntry entry in h)
                {
                    string[] row = new string[2];
                    row[0] = entry.Key.ToString();
                    row[1] = entry.Value.ToString();
                    if (entry.Key.ToString() != "")
                        dgListaSustitucion.Rows.Add(row);
                }
                dgListaSustitucion.Sort(dgListaSustitucion.Columns[0], ListSortDirection.Ascending);
            }
        }

        private void dgListaSustitucion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgListaSustitucion_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgListaSustitucion_RowIndex = e.RowIndex;
            tbTexto.Text = dgListaSustitucion.Rows[e.RowIndex].Cells[0].Value.ToString();
            tbTextoSustituto.Text = dgListaSustitucion.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void cmdModificarTexto_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            mCfg.SetKeyValue(cbListaSustitucion.Text, tbTexto.Text, tbTextoSustituto.Text);
            mCfg.Save();
            tbTexto.Text = "";
            tbTextoSustituto.Text = "";
            RecargarListaSubstitucion();
            Cursor = Cursors.Default;
        }

        private void cmdNuevaSeccion_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmConfirmacion = new Confirmacion();
            frmConfirmacion.MsgConfirmacion(this, "Teclee ELIMINAR para confirmar el boorado", button1_Confirmacion);
        }
        void button1_Confirmacion(string respuesta, Confirmacion frm)
        {
            if (frm.respuesta == "ELIMINAR")
                if (cbListaSustitucion.Text != "")
                {
                    string seccion = cbListaSustitucion.Items[cbListaSustitucion.SelectedIndex].ToString();
                    mCfg.RemoveSection(seccion);
                    cbListaSustitucion.Items.Remove(seccion);
                    dgListaSustitucion.Rows.Clear();
                }
            frm.Dispose();
        }

        private void cmdEliminarSeccion_Click(object sender, EventArgs e)
        {
            frmConfirmacion = new Confirmacion();
            frmConfirmacion.MsgConfirmacion(this, "Teclee ELIMINAR para confirmar el boorado", button1_Confirmacion);
        }
        void cmdEliminarSeccion_Confirmacion(string respuesta, Confirmacion frm)
        {
            if (frm.respuesta == "ELIMINAR")
            {
                if (cbGramaticas.Text != "")
                {
                    string seccion = cbGramaticas.Items[cbGramaticas.SelectedIndex].ToString();
                    mCfg.RemoveSection(seccion + Idioma);
                    mCfg.RemoveSection(seccion + "Valores");
                    mCfg.RemoveSection(seccion + "Ayuda" + Idioma);
                    mCfg.RemoveSection(seccion + "Precision" + Idioma);
                    cbGramaticas.Items.Remove(seccion);
                    CargarConfiguracion();
                }
            }
            frm.Dispose();
        }
        private void cmdNuevoComando_Click(object sender, EventArgs e)
        {
            if (cbGramaticas.Text != "")
                LlamadaEditarComando(COMANDO_NUEVO);
        }

        private void cmdBorrarElementoLista_Click(object sender, EventArgs e)
        {
            frmConfirmacion = new Confirmacion();
            frmConfirmacion.MsgConfirmacion(this, "Teclee SI para confirmar el boorado", cmdBorrarElementoLista_Confirmacion);
        }
        void cmdBorrarElementoLista_Confirmacion(string respuesta, Confirmacion frm)
        {
            if (frm.respuesta == "SI")
            {
                if (tbTexto.Text != "")
                {
                    if (cbListaSustitucion.Text != "")
                    {
                        Cursor = Cursors.WaitCursor;
                        mCfg.cfgDeleteKey(cbListaSustitucion.Text, tbTexto.Text);
                        mCfg.Save();
                        RecargarListaSubstitucion();
                        Cursor = Cursors.Default;
                    }
                }
            }
            frm.Dispose();
        }

        private void cbListaSustitucion_Leave(object sender, EventArgs e)
        {
            RecargarListaSubstitucion();
        }

        private void cbGramaticas_Leave(object sender, EventArgs e)
        {
            CargarGramatica();
        }

        private void cmdBorrar_Click(object sender, EventArgs e)
        {
            frmConfirmacion = new Confirmacion();
            frmConfirmacion.MsgConfirmacion(this, "Teclee SI para confirmar el boorado", cmdBorrar_Confirmacion);
        }
        void cmdBorrar_Confirmacion(string respuesta, Confirmacion frm)
        {
            if (frm.respuesta == "SI")
            {
                if (tbVarGramatica.Text != "")
                {
                    Cursor.Current = Cursors.WaitCursor;
                    mCfg.cfgDeleteKey("appSettings", tbVarGramatica.Text);
                    mCfg.Save();
                    CargarConfiguracion();
                    Cursor.Current = Cursors.Default;
                }
            }
            frm.Dispose();
        }

        private void cmdActivarModos_Click(object sender, EventArgs e)
        {
            dgModos.Focus();
        }

        private void cmdActivarEditarModo_Click(object sender, EventArgs e)
        {
            tbVarGramatica.Focus();
        }

        private void cmdEditarVar_Click(object sender, EventArgs e)
        {
            tbValor.Focus();
        }

        private void cmdActivarGridGramaticas_Click(object sender, EventArgs e)
        {
            dgGramaticas.Focus();
        }

        private void cmdActivarSeleccionGramaticas_Click(object sender, EventArgs e)
        {
            cbGramaticas.Focus();
        }

        private void cmdActivarGridLista_Click(object sender, EventArgs e)
        {
            dgListaSustitucion.Focus();
        }

        private void cmdActivarSeleccionLista_Click(object sender, EventArgs e)
        {
            cbListaSustitucion.Focus();
        }

        private void cmdActivarEdicionElementoLista_Click(object sender, EventArgs e)
        {
            tbTexto.Focus();
        }

        private void cmdEditarComandoGramatica_Click(object sender, EventArgs e)
        {
            LlamadaEditarComando(FilaSeleccionada);
        }

        private void Configuracion_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Impedir que el formulario se cierre pulsando X o Alt + F4 
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    e.Cancel = true;
                    this.Hide();
                    break;
            }

        }

        private void Configuracion_SizeChanged(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void dgGramaticas_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            LlamadaEditarComando(e.RowIndex);
            CargarGramatica();
        }

        private void cmdCfgRecVoz_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "recvoz.lnk";
            p.StartInfo.Arguments = "";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            p.Start();
        }

        private void cmdRegarcarConfig_Click(object sender, EventArgs e)
        {
            if (ControlVoz != null)
                ControlVoz.RecargarConfiguracion();
        }

        public void CargarDirectorio(String SeccionDirectorio)
        {

            //Recuperamos la información de configuración
            Hashtable h = mCfg.Claves(SeccionDirectorio);


            foreach (DictionaryEntry entry in h)
            {
                string sDireccion = "";
                string sComandoVoz = "";
                string[] row = new string[2];

                sDireccion = entry.Key.ToString();
                sComandoVoz = entry.Value.ToString();
                row[0] = sDireccion;
                row[1] = sComandoVoz;
                if (sDireccion != "")
                    dgVarDir.Rows.Add(row);
            }
        }

        private void cmdModificarDir_Click(object sender, EventArgs e)
        {
            if ((tbVarDir.Text != "") && (tbValorDir.Text != ""))
            {
                mCfg.SetKeyValue(SECCION_DIRECTORIO, tbVarDir.Text, tbValorDir.Text);
                mCfg.Save();
                dgVarDir.Rows[dgVar_RowIndex].Cells[0].Value = tbVarDir.Text;
                dgVarDir.Rows[dgVar_RowIndex].Cells[1].Value = tbValorDir.Text;
                tbVarDir.Text = "";
                tbValorDir.Text = "";
            }
        }

        private void dgVarDir_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgVarDir_RowIndex = e.RowIndex;
            tbVarDir.Text = dgVarDir.Rows[e.RowIndex].Cells[0].Value.ToString();
            tbValorDir.Text = dgVarDir.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

    }
}

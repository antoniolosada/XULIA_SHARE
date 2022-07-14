using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using Microsoft.VisualBasic;
using System.Windows.Input;

namespace XULIA
{
    public partial class BuscarComandos : Form
    {
        string Idioma = "";
        ConfigXml mCfg;
        public BuscarComandos()
        {
            InitializeComponent();
        }

        private void BuscarComandos_Load(object sender, EventArgs e)
        {
        }
        public void CargarModos()
        {
 
        }
        public bool CargarGramatica(string gramatica, DataGridView dgGramaticas, string filtro)
        {
            //Recuperamos la información de configuración
            bool HayComandos = false;
            string Seccion = gramatica + Idioma;
            string SeccionValores = "";
            bool SecPrecision = false;
            Hashtable h = mCfg.Claves(Seccion);
            Hashtable hAyuda = new Hashtable();
            Hashtable hPrecision = new Hashtable();
            Hashtable hValores = new Hashtable();
            bool SecAyuda = false;
            float Precision = 0;

            //ICollection ckeys;
            //ckeys = h.Keys;
            if (h.Count == 0)
                return HayComandos;
            SeccionValores = h["SeccionValores"].ToString();
            try
            {
                hValores = mCfg.Claves(SeccionValores);
            }
            catch (Exception e)
            {
                MessageBox.Show("Cargando claves de: " + SeccionValores + ", compruebe claves duplicadas o no existentes", "ERROR");
            }
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
                string sClave = "";
                string sComandoVoz = "";
                string sAyuda = "";
                string sMacro = "";
                string[] row = new string[4];

                if ((entry.Key.ToString() != "SeccionValores") && (entry.Key.ToString() != "SeccionAyuda") && (entry.Key.ToString() != "SeccionPrecision") && (entry.Key.ToString() != "GramaticaExtendida"))
                {
                    sClave = entry.Key.ToString();
                    sComandoVoz = entry.Value.ToString();
                    sAyuda = mCfg.RecuperarValorSeccion(hAyuda, sClave, SecAyuda);
                    try
                    {
                        if (SeccionValores != "")
                            sMacro = hValores[entry.Key.ToString()].ToString();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Valor no encontrado, sección: "+ gramatica + ",clave: " + entry.Key.ToString(), "ERROR");
                    }
                    row[0] = sComandoVoz;
                    row[1] = sAyuda;
                    row[2] = gramatica;
                    row[3] = sMacro;
                    if (sClave != "")
                        if (Strings.InStr(sComandoVoz, filtro) > 0)
                        {
                            HayComandos = true;
                            dgGramaticas.Rows.Add(row);
                        }
                }
            }
            return HayComandos;
        }

        private void lstGramaticas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstGramaticas.SelectedIndex >= 0)
            {
                dgGramaticas.Rows.Clear();
                CargarGramatica(lstGramaticas.Items[lstGramaticas.SelectedIndex].ToString(), dgGramaticas, "");
            }
        }

        private void cmdBuscarComando_Click(object sender, EventArgs e)
        {
            tbComando.BackColor = (IniciarBusquedaComandos()?Color.White: Color.Red);
        }
        private bool IniciarBusquedaComandos()
        {
            bool HayComandos = false;
            dgComandos.Rows.Clear();
            for (int i = 0; i < lstGramaticas.Items.Count; i++)
                HayComandos = (CargarGramatica(lstGramaticas.Items[i].ToString(), dgComandos, tbComando.Text)?true:HayComandos);
                
            return HayComandos;

        }
        public void ActivarBuscarComandos()
        {
            dgComandos.Rows.Clear();
            dgGramaticas.Rows.Clear();
            tbComando.Text = "";
            string fic;
            // Usar el path del ejecutable
            fic = Application.StartupPath + @"\XULIA.exe.config";
            // La clase debemos instanciarla indicando el path a usar
            // y opcionalmente si se guarda cada vez que se asigne un valor.
            mCfg = new ConfigXml(fic, true);

            //REcuperamos la información de idioma
            Idioma = mCfg.GetValue("appSettings", "IdiomaGramaticas");
            //Recuperamos la información de configuración
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
                    lstGramaticas.Items.Add(gram);
                }
            }
            //Recuperamos la información de MODOS
            Hashtable h = mCfg.Claves("appSettings");
            string[] aVar = new string[2];

            //ICollection ckeys;
            //ckeys = h.Keys;
            foreach (DictionaryEntry entry in h)
            {
                string var = entry.Key.ToString();
                if ((var.Substring(0, 1) == "·") || (var.Substring(0, 1) == "$"))
                { //Modos de operación
                    if (Strings.InStr(var, "·SUSTITUCIONES·") == 0)
                    {
                        lstModos.Items.Add(entry.Key.ToString());
                    }
                }
            }
            this.Show();
            AIMLGUI.ProcesamientoComandos.SiempreEncima((int)this.Handle);
        }

        private void BuscarComandos_FormClosing(object sender, FormClosingEventArgs e)
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

        private void BuscarComandos_SizeChanged(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void lstModos_SelectedIndexChanged(object sender, EventArgs e)
        {

            string modo = mCfg.GetValue("appSettings", lstModos.Items[lstModos.SelectedIndex].ToString());
            string[] gram = modo.Split(',');

            lstGramaticaModo.Items.Clear();
            foreach (string s in gram)
            {
                lstGramaticaModo.Items.Add(s);
            }

        }

        private void tbComando_TextChanged(object sender, EventArgs e)
        {
            tbComando.BackColor = Color.White;
        }

        private void tbComando_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 13)
                IniciarBusquedaComandos();
        }
    }
}

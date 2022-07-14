using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace XULIA
{
    public partial class EditarComando : Form
    {
        List<Configuracion.CommandHelp> Comandos;
        Label[] lblPar = new Label[3];
        TextBox[] tbPar = new TextBox[3];
        CfgComando Comando = new CfgComando();
        string Gramatica;
        ConfigXml mCfg;
        string Idioma;
        string m_clave;
        string m_precision;
        string m_ayuda;
        Configuracion frmCfg;
        Confirmacion frmConfirmacion;

        public struct CfgComando
        {
            public string Ayuda;
            public string Separador;
            public string Comando;
            public string TiposParametros;
            public string Parametros;
        }
        public EditarComando()
        {
            InitializeComponent();
        }

        public void Editar(string Gram, string clave, string comandovoz, string precision, string ayuda, string macro, List<Configuracion.CommandHelp> ListaComandos, ConfigXml Cfg, string pIdioma, Configuracion frmConfig)
        {
            Comandos = ListaComandos;
            Gramatica = Gram;
            mCfg = Cfg;
            Idioma = pIdioma;
            m_precision = precision;
            m_ayuda = ayuda;
            m_clave = clave;
            frmCfg = frmConfig;

            frmCfg.Enabled = false;

            tbClave.Text = clave;
            if (clave == "") tbClave.Enabled = true;
            tbComandoVoz.Text = comandovoz;
            tbPrecision.Text = precision;
            tbAyuda.Text = ayuda;
            string[] comandos = macro.Split('|');
            for (int i = 0; i < comandos.Length; i++)
            {
                if (comandos[i] != "")
                    lstMacro.Items.Add(comandos[i]);
            }

            foreach (Configuracion.CommandHelp c in ListaComandos)
                cbComandos.Items.Add(c.Comando);

            this.Show();
        }

        private void EditarComando_Load(object sender, EventArgs e)
        {
            lblPar[0] = lblPar1;
            lblPar[1] = lblPar2;
            lblPar[2] = lblPar3;

            tbPar[0] = tbPar1;
            tbPar[1] = tbPar2;
            tbPar[2] = tbPar3;
        }

        private void cbComandos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selec = cbComandos.Text;
            tbModificador.Text = "";
            Configuracion.CommandHelp comando = Comandos.Find(x => x.Comando == selec);
            if (comando.Comando != null)
            {
                string[] com = comando.Valor.Split('|');
                Comando.Ayuda = com[0];
                Comando.Separador = com[1];
                Comando.Comando = com[2];
                if (com.Length > 3)
                {
                    Comando.TiposParametros = com[3];
                    Comando.Parametros = com[4];
                }
                else
                {
                    Comando.TiposParametros = "";
                    Comando.Parametros = "";
                }
                //Configuramos los cuadros de texto
                lblAyuda.Text = Comando.Ayuda;
                int i = 0;
                if (Comando.TiposParametros.Length > 0)
                {
                    string[] Parametros;
                    Parametros = Comando.Parametros.Split(',');
                    for (i = 0; i < Comando.TiposParametros.Length; i++)
                    {
                        lblPar[i].Visible = true;
                        tbPar[i].Visible = true;
                        tbPar[i].Text = "";
                        lblPar[i].Text = Parametros[i];
                    }
                }
                while (i<3)
                {
                    lblPar[i].Visible = false;
                    tbPar[i].Visible = false;
                    i++;
                }
            }

        }

        private void cmdSubir_Click(object sender, EventArgs e)
        {
            int index = lstMacro.SelectedIndex ;
            if (index > 0)
            {
                object comando = lstMacro.Items[index];
                lstMacro.Items[index] = lstMacro.Items[index - 1];
                lstMacro.Items[index - 1] = comando;
                lstMacro.SelectedIndex -= 1;
            }
            lstMacro.Refresh();
        }

        private void cmdBajar_Click(object sender, EventArgs e)
        {
            int index = lstMacro.SelectedIndex;
            if (index < lstMacro.Items.Count-1)
            {
                object comando = lstMacro.Items[index];
                lstMacro.Items[index] = lstMacro.Items[index + 1];
                lstMacro.Items[index + 1] = comando;
                lstMacro.SelectedIndex += 1;
            }
            lstMacro.Refresh();
        }

        private void cmdBorrar_Click(object sender, EventArgs e)
        {
            int index = lstMacro.SelectedIndex;
            if (index >= 0)
            {
                lstMacro.Items.Remove(lstMacro.Items[index]);
                lstMacro.Refresh();
            }
        }

        private void cmdAgregarComando_Click(object sender, EventArgs e)
        {
            string comando = "";

            if (Comando.Comando != null)
            {
                //Comprobamos los tipos de datos
                for (int i = 0; i < Comando.TiposParametros.Length; i++)
                {
                    string TipoDato = Comando.TiposParametros[i].ToString();

                    if (TipoDato == "i")
                    {
                        bool isNum;
                        double retNum;
                        isNum = Double.TryParse(tbPar[i].Text, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
                        if (!isNum)
                        {
                            MessageBox.Show("El parámetro " + i + " debe ser un número.");
                            return;
                        }
                    }
                    else
                    {
                        if ((tbPar[i].Text.IndexOf('|') >= 0) || (tbPar[i].Text.IndexOf('&') >= 0) || (tbPar[i].Text.IndexOf('<') >= 0) || (tbPar[i].Text.IndexOf('>') >= 0))
                        {
                            MessageBox.Show("La cadena no debe contener barras verticales, ni ampersand, ni signos de > o <");
                            return;
                        }
                    }
                }

                //Insertamos el comando
                comando = cbComandos.Text;
                for (int i = 0; i < Comando.TiposParametros.Length; i++)
                {
                    if (i > 0)
                        comando += Comando.Separador;
                    comando += tbPar[i].Text;
                }

                comando += tbModificador.Text;
            }
            else //No se ha seleccionado ningún comando
                comando = tbModificador.Text;

            if (comando != "")
            {
                lstMacro.Items.Add(comando);
                lstMacro.Refresh();
            }
        }

        private void Grabar_Click(object sender, EventArgs e)
        {
            string valor = "";
            if (tbClave.Text != "")
            {
                Cursor = Cursors.WaitCursor;
                foreach (string v in lstMacro.Items)
                {
                    if (valor != "") valor += "|";
                    valor += v;
                }
                if (valor.Trim() == "")
                {
                    MessageBox.Show("La macro a ejecutar debe contener algún comando");
                    return;
                }

                mCfg.SetKeyValue(Gramatica + Idioma, tbClave.Text, tbComandoVoz.Text);
                mCfg.SetKeyValue(Gramatica + Idioma, "SeccionValores", Gramatica + "Valores");
                mCfg.SetKeyValue(Gramatica + "Valores", tbClave.Text, valor);

                if (tbAyuda.Text != "")
                {
                    mCfg.SetKeyValue(Gramatica + "Ayuda" + Idioma, tbClave.Text, tbAyuda.Text);
                    mCfg.SetKeyValue(Gramatica + Idioma, "SeccionAyuda", Gramatica + "Ayuda" + Idioma);
                }
                else
                    mCfg.cfgDeleteKey(Gramatica + "Ayuda" + Idioma, tbClave.Text);

                if (tbPrecision.Text != "")
                {
                    //Recuperamos el identificador de comandos con precisión individual
                    //Comprobamos que la clave actual se encuentra dentro, sino la introducimos
                    mCfg.SetKeyValue(Gramatica + "Precision" + Idioma, tbClave.Text, tbPrecision.Text);
                    mCfg.SetKeyValue(Gramatica + Idioma, "SeccionPrecision", Gramatica + "Precision" + Idioma);
                }
                else
                    mCfg.cfgDeleteKey(Gramatica + "Precision" + Idioma, tbClave.Text);

                mCfg.Save();
                //frmCfg.CargarConfiguracion();
                frmCfg.CargarGramatica();
                frmCfg.Enabled = true;
                Cursor = Cursors.Default ;
                this.Dispose();
            }
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            frmCfg.Enabled = true;
            this.Dispose();
        }

        private void cmdLimpiar_Click(object sender, EventArgs e)
        {
            cbComandos.SelectedIndex = -1;
            tbModificador.Text = "";
            tbModificador.Focus();
            for (int i = 0; i < 3; i++)
            {
                lblPar[i].Visible = false;
                tbPar[i].Visible = false;
            }
        }

        private void tbModificador_TextChanged(object sender, EventArgs e)
        {
        }

        private void tbModificador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbComandos.Text != "")
            {
                //Solo aceptamos modificadores del comando
                char c = e.KeyChar;
                if ((c != '^') && (c != '%') && (c != '+') && (c != '$'))
                {
                    e.Handled = true;
                }
            }
        }

        private void cmdBorrarComando_Click(object sender, EventArgs e)
        {
            frmConfirmacion = new Confirmacion();
            frmConfirmacion.MsgConfirmacion(this, "Teclee SI para confirmar el boorado", cmdBorrarComando_Confirmacion);
        }
        private void cmdBorrarComando_Confirmacion(string respuesta, Confirmacion frm)
        {
            if (frm.respuesta == "SI")
            {
                if (tbClave.Text != "")
                {
                    Cursor = Cursors.WaitCursor;
                    mCfg.cfgDeleteKey(Gramatica + Idioma, tbClave.Text);
                    mCfg.cfgDeleteKey(Gramatica + "Ayuda" + Idioma, tbClave.Text);
                    mCfg.cfgDeleteKey(Gramatica + "Precision" + Idioma, tbClave.Text);
                    mCfg.cfgDeleteKey(Gramatica + "Valores", tbClave.Text);
                    mCfg.Save();
                    frmCfg.CargarGramatica();
                    frmCfg.Enabled = true;
                    this.Dispose();
                    Cursor = Cursors.Default;
                }
            }
            frm.Dispose();
        }

        private void cmdPegarCoordenadas_Click(object sender, EventArgs e)
        {
            string c = Clipboard.GetText();
            if (c != "")
            { 
                string[] s = c.Split(',');
                for(int i = 0; i< s.Length; i++)
                {
                    if (tbPar[i].Visible )
                        tbPar[i].Text = s[i];
                }

            }
        }

        private void EditarComando_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmCfg.Enabled = true;
        }
    }
}

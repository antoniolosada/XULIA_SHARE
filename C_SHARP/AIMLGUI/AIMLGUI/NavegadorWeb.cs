using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Microsoft.VisualBasic;
using AIMLGUI;

namespace XULIA
{
    public partial class NavegadorWeb : Form
    {
        const int MARGEN_ANCHO = 40;
        const int MARGEN_ALTO = 80;
        string NoLocalizado = "";
        string BuscandoInformacion = "";
        ProcesamientoComandos m_pc;
        List<sBusquedasTexto> Busquedas = new List<sBusquedasTexto>();
        List<sBusquedasQuien> BusquedasQuien = new List<sBusquedasQuien>();
        List<sBusquedasTiempo> BusquedasTiempo = new List<sBusquedasTiempo>();
        bool DocumentoCompleto = false;
        ProcesamientoComandos.Preguntas m_Pregunta;
        public struct sBusquedasTexto
        {
            public string[] texto_ini;
            public string[] texto_fin;
        }
        public struct sBusquedasQuien
        {
            public string Localizador;
            public string[] propiedades;
        }
        public struct sBusquedasTiempo
        {
            public string propiedad;
            public string final_linea;
            public int num_lineas;
            public bool final_linea_incluida;
        }
        public NavegadorWeb()
        {
            InitializeComponent();
        }
        private void btAtras_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void btAdelante_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void btParar_Click(object sender, EventArgs e)
        {
            webBrowser1.Stop();
        }

        private void btActualizar_Click(object sender, EventArgs e)
        {
            webBrowser1.Update();
        }

        private void btInicio_Click(object sender, EventArgs e)
        {
            webBrowser1.GoHome();
        }

        private void btIrA_Click(object sender, EventArgs e)
        {
            rtbRespuesta.Visible = false;
            webBrowser1.Navigate(tbDireccionWeb.Text);
        }

        private void cmdVerDoc_Click(object sender, EventArgs e)
        {
            HtmlDocument document = webBrowser1.Document;
            document.ExecCommand("SelectAll", true, null);
            document.ExecCommand("Copy", false, null); 
        }

        public void Buscar(string pregunta, ProcesamientoComandos pc, int x, int y, ProcesamientoComandos.Preguntas Pregunta)
        {
            m_Pregunta = Pregunta;
            if (Pregunta == ProcesamientoComandos.Preguntas.QueEs)
            {
                m_pc = pc;
                this.Visible = true;
                tbDireccionWeb.Text = "https://www.google.es/webhp?sourceid=chrome-instant&ion=1&espv=2&ie=UTF-8#q=que%20es%20" + pregunta.Replace(" ", "%20");
                DocumentoCompleto = false;
                this.Location = new Point(x, y - this.Height);
                picXulia.Visible = true;
                rtbRespuesta.Visible = false;
                rtbRespuesta.Refresh();
                picXulia.Refresh();
                CargarBusquedaTexto();
                if (BuscandoInformacion != "")
                    m_pc.Hablar(BuscandoInformacion);
                webBrowser1.Navigate(tbDireccionWeb.Text);
            }
            else if (Pregunta == ProcesamientoComandos.Preguntas.QuienEs)
            {
                m_pc = pc;
                this.Visible = true;
                tbDireccionWeb.Text = "https://www.google.es/webhp?sourceid=chrome-instant&ion=1&espv=2&ie=UTF-8#q=quien+es+" + pregunta.Replace(" ", "+");
                DocumentoCompleto = false;
                this.Location = new Point(x, y - this.Height);
                picXulia.Visible = true;
                picXulia.Refresh();
                CargarBusquedaTexto();
                webBrowser1.Navigate(tbDireccionWeb.Text);
            }
            else if (Pregunta == ProcesamientoComandos.Preguntas.QueTiempoHace)
            {
                m_pc = pc;
                this.Visible = true;
                tbDireccionWeb.Text = "https://www.google.es/webhp?sourceid=chrome-instant&ion=1&espv=2&ie=UTF-8#q=que+tiempo+hace+en+" + pregunta.Replace(" ", "+");
                DocumentoCompleto = false;
                this.Location = new Point(x, y - this.Height);
                picXulia.Visible = true;
                picXulia.Refresh();
                CargarBusquedaTexto();
                webBrowser1.Navigate(tbDireccionWeb.Text);
            }
        }
        void CargarBusquedaTexto()
        {
            //Recuperamos la información de Comandos
            Hashtable h = m_pc.cfg.ReadSection ("InfoSearch");
            sBusquedasTexto Busqueda = new sBusquedasTexto();

            Busquedas.Clear();
            BusquedasQuien.Clear();
            BusquedasTiempo.Clear();

            foreach (DictionaryEntry entry in h)
            {
                if (Strings.InStr(entry.Key.ToString(), "que_es") > 0)
                {
                    string[] s = entry.Value.ToString().Split('|');
                    Busqueda.texto_ini = s[0].Split('·');
                    Busqueda.texto_fin = s[1].Split('·');

                    Busquedas.Add(Busqueda);
                }
                else if (Strings.InStr(entry.Key.ToString(), "quien_es") > 0)
                {
                    sBusquedasQuien BusquedaQuien = new sBusquedasQuien();
                    
                    string[] s = entry.Value.ToString().Split('|');
                    BusquedaQuien.Localizador = s[0];
                    BusquedaQuien.propiedades = s[1].Split('·');

                    BusquedasQuien.Add(BusquedaQuien);
                }
                else if (Strings.InStr(entry.Key.ToString(),"no_localizado") > 0)
                {
                    NoLocalizado = entry.Value.ToString();
                }
                else if (Strings.InStr(entry.Key.ToString(),"buscando_informacion") > 0)
                {
                    BuscandoInformacion = entry.Value.ToString();
                }
                else if (Strings.InStr(entry.Key.ToString(),"que_tiempo_hace") > 0 )
                {
                    string[] propiedades = entry.Value.ToString().Split(',');
                    foreach(string pro in propiedades)
                    {
                        sBusquedasTiempo BusquedaTiempo = new sBusquedasTiempo();
                        string[] campos = pro.Split('·');
                        BusquedaTiempo.propiedad = campos[0];
                        BusquedaTiempo.final_linea = campos[1];
                        BusquedaTiempo.num_lineas = (int)Conversion.Val(campos[2]);
                        BusquedaTiempo.final_linea_incluida = (campos[2] == "S" ? true : false);

                        BusquedasTiempo.Add(BusquedaTiempo);
                    }

                }
            }

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            int pos_ini = 0;
            int pos_fin = 0;

            if (!DocumentoCompleto)
            {
                DocumentoCompleto = true;
                picXulia.Visible = false;
                picXulia.Refresh();
                HtmlDocument document = webBrowser1.Document;
                document.ExecCommand("SelectAll", true, null);
                document.ExecCommand("Copy", false, null);
                document.ExecCommand("Unselect", true, null);
                rtbRespuesta.Visible = true;
                rtbRespuesta.Text = "";
                if (m_Pregunta == ProcesamientoComandos.Preguntas.QueEs)
                {
                    bool encontrado = false;
                    if (Clipboard.ContainsText())
                    {
                        pos_ini = 0;
                        pos_fin = 0;
                        string cad = Clipboard.GetText();
                        foreach (sBusquedasTexto b in Busquedas)
                        {
                            foreach (string s in b.texto_ini)
                            {
                                pos_ini = Strings.InStr(cad, s);
                                if (pos_ini > 0)
                                {
                                    cad = cad.Substring(pos_ini - 1 + s.Length);
                                    encontrado = true;
                                    break;
                                }
                            }
                            if (encontrado)
                            {
                                encontrado = false;
                                foreach (string s in b.texto_fin)
                                {
                                    pos_fin = Strings.InStr(cad, s);
                                    if (pos_fin > 0)
                                    {
                                        encontrado = true;
                                        break;
                                    }
                                }
                            }
                            if (encontrado)
                            {
                                string texto = cad.Substring(0, pos_fin - 1);
                                if (texto != "")
                                {
                                    rtbRespuesta.Visible = true;
                                    rtbRespuesta.Text = texto;
                                    m_pc.Hablar(texto);
                                    return;
                                }
                            }
                        }
                    }
                }
                else if (m_Pregunta == ProcesamientoComandos.Preguntas.QuienEs)
                {
                    pos_ini = 0;
                    pos_fin = 0;
                    if (Clipboard.ContainsText())
                    {
                        string cad = Clipboard.GetText();
                        string descripcion = "";
                        foreach (sBusquedasQuien b in BusquedasQuien)
                        {
                            pos_ini = Strings.InStr(cad, b.Localizador);
                            if (pos_ini > 0)
                            {
                                //Localizamos el inicio de la línea donde hemos encontrado el localizador
                                char c;
                                int i = pos_ini-1;
                                c = cad[i];
                                while ((i > 0) && ((cad[i] != 10) && (cad[i] != 13)))
                                    i--;
                                if (i > 0)
                                { //Hemos localizado el inicio de la línea
                                    descripcion = cad.Substring(i+1, pos_ini - i);
                                    cad = cad.Substring(pos_ini - 1 + b.Localizador.Length);
                                    rtbRespuesta.Text = descripcion + Environment.NewLine;
                                    //Localizamos las propiedades
                                    foreach (string s in b.propiedades)
                                    {
                                        pos_ini = Strings.InStr(cad, s);
                                        if (pos_ini > 0)
                                        {
                                            i = pos_ini;
                                            //Localizamos el final de la línea de la propiedad
                                            while ((i < cad.Length) && ((cad[i] != 10) && (cad[i] != 13)))
                                                i++;
                                            if (i < cad.Length)
                                            {
                                                rtbRespuesta.AppendText(cad.Substring(pos_ini - 1, i - pos_ini + 2) + "." + Environment.NewLine + Environment.NewLine);
                                            }
                                        }
                                    }
                                    if (rtbRespuesta.Text != "")
                                    {
                                        m_pc.Hablar(rtbRespuesta.Text);
                                        return;
                                    }
                                }

                            }
                        }
                    }
                }
                else if (m_Pregunta == ProcesamientoComandos.Preguntas.QueTiempoHace)
                {
                    pos_ini = 1;
                    pos_fin = 0;
                    int i;
                    string cad = "";
                    if (Clipboard.ContainsText())
                    {
                        cad = Clipboard.GetText();
                        foreach (sBusquedasTiempo b in BusquedasTiempo)
                        {
                            pos_ini = 1;
                            do
                            {
                                pos_ini = Strings.InStr(pos_ini, cad, b.propiedad);
                                if (pos_ini > 0)
                                {
                                    //Localizamos el inicio de la línea donde hemos encontrado el localizador
                                    i = pos_ini - 1;
                                    if (LocalizarInicioLinea(ref i, cad))
                                    {
                                        if (i == pos_ini - 2)
                                        { //La propiedad comienza la línea
                                            i = pos_ini - 1;
                                            if (b.final_linea == "")
                                            {
                                                if (LocalizarFinLinea(ref i, cad))
                                                {
                                                    string resp = cad.Substring(pos_ini - 1, i - pos_ini);
                                                    rtbRespuesta.AppendText(resp + Environment.NewLine + Environment.NewLine);
                                                }
                                            }
                                            else
                                            {
                                                pos_fin = Strings.InStr(pos_ini, cad, b.final_linea);
                                                if (pos_fin > 0)
                                                {
                                                    string resp = "";
                                                    if (b.final_linea_incluida)
                                                        resp = cad.Substring(pos_ini - 1, pos_fin - pos_ini - 1);
                                                    else
                                                        resp = cad.Substring(pos_ini - 1, pos_fin - pos_ini + b.final_linea.Length + 1);
                                                    rtbRespuesta.AppendText(resp + Environment.NewLine + Environment.NewLine);
                                                }
                                                LocalizarFinLinea(ref i, cad);
                                            }

                                            //Comprobamos si necesitamos leer más líneas
                                            int lineas = b.num_lineas;
                                            while (lineas > 0)
                                            {
                                                pos_ini = i;
                                                i += 2;
                                                if (LocalizarFinLinea(ref i, cad))
                                                {
                                                    string resp = cad.Substring(pos_ini - 1, i - pos_ini);
                                                    rtbRespuesta.AppendText(resp + Environment.NewLine + Environment.NewLine);
                                                    lineas--;
                                                }
                                                else
                                                    break;
                                            }
                                            pos_ini++;
                                        }
                                        else
                                            pos_ini++;
                                    }
                                }
                            } while (pos_ini > 0);
                        }
                    }
                } //else if
            }
            if (rtbRespuesta.Text != "")
            {
                m_pc.Hablar(rtbRespuesta.Text);
                Clipboard.Clear();
                return;
            }
            m_pc.Hablar(NoLocalizado);
        }

        bool LocalizarFinLinea(ref int i, string cad)
        {
            while ((i < cad.Length) && ((cad[i] != 10) && (cad[i] != 13)))
                i++;
            if (i < cad.Length)
                return true;
            else
                return false;
        }
        bool LocalizarInicioLinea(ref int i, string cad)
        {
            while ((i > 0) && ((cad[i] != 10) && (cad[i] != 13)))
                i--;
            if (i > 0)
                return true;
            else
                return false;
        }

        private void NavegadorWeb_Resize(object sender, EventArgs e)
        {
            webBox.Width = this.Width - MARGEN_ANCHO;
            webBox.Height = this.Height - MARGEN_ALTO;
        }

        private void cmdAtras_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void cmdAdelante_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void NavegadorWeb_Load(object sender, EventArgs e)
        {

        }

        private void NavegadorWeb_FormClosing(object sender, FormClosingEventArgs e)
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

    }
}

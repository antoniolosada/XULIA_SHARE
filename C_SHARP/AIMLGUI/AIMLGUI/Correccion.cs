using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using AIMLGUI;

namespace XULIA
{
    public partial class Correccion : Form
    {
        const int ESPACIO_COMPONENTES = 10;
        const int ESPACIO_VERTICAL = 10;
        const int INI_X = 10;
        const int INI_Y = 100;
        const int ALTURA_TB = 20;

        const int TECLA_ESC = 27;
        const int TECLA_ENTER = 13;
        const int TECLA_BORRAR = 8;
        string[] palabras;
        string[] palabrasEntrada;
        string[] palabrasCancelar;
        int Pos = 0;
        ProcesamientoComandos m_pc;

        Label[] tbPalabras;
        Label[] lblPalabras;
        public Correccion()
        {
            InitializeComponent();
        }

        private void Correccion_Load(object sender, EventArgs e)
        {
        }
        void RedibujarFrase()
        {
            //Recuperamos la frase de salida actual
            string salida = palabras[0];
            GrabarLogCorreccion();
            for (int i = 1; i < palabras.Length; i++)
            {
                //Si se ha borrado una palabra se interpreta que no quiere colocarse el espacio que le corresponde
                if (!((palabras[i] == "") && (palabrasEntrada[i] != "")))
                    salida += " " + palabras[i];
            }
            //Eliminamos todos los cotroles
            for (int i = 0; i < tbPalabras.Length; i++)
            {
                tbPalabras[i].Dispose();
                lblPalabras[i].Dispose();
            }
            //Redibujamos todos los controles
            string frase = salida;
            palabras = frase.Split(' ');
            palabrasEntrada = (string[])palabras.Clone();

            DibujarControles(frase, m_pc);
        }
        public void DibujarControles(string frase, ProcesamientoComandos pc)
        {
            int X = INI_X, Y = INI_Y;
            Graphics g = this.CreateGraphics();
            Label tbPalabra;
            Label lblPalabra;
            int AnchoPalabra;

            m_pc = pc;

            if (m_pc.ModoCorreccion != "")
                m_pc.CambioModo(m_pc.ModoCorreccion);

            int alto = Screen.PrimaryScreen.Bounds.Height;
            int ancho = Screen.PrimaryScreen.Bounds.Width;

            this.Width = ancho;
            tbPalabras = new Label[palabras.Length];
            lblPalabras = new Label[palabras.Length];
            for (int i = 0; i < palabras.Length; i++)
            {
                tbPalabra = new Label();
                lblPalabra = new Label();
                tbPalabras[i] = tbPalabra;
                lblPalabras[i] = lblPalabra;
                tbPalabra.Font = new System.Drawing.Font("Arial", 11);
                lblPalabra.Font = new System.Drawing.Font("Arial", 11);
                lblPalabra.BackColor = System.Drawing.Color.Azure;
                tbPalabra.BackColor = System.Drawing.Color.White;
                lblPalabra.BorderStyle = BorderStyle.FixedSingle;
                tbPalabra.BorderStyle = BorderStyle.FixedSingle;

                lblPalabra.Text = i.ToString();
                tbPalabra.Text = palabras[i];
                this.CreateGraphics();
                AnchoPalabra = (int)g.MeasureString(palabras[i], new Font("Arial", 11)).Width + 2 + ESPACIO_COMPONENTES;

                tbPalabra.Width = AnchoPalabra;
                lblPalabra.Width = AnchoPalabra;
                lblPalabra.TextAlign = ContentAlignment.MiddleCenter;

                if (X + AnchoPalabra + INI_X >= ancho - INI_X)
                {
                    int Alto = tbPalabra.Height + lblPalabra.Height + ESPACIO_VERTICAL;
                    Y += Alto;
                    X = INI_X;
                    this.Height += Alto;
                }
                tbPalabra.Location = new System.Drawing.Point(X, Y);
                lblPalabra.Location = new System.Drawing.Point(X, Y - ALTURA_TB);

                X += AnchoPalabra;

                this.Controls.Add(tbPalabra);
                this.Controls.Add(lblPalabra);
            }
        }
        public string Corregir(string frase, ProcesamientoComandos pc)
        {
            palabras = frase.Split(' ');
            palabrasCancelar = (string[])palabras.Clone();
            palabrasEntrada = (string[])palabras.Clone();

            DibujarControles(frase, pc);

            this.Location = new Point(0, 0);
            this.Show();
            ProcesamientoComandos.SiempreEncima((int)this.Handle);
            Interaction.AppActivate("Corrección Xulia");
            return frase;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Correccion_Activated(object sender, EventArgs e)
        {
            tbPos.Focus();
        }

        private void cmdCancelar_Click(object sender, EventArgs e)
        {
            string salida = palabrasCancelar[0];
            for (int i = 1; i < palabrasCancelar.Length; i++)
                salida += " " + palabrasCancelar[i];

            this.Hide();
            SendKeys.SendWait(salida);
        }

        private void tbEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            Pos = (int)Conversion.Val(tbPos.Text);
            if (e.KeyChar == TECLA_ENTER) //enter
            {
                GrabarPalabra();
            }
            else if (e.KeyChar == TECLA_ESC) //ESC
                Limpiar();
        }

        private void tbPos_TextChanged(object sender, EventArgs e)
        {
            Pos = (int)Conversion.Val(tbPos.Text);
            if (tbPos.Text.Length > 1)
            {
                if (palabras.Length > Pos)
                {
                    tbEdit.Text = palabras[Pos];
                    tbPos.Enabled = false;
                    tbEdit.Focus();
                }
                else
                    tbPos.Text = "";
            }
        }

        private void tbPos_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Pos = (int)Conversion.Val(tbPos.Text);
            if ((e.KeyChar == 13))
            {
                if (palabras.Length > Pos)
                {
                    tbEdit.Text = palabras[Pos];
                    tbEdit.Focus();
                }
                else
                    tbPos.Text = "";
            }
            else if (e.KeyChar == TECLA_ESC)
                GrabarFrase();
            else if (!(((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (e.KeyChar == TECLA_BORRAR)))
            {
                e.Handled = true;
            }
        }

        private void tbEdit_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmdGrabar_Click(object sender, EventArgs e)
        {
            GrabarFrase();
        }
        void GrabarFrase()
        {
            string salida = palabras[0];
            for (int i = 1; i < palabras.Length; i++)
            {
                //Si se ha borrado una palabra se interpreta que no quiere colocarse el espacio que le corresponde
                if (!((palabras[i] == "") && (palabrasEntrada[i] != "")))
                    salida += " " + palabras[i];
            }
            this.Hide();
            if (chkSinDoblesEspacios.Checked)
                salida = SinDoblesEspacios(salida);
            SendKeys.SendWait(FormatearCaracteresEspeciales(salida));
            GrabarLogCorreccion();
        }

        private void cmdLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Correccion_Deactivate(object sender, EventArgs e)
        {
            if (m_pc.ModoCorreccion != "")
                m_pc.CambioModo(m_pc.ModoActualCorreccion);
        }

        private void cmdGrabarPalabra_Click(object sender, EventArgs e)
        {
            GrabarPalabra();
        }

        void GrabarPalabra()
        {
            palabras[Pos] = tbEdit.Text;
            if (palabras[Pos] != palabrasEntrada[Pos])
            { //La palabra se ha modificado
                tbPalabras[Pos].Text = tbEdit.Text;
                tbEdit.Text = "";
                tbPos.Text = "";
                RedibujarFrase();
                tbPos.Enabled = true;
                tbPos.Focus();
            }
        }

        void Limpiar()
        {
            tbEdit.Text = "";
            tbPos.Text = "";
            tbPos.Enabled = true;
            tbPos.Focus();
        }

        private void cmdUnaMayuscula_Click(object sender, EventArgs e)
        {
            if (tbEdit.Text != "")
            {
                string cad = tbEdit.Text;
                tbEdit.Text = cad.Substring(0, 1).ToUpper() +cad.Substring(1).ToLower();
            }
        }

        private void cmdTodasMayusculas_Click(object sender, EventArgs e)
        {
            if (tbEdit.Text != "")
            {
                string cad = tbEdit.Text;
                tbEdit.Text = cad.ToUpper();
            }
        }

        private void cmdMinusculas_Click(object sender, EventArgs e)
        {
            if (tbEdit.Text != "")
            {
                string cad = tbEdit.Text;
                tbEdit.Text = cad.ToLower();
            }
        }

        string FormatearCaracteresEspeciales(string cad)
        {
            string salida = "";
            for (int i=0; i < cad.Length; i++)
            {
                switch (cad[i])
                {
                    case '{':
                    case '}':
                    case '(':
                    case ')':
                    case '+':
                    case '%':
                    case '^':
                        salida += "{" + cad[i] + "}";
                            break;
                    default:
                            salida += cad[i];
                            break;
                }
            }
            return salida;
        }

        private void cmdCorregirMayusculas_Click(object sender, EventArgs e)
        {
            bool punto = false;
            for (int i = 1; i < palabras.Length; i++)
            {
                string cad = palabras[i];
                string salida = "";
                int pos = cad.IndexOf('.');

                if (punto)
                {
                    if (cad.Trim() != "")
                    { 
                        punto = false;
                        string tmp = cad.Substring(0, 1).ToUpper();
                        if (cad.Length > 1)
                            tmp += cad.Substring(1).ToLower();
                        cad = tmp;
                    }
                }
                if (pos >= 0)
                { 
                    salida = cad.Substring(0,pos+1);
                    if (pos + 1 < cad.Length)
                    {
                        salida += cad.Substring(pos + 1, 1).ToUpper();
                        if (pos + 2 < cad.Length)
                            salida += cad.Substring(pos + 2).ToLower();
                        //Si el punto no finaliza la palabra, pero solo hay espacios también se activa el punto
                        if (cad.Substring(pos + 1).Trim() == "")
                            punto = true;
                    }
                    else //Si el punto finaliza la palabra activamos punto
                        punto = true;

                    palabras[i] = salida;
                    tbPalabras[i].Text = salida;
                }
            }
        }

        string SinDoblesEspacios(string salida)
        {
            string tmp = "";
            bool espacio = false;
            for (int i = 0; i < salida.Length; i++)
            {
                if (salida[i] == ' ')
                {
                    if (!espacio)
                    {
                        espacio = true;
                        tmp += " ";
                    }
                }
                else
                {
                    //Quitamos los espacios antes de signos de puntuación
                    switch (salida[i])
                    { 
                        case '.':
                        case ',':
                        case ':':
                        case ';':
                            tmp += Strings.RTrim(tmp)+salida[i];
                            break;
                        default:
                            tmp += salida[i];
                            break;
                    }
                    espacio = false;
                }
            }
            return tmp;
        }

        void GrabarLogCorreccion()
        {
            if (m_pc.GrabarCoreccion == "S")
            {
                string salida = palabras[0];
                for (int i = 1; i < palabras.Length; i++)
                {
                    //Si se ha borrado una palabra se interpreta que no quiere colocarse el espacio que le corresponde
                    if (!((palabras[i] == "") && (palabrasEntrada[i] != "")))
                        salida += " " + palabras[i];
                }
                string entrada = palabrasEntrada[0];
                for (int i = 1; i < palabrasEntrada.Length; i++)
                    entrada += " " + palabrasEntrada[i];

                System.IO.StreamWriter sw;
                sw = new System.IO.StreamWriter("C:\\TMP\\CORRECCION.TXT", true, Encoding.Default);
                sw.WriteLine("Entrada->" + entrada);
                sw.WriteLine("Salida-->" + salida);
                sw.WriteLine(".");
                sw.Close();

            }
        }
    }
}

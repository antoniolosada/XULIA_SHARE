using AIMLGUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XULIA
{
    public partial class frmGPT : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        private const int WM_VSCROLL = 0x115;
        private const int SB_BOTTOM = 7;
        ProcesamientoComandos pComandos;
        public frmGPT()
        {
            InitializeComponent();
            //SetRoundedRegion();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void SetRoundedRegion()
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(new Rectangle(0, 0, 30, 30), 180, 90);
            path.AddLine(30, 0, this.Width - 30, 0);
            path.AddArc(new Rectangle(this.Width - 30, 0, 30, 30), -90, 90);
            path.AddLine(this.Width, 30, this.Width, this.Height - 30);
            path.AddArc(new Rectangle(this.Width - 30, this.Height - 30, 30, 30), 0, 90);
            path.AddLine(this.Width - 30, this.Height, 30, this.Height);
            path.AddArc(new Rectangle(0, this.Height - 30, 30, 30), 90, 90);
            path.CloseFigure();
            this.Region = new Region(path);
        }

        public void MostrarGPT(ProcesamientoComandos pc)
        {
            pComandos = pc;
            this.Show();
            pbPensando.Visible=false;
            tbGPT.Focus();
        }

        static bool LlamadaFuncion = false;
        static string Funcion = "";
        public async Task<string> RespuestaGPT(string texto)
        {

            pbPensando.Visible = false;
            EscribirTextoConColor(texto, Color.Black);
            SendMessage(tbSalidaGPT.Handle, WM_VSCROLL, (IntPtr)SB_BOTTOM, IntPtr.Zero);

            int pos = texto.IndexOf("<call>");
            if (pos > -1)
            {
                LlamadaFuncion = true;
                texto = texto.Substring(pos + 6);
            }
            pos = texto.IndexOf("</call>");
            if (pos > -1)
            {
                LlamadaFuncion = false;
                Funcion += texto.Substring(0, pos - 1);
            }

            if (!LlamadaFuncion && pComandos.GPT_Voz)
                pComandos.Hablar(texto);
            else if (LlamadaFuncion)
                Funcion += texto;

            return texto;
        }

        public void Pregunta(string texto)
        {
            tbGPT.Text = texto;
        }
        public void ResponderPregunta()
        {
            pbPensando.Visible = true;
            LlamarGPT();
        }

        private void cmdGPT_Click(object sender, EventArgs e)
        {
            ResponderPregunta();
        }

        private void frmGPT_Load(object sender, EventArgs e)
        {

        }

        private void tbGPT_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == Convert.ToChar(13)) LlamarGPT();
        }
        private void EscribirTextoConColor(string texto, Color color)
        {
            tbSalidaGPT.SelectionStart = tbSalidaGPT.TextLength;
            tbSalidaGPT.SelectionLength = 0;
            tbSalidaGPT.SelectionColor = color;
            tbSalidaGPT.AppendText(texto);
            tbSalidaGPT.SelectionColor = tbSalidaGPT.ForeColor; // Restablecer el color
        }
        public void EjecFuncion(bool ejec)
        {
            picEjecFuncion.Visible = ejec;
        }
        async void LlamarGPT()
        {
            EscribirTextoConColor(Environment.NewLine + string.Concat(Enumerable.Repeat("-", 100)), Color.Green);
            EscribirTextoConColor(Environment.NewLine + tbGPT.Text, Color.Blue);
            tbSalidaGPT.Refresh();
            await pComandos.GPT(tbGPT.Text, RespuestaGPT);
            tbGPT.Text = "";
            if (Funcion != "")
            {
                pComandos.LlamarFuncion(Funcion);
                Funcion = "";
            }
        }

        private void cmdCerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

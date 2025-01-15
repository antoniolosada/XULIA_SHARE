using AIMLGUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
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
            ProcesamientoComandos.SetForegroundWindow(this.Handle);
            pbPensando.Visible=false;
            tbGPT.Focus();
        }

        static bool LlamadaFuncion = false;
        static string Funcion = "";
        public async Task<string> RespuestaGPT(string input)
        {
            string texto = input;

            pbPensando.Visible = false;

            int pos = texto.IndexOf("<call>");
            if (pos > -1)
            {
                LlamadaFuncion = true;
                texto = texto.Substring(pos + 6);
            }

            if (LlamadaFuncion)
                EscribirTextoConColor(input, Color.Green);
            else
                EscribirTextoConColor(input, Color.Black);

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

            SendMessage(tbSalidaGPT.Handle, WM_VSCROLL, (IntPtr)SB_BOTTOM, IntPtr.Zero);
            tbSalidaGPT.Refresh();
            Application.DoEvents();

            return texto;
        }

        public void Pregunta(string texto)
        {
            tbGPT.Text = texto;
            EnviarPrompt(tbGPT.Text);
        }
        public void ResponderPregunta()
        {
            pbPensando.Visible = true;
            LlamarGPT();
        }

        private void cmdGPT_Click(object sender, EventArgs e)
        {
            EnviarPrompt(tbGPT.Text);
        }

        public void EnviarPrompt(string prompt)
        {
            tbGPT.Text = prompt;
            ResponderPregunta();
        }

        private void frmGPT_Load(object sender, EventArgs e)
        {

        }

        private void tbGPT_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
        private void EscribirTextoConColor(string texto, Color color)
        {
            tbSalidaGPT.SelectionStart = tbSalidaGPT.TextLength;
            tbSalidaGPT.SelectionLength = 0;
            tbSalidaGPT.SelectionColor = color;
            tbSalidaGPT.SelectionFont = new Font(tbSalidaGPT.Font.FontFamily, 16);
            tbSalidaGPT.AppendText(texto);
            tbSalidaGPT.SelectionColor = tbSalidaGPT.ForeColor; // Restablecer el color
            tbGPT.Focus();
        }
        public void EjecFuncion(bool ejec)
        {
            picEjecFuncion.Visible = ejec;
        }
        async void LlamarGPT()
        {
            EscribirTextoConColor(Environment.NewLine + string.Concat(Enumerable.Repeat("_", 63)), Color.Green);
            EscribirTextoConColor(Environment.NewLine + tbGPT.Text + Environment.NewLine, Color.Blue);
            tbSalidaGPT.Refresh();
            Application.DoEvents(); 
            await pComandos.callGPT.GPT(tbGPT.Text, RespuestaGPT);
            tbGPT.Text = "";
            if (Funcion != "")
            {
                string salida_funcion = pComandos.callGPT.LlamarFuncion(Funcion);
                if (salida_funcion != "") EnviarPrompt(salida_funcion);
                Funcion = "";
            }
        }

        private void cmdCerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //pComandos.webdriver.AbrirFIDES(SeleniumWeb.eFuncionesFides.Baremo,"alosgon","DaniXulia1082.","34996197H");

            //pComandos.webdriver.AbrirMatrhix(SeleniumWeb.eFuncionesMatrhix.InformesAlmacenados, "34996197H", "logos1060", "34996197H", "FCC070000015770070");

            //pComandos.webdriver.AbrirMorfeo(SeleniumWeb.eFuncionesMorfeo.ValidarPermisos, "34996197H", "logos1060");
            pComandos.webdriver.AbrirMoura(SeleniumWeb.eFuncionesMoura.ConsultarSolicitudGestion, "34996197H", "logos1060", "DXFP SSCC", "67492");
            

        }
    }
}

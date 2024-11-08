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

        public async Task<string> RespuestaGPT(string texto)
        {
            pbPensando.Visible = false;
            tbSalidaGPT.Text = tbSalidaGPT.Text + Environment.NewLine+ texto;
            SendMessage(tbSalidaGPT.Handle, WM_VSCROLL, (IntPtr)SB_BOTTOM, IntPtr.Zero);

            if (pComandos.GPT_Voz)
                pComandos.Hablar(texto);

            return texto;
        }

        public void Pregunta(string texto)
        {
            tbGPT.Text = texto;
        }
        void ResponderPregunta()
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

        void LlamarGPT()
        {
            tbSalidaGPT.Text = tbSalidaGPT.Text + Environment.NewLine + string.Concat(Enumerable.Repeat("-", 200)) +
                                    Environment.NewLine + tbGPT.Text+ Environment.NewLine;
            pComandos.GPT(tbGPT.Text, RespuestaGPT);
            tbGPT.Text = "";

        }

        private void cmdCerrar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

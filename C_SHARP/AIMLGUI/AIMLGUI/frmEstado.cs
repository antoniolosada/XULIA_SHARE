using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Configuration;
using System.Runtime.InteropServices;
using XULIA;


namespace AIMLGUI
{
    public partial class frmEstado : Form
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        const int MYACTION_HOTKEY_ID = 1;
        aimlForm frmXulia;
        ProcesamientoComandos ControlVoz;
        public frmEstado()
        {
            InitializeComponent();
            // Modifier keys codes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8
            // Compute the addition of each combination of the keys you want to be pressed
            // ALT+CTRL = 1 + 2 = 3 , CTRL+SHIFT = 2 + 4 = 6...
            RegisterHotKey(this.Handle, MYACTION_HOTKEY_ID, 6, (int)'X');
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == MYACTION_HOTKEY_ID)
            {
                // My hotkey has been typed

                // Do what you want here
                // ...
                this.Visible = true;
            }
            base.WndProc(ref m);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void ActualizaPantalla(ProcesamientoComandos pc, string Comando, string ClaseVetnana, string TituloVentana)
        {
            lblAcento.Visible = pc.ACENTO || pc.ACENTO_CIRCUNFLEJO || pc.ACENTO_GRAVE || pc.DIERESIS || pc.TILDE;
            lblMAY.Visible = pc.MAYUSCULAS;

            lblControl.Visible = pc.CONTROL;
            lblShift.Visible = pc.SELECCION;
            lblALT.Visible = pc.ALT;
            pbActivo.Visible = !pc.XULIA_ACTIVA;
            lblAviso.Visible = pc.Aviso;
            lblCodTeclaPresionado.Visible = (pc.PressKey.Count > 0);

            tbModo.BackColor = ProcesamientoComandos.OkXulia ? Color.Green : Color.FromArgb(255,64,64,64);

            tbClase.Text = ClaseVetnana;
            tbTitulo.Text = TituloVentana;
            if (Comando != "")
                tbComando.Text = Comando + " (" + pc.precision.ToString().Substring(0,3) + "%)";
            tbModo.Text = pc.MODO;

            tbNumero.Text = pc.Multiplicador.ToString();

            Visible = true;
            if (pc.OcultarEstadoSegundos > 0)
            {
                tmrOcultar.Interval = pc.OcultarEstadoSegundos * 1000;
                tmrOcultar.Enabled = true;
            }

        }
        public void ActualizarComando(string comando)
        {
            tbComando.Text = comando;
        }
        private void tmrOcultar_Tick(object sender, EventArgs e)
        {
            Visible = false;
            tmrOcultar.Enabled = false;
        }

        private void frmEstado_Activated(object sender, EventArgs e)
        {
            //tmrOcultar.Enabled = true;

        }
        public void IconoEspera(bool Estado)
        {
            picEspera.Visible = Estado;
            picEspera.Refresh();
        }

        private void pbarPrecision_Click(object sender, EventArgs e)
        {

        }

        public void AsignarPrecision(float precision)
        {
            pbarPrecision.Value = (int)(precision * 100);
            pbarPrecision.Refresh();
        }
        public void OcultarBarraPrecision()
        {
            pbarPrecision.Visible = false;
        }
        public void MostrarBarraPrecision()
        {
            pbarPrecision.Visible = true;
        }
        public void ComandoEncontrado(ProcesamientoComandos.Comandos valor)
        {
            if (valor == ProcesamientoComandos.Comandos.ComandoEncontrado )
                tbComando.ForeColor = System.Drawing.Color.LightGreen;
            else if (valor == ProcesamientoComandos.Comandos.ComandoNoEncontrado)
                tbComando.ForeColor = System.Drawing.Color.LightYellow;
            else if (valor == ProcesamientoComandos.Comandos.ComandoNoPreciso)
                tbComando.ForeColor = System.Drawing.Color.LightSalmon;
            tbComando.Refresh();
        }

        private void frmEstado_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void frmEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void frmEstado_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Impedir que el formulario se cierre pulsando X o Alt + F4 
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    e.Cancel = true;
                    break;
            }  

        }

        private void frmEstado_Load(object sender, EventArgs e)
        {

        }

        private void frmEstado_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Location.X > 10) && (e.Location.X < 40) && (e.Location.Y > 10) && (e.Location.Y < 40))
            {
                ControlVoz.DesactivarXulia();
            }
            else if (!(frmXulia == null))
                frmXulia.Show();
        }

        public void EnlaceFormXulia(aimlForm frm, ProcesamientoComandos pc)
        {
            frmXulia = frm;
            ControlVoz = pc;
        }

        private void pbActivo_MouseDown(object sender, MouseEventArgs e)
        {
            ControlVoz.ActivarXulia();
        }

        private void tmrDesactivar_Tick(object sender, EventArgs e)
        {

        }

        private void frmEstado_SizeChanged(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void picEspera_Click(object sender, EventArgs e)
        {

        }
        public void MostrarIconoDesactivarXulia(bool estado)
        {
            pbActivo.Visible = estado;
        }

        private void tmrDesactivacionAutomatica_Tick(object sender, EventArgs e)
        {
            tmrDesactivacionAutomatica.Enabled = false;
            ControlVoz.DesactivarXulia();
        }
        public void ActivarTimerDesactivacion()
        {
                tmrDesactivacionAutomatica.Enabled = false;
                Application.DoEvents();
                tmrDesactivacionAutomatica.Enabled = true;
        }
        public void DesactivarTimerDesactivacion()
        {
            tmrDesactivacionAutomatica.Enabled = false;
        }

        static DateTime UltimoControl = DateTime.Now;
        private void tmrResetRecVoz_Tick(object sender, EventArgs e)
        {
            long Minutos = DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Minute, DateTime.Now, UltimoControl);
            if (Minutos > ControlVoz.MinutosResetControlVoz)
                ControlVoz.ResetearControlVoz();
            UltimoControl = DateTime.Now;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace AIMLGUI
{
    public partial class frmAyuda : Form
    {
        ProcesamientoComandos pc;
        public frmAyuda()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void MostrarAyuda(ProcesamientoComandos pcom)
        {
            pc = pcom;
            lstGramaticas.Items.Clear();
            lstGramaticasDesactivas.Items.Clear();
            foreach (string g in pc.BuscarNombresGramaticas(true))
                lstGramaticas.Items.Add(g);
            foreach (string g in pc.BuscarNombresGramaticas(false))
                lstGramaticasDesactivas.Items.Add(g);
            Visible = true;
            this.Focus();
            lstGramaticas.Focus();
            lstGramaticas.SelectedIndex = 0;
            ProcesamientoComandos.SiempreEncima((int)this.Handle);
        }

        private void lstGramaticas_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarElementosGramatica(lstGramaticas.Items[lstGramaticas.SelectedIndex].ToString());
        }

        private void lstElementos_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstComandos.SelectedIndex = lstElementos.SelectedIndex;
            lstAyuda.SelectedIndex = lstElementos.SelectedIndex;

            lstComandos.TopIndex = lstElementos.TopIndex;
            lstAyuda.TopIndex = lstElementos.TopIndex;
        }

        private void lstGramaticasDesactivas_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarElementosGramatica(lstGramaticasDesactivas.Items[lstGramaticasDesactivas.SelectedIndex].ToString());
        }
        void CargarElementosGramatica(string g)
        {
            List<string> Elementos = pc.BuscarDescripcionElementosGramatica(g);

            Elementos.Sort();
            lstElementos.Items.Clear();
            lstComandos.Items.Clear();
            lstAyuda.Items.Clear();
            foreach (string elemento in Elementos)
            {
                string[] s = elemento.Split(Constants.vbTab[0]);
                lstElementos.Items.Add(s[0]);
                lstComandos.Items.Add(s[1]);
                lstAyuda.Items.Add(s[2]);
            }
        }

        private void frmAyuda_FormClosing(object sender, FormClosingEventArgs e)
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

        private void frmAyuda_SizeChanged(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }
    }
}

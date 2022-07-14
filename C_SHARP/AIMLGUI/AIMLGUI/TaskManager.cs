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
    public partial class TaskManager : Form
    {
        VentanasActivas mVentanas; 
        public TaskManager()
        {
            InitializeComponent();
        }

        void Recargar()
        {
            Cursor = Cursors.WaitCursor;
            lstAplicaciones.Items.Clear();
            lstAplicaciones.Refresh();
            mVentanas.CargarVentanas();
            mVentanas.CargarVentanasLista(lstAplicaciones);
            Cursor = Cursors.Default;
        }
        private void cmdVerAplicaciones_Click(object sender, EventArgs e)
        {
            Recargar();
        }

        public void MostrarVentanasActivas(VentanasActivas Ventanas)
        {
            mVentanas=Ventanas;
            this.Show();
            if (lstAplicaciones.Items.Count == 0)
            {
                this.Refresh();
                Recargar();
            }
            lstAplicaciones.Focus();
        }

        private void cmdActivar_Click(object sender, EventArgs e)
        {
            SeleccionarAplicacion();
        }

        private void TaskManager_Activated(object sender, EventArgs e)
        {
            lstAplicaciones.Focus();
        }

        private void lstAplicaciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == Constants.vbCr)
                SeleccionarAplicacion();
            if (e.KeyChar.ToString().ToUpper() == "R")
            {
                lstAplicaciones.Items.Clear();
                lstAplicaciones.Refresh();
                mVentanas.CargarVentanas();
                mVentanas.CargarVentanasLista(lstAplicaciones);
            }
        }

        void SeleccionarAplicacion()
        {
            string titulo = lstAplicaciones.Items[lstAplicaciones.SelectedIndex].ToString();
            titulo = titulo.Substring(titulo.IndexOf("-") + 2);
            try
            {
                int Numero = (int)Conversion.Val(lstAplicaciones.Items[lstAplicaciones.SelectedIndex].ToString());
                mVentanas.ActivarVentana(Numero);
                //Interaction.AppActivate(titulo);
            } catch{}

            this.Hide();
        }

        private void lstAplicaciones_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void TaskManager_FormClosing(object sender, FormClosingEventArgs e)
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

        private void cmdRenumerar_Click(object sender, EventArgs e)
        {
            mVentanas.RenumerarVentanasLista(lstAplicaciones);
        }

        private void TaskManager_Load(object sender, EventArgs e)
        {

        }

        private void TaskManager_SizeChanged(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }
    }
}

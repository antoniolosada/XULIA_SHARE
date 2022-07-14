using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XULIA
{
    public partial class frmEditarModo : Form
    {
        List<string> Gramaticas;
        List<string> GramSeleccion;
        bool m_Salir;
        string m_Modo = "";
        ConfigXml m_mCfg;
        Configuracion m_frmCfg;
        public frmEditarModo()
        {
            InitializeComponent();
        }
        public void ModificarModo(string Modo, List<string> Gram, List<string> Seleccion, IntPtr Handle, ConfigXml mCfg, Configuracion Cfg)
        {
            m_frmCfg = Cfg;
            m_mCfg = mCfg;
            m_Modo = Modo;
            m_frmCfg.Enabled = false;
            List<string> SelGram = new List<string>();
            Gramaticas = Gram;
            GramSeleccion = Seleccion;
            CargarGramaticas(Gram, Seleccion);
            this.Show();
        }
        public void CargarGramaticas(List<string> Gram, List<string> Eliminar)
        {
            CargarListas(Gram, lstGramaticas, Eliminar);
            lstSeleccion.Items.Clear();
            CargarListas(Eliminar, lstSeleccion , new List<string>());
        }

        void CargarListas(List<string> Gramaticas, ListBox lst, List<string> Eliminar)
        {
            lst.Items.Clear();
            foreach (string s in Gramaticas)
            {
                if (!Eliminar.Exists(x => x == s))
                    lst.Items.Add(s);
            }
        }
        private void cmdAgregar_Click(object sender, EventArgs e)
        {
            Agregar(lstGramaticas, lstSeleccion);
        }
        private void cmdQuitar_Click(object sender, EventArgs e)
        {
            Agregar(lstSeleccion, lstGramaticas);
        }

        void Agregar(ListBox lstOrigen, ListBox lstDestino)
        {
            int index = lstOrigen.SelectedIndex;
            if (lstOrigen.SelectedIndex != -1)
            {
                lstDestino.Items.Add(lstOrigen.Items[index].ToString());
                lstOrigen.Items.Remove(lstOrigen.Items[index]);
                if (lstOrigen.Items.Count > 0)
                {
                    if (index < lstOrigen.Items.Count)
                        lstOrigen.SelectedIndex = index;
                    else
                        lstOrigen.SelectedIndex = lstOrigen.Items.Count - 1;
                }
            }
        }

        private void cmdAgregarTodo_Click(object sender, EventArgs e)
        {
            CargarListas(Gramaticas, lstSeleccion, new List<string>());
            lstGramaticas.Items.Clear();
        }

        private void cmdEliminar_Click(object sender, EventArgs e)
        {
            CargarListas(Gramaticas, lstGramaticas, GramSeleccion);
            lstSeleccion.Items.Clear();
        }

        private void cmdGrabar_Click(object sender, EventArgs e)
        {
            string salida = "";

            foreach (string s in lstSeleccion.Items)
                salida = salida + "," + s;
            if (salida != "")
                salida = salida.Substring(1);

            if (salida.Trim() == "")
            {
                MessageBox.Show("Debe asignar algún valor");
                return;
            }
            else
            {
                m_mCfg.SetKeyValue("appSettings", m_Modo, salida);
                Cursor.Current = Cursors.WaitCursor;
                m_mCfg.Save();
                m_frmCfg.CargarConfiguracion();
                Cursor.Current = Cursors.Default;
            }
            m_frmCfg.Enabled = true;
            this.Dispose();
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            m_frmCfg.Enabled = true;
            m_Salir = true;
            this.Hide();
        }

    }

}

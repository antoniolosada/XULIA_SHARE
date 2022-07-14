using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XULIA
{
    public partial class Confirmacion : Form
    {
        Form m_padre;
        public string respuesta;
        public Confirmacion()
        {
            InitializeComponent();
        }
        public delegate void cmdBorrarElementoLista_Click(string respuesta, Confirmacion frm);
        cmdBorrarElementoLista_Click m_BorrarFuncion;
        public void MsgConfirmacion(Form padre, string msg, cmdBorrarElementoLista_Click BorrarElemento)
        {
            m_padre = padre;
            m_BorrarFuncion = BorrarElemento;
            padre.Enabled = false;
            lblMsg.Text = msg;
            this.Show();
        }


        private void Confirmacion_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Aceptar();
        }

        void Aceptar()
        {
            m_padre.Enabled = true;
            respuesta = tbRespuesta.Text;
            m_BorrarFuncion(respuesta, this);
        }

        private void cmdCancelar_Click(object sender, EventArgs e)
        {
            m_padre.Enabled = true;
            respuesta = "";
            m_BorrarFuncion(respuesta, this);
        }

        private void tbRespuesta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                Aceptar();
        }
    }
}

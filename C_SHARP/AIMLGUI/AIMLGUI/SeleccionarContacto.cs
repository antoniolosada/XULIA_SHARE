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
    public partial class SeleccionarContacto : Form
    {
        public delegate Boolean delEnviarCorreoContacto(string asunto, string cuerpo, List<string> anexos, string contacto);
        delEnviarCorreoContacto m_EnviarCorreo;
        string m_asunto;
        string m_cuerpo;
        List<string> m_anexos;
        public SeleccionarContacto()
        {
            InitializeComponent();
        }

        public void SeleccionarContactoCorreo(string asunto, string cuerpo, List<string> anexos, List<string> Contactos, delEnviarCorreoContacto fEnviarCorreo)
        {
            m_EnviarCorreo = fEnviarCorreo;
            m_asunto = asunto;
            m_cuerpo = cuerpo;
            m_anexos = anexos;
            foreach(string s in Contactos)
            {
                lstContactos.Items.Add(Contactos);
            }
        }
        private void cmdCamcelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cmdAceptar_Click(object sender, EventArgs e)
        {
            if (lstContactos.SelectedIndex >= 0)
            {
                m_EnviarCorreo(m_asunto, m_cuerpo, m_anexos, lstContactos.Items[lstContactos.SelectedIndex].ToString());
            }
        }
    }
}

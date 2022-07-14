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
    public partial class InfoActualizar : Form
    {
        public InfoActualizar()
        {
            InitializeComponent();
        }
        public void MostrarInfoVersion(string info)
        {
            tbInfoVersion.Text = info;
            this.Show();
        }
        private void cmdAceptar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

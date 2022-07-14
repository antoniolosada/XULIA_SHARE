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
    public partial class AyudaRapida : Form
    {
        public bool Cargado = true;
        public AyudaRapida()
        {
            InitializeComponent();
        }

        private void AyudaRapida_Load(object sender, EventArgs e)
        {
            pbAyuda.Image = Image.FromFile("COMANDOS.gif");
        }
    }
}

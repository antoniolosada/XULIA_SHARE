using AIMLGUI;
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
    public partial class SalidaGPTmail : Form
    {
        CorreoOffice comandos;
        public SalidaGPTmail()
        {
            InitializeComponent();
        }
        public void MostrarSalidaGPT(CorreoOffice comandos)
        {
            this.comandos = comandos;
            RedimensionarMitadDerecha();
            this.Show();
        }
        private void RedimensionarMitadDerecha()
        {
            // Obtener el tamaño de la pantalla
            Rectangle screenBounds = Screen.PrimaryScreen.Bounds;

            // Calcular el tamaño y la posición para ocupar la mitad derecha
            int nuevaAnchura = screenBounds.Width / 2;
            int nuevaAltura = screenBounds.Height;
            int nuevaPosX = screenBounds.Width / 2;
            int nuevaPosY = 0;

            // Establecer el tamaño y la posición del formulario
            this.SetBounds(nuevaPosX, nuevaPosY, nuevaAnchura, nuevaAltura);
        }
        public void AddColumn(string id, string desc, int ancho)
        {
            grid.Columns.Add(id, desc);
            grid.Columns[id].Width = ancho;
        }
        public void AddRows(params object[] values)
        {
            grid.Rows.Add(values);
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            comandos.AbrirMensajeCorreo(grid[1, e.RowIndex].Value.ToString());
        }

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

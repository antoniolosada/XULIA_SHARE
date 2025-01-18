using AIMLGUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XULIA
{
    public partial class frmRecuerdos : Form
    {
        ProcesamientoComandos pc;
        public frmRecuerdos(ProcesamientoComandos pc1)
        {
            pc = pc1;
            InitializeComponent();
        }
        public void MostrarRecuerdosGPT()
        {
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

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Asegurarse de que no se ha hecho clic en el encabezado
            {
                // Mostrar cuadro de diálogo de confirmación
                DialogResult result = MessageBox.Show("¿Está seguro de que desea borrar este recuerdo?", "Confirmar borrado", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    grid.Rows.RemoveAt(e.RowIndex);
                    ActualizarRecuerdosGrid();
                }
            }
        }
        public void LeerRecuerdos()
        {
            grid.Rows.Clear();
            grid.Columns.Clear();
            AddColumn("numero", "número", 50);
            AddColumn("Recuerdo", "Recuerdo", 800);
            int numero = 1;

            string filePath = Application.StartupPath;
            string contenido = "";
            try {
                contenido = File.ReadAllText(filePath + "\\Recuerdos.txt");
                contenido = contenido.Replace(Environment.NewLine, "|");
                pc.GPT_Recuerdos = contenido;
            }
            catch { }

            string[] Recuerdos = Recuerdos = pc.GPT_Recuerdos.Split('|');
            foreach (string recuerdo in Recuerdos)
            {
                AddRows(numero++, recuerdo);
            }
            MostrarRecuerdosGPT();
        }

        public void Recuerda(string recuerdo)
        {
            if (recuerdo != "")
            {
                if (pc.GPT_Recuerdos == "")
                    pc.GPT_Recuerdos = recuerdo;
                else
                    pc.GPT_Recuerdos = pc.GPT_Recuerdos + "|" + recuerdo;
                ActualizarRecuerdos(pc.GPT_Recuerdos);
            }
        }
        void ActualizarRecuerdosGrid()
        {
            StringBuilder sRecuerdos = new StringBuilder();
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    if (sRecuerdos.Length > 0)
                        sRecuerdos.Append(Environment.NewLine);
                    sRecuerdos.Append(row.Cells[1].Value.ToString());
                }
            }
            ActualizarRecuerdos(sRecuerdos.ToString());
        }
        void ActualizarRecuerdos(string sRecuerdos)
        {
            sRecuerdos = sRecuerdos.Replace("|", Environment.NewLine);
            string filePath = Application.StartupPath;
            File.WriteAllText(filePath + "\\Recuerdos.txt", sRecuerdos);
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

        private void grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ActualizarRecuerdosGrid();
        }

        private void frmRecuerdos_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}

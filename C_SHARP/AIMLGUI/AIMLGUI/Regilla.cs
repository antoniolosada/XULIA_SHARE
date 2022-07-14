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
    public partial class Regilla : Form
    {
        float alto;
        float ancho;
        float salto_x;
        float salto_y;

        public Regilla()
        {
            InitializeComponent();
        }

        private void Regilla_Paint(object sender, PaintEventArgs e)
        {
            PintarRejilla();
        }

        public void PintarRejilla()
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 18, FontStyle.Bold);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
            System.Drawing.SolidBrush drawBrushYellow = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();

            int x, y;
            int cont = 0;
            alto = this.Height;
            ancho = this.Width;
            salto_x = ancho / 26;
            salto_y = alto / 26;


            for (x = 0; x < 26; x++)
            {
                int posx = (int)(cont * salto_x);
                formGraphics.FillRectangle(myBrush, new Rectangle(posx, 0, 1, (int)alto - 1));
                formGraphics.FillRectangle(drawBrushYellow, new Rectangle(posx + 1, 0, 1, (int)alto - 1));

                formGraphics.FillRectangle(drawBrushYellow, new Rectangle((int)(posx + salto_x / 2), 2, 30, 30));
                formGraphics.DrawString(Microsoft.VisualBasic.Strings.Chr('A' + cont).ToString(), drawFont, drawBrush, posx + salto_x / 2, 2, drawFormat);
                cont++;
            }
            cont = 0;
            for (y = 0; y < 26; y++)
            {
                int posy = (int)(cont * salto_y);
                formGraphics.FillRectangle(myBrush, new Rectangle(0, posy, (int)ancho, 1));
                formGraphics.FillRectangle(drawBrushYellow, new Rectangle(0, posy + 1, (int)ancho, 1));

                formGraphics.FillRectangle(drawBrushYellow, new Rectangle(2, (int)(posy + salto_y / 2) - 10, 30, 30));
                formGraphics.DrawString(Microsoft.VisualBasic.Strings.Chr('A' + cont).ToString(), drawFont, drawBrush, 2, posy + salto_y / 2 - 10, drawFormat);
                cont++;
            }
            myBrush.Dispose();
            formGraphics.Dispose();
        }
        public void MarcarRejillaFila(int posy)
        {
            System.Drawing.Graphics formGraphics = this.CreateGraphics();
            System.Drawing.SolidBrush drawBrushYellow = new System.Drawing.SolidBrush(System.Drawing.Color.LightBlue);

            formGraphics.FillRectangle(drawBrushYellow, new Rectangle(0, (int)(posy * salto_y + 2), (int)ancho, (int)(salto_y - 2)));
        }

        public void MarcarRejillaColumna(int posx)
        {
            System.Drawing.Graphics formGraphics = this.CreateGraphics();
            System.Drawing.SolidBrush drawBrushYellow = new System.Drawing.SolidBrush(System.Drawing.Color.LightBlue);

            formGraphics.FillRectangle(drawBrushYellow, new Rectangle((int)(posx * salto_x + 2), 0, (int)(salto_x - 2), (int)alto));
        }

        private void Regilla_Load(object sender, EventArgs e)
        {
        }
    }
}

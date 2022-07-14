using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using AIMLGUI;

namespace XULIA
{
    public partial class Avatar : Form
    {
        int contador = 0;
        public Avatar()
        {
            InitializeComponent();
        }
        public void MostrarAvatar()
        {

            this.Show();
            ProcesamientoComandos.SiempreEncima((int)this.Handle);
            ProcesamientoComandos.NoSiempreEncima((int)this.Handle);
        }
        public void ActivarAvatar()
        {

            tmrAvatar.Enabled = true;
            this.Show();
            ProcesamientoComandos.SiempreEncima((int)this.Handle);
            ProcesamientoComandos.NoSiempreEncima((int)this.Handle);
        }

        public void PararAvatar()
        {

            tmrAvatar.Enabled = false;
            imgAvatar.Image = Image.FromFile(@".\Iconos\Xulia.jpg");
            imgAvatar.Refresh();
        }
        public void TextoReconocido(string texto)
        {
            tbRec.Text = texto;
        }
        public void Palabra(string palabra)
        {
            /*tbTexto.Text = palabra + Microsoft.VisualBasic.Constants.vbCrLf + tbTexto.Text;
            tbTexto.Refresh();*/
        }
        public void Pausa()
        {
            imgAvatar.Image = Image.FromFile(@".\Iconos\xc.jpg");
            imgAvatar.Refresh();
            tmrAvatar.Enabled = false;
            Thread.Sleep(300);
            tmrAvatar.Enabled = true;
            this.Show();
        }

        private void tmrAvatar_Tick(object sender, EventArgs e)
        {
            contador++;
            int numero = contador % 2;
            if (numero == 0)
            {
                imgAvatar.Image = Image.FromFile(@".\Iconos\xa.jpg");
            }
            if (numero == 1)
            {
                imgAvatar.Image = Image.FromFile(@".\Iconos\xc.jpg");
            }
            imgAvatar.Refresh();
        }

        private void Avatar_Load(object sender, EventArgs e)
        {

        }

        private void Avatar_SizeChanged(object sender, EventArgs e)
        {
            imgAvatar.Height = this.Height;
            imgAvatar.Width = this.Width;
        }

        private void Avatar_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Impedir que el formulario se cierre pulsando X o Alt + F4 
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    e.Cancel = true;
                    this.Hide();
                    return;
            }
        }

        public void PensarActivo()
        {
            pbPensar.Visible = true;
        }
        public void PensarDesactivo()
        {
            pbPensar.Visible = false;
            pbPensar.Refresh();
            tbRec.Text = "";
        }
    }

}

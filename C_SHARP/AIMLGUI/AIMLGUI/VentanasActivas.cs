using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using AIMLGUI;

namespace XULIA
{
    public class VentanasActivas
    {
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);
        public struct sVentana
        {
            public int Numero;
            public long IdProceso;
            public IntPtr hWnd;
            public string Titulo;
            public bool Activa;
        };

        List<int> NumerosVentanaLibres = new List<int>();
        List<sVentana> mVentanasActivas = new List<sVentana>();

        public string BuscarVentanaNumero(int numero)
        {
            sVentana v = mVentanasActivas.Find(x => x.Numero == numero);
            if (v.Titulo != null)
                return v.Titulo;
            return "";
        }
        public void CargarVentanasLista(ListBox lst)
        {
            foreach (sVentana v in mVentanasActivas)
            {
                lst.Items.Add(v.Numero+" - " + v.Titulo );
            }
        }

        public void RenumerarVentanasLista(ListBox lst)
        {
            int i = 1;
            lst.Items.Clear();
            sVentana[] mVentanas = mVentanasActivas.ToArray();
            foreach (sVentana v in mVentanas)
            {
                int i1 = mVentanasActivas.IndexOf(v);
                sVentana v1 = mVentanasActivas[i1];
                v1.Numero = i++;
                mVentanasActivas[i1] = v1;
                lst.Items.Add(v1.Numero + " - " + v1.Titulo);
            }
        }

        public void CargarVentanas()
        {
            Win32APITools api = new Win32APITools();
            List<sVentana> Ventanas = new List<sVentana>();

            api.LeerVentanas(Ventanas);
            for (int i = 0; i < mVentanasActivas.Count; i++)
            {
                sVentana v;
                v = mVentanasActivas[i];
                v.Activa = false;
                mVentanasActivas[i] = v;
            }

            foreach (sVentana v in Ventanas)
            {
                sVentana ventana = new sVentana();
                int pos = mVentanasActivas.FindIndex(x => ((x.Titulo == v.Titulo) && (x.hWnd == v.hWnd)) );
                if (pos < 0)
                {
                    ventana = new sVentana();
                    if (NumerosVentanaLibres.Count > 0)
                    {
                        int Numero = NumerosVentanaLibres[0];
                        ventana.Numero = Numero;
                        NumerosVentanaLibres.Remove(Numero);
                    }
                    else
                        ventana.Numero = mVentanasActivas.Count;
                    ventana.Titulo = v.Titulo;
                    ventana.hWnd = v.hWnd;
                    ventana.Activa = true;
                    mVentanasActivas.Add(ventana);
                }
                else
                {
                    ventana = mVentanasActivas[pos];
                    ventana.Activa = true;
                    mVentanasActivas[pos] = ventana;
                }
            }

            List<sVentana> BorrarVentanas = mVentanasActivas.FindAll(x => x.Activa == false);
            foreach (sVentana v in BorrarVentanas)
            {
                NumerosVentanaLibres.Add(v.Numero);
                mVentanasActivas.Remove(v);
            }
            
        }

        public void ActivarVentana(int Numero)
        {
            sVentana v = mVentanasActivas.Find(x => x.Numero == Numero);
            if (v.Titulo != "")
                ProcesamientoComandos.ActivarVentana(v.hWnd);
                //SetForegroundWindow (v.hWnd);
        }
    }
}

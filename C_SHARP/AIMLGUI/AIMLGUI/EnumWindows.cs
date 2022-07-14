using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;


namespace XULIA
{
    class Win32APITools
    {
        static ListBox lstAPP;
        static List<VentanasActivas.sVentana> ListaVentanas;

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        /// <summary>Almacena el ID de proceso y el handler de una ventana</summary> 
        private class AuxInfo
        {
            public int processID;
            public IntPtr handler;
        }

        /// <summary>
        /// Delegado para hacer de callback
        /// </summary>
        /// <param name="hwnd" />handler de la ventana
        /// <param name="lParam" />paramétro con la informacion necesaria para el proceso
        /// <returns>Valor de retorno del proceso</returns>
        private delegate bool EnumWindowsProc(IntPtr hwnd, AuxInfo lParam);

        /// <summary>
        /// Recorre las ventanas y ejecuta un proceso para cada una de ellas
        /// </summary>
        /// <param name="lpEnumFunc" />Delegado con el proceso a utilizar para cada ventana
        /// <param name="lParam" />paramétro con la informacion necesaria para el proceso
        /// <returns>Retorna true si se recorren todas las ventanas, de lo contrario false o segun determine el usuario a trabes del callback</returns>
        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, AuxInfo lParam);

        /// <summary>
        /// Devuelve el ID del proceso al que pertenece el hilo de la ventana
        /// </summary>
        /// <param name="hwnd" />handler de la ventana
        /// <param name="lpdwProcessId" />ID del proceso (parámetro de salida)
        /// <returns>ID del Thread que creó la ventana</returns>
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hwnd, out int lpdwProcessId);

        /// <summary>
        /// Indica si una ventana es o no visible
        /// </summary>
        /// <param name="hWnd" />handler de la ventana
        /// <returns>Indicador de si la v entana es o no visible</returns>
        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        /// <summary>
        /// Obtiene el handler de la ventana asociada a un proceso
        /// Este procedimiento es solo de utileria para usarse con EnumWindows 
        /// y no deberia ser invocado directamente
        /// </summary>
        /// <param name="hwnd" />handler de la ventana actual
        /// <param name="info" />informacion auxiliar para el proceso
        /// <returns>false si encuentra la ventana, true sino</returns>
        private static bool _GetProcessWindowHandler(IntPtr hwnd, AuxInfo info)
        {
            int processID;
            GetWindowThreadProcessId(hwnd, out processID);

            if (processID == info.processID)
            {
                info.handler = hwnd;
                return false;
            }
            else
            {
                info.handler = IntPtr.Zero;
                return true;
            }
        }

        private static bool _GetProcessWindowHandler1(IntPtr hwnd, AuxInfo info)
        {
            if (hwnd != IntPtr.Zero)
            {
                StringBuilder s = new StringBuilder(201);
                GetWindowText(hwnd, s, 200);
                if (s.ToString() != "")
                {
                    if (IsWindowVisible(hwnd))
                    {
                        lstAPP.Items.Add(s.ToString());
                        lstAPP.Refresh();
                    }
                }
            }
            return true;
        }

        private static bool _GetProcessWindowHandlerLista(IntPtr hwnd, AuxInfo info)
        {
            if (hwnd != IntPtr.Zero)
            {
                StringBuilder s = new StringBuilder(201);
                    if (IsWindowVisible(hwnd))
                    {
                        GetWindowText(hwnd, s, 200);
                        if (s.ToString() != "")
                        {
                            VentanasActivas.sVentana Ventana = new VentanasActivas.sVentana();
                            Ventana.Titulo = s.ToString();
                            Ventana.hWnd = hwnd;
                            ListaVentanas.Add(Ventana);
                        }
                    }
            }
            return true;
        }


        /// <summary>
        /// Devuelve el handler de la ventana asociada al proceso
        /// </summary>
        /// <param name="pid" />Id del proceso
        /// <returns>handler de la ventana</returns>
        public static IntPtr GetProcessWindowHandler(int pid)
        {
            //Delegado con el proceso auxiliar de búsqueda
            EnumWindowsProc getHandlerVentana = new EnumWindowsProc(_GetProcessWindowHandler);
            //Informacion auxiliar
            AuxInfo informacion = new AuxInfo();
            informacion.processID = pid;

            /*Repetir bucle hasta que este presente la ventana del proceso
             *(puede que la enumeracion se realice y windows  aún no haya creado 
             *la primera ventana del proceso o bien no la haya hecho visible, 
             *por lo cual se debe repetir el bucle hasta encontrala)*/
            do
            {
                /*Enumerar las ventanas buscando la que coincida con
                 *el id de proceso contenido en informacion */
                EnumWindows(getHandlerVentana, informacion);
            } while (informacion.handler == IntPtr.Zero || !IsWindowVisible(informacion.handler));

            return informacion.handler;
        }
        public void ListWindows(ListBox lst)
        {
            lstAPP = lst;
            //Delegado con el proceso auxiliar de búsqueda
            EnumWindowsProc getHandlerVentana = new EnumWindowsProc(_GetProcessWindowHandler1);
            //Informacion auxiliar
            AuxInfo informacion = new AuxInfo();

                /*Enumerar las ventanas buscando la que coincida con
                 *el id de proceso contenido en informacion */
                EnumWindows(getHandlerVentana, informacion);
            return;
        }
        public void LeerVentanas(List<VentanasActivas.sVentana> lista)
        {
            ListaVentanas = lista;
            //Delegado con el proceso auxiliar de búsqueda
            EnumWindowsProc getHandlerVentana = new EnumWindowsProc(_GetProcessWindowHandlerLista);
            //Informacion auxiliar
            AuxInfo informacion = new AuxInfo();

            /*Enumerar las ventanas buscando la que coincida con
             *el id de proceso contenido en informacion */
            EnumWindows(getHandlerVentana, informacion);
            return;
        }
    }
}

//using System;
//using System.Diagnostics;

//namespace GetProcessWindowHandler
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Process proc = new Process();
//            ProcessStartInfo psi = new ProcessStartInfo("calc.exe");
//            proc.StartInfo = psi;

//            proc.Start();

//            IntPtr handler = Win32APITools.GetProcessWindowHandler(proc.Id);  

//            Console.WriteLine("El Handler obtenido para la ventana de este proceso es: {0}", handler);
//            Console.ReadLine();
//        }
//    }
//}
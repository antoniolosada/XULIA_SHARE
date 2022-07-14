using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;


namespace XuliaRun
{
    public partial class XuliaRun : Form
    {
        public const string URL_VERSIONES = @"https://sites.google.com/site/accesibilidadinteligente/xulia---xestion-unificada-da-linguaxe-con-intelixencia-artificial/descarga-e-instalacion/versiones.txt?attredirects=0&d=1";
        const string DIR_COPIAS = @"C:\TMP\XULIA\COPIAS\";
        const string DIR_ACT = @"C:\TMP\XULIA\";
        bool m_salir = false;
        bool m_error_dir = false;
        string[] arg;
        public XuliaRun()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void XuliaRun_Activated(object sender, EventArgs e)
        {
            try
            {
                arg = Environment.GetCommandLineArgs();
                string Comando = arg[1];
                if (Comando == "RESET")
                {
                    lblProceso.Text = "RESET";
                    lblFichero.Text = "XULIA";
                    //Esperamos a que XULIA salga
                    while (BuscarProceso("XULIA"))
                    {
                        Application.DoEvents();
                        Thread.Yield();
                        Thread.Sleep(200);
                    };
                    //Ejecutamos Xulia de nuevo
                    System.Diagnostics.Process.Start("XULIA.EXE");
                }
                else
                {
                    //Thread.Sleep(5000);
                    ComprobarActualizaciones();
                }
                Application.Exit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void ComprobarActualizaciones()
        {
            string InfoVersion = "";
            WebClient miwebclient = new WebClient();
            string versionXulia = arg[2];
            string[] versionApp = versionXulia.Split('.');
            bool mostrarVersion = false;

            Uri miurl = new Uri(URL_VERSIONES);

            try
            {
                if (!System.IO.Directory.Exists(DIR_ACT))
                    Directory.CreateDirectory(DIR_ACT);
                if (!System.IO.Directory.Exists(DIR_COPIAS))
                    Directory.CreateDirectory(DIR_COPIAS);
            }
            catch (Exception e)
            {
                m_error_dir = true;
                lblProceso.Text = "ERROR";
                lblFichero.Text = "Error creando directorios";
                this.Refresh();
                Application.DoEvents();
                Thread.Sleep(10000);
            }

            try
            {
                miwebclient.DownloadFile(miurl, DIR_ACT + "versiones.txt");
                System.IO.StreamReader sr;
                sr = new System.IO.StreamReader(DIR_ACT + "versiones.txt", Encoding.Default);
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    if (mostrarVersion)
                    {
                        InfoVersion += line + Environment.NewLine;
                        if (line == "@")
                            mostrarVersion = false;
                    }
                    else
                    {
                        if (line != "")
                        {
                            if (line.Substring(0, 1) == "*")  //Comprobamos si es un número de versión
                            {
                                string version = line.Substring(2);
                                string[] numeros = version.Split('.');
                                long nversion = Convert.ToInt32(numeros[0]) * 10000 + Convert.ToInt32(numeros[1]) * 100 + Convert.ToInt32(numeros[2]);
                                long nversionApp = Convert.ToInt32(versionApp[0]) * 10000 + Convert.ToInt32(versionApp[1]) * 100 + Convert.ToInt32(versionApp[2]);
                                if (nversion > nversionApp)
                                {
                                    mostrarVersion = true;
                                    InfoVersion += "Versión: " + version + Environment.NewLine;
                                }
                            }
                            else if (line.Substring(0, 1) == "#")  //Comprobamos si es un comando
                            {
                                if (line.IndexOf("ESPERAR_FIN_EJECUCION") >= 0)
                                {
                                    string programa = sr.ReadLine();
                                    lblProceso.Text = "ESPERAR FINALIZACIÓN";
                                    lblFichero.Text = programa;
                                    Application.DoEvents();
                                    m_salir = false;
                                    while ((BuscarProceso(programa)) && (!m_salir))
                                    {
                                        Application.DoEvents();
                                        Thread.Yield();
                                        Thread.Sleep(200);
                                    };
                                    if (m_salir)
                                    {
                                        lblProceso.Text = "ERROR";
                                        lblFichero.Text = "Tiempo excedido";
                                        this.Refresh();
                                        Application.DoEvents();
                                        Thread.Sleep(10000);
                                        Application.Exit();
                                    }
                                }
                                else if (line.IndexOf("CREAR_DIRECTORIO") >= 0)
                                {
                                    string directorio = sr.ReadLine();
                                    lblProceso.Text = "CREAR DIRECTORIO";
                                    lblFichero.Text = directorio;
                                    Application.DoEvents();
                                    if (!System.IO.Directory.Exists(directorio))
                                        System.IO.Directory.CreateDirectory(directorio);
                                }
                                else if ((line.IndexOf("COPIAR") >= 0) || (line.IndexOf("COPIA_CONDICIONAL_CONTENIDO") >= 0))
                                {
                                    bool salir = false;
                                    //Si localizamos el texto en el fichero copiamos el archivo
                                    if (line.IndexOf("COPIA_CONDICIONAL_CONTENIDO") >= 0)
                                    {
                                        salir = true;
                                        bool copiar = true;
                                        string fichero_buscar = sr.ReadLine();
                                        string contenido = sr.ReadLine();
                                        string contenido_no_copiar = sr.ReadLine();
                                        lblProceso.Text = "Buscando texto en ...";
                                        lblFichero.Text = fichero_buscar;
                                        Application.DoEvents();
                                        System.IO.StreamReader srb;
                                        fichero_buscar = fichero_buscar.Replace("{app_dir}", Application.StartupPath + @"\");
                                        srb = new System.IO.StreamReader(fichero_buscar, Encoding.UTF8);
                                        while (!srb.EndOfStream)
                                        {
                                            string linea = srb.ReadLine();
                                            if (linea.IndexOf(contenido) >= 0)
                                                salir = false;
                                            if(contenido_no_copiar != "")
                                                if (linea.IndexOf(contenido_no_copiar) >= 0)
                                                    copiar = false;
                                        }
                                        srb.Close();
                                        if (!copiar) salir = true;
                                    }
                                    if (!salir)
                                    {
                                        Application.DoEvents();
                                        Thread.Sleep(1000);
                                        string fichero = sr.ReadLine();
                                        string origen = sr.ReadLine();
                                        string destino = sr.ReadLine();
                                        // Solo copiamos si hay alguna versión que actualizar
                                        if ((InfoVersion != "") && (!m_error_dir))
                                        {
                                            lblProceso.Text = "Descargando...";
                                            lblFichero.Text = fichero;
                                            Application.DoEvents();

                                            miurl = new Uri(origen);
                                            miwebclient.DownloadFile(miurl, DIR_ACT + fichero);

                                            destino = destino.Replace("{app_dir}", Application.StartupPath);

                                            lblProceso.Text = "Copiando...";
                                            Application.DoEvents();
                                            //Si existe la copia la borramos
                                            if (System.IO.File.Exists(DIR_COPIAS + fichero))
                                                System.IO.File.Delete(DIR_COPIAS + fichero);
                                            //Single existe else fichero hacemos una copia
                                            if (System.IO.File.Exists(destino + @"\" + fichero))
                                                System.IO.File.Copy(destino + @"\" + fichero, DIR_COPIAS + fichero);
                                            //Si existe el fichero de destino lo borramos
                                            if (System.IO.File.Exists(destino + @"\" + fichero))
                                                System.IO.File.Delete(destino + @"\" + fichero);
                                            //Actualizamos el fichero
                                            System.IO.File.Copy(DIR_ACT + fichero, destino + @"\" + fichero);
                                            //Borramos la descargar
                                            System.IO.File.Delete(DIR_ACT + fichero);

                                            lblProceso.Text = "Actualizando";
                                            lblFichero.Text = "";
                                        }
                                    }
                                }
                                else if (line.IndexOf("ESPERAR") >= 0)
                                {
                                    int tiempo = Convert.ToInt16(sr.ReadLine());
                                    lblProceso.Text = "ESPERA";
                                    lblFichero.Text = tiempo.ToString();
                                    Application.DoEvents();
                                    Thread.Sleep(tiempo);
                                }
                                else if (line.IndexOf("EJECUTAR_Y_SALIR") >= 0)
                                {
                                    string programa = sr.ReadLine();
                                    lblProceso.Text = "EJECUTAR Y SALIR";
                                    lblFichero.Text = programa;
                                    Application.DoEvents();
                                    sr.Close();
                                    System.Diagnostics.Process.Start(programa);
                                    return;
                                }
                                else if (line.IndexOf("EJECUTAR") >= 0)
                                {
                                    string programa = sr.ReadLine();
                                    lblProceso.Text = "EJECUTAR";
                                    lblFichero.Text = programa;
                                    Application.DoEvents();
                                    System.Diagnostics.Process.Start(programa);
                                }
                                else if (line.IndexOf("SALIR") >= 0)
                                {
                                    sr.Close();
                                    return;
                                }
                            }
                        }
                    }
                }
                sr.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }
        bool BuscarProceso(string nombre)
        {
            Process[] myProcesses = Process.GetProcesses();
            foreach (Process myProcess in myProcesses)
            {
                if (myProcess.ProcessName == nombre)
                    return true;
            }
            return false;
        }

        private void tmrEsperaFinProceso_Tick(object sender, EventArgs e)
        {
            m_salir = true;
        }
    }
}

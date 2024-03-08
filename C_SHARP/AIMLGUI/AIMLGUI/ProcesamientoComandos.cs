
//#define SAPI11
//#define SAPI5
#define SAPI11_5
#define VOZ_UWP


using System.Net.Mail;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
#if (SAPI11 || SAPI11_5)
using Microsoft.Speech.Recognition;
#else
    using System.Speech.Recognition;
#endif
#if VOZ_UWP
#endif
using System.Collections;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Globalization;
using Microsoft.Office;

using XULIA;
using _09_HTTPLISTENER_WEBSERVER;
using static AIMLGUI.ProcesamientoComandos;

namespace AIMLGUI
{

    public class ProcesamientoComandos
    {
        const Boolean SR_TOMCAT = false;

        const string SECCION_DIRECTORIO = "Directorio";
        const bool CONFIG_XML = true;
        const int MAX_PORTAPAPELES = 11;
        const int ESPERA_PARADA_REC_GOOGLE = 800; //ms
        const int MAX_ESPERA_CIERRE_CHROME_100MS = 20;

        #region Constantes_VK
        const byte HW_LSHIFT = 0xAA;
        const byte HW_LWIN = 0x5B;
        const byte HW_LCONTROL = 0x9D;
        const byte HW_LMENU = 0xB8;

        const byte VK_SHIFT = 0x10;
        const byte VK_PAUSE = 0x13;
        const byte VK_SNAPSHOT = 0x2C;
        const byte VK_INSERT = 0x2D;
        const byte VK_DELETE = 0x2E;
        const byte VK_LWIN = 0x5B;
        const byte VK_NUMLOCK = 0x90;
        const byte VK_RWIN = 0x5C;
        const byte VK_SCROLL = 0x91;
        const byte VK_PRINT = 0x2A;
        const byte VK_LSHIFT = 0xA0;
        const byte VK_RSHIFT = 0xA1;
        const byte VK_LCONTROL = 0xA2;
        const byte VK_RCONTROL = 0xA3;
        const byte VK_LMENU = 0xA4;
        const byte VK_RMENU = 0xA5;
        const byte VK_CAPITAL = 0x14;
        const byte VK_SPACE = 0x20;
        const byte VK_RETURN = 0x0D;
        const byte VK_BACK = 0x08;
        const byte VK_CANCEL = 0x03;
        const byte VK_TAB = 0x09;
        const byte VK_ESCAPE = 0x1B;

        const byte VK_END = 0x23;
        const byte VK_HOME = 0x24;
        const byte VK_RIGHT = 0x27;
        const byte VK_LEFT = 0x25;
        const byte VK_DOWN = 0x28;
        const byte VK_UP = 0x26;

        const byte VK_NUMPAD7 = 0x67;
        const byte VK_NUMPAD8 = 0x68;
        const byte VK_NUMPAD9 = 0x69;
        const byte VK_MULTIPLY = 0x6A;
        const byte VK_ADD = 0x6B;
        const byte VK_CONTROL = 0x11;
        const byte VK_SEPARATOR = 0x6C;
        const byte VK_MENU = 0x12;
        const byte VK_SUBTRACT = 0x6D;
        const byte VK_DECIMAL = 0x6E;
        const byte VK_DIVIDE = 0x6F;
        const byte VK_F1 = 0x70;
        const byte VK_F2 = 0x71;
        const byte VK_F3 = 0x72;
        const byte VK_F4 = 0x73;
        const byte VK_F5 = 0x74;
        const byte VK_F6 = 0x75;
        const byte VK_F7 = 0x76;
        const byte VK_F8 = 0x77;
        const byte VK_F9 = 0x78;
        const byte VK_F10 = 0x79;
        const byte VK_F11 = 0x7A;
        const byte VK_F12 = 0x7B;
        const byte VK_NUMPAD0 = 0x60;
        const byte VK_NUMPAD1 = 0x61;
        const byte VK_NUMPAD2 = 0x62;
        const byte VK_NUMPAD3 = 0x63;
        const byte VK_NUMPAD4 = 0x64;
        const byte VK_NUMPAD5 = 0x65;
        const byte VK_NUMPAD6 = 0x66;

        const uint KEYEVENTF_KEYUP = 0x0002;
        const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        const uint KEYEVENTF_SILENT = 0x0004;

        #endregion Constantes_VK

        #region variables

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetForegroundWindow();

        //[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //public static extern long GetWindowText(long hwnd, string lpString, long cch);
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx", CharSet = CharSet.Auto)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll")]
        private static extern int GetWindowPlacement(IntPtr hwnd, out WINDOWPLACEMENT lpwndpl);

        const int LIMITE_VELOCIDAD_RATON = 50;
        const int MAX_TITULOS_VENTANAS = 20;

        [DllImport("user32.DLL")]
        private static extern void SetWindowPos(int hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, int wFlags);

        const int NUM_FILAS_REJILLA = 26;
        const int NUM_COLS_REJILLA = 26;

        ConfigXml mCfgListas;
        public bool ARRANQUE_COMPLETADO = false;
        public Process ProcesoChrome = null;
        struct sAlmacenamiento
        {
            public string directorio;
            public string path;
        };
        List<sAlmacenamiento> Directorios = new List<sAlmacenamiento>();
        public bool XULIA_ACTIVA = true;
        public bool SELECCION = false;
        public bool CONTROL = false;
        public bool ATENCION = false;
        public bool ALT = false;
        public bool ALT_GR = false;
        public bool ACENTO = false;
        public bool ACENTO_GRAVE = false;
        public bool ACENTO_CIRCUNFLEJO = false;
        public bool DIERESIS = false;
        public bool MAYUSCULAS = false;
        public bool UNAMAYUSCULA = false;
        public bool AYUDA_RAPIDA = false;
        public bool TILDE = false;
        bool NUEVA_LINEA = true; //Indicador de nueva línea para dictados chrome
        public MovimientoRaton DIR_RATON = MovimientoRaton.Parar;
        public bool RATON_CONTINUO = false;
        public int RATON_VELOCIDAD = 1;
        public int LECTURA_VELOCIDAD;
        float PrecisionMinima = (float)0.636;
        float PrecisionModoDictado = (float)0.206;
        public string ArchivoRecVoz = "";
        public string ArchivoComandos = "";
        string URLBaseIdioma = "";
        string DescargarGramaticasCambioMODO = "N";
        string MantenerPulsadoControl = "N";
        string MantenerPulsadoAlternativo = "N";
        string MantenerPulsadoSeleccion = "N";
        string DescargarGramaticasDictado = "S";
        string DescargarRestoGramaticasDictado = "N";
        string XuliaActivar = "activar";
        string[] XuliaComandosDesactivados;
        string XuliaDesactivar = "desactivar";
        string XuliaAtencion = "atencion";
        public int MinutosResetControlVoz = 10;
        public static bool DictadoSAPI5 = false;
        string PalabrasVolverModo = "";
        public int OcultarEstadoSegundos = 0;
        string ArranqueTomcat = "";
        string ArranqueTomcat_unidad = "";
        string ArranqueTomcat_parametros = "";
        int TiempoArranqueTomcat = 1;
        int TiempoEsperaArranqueChrome = 4;
        public string ArranqueXuliaVisible = "N";
        public string DictadoGoogleSiempreMinusculas = "S";
        bool MinimizarChrome = false;
        string IdiomaGramaticas = "";
        public bool Aviso = false;
        const int MAX_BUFFER_FRASES = 20;
        string[] FrasesDictado = new string[MAX_BUFFER_FRASES];
        int indFrasesDictado = -1;
        bool m_CerebroCargado = false;
        string RatonContinuoInicial = "";
        public string RutaCerebroXulia = "";
        public string voz = "";
        string m_ModoRetornoUnComando = "";
        bool m_UnComando = false;
        public string ModoCorreccion = "";
        public string ModoActualCorreccion = "";
        string[] Portapapeles = new string[MAX_PORTAPAPELES];
        public string DireccionIP;
        public Int32 Puerto;
        bool RV_Windows = false;
        public string CortarTextoCorreccion = "N";
        public OleDbConnection conn;
        string ActivarBDCoreccion = "";
        string ProveedorBD = "";
        string BDCoreccion = "";
        public string GrabarCoreccion = "";
        string MostrarGloboComandos = "N";
        string MostrarGloboModos = "N";
        public string ModoGloboBasico = "S";
        public string ModoComandoGoogle = "N";
        public string IdiomaComandoGoogle = "ESPANOL";
        public string CodigoIdiomaComandoGoogle = "es-ES";
        public string ArranqueDesactivado = "N";
        int EsperaArranqueTomcatModoComandoGoogle = 10;
        bool InteractuarConXulia = true;
        public bool ActivarAvatar = false;
        public bool AutoDesactivacion = false;
        public bool MaximizarAvatar = false;
        AyudaRapida frmAyudaRapida = new AyudaRapida();
        static public bool OkXulia = false;
        int EsperaCompilacionGramaticaUWP = 2000;
        bool OK_XULIA_UnComando = true;

        sAlmacenamiento Almacenamiento = new sAlmacenamiento();
        public enum Preguntas : int { CuantosAnos, QueEs, QuienEs, QueTiempoHace };


        public string ServidorSMTP = "";
        public string UsuarioSMTP = "";
        public string ClaveSMTP = "";
        public string OrigenSMTP = "";

        DictadoWindowsMedia DictadoUWP;
        NavegadorWeb web = new NavegadorWeb();
        frmAyuda Ayuda = new frmAyuda();
        public BuscarComandos frmBuscarComandos = new BuscarComandos();
        Configuracion frmConfiguracion = new Configuracion();
        Versiones frmVersiones = new Versiones();
        TaskManager frmTasks = new TaskManager();

        string[] mTitulosVentanas = new string[MAX_TITULOS_VENTANAS];
        long[] mhWndVentanas = new long[MAX_TITULOS_VENTANAS];
        float GRADO_PRECISION_TEXTO = 0;
        int RatonSaltoVelocidad = 1;
        int LecturaEspera = 1;
        int LecturaIncrementoEspera = 1;
        string OcultarBarraPrecision = "N";
        VentanasActivas mVentanas = new VentanasActivas();
        public enum EstadosLectura : int { Arriba, Abajo, Parar };
        public EstadosLectura EstadoLectura;

        int RejillaPosY;
        int RejillaPosX;
        public List<int> PressKey = new List<int>();
        const int SWP_NOSIZE = 1;
        const int SWP_NOMOVE = 2;
        const int SWP_NOACTIVATE = 16;
        const int wFlags = (SWP_NOMOVE
                    | (SWP_NOSIZE | SWP_NOACTIVATE));
        //    Valores de hwndInsertAfter
        const int HWND_TOPMOST = -1;
        const int HWND_NOTOPMOST = -2;
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;
        // RECT structure required by WINDOWPLACEMENT structure
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.Left = left;
                this.Top = top;
                this.Right = right;
                this.Bottom = bottom;
            }
        }

        // POINT structure required by WINDOWPLACEMENT structure
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        // WINDOWPLACEMENT stores the position, size, and state of a window
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPLACEMENT
        {
            public uint length;
            public uint flags;
            public uint showCmd;
            public POINT minPosition;
            public POINT maxPosition;
            public RECT normalPosition;
        }

        static aimlForm MeForm;
        static ProcesamientoComandos Me;
        static RichTextBox txtSalida;

        public List<sGramatica> ElementosGramatica = new List<sGramatica>();
        public struct sGramatica
        {
            public string Nombre;
            public Single Precision;
            public bool GramaticaAplicacion;
            public string Clave;
            public string palabra;
            public string ayuda;
            public string resultado;
            public bool ComandoExtendido;
            public string[] parametros;
        }
        public string salida = ""; //Salida del sistema de reconocimiento de voz
        public float precision; //Precision del reconocimiento de voz
        public struct sSustitucionesDictado
        {
            public int Orden;
            public string FraseEntrada;
            public string FraseSalida;
        }
        public struct DictadoContinuo
        {
            public string idioma;
            public Process proceso;
            public string codigo;
            public string InstruccionSalida;
            public string InstruccionLiteral;
            public List<sSustitucionesDictado> Sustituciones;
        }
        List<DictadoContinuo> DictadosActivos = new List<DictadoContinuo>();
        public struct GramaticaCargada
        {
            public string Nombre;
            public Grammar Gramatica;
            public int GramaticaSAPI4;
            public bool Activa;
            public bool GramCargada;
            public bool GramaticaAplicacion;
            public bool GramaticaExtendida;
        }
        //Predicate<GramaticaCargada> buscar = BuscarGramaticasCargadas;
        public List<GramaticaCargada> GramaticasCargadas = new List<GramaticaCargada>();
        struct datos_rejilla
        {
            public int AjusteFino;
        }
        public struct sDireccion
        {
            public string direccion;
            public string comando;
        };
        public struct sRecordatorio
        {
            public int Numero;
            public string recordar;
        };

        enum VentanaPosicion { derecha, izquierda, arriba, abajo };
        public enum Comandos { ComandoNoPreciso, ComandoEncontrado, ComandoNoEncontrado };

        public Mouse ControlRaton = new Mouse();
        public CursorRaton frmCursor = new CursorRaton();
        Regilla Rejilla = new Regilla();

        enum ModoRejilla { Fila, Columna, PosicionFina };

        public enum MovimientoRaton : int { Arriba, Abajo, Derecha, Izquierda, Parar, Diagonal1, Diagonal2, Diagonal3, Diagonal4 };

        public string MODO = "";
        string MODO_ANT = "";
        string MODO_CANCELAR = "";
        string AplicacionActiva = "";

        ModoRejilla MODORERIJA = ModoRejilla.Fila;
        datos_rejilla DatosRejilla = new datos_rejilla();
        public int Multiplicador = 1;
        bool Espera = true;
        bool MarcarSeleccionRejilla = false;

        public Point PosCursor = new Point(0, 0);

#if SAPI11 
        Grammar GramaticaDictado;
#else
        System.Speech.Recognition.DictationGrammar GramaticaDictado;
#endif
        public SpeechRecognitionEngine recognizer;
#if SAPI11_5
        public System.Speech.Recognition.SpeechRecognitionEngine recognizerDictado;
#endif

        bool GramaticaDictadoCargada = false;

        string ClaseAppActiva = "";
        string TituloVentanaAppActiva = "";

        public ConfigApp cfg = new ConfigApp();

        string Cultura;
        public int DesplazamientoVentanaEstado;
        public int AltoBarraEstado = 0;

        public frmEstado Estado;
        public aimlForm frmAIML;
        public ServidorWeb ServWeb;

        public class sListasRecuerdos
        {
            public string NombreLista;
            public string CodigoLista;
            public List<sRecordatorio> Recuerdos = new List<sRecordatorio>();
        }

        List<sDireccion> DireccionesDestino;
        List<sListasRecuerdos> ListasRecuerdos = new List<sListasRecuerdos>();

        #endregion variables

        // Métodos ***************************************************************************************************************************************************************************************
        #region iniciacion
        public void ActivarReconocimientoVoz(ProcesamientoComandos Com, aimlForm Form, frmEstado FormEstado, RichTextBox Salida, ServidorWeb srw)
        {
            /******************      IMPORTANTE   *******************************************************
             * En caso de activar el idioma _EN con SAPI11 y un idioma distinto en wl S.O., 
             * XULIA da un error de protección general
             ******************      IMPORTANTE   *******************************************************/

            int height = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height;
            int width = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width;

            Me = Com;
            frmAIML = MeForm = Form;
            Estado = FormEstado;
            txtSalida = Salida;
            ServWeb = srw;

            try
            {
                Estado.ActualizarComando("Estatus:  Config Loading");
                cfg.IniCfg();

                CargarVariablesConfiguracion();

                Estado.ActualizarComando("Estatus:  Contact Loading");
                DireccionesDestino = CargarDireccionesDestino();
                ListasRecuerdos = CargarListasRecuerdos();

                //Configurar parámetros iniciales de idioma para GoogleChrome en la clase del servidor web
                srw.ConfigVar(IdiomaComandoGoogle, CodigoIdiomaComandoGoogle, ModoComandoGoogle);

                if (ArchivoRecVoz != "")
                    System.IO.File.WriteAllText(ArchivoRecVoz, "");

                if (ActivarBDCoreccion == "S")
                {
                    string CadenaConexion = ProveedorBD + "Data Source=" + SustituirDirectorios(BDCoreccion);
                    conn = new OleDbConnection(CadenaConexion);
                    conn.Open();
                }

                if ((RutaCerebroXulia != "") && (!m_CerebroCargado))
                {
                    m_CerebroCargado = true;
                    Estado.ActualizarComando("Estatus:  AIML Loading");
                    frmAIML.CargarFicherosAIML(Application.StartupPath + RutaCerebroXulia);
                }

                Estado.ActualizarComando("Estatus:  Init voice recognition");

                InicializarReconocedorVoz();

                if (RatonContinuoInicial == "S")
                {
                    RATON_CONTINUO = true;
                    DIR_RATON = MovimientoRaton.Parar;
                    frmAIML.TimerRatonActivo(true);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Error cargando variables de configuración:" + e.Message);
            }


            try
            {
                FormEstado.Location = new Point(width / 2 - Estado.Width / 2 + DesplazamientoVentanaEstado, height - Estado.Height);

                FormEstado.Show(null);
                if (frmAIML.ActivarSAPI4)
                {
                    frmAIML.ConectarSAPI4(DireccionIP, Puerto);
                    frmAIML.ActivarTimerSockSAPI4();
                }

                Estado.ActualizarComando("Estatus:  Loading Grammars");
                InicializarGramaticas();
                //recognizer.UnloadAllGrammars();
                //recognizer.LoadGrammar(new DictationGrammar());

                if ((!frmAIML.ActivarSAPI4) && (!(ModoComandoGoogle == "S")))
                    ActivarReconocedorSAPI();

                SiempreEncima((int)FormEstado.Handle);
                mVentanas.CargarVentanas();

            }
            catch (Exception e)
            {
                LOG("Error inicializando en sistema de reconocimiento:" + e.Message);
            }
            AbrirGoogleChrome();
        }
        public void EliminarRecursos()
        {
            if (ActivarBDCoreccion == "S")
            {
                conn.Close();
            }
        }

        string SustituirDirectorios(string dir)
        {
            dir = dir.Replace("{app}", Application.StartupPath);
            dir = dir.Replace("{unidad_app}", Application.StartupPath.ToString().Substring(0, 2));

            return dir;
        }
        #endregion iniciacion     
        // ***************************************************************************************************************************************************************************************
        #region gramatica
        public void InicializarGramaticas()
        { //Carga inicial de las gramáticas disponibles en el arranque

#if (SAPI5 || SAPI11_5)
            if (DictadoSAPI5)
                if (!frmAIML.ActivarSAPI4)
                    GramaticaDictado = new System.Speech.Recognition.DictationGrammar();
#endif

            //Recuperamos las gramáticas activas para el modo actual
            CargarGramaticasModo(this.MODO_CANCELAR, false);
        }

        //Carga la gramática en el sistema de reconocimiento
        Grammar CargarGramaticaReconocedor(string[] palabras, string cultura, GrammarBuilder grammar)
        {
            Grammar g;
            grammar.Culture = new System.Globalization.CultureInfo(cultura);
            grammar.Append(new Choices(palabras));
            g = new Grammar(grammar);

            CargarGramatica(g);

            return g;
        }
        //Recupera los elementos de cada gramática y las carga en el contenedor de elementos devolviendo los componentes a reconocer
        string[] RecuperarElementosGramatica(string Gramatica, bool GramaticaAplicacion)
        {
            ICollection ckeys;
            Hashtable h;
            Hashtable hValores;
            Hashtable hPrecision = new Hashtable();
            Hashtable hAyuda = new Hashtable();
            string sTextoAyuda;
            List<string> lista = new List<string>();
            string palabra;
            String sPrecision = "";
            bool SecPrecision = false;
            bool SecAyuda = false;
            bool GramaticaExtendida = false;

            try
            {
                h = cfg.ReadSection(Gramatica);
                ckeys = h.Keys;

                if (h.Count > 0)
                {
                    //string valores = cfg.ReadSectionKey(Gramatica, "SeccionValores");
                    string valores = h["SeccionValores"].ToString();
                    hValores = cfg.ReadSection(valores);

                    Single PrecisionSeccion = PrecisionMinima;
                    Single Precision = PrecisionMinima;
                    if (h.ContainsKey("GramaticaExtendida"))
                    {
                        if (h["GramaticaExtendida"].ToString() == "S")
                            GramaticaExtendida = true;
                    }

                    if (h.ContainsKey("ValorPrecision"))
                    {
                        sPrecision = h["ValorPrecision"].ToString();
                        PrecisionSeccion = Convert.ToSingle(sPrecision) / 100;
                    }
                    if (h.ContainsKey("SeccionPrecision"))
                    {
                        SecPrecision = true;
                        hPrecision = cfg.ReadSection(h["SeccionPrecision"].ToString());
                    }
                    if (h.ContainsKey("SeccionAyuda"))
                    {
                        SecAyuda = true;
                        hAyuda = cfg.ReadSection(h["SeccionAyuda"].ToString());
                    }

                    foreach (DictionaryEntry entry in h)
                    {
                        string s = entry.Key.ToString();
                        if (!SeccionConfiguracion(s))
                        {
                            Precision = PrecisionMinima;
                            if (SecPrecision)
                                if (hPrecision.Contains(s))
                                    Precision = Convert.ToSingle(hPrecision[s].ToString()) / 100;
                            sTextoAyuda = "";
                            if (SecAyuda)
                                if (hAyuda.Contains(s))
                                    sTextoAyuda = hAyuda[s].ToString();
                            //Devolvemos la gramática y la cargamos en el elemento contenedor de todas las gramáticas de esta clase
                            sGramatica g = new sGramatica();

                            palabra = entry.Value.ToString();

                            g.Nombre = Gramatica;
                            g.Precision = Precision;
                            g.Clave = s;
                            g.palabra = palabra;
                            g.ayuda = sTextoAyuda;
                            g.resultado = hValores[s].ToString();
                            g.ComandoExtendido = GramaticaExtendida;
                            //g.resultado = cfg.ReadSectionKey(valores, s);
                            g.GramaticaAplicacion = GramaticaAplicacion;


                            ElementosGramatica.Add(g);

                            lista.Add(palabra);
                        }
                    }
                }

                //string[] elementos = Array.ConvertAll<int, string>(lista.ToArray(), o=>o.ToString())
                string[] elementos = lista.ToArray();
                return elementos;
            }
            catch (Exception e)
            {
                LOG("ERROR en RecuperarElementosGramatica: " + Gramatica + " - " + e.Message);
                string[] elementos = lista.ToArray();
                return elementos;
            }
        }

        string[] LeerGramaticasModo(string MODO)
        {
            try
            {
                string v = cfg.ReadAppSettingsKey(MODO);

                if (!String.IsNullOrEmpty(v))
                {
                    return v.Split(',');
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        void CargarGramaticasModo(string MODO, bool GramaticaAplicacion)
        {

            string[] Gramaticas = LeerGramaticasModo(MODO + IdiomaGramaticas);

            if (Gramaticas != null)
            {
                foreach (string gramatica in Gramaticas)
                {
                    string g = gramatica;
                    g += IdiomaGramaticas;
                    GramaticaCargada gc = new GramaticaCargada();
                    //Comprobamos si la gramática está creada
                    int i = GramaticasCargadas.FindIndex(x => x.Nombre == g);
                    if (i == -1)
                    { //La gramática nunca ha sido creada
                        string[] ListaElementos = Me.RecuperarElementosGramatica(g, GramaticaAplicacion);
                        if (ListaElementos.Length > 0)
                        {
                            string gramaticaExt = cfg.ReadSectionKey(g, "GramaticaExtendida");
                            if (gramaticaExt == "S")
                                gc.GramaticaExtendida = true;
                            else
                                gc.GramaticaExtendida = false;
                            gc.Nombre = g;
                            gc.GramCargada = true;
                            gc.GramaticaAplicacion = GramaticaAplicacion;
                            gc.Activa = true;

                            if (ModoComandoGoogle == "S") //No es necesario cargar las gramáticas
                            { }
                            else if (frmAIML.ActivarSAPI4)
                            { //Cargamos la gramática por sockets en el reconocedor SAPI4
                                gc.GramaticaSAPI4 = frmAIML.CargarGramatica(ListaElementos, g);
                            }
                            else
                            {
                                if (!gc.GramaticaExtendida) //Los comandos de gramáticas extendidas no deben cargarse en el reconocedor
                                {
                                    Grammar gr = CargarGramaticaReconocedor(ListaElementos, Me.Cultura, new GrammarBuilder());
                                    gc.Gramatica = gr;
                                }
                            }
                            GramaticasCargadas.Add(gc);
                        }
                    }
                    else
                    {
                        GramaticaCargada gram = GramaticasCargadas[i];
                        if (!gram.GramCargada) //Si existe, pero no está cargada
                        {
                            gram.GramCargada = true;

                            if (frmAIML.ActivarSAPI4)
                                frmAIML.ActivarGramatica(gram.GramaticaSAPI4);
                            else
                            {
                                CargarGramatica(gram.Gramatica);
                            }
                        }
                        gram.Activa = true;
                        GramaticasCargadas[i] = gram;
                    }
                }
            }
        }
        //Desactiva las gramáticas indicadas
        void DesactivarGramaticas(bool GramaticasAplicacionActiva)
        {
            int i = 0;

            while ((i = GramaticasCargadas.FindIndex(i, x => ((x.GramaticaAplicacion == GramaticasAplicacionActiva) && (x.Activa == true)))) != -1)
            {
                GramaticaCargada gram = GramaticasCargadas[i];
                gram.Activa = false;
                GramaticasCargadas[i] = gram;
                i++;
            }
        }
        void DescargarGramaticasDesactivas(bool GramaticasAplicacion)
        {
            int i = 0;

            while ((i = GramaticasCargadas.FindIndex(i, x =>
                        ((x.Activa == false) &&
                        (x.GramCargada == true) &&
                        (x.GramaticaAplicacion == GramaticasAplicacion)))
                        ) != -1)
            {
                GramaticaCargada gram = GramaticasCargadas[i];

                if (frmAIML.ActivarSAPI4)
                    frmAIML.DesactivarGramatica(gram.GramaticaSAPI4);
                else
                    DescargarGramatica(gram.Gramatica);

                gram.GramCargada = false;
                GramaticasCargadas[i] = gram;
                i++;
            }
        }

        //Recarga las gramáticas activas en caso de que cambiara la aplicación o el modo
        public void RecargaGramaticasAplicacion()
        {
            string AppActiva = BuscarClaseAppActiva();
            //RECARGA de gramáticas de aplicacicón
            if ((AppActiva != AplicacionActiva) && (AppActiva != ""))
            { //Solo las recargamos si hay un cambio de aplicación
                //if (AdmiteGramaticasAplicacion(this.MODO))
                { //El modo actual soporta gramáticas de aplicación (Si no termina con · no las soporta)
                    AplicacionActiva = AppActiva;
                    DesactivarGramaticas(true);
                    Me.CargarGramaticasModo("$" + AplicacionActiva, true);
                }
            }
        }
        public List<string> BuscarNombresGramaticas(bool Activa)
        {
            List<string> GramActivas = new List<string>();
            foreach (GramaticaCargada g in GramaticasCargadas)
                if (g.Activa == Activa) GramActivas.Add(g.Nombre);
            return GramActivas;
        }

        public List<string> BuscarDescripcionElementosGramatica(string Gramatica)
        {
            List<sGramatica> Elementos = ElementosGramatica.FindAll(x => x.Nombre == Gramatica);
            List<string> salida = new List<string>();

            foreach (sGramatica elemento in Elementos)
                salida.Add(elemento.palabra + Constants.vbTab + elemento.resultado + Constants.vbTab + elemento.ayuda);

            return salida;
        }

        public void CargarGramatica(Grammar gram)
        {
            recognizer.LoadGrammar(gram);
        }

        public void DescargarGramatica(Grammar g)
        {
            recognizer.UnloadGrammar(g);
        }

#if SAPI5
        public void CargarGramaticaDictado(DictationGrammar dic)
        {
                recognizer.LoadGrammar(GramaticaDictado);
        }
        public void DescargarGramaticaDictado(DictationGrammar dic)
        {
                recognizer.UnloadGrammar(GramaticaDictado);
        }
#elif SAPI11_5
        public void CargarGramaticaDictado(System.Speech.Recognition.DictationGrammar dic)
        {
            if (DictadoSAPI5)
                recognizerDictado.LoadGrammar(GramaticaDictado);
        }
        public void DescargarGramaticaDictado(System.Speech.Recognition.DictationGrammar dic)
        {
            recognizerDictado.UnloadGrammar(GramaticaDictado);
        }
#endif
        public void DescargarTodasLasGramaticas()
        {
            recognizer.UnloadAllGrammars();
        }

        bool SeccionConfiguracion(string s)
        {
            if ((s != "SeccionValores") && (s != "SeccionPrecision") && (s != "ValorPrecision") && (s != "SeccionAyuda") && (s != "GramaticaExtendida"))
                return false;
            else
                return true;
        }

        #endregion gramatica
        //*****************************************************************************************************************************************************************************************
        #region reconocimiento

        static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Me.TextoReconocido(e.Result.Text, e.Result.Confidence, false);
        }
#if SAPI11_5
        static void recognizer_SpeechRecognized_dictado(object sender, System.Speech.Recognition.SpeechRecognizedEventArgs e)
        {
            if (DictadoSAPI5)
                Me.TextoReconocido(e.Result.Text, e.Result.Confidence, true);
        }
#endif
        public void TextoReconocidoSAPI4(string texto)
        {
            TextoReconocido(texto, (float)0.9999, false);
        }
        public void TextoReconocidoComandoGoogle(string texto)
        {
            //Solo  llegamos aquí desde el delegado si estamos en modos GOOGLE
            while (texto != "")
            {
                texto = texto.Trim();
                string resultado = TextoReconocido(texto, (float)0.9999, false);
                if ((resultado == "") || (resultado == null))
                    break;
                //Eliminamos la parte del comando y comprobamos si puede haber nuevos comandos detrás
                texto = texto.Substring(resultado.Length);
            }
        }

        //************************ Procesamiento inicial de entradas de eventos de reconocimiento ************************************
        public string TextoReconocido(string texto, float GradoPrecision, bool ModoDictadoIdiomas)
        {
            bool OkXulia_entrada = OkXulia;
            if ((texto == "") || (!ARRANQUE_COMPLETADO)) return "";
            if (AutoDesactivacion)
                Estado.ActivarTimerDesactivacion();

            try
            {
                sGramatica ElementoGram = new sGramatica();

                Estado.AsignarPrecision(GradoPrecision);
                EstadoOcultarBarraPrecision();

                GRADO_PRECISION_TEXTO = GradoPrecision;
                salida = texto;
                precision = GradoPrecision;
                if (!XULIA_ACTIVA)
                {
                    bool comando_encontrado = false;
                    for (int i = 0; i < XuliaComandosDesactivados.Length; i++)
                    {
                        if (salida == XuliaComandosDesactivados[i])
                            comando_encontrado = true;
                    }
                    if (!comando_encontrado && (salida != XuliaActivar) && (salida != XuliaAtencion))
                    {
                        frmAIML.CambiarIcono(aimlForm.TipoIcono.desactiva);
                        return "";
                    }
                }

                txtSalida.AppendText(salida + " (" + GradoPrecision.ToString().Substring(0, 3) + "%)" + Environment.NewLine);

                //Establece los iconos según el reconocimiento
                if ((MODO == "·DICTADO") || (MODO == "·CONVERSACION"))
                {
                    if (GradoPrecision < PrecisionModoDictado)
                    {
                        Estado.ComandoEncontrado(Comandos.ComandoNoPreciso);
                        frmAIML.CambiarIcono(aimlForm.TipoIcono.falta_precision);
                        return "";
                    }
                    else
                        EstadoMostrarBarraPrecision();
                }

                if ((DictadoGoogleSiempreMinusculas == "S") && ModoDictadoIdiomas)
                    texto = texto.ToLower();

                Estado.IconoEspera(true);

                bool ComandoExtendidoEncontrado = false;
                //Si estamos en OkXulia buscamos comandos extendidos, sino pasamos a comandos de gramáticas normales
                if (OkXulia)
                {
                    sGramatica Elemento = BuscarComandoExtendido(false, texto, (float)0.99);
                    if (Elemento.Nombre != null)
                        ComandoExtendidoEncontrado = true;
                    ElementoGram = Elemento;
                }
                //Si no encontramos comandos extendidos o no estamos en modo OKXulia buscamos comandos normales
                if (!ComandoExtendidoEncontrado)
                {
                    RecargaGramaticasAplicacion();
                    if (!AdmiteGramaticasAplicacion(this.MODO))
                        //Buscamos solo en las gramáticas estandar
                        ElementoGram = Me.BuscarComando(false, salida, GradoPrecision);
                    else
                    { //Buscamos en las gramáticas de aplicación y si no en las estandar
                        ElementoGram = Me.BuscarComando(true, salida, GradoPrecision);
                        if (ElementoGram.resultado == "")
                            ElementoGram = Me.BuscarComando(false, salida, GradoPrecision);
                    }
                }

                if ((Strings.InStr(this.MODO, "·DICTADO") > 0) || (Strings.InStr(this.MODO, "·DICTWIN") > 0) || (Strings.InStr(this.MODO, "·DICTUWP") > 0))
                {
                    frmAIML.CambiarIcono(aimlForm.TipoIcono.dictado);
                    Estado.ComandoEncontrado(Comandos.ComandoEncontrado);
                    EstadoMostrarBarraPrecision();
                }
                else
                {
                    if (ElementoGram.resultado != "")
                    {
                        frmAIML.CambiarIcono(aimlForm.TipoIcono.activa);
                        if (MostrarGloboComandos == "S")
                            frmAIML.MostrarGlobo(texto + " (" + GradoPrecision.ToString().Substring(0, 4) + "%)", MODO, ClaseAppActiva);
                        Estado.ComandoEncontrado(Comandos.ComandoEncontrado);
                        EstadoMostrarBarraPrecision();
                    }
                    else
                    {
                        frmAIML.CambiarIcono(aimlForm.TipoIcono.falta_precision);
                        Estado.ComandoEncontrado(Comandos.ComandoNoEncontrado);
                    }
                }

                //Ejecutamos el comando según el modo CONVERSACION, DICTADO, REJILLA o COMANDO
                InterpretarComandoEnModo(ElementoGram, salida, ModoDictadoIdiomas);

                if (OkXulia_entrada && OK_XULIA_UnComando)
                {
                    OkXulia = false;
                    VolverModo(MODO, MODO_CANCELAR);
                }

                if (OcultarEstadoSegundos > 0)
                    ActualizarPantallas(true);
                else
                    ActualizarPantallas(false);
                RecargaGramaticasAplicacion();

                Estado.IconoEspera(false);

                return ElementoGram.palabra;
            }
            catch (Exception e)
            {
                LOG("Error en TextoReconocido" + e.Message);
            }
            return "";
        }
        public void EnviarTecla(string tecla)
        {
            try
            {
                if (ACENTO || ACENTO_CIRCUNFLEJO || ACENTO_GRAVE || DIERESIS || TILDE)
                {
                    if (EsVocal(tecla))
                    {
                        switch (Microsoft.VisualBasic.Strings.LCase(tecla)[0])
                        {
                            case 'a':
                                {
                                    if (ACENTO)
                                        tecla = "á";
                                    else if (ACENTO_GRAVE)
                                        tecla = "à";
                                    else if (ACENTO_CIRCUNFLEJO)
                                        tecla = "â";
                                    else if (DIERESIS)
                                        tecla = "ä";
                                    else if (TILDE)
                                        tecla = "ã";
                                    break;
                                }
                            case 'e':
                                {
                                    if (ACENTO)
                                        tecla = "é";
                                    else if (ACENTO_GRAVE)
                                        tecla = "è";
                                    else if (ACENTO_CIRCUNFLEJO)
                                        tecla = "ê";
                                    else if (DIERESIS)
                                        tecla = "ë";
                                    break;
                                }
                            case 'i':
                                {
                                    if (ACENTO)
                                        tecla = "í";
                                    else if (ACENTO_GRAVE)
                                        tecla = "ì";
                                    else if (ACENTO_CIRCUNFLEJO)
                                        tecla = "î";
                                    else if (DIERESIS)
                                        tecla = "ï";
                                    break;
                                }
                            case 'o':
                                {
                                    if (ACENTO)
                                        tecla = "ó";
                                    else if (ACENTO_GRAVE)
                                        tecla = "ò";
                                    else if (ACENTO_CIRCUNFLEJO)
                                        tecla = "ô";
                                    else if (DIERESIS)
                                        tecla = "ö";
                                    else if (TILDE)
                                        tecla = "õ";
                                    break;
                                }
                            case 'u':
                                {
                                    if (ACENTO)
                                        tecla = "ú";
                                    else if (ACENTO_GRAVE)
                                        tecla = "ù";
                                    else if (ACENTO_CIRCUNFLEJO)
                                        tecla = "û";
                                    else if (DIERESIS)
                                        tecla = "ü";
                                    break;
                                }
                        }
                    }
                }

                string Modificador = "";
                if (Me.MAYUSCULAS) tecla = tecla.ToUpper();

                if (Me.UNAMAYUSCULA)
                {
                    if (tecla.Length > 1)
                        tecla = tecla.Substring(0, 1).ToUpper() + tecla.Substring(1);
                    else
                        tecla = tecla.ToUpper();
                    Me.UNAMAYUSCULA = false;
                }

                if (CONTROL) Modificador += "^";
                if (ALT) Modificador += "%";
                if (SELECCION) Modificador += "+";

                DesactivarTelcasModificadoras();
                //Desactivar acentos
                ACENTO = ACENTO_GRAVE = TILDE = ACENTO_CIRCUNFLEJO = DIERESIS = false;

                tecla = Modificador + tecla;

                for (int i = 0; i < Me.Multiplicador; i++)
                    EnviarEventosTeclas(tecla);

                Me.Multiplicador = 1;
            }
            catch (Exception e)
            {
                LOG("Error en EnviarTecla: " + tecla + ", Error: " + e.Message);
            }
        }

        bool EsVocal(string s)
        {
            try
            {
                if (Strings.InStr("aeiou", s.ToLower().Substring(0, 1)) > 0)
                    return true;
            }
            catch (Exception e)
            {
                LOG("Error en EsVocal: " + e.Message);
            }
            return false;
        }

        //Busca comando en gramáticas normales con un comando por palabra
        sGramatica BuscarComando(bool GramaticaAplicacion, string entrada, float GradoPrecision)
        {
            //Método con otra firma
            return BuscarComando(GramaticaAplicacion, entrada, GradoPrecision, true);
        }
        //Busca comando en gramáticas normales con un comando por palabra
        sGramatica BuscarComando(bool GramaticaAplicacion, string entrada, float GradoPrecision, bool GramaticaActiva)
        {
            //var consulta = from elemento in ElementosGramatica where (elemento.GramaticaAplicacion == GramaticaAplicacion) && (elemento.palabra == entrada)  select elemento;
            //foreach (sGramatica elemento in consulta)
            //    return elemento;
            sGramatica gramatica = new sGramatica();

            try
            {
                List<GramaticaCargada> GramActivas = GramaticasCargadas.FindAll(x => ((x.Activa == GramaticaActiva) && (x.GramaticaAplicacion == GramaticaAplicacion) && (x.GramaticaExtendida == false)));

                //Buscamos en las gramáticas activas del tipo especificado
                foreach (GramaticaCargada gra in GramActivas)
                {
                    sGramatica Elemento;
                    if (ModoComandoGoogle == "S")
                        Elemento = ElementosGramatica.Find(x => ((x.Nombre == gra.Nombre) && (entrada.ToLower().IndexOf(x.palabra) == 0) && (x.ComandoExtendido == false) && (x.Precision <= GradoPrecision)));
                    else
                        Elemento = ElementosGramatica.Find(x => ((x.Nombre == gra.Nombre) && (x.palabra == entrada) && (x.ComandoExtendido == false) && (x.Precision <= GradoPrecision)));
                    if (Elemento.Nombre != null)
                    { //Comando encontrado con la precisión necesaria
                        return Elemento;
                    }
                }

                gramatica = new sGramatica();
                gramatica.resultado = "";
            }
            catch (Exception e)
            {
                LOG("Error en BuscarComando:" + e.Message);
            }

            return gramatica;
        }
        //Busca comando en gramáticas extendidas con comandos con múltiples parámetros
        sGramatica BuscarComandoExtendido(bool GramaticaAplicacion, string entrada, float GradoPrecision)
        {
            //var consulta = from elemento in ElementosGramatica where (elemento.GramaticaAplicacion == GramaticaAplicacion) && (elemento.palabra == entrada)  select elemento;
            //foreach (sGramatica elemento in consulta)
            //    return elemento;
            sGramatica gramatica = new sGramatica();
            List<sGramatica> Elementos = new List<sGramatica>();

            try
            {
                List<GramaticaCargada> GramActivas = GramaticasCargadas.FindAll(x => ((x.Activa == true) && (x.GramaticaAplicacion == GramaticaAplicacion) && (x.GramaticaExtendida == true)));

                //Buscamos en las gramáticas activas del tipo especificado
                foreach (GramaticaCargada gra in GramActivas)
                {
                    string[] par = new string[1];
                    Elementos = ElementosGramatica.FindAll(x => ((x.Nombre == gra.Nombre) && (x.ComandoExtendido == true) && (x.Precision <= GradoPrecision)));
                    foreach (sGramatica elem in Elementos)
                    {
                        //Recupera la información de los parámetros
                        if (Match(elem.palabra.ToLower(), entrada.ToLower(), ref par))
                        {
                            gramatica = elem;
                            gramatica.parametros = par;
                            return gramatica;
                        }
                    }
                }

                gramatica = new sGramatica();
                gramatica.resultado = "";
            }
            catch (Exception e)
            {
                LOG("Error en BuscarComando:" + e.Message);
            }

            return gramatica;
        }
        public bool Match(string patron, string entrada, ref string[] par)
        {
            string[] cad = patron.Split('*');
            par = new string[cad.Length];
            //Localizamos todos los componentes
            int pos_ini = -1;
            int pos_par = 0;
            bool encontrado = true;
            int pos = 0;
            string buscarCad = cad[0];
            int i = 0;

            if (buscarCad == "")
                pos_ini = 0;
            else
            {
                pos_ini = Strings.InStr(entrada, buscarCad);
                if (pos_ini > 0)
                    pos_ini += buscarCad.Length - 1;
                else
                    return false;
            }
            if (pos_ini >= 0)
            {
                for (i = 1; i < cad.Length; i++)
                {
                    buscarCad = cad[i];

                    if (buscarCad == "")
                    { //Solo puede ser vacío el último
                        par[pos_par++] = entrada.Substring(pos_ini);
                        break;
                    }
                    else
                        pos = Strings.InStr(entrada, buscarCad);
                    if (pos > 0)
                    {
                        par[pos_par++] = entrada.Substring(pos_ini, pos - 1 - pos_ini);
                        pos_ini = pos - 1 + buscarCad.Length;
                        entrada = entrada.Substring(pos_ini);
                        pos_ini = 0;
                    }
                    else
                    {
                        encontrado = false;
                        break;
                    }
                }
            }
            else
                encontrado = false;

            //Hemos encontrado un elemento vacío y no era el último
            if (encontrado && (i != cad.Length - 1))
                encontrado = false;

            return encontrado;
        }

        //Comprueba los modos activos de comportamiento programado especial
        void InterpretarComandoEnModo(sGramatica ElementoGram, string salida, bool ModoDictadoIdiomas)
        {
            try
            {
                //Primero comprobamos si estamos en modos de comportamiento específico
                if (ElementoGram.resultado != "")
                {
                    if (MODO == "·REJILLA·")
                    #region REJILLA
                    {
                        if ((ElementoGram.Clave == "CANCELAR") || ((ElementoGram.Clave == "VOLVER")))
                        {
                            if (ElementoGram.Clave == "CANCELAR")
                            {
                                VolverModo(MODO, MODO_CANCELAR);
                                MODO = MODO_CANCELAR;
                            }
                            else
                            {
                                VolverModo(MODO, MODO_ANT);
                                MODO = MODO_ANT;
                            }
                            Me.MODORERIJA = ModoRejilla.Columna;
                            Me.Rejilla.Hide();
                            return;
                        }

                        int pos = 0;
                        try
                        {
                            pos = Convert.ToChar(ElementoGram.Clave.Substring(0, 1));
                        }
                        catch
                        {
                            return;
                        }

                        if ((pos >= 'a') && (pos <= 'z'))
                        { //Se ha localizado un código de columna
                            pos = pos - 'a';
                            switch (Me.MODORERIJA)
                            {
                                case ModoRejilla.Columna:
                                    {
                                        float salto = (float)Me.Rejilla.Width / NUM_COLS_REJILLA;

                                        if (Me.DatosRejilla.AjusteFino > 0)
                                            RejillaPosX = (int)(salto * pos + (salto / 3) * ((Me.DatosRejilla.AjusteFino - 1) % 3) + (salto / 3) / 2);
                                        else
                                            RejillaPosX = (int)(salto * pos + salto / 2);

                                        //Cambio a columna
                                        Me.MODORERIJA = ModoRejilla.Fila;
                                        //if (MarcarSeleccionRejilla) Me.Rejilla.MarcarRejillaFila(pos);
                                        if (MarcarSeleccionRejilla) Me.Rejilla.MarcarRejillaColumna(pos);
                                        Me.Rejilla.PintarRejilla();
                                    }
                                    break;
                                case ModoRejilla.Fila:
                                    {
                                        float salto = (float)Me.Rejilla.Height / NUM_FILAS_REJILLA;

                                        if (Me.DatosRejilla.AjusteFino > 0)
                                            RejillaPosY = (int)(salto * pos + (salto / 3) * ((int)(Me.DatosRejilla.AjusteFino - 1) / (int)3) + (salto / 3) / 2);
                                        else
                                            RejillaPosY = (int)(salto * pos + salto / 2);
                                        Me.PosCursor.Y = RejillaPosY;
                                        Me.PosCursor.X = RejillaPosX;

                                        //Posicionarse y salir del modo
                                        ControlRaton.MouseMove(PosCursor);
                                        VolverModo(MODO, MODO_ANT);
                                        MODO = MODO_ANT;
                                        Me.MODORERIJA = ModoRejilla.Columna;
                                        Me.Rejilla.Hide();
                                    }
                                    break;
                            }
                        }
                        else if (Me.MODORERIJA == ModoRejilla.Columna)
                        {
                            pos = (int)Conversion.Val(ElementoGram.Clave);
                            if ((pos >= 1) && (pos <= 9))
                            { //Estamos indicando un ajuste fino de la rejilla
                                Me.DatosRejilla.AjusteFino = pos;
                            }
                        }
                    }
                    #endregion REJILLA
                    else //Comandos comunes al resto de los modos, incluído los cambios de MODO **************************************
                    {
                        Me.InterpretarSecuencia(ElementoGram);
                    }
                }
                else // ·DICTADO y ·CONVERSACION. No se ha reconocido un comando en las gramáticas activas pasamos a dictado y dictado google
                {
                    #region DICTADO
                    if (MODO == "·CONVERSACION")
                    {
                        ProcesarEntradaModoConversacion(salida);
                    }
                    if ((Strings.InStr(MODO, "·DICTUWP·") > 0) || (Strings.InStr(MODO, "·DICTWIN·") > 0) || (Strings.InStr(MODO, "·DICTADO·") > 0) || (Strings.InStr(MODO, "·CONVERS·") > 0))
                    { //Dictado o conversación con Google Chrome
                        if (!ModoDictadoIdiomas) return;
                        string Idioma = MODO.Substring(9);
                        //Aplicamos las sustituciones a la cadena
                        DictadoContinuo dc = DictadosActivos.Find(X => X.idioma == Idioma);
                        if (dc.codigo != null)
                        {
                            salida = salida.ToLower();
                            if (NUEVA_LINEA)
                                salida = NuevaLinea(salida);

                            NUEVA_LINEA = false;
                            foreach (sSustitucionesDictado s in dc.Sustituciones)
                                NUEVA_LINEA = NUEVA_LINEA | SustituirFraseEntrada(ref salida, s.FraseEntrada, s.FraseSalida, dc.InstruccionLiteral);

                            int pos = Strings.InStr(salida, dc.InstruccionSalida);
                            if (pos > 0)
                            {
                                if (pos > 1)
                                    salida = salida.Substring(0, pos - 2) + salida.Substring(pos - 1 + dc.InstruccionSalida.Length);
                                else
                                    salida = salida.Substring(pos - 1 + dc.InstruccionSalida.Length);
                                VolverModo(MODO, MODO_CANCELAR);
                                MODO_ANT = MODO_CANCELAR;
                                MODO = MODO_CANCELAR;
                            }
                            if (Me.MAYUSCULAS) salida = salida.ToUpper();
                            if (Strings.InStr(MODO, "·CONVERS·") > 0)
                                ProcesarEntradaModoConversacion(salida);
                            else
                                if (Strings.InStr(MODO, "·DICTUWP·") > 0)
                                EnviarTeclasVentanaUWP(salida);
                            else
                                EnviarEventosTeclas(salida);
                        }
                    }
                    #endregion DICTADO
                }
            }
            catch (Exception e)
            {
                LOG("Error en InterpretarComandoEnModo con entrada: " + ElementoGram.resultado + ", " + salida + ", Error: " + e.Message);
            }
        }
        void ProcesarEntradaModoConversacion(string salida)
        {
            //Intentamos localizar un comando extendido
            sGramatica Elemento = BuscarComandoExtendido(false, salida, (float)0.99);
            if (Elemento.Nombre != null)
                Me.InterpretarSecuencia(Elemento);
            else
            {
                //Comprobamos que no se ha detectado algún comando cargado
                sGramatica cmd = Me.BuscarComando(false, salida, (float)0.99, false);
                if (cmd.Nombre == null)
                {
                    String SalidaAIML = "";
                    cmd = Me.BuscarComando(true, salida, (float)0.99, false);
                    if ((cmd.Nombre == null) && InteractuarConXulia)
                    {
                        SalidaAIML = frmAIML.ProcesarEntrada(salida);
                    }
                }
            }
        }
        bool SustituirFraseEntrada(ref string frase, string FraseEntradaSus, string FraseSalida, string literal)
        {
            bool nueva_linea = false;
            try
            {
                int pos_ini = 1;
                string frase_d = "";
                string frase_i = "";
                bool SpcDerecha = false;
                bool SpcIzquierda = false;
                int cont_espacios_derecha = 0;
                int cont_espacios_izquierda = 0;
                string FraseEntrada = FraseEntradaSus;

                if (Strings.Right(FraseEntrada, 1) == "*")
                {
                    SpcDerecha = true;
                    FraseEntrada = FraseEntrada.Substring(0, FraseEntrada.Length - 1);
                }
                if (Strings.Left(FraseEntrada, 1) == "*")
                {
                    SpcIzquierda = true;
                    FraseEntrada = FraseEntrada.Substring(1);
                }

                int pos = Strings.InStr(pos_ini, frase, FraseEntrada);
                //Localizamos el patrón aunque google lo devuelva con la primera en mayúsculas
                if ((DictadoGoogleSiempreMinusculas == "S") && (pos < 0))
                    pos = Strings.InStr(pos_ini, frase, FraseEntrada.Substring(0, 1).ToUpper() + FraseEntrada.Substring(1));

                while (pos > 0)
                {

                    int literal_pos = Strings.InStr(pos_ini, frase, literal + " " + Strings.Trim(FraseEntrada));
                    if ((literal_pos == pos - 1 - literal.Length))
                    { //la frase a sustituir tiene literal delante => no se puede sustituir y se elimina literal
                        pos = pos + FraseEntrada.Length;
                        frase = frase.Substring(0, literal_pos - 1) + frase.Substring(literal_pos - 1 + literal.Length + 1);
                    }
                    else
                    {
                        cont_espacios_izquierda = 0;
                        if (SpcIzquierda)
                        {
                            int pos_spc = pos - 1;
                            while (pos_spc - 1 >= 0)
                            {
                                if (frase.Substring(pos_spc - 1, 1) == " ")
                                    cont_espacios_izquierda++;
                                else
                                    break;
                                pos_spc--;
                            }
                        }
                        cont_espacios_derecha = 0;
                        if (SpcDerecha)
                        {
                            int pos_spc = pos + FraseEntrada.Length;
                            while (pos_spc < frase.Length)
                            {
                                if (frase.Substring(pos_spc - 1, 1) == " ")
                                    cont_espacios_derecha++;
                                else
                                    break;
                                pos_spc++;
                            }
                        }

                        frase_i = (pos - 1 > 0 ? frase.Substring(0, pos - 1 - cont_espacios_izquierda) : "");
                        frase_d = frase.Substring(pos - 1 + cont_espacios_derecha + FraseEntrada.Length);
                        if (frase_d.Trim() == "") frase_d = "";

                        string salida = FraseSalida;
                        if (Strings.InStr(FraseSalida, "[NUEVA_LINEA]") > 0)
                        {
                            salida = Strings.Replace(salida, "[NUEVA_LINEA]", "{ENTER}");
                            if (Strings.Trim(frase_d) == "")
                                nueva_linea = true;
                            else
                                frase_d = NuevaLinea(frase_d);
                        }
                        if (Strings.InStr(FraseSalida, "[NUEVA_FRASE]") > 0)
                        {
                            salida = Strings.Replace(salida, "[NUEVA_FRASE]", "");
                            if (Strings.Trim(frase_d) == "")
                                nueva_linea = true;
                            else
                                frase_d = NuevaLinea(frase_d);
                        }
                        else if (Strings.InStr(FraseSalida, "[COMILLAS]") > 0)
                            salida = salida = Strings.Replace(salida, "[COMILLAS]", "\"");
                        else if (Strings.InStr(FraseSalida, "[MENOR]") > 0)
                            salida = salida = Strings.Replace(salida, "[MENOR]", "<");
                        else if (Strings.InStr(FraseSalida, "[MAYOR]") > 0)
                            salida = salida = Strings.Replace(salida, "[MAYOR]", ">");
                        else if (Strings.InStr(FraseSalida, "[AMP]") > 0)
                            salida = salida = Strings.Replace(salida, "[AMP]", "&");

                        frase = frase_i + salida + frase_d;
                        pos += salida.Length;
                    }
                    pos_ini = pos - cont_espacios_izquierda;
                    pos = Strings.InStr(pos_ini, frase, FraseEntrada);
                }
            }
            catch (Exception e)
            {
                LOG("Error en SustituirFraseEntrada, frase:" + frase + ", fraseentradasus: " + FraseEntradaSus + ", FraseSalida:" + FraseSalida + ", literal: " + literal + ", Error: " + e.Message);
            }
            return nueva_linea;
        }
        string NuevaLinea(string frase)
        {
            try
            {
                if (frase != "")
                {
                    frase = frase.Trim();
                    return frase.Substring(0, 1).ToUpper() + frase.Substring(1);
                }
            }
            catch (Exception e)
            {
                LOG("Error en nueva línea:" + e.Message);
            }
            return "";
        }
        bool Modificador(string comando)
        {
            if ((comando == "[CONTROL]") || (comando == "[ALT]") || (comando == "[UNAMAYUSCULA]") || (comando == "[ACENTO]") ||
                (comando == "[ACENTO_GRAVE]") || (comando == "[ACENTO_CIRCUNFLEJO]") ||
                (comando == "[DIERESIS]") || (comando == "[ACENTO_TILDE]") || (comando == "[ALT]"))
                return true;
            else
                return false;
        }
        DictadoContinuo CargarCadenasSustitucion(string Idioma)
        {
            DictadoContinuo dc = DictadosActivos.Find(x => x.idioma == Idioma);

            if (dc.codigo == null)
            {
                dc = new DictadoContinuo();
                dc.InstruccionLiteral = "literal";
                dc.InstruccionSalida = "cancelardictado";
                dc.idioma = Idioma;
                dc.proceso = null;
                dc.Sustituciones = new List<sSustitucionesDictado>();
                //Cargamos las sustituciones de este idioma
                ICollection ckeys;
                Hashtable h;
                string clave = cfg.ReadAppSettingsKey("·DICTADO·" + Idioma + "·SUSTITUCIONES·");
                string[] Listas = clave.Split(',');
                for (int i = 0; i < Listas.Length; i++)
                {
                    h = cfg.ReadSection(Listas[i]);
                    ckeys = h.Keys;
                    int Orden = 0;
                    foreach (string entrada in ckeys)
                    {
                        //string salida = cfg.ReadSectionKey(Listas[i], entrada);
                        string[] salida = h[entrada].ToString().Split('·');
                        string FraseSalida = "";
                        if (salida.Length == 2)
                        {
                            FraseSalida = salida[1];
                            Orden = Convert.ToInt16(salida[0]);
                        }
                        else
                        {
                            FraseSalida = salida[0];
                            Orden = 0;
                        }

                        if (entrada == "·CodigoIdioma")
                            dc.codigo = FraseSalida;
                        else if (entrada == "·SalirDictado")
                            dc.InstruccionSalida = FraseSalida;
                        else if (entrada == "·Literal")
                            dc.InstruccionLiteral = FraseSalida;
                        else
                        {
                            sSustitucionesDictado Sustitucion = new sSustitucionesDictado();
                            Sustitucion.FraseEntrada = entrada;
                            Sustitucion.FraseSalida = FraseSalida;
                            Sustitucion.Orden = Orden++;
                            dc.Sustituciones.Add(Sustitucion);
                        }
                    }
                    dc.Sustituciones = dc.Sustituciones.OrderBy(x => x.Orden).ToList();
                }
            }
            DictadoContinuo dc1 = DictadosActivos.Find(x => x.idioma == Idioma);
            if (dc1.codigo == null)
                DictadosActivos.Add(dc);

            return dc;
        }
        void ActivarGramaticaModoDictado()
        {
#if (SAPI5 || SAPI11_5) //En SAPI11 no funciona el dictado contínuo
            CargarGramaticaDictado(GramaticaDictado);
            GramaticaDictadoCargada = true;
#if SAPI11_5
            //SAPI11 no soporta dictado, por lo que se activa en SAPI5 (Si estamos en SAPI 5 empleamos el mismo reconocedor)
            if (DictadoSAPI5)
                ActivarReconocedorDictado();
#endif
#endif
        }
        //Realiza el cambio de modo y la carga de las gramáticas del nuevo modo, menos en dictado google
        int LocalizarSaltoParrafo(string cad)
        {
            int pos13 = cad.IndexOf((char)13);
            int pos10 = cad.IndexOf((char)10);
            int pos = (pos10 > pos13 ? pos10 : pos13);

            return pos10;
        }
        void DesactivarTelcasModificadoras()
        {
            if (MantenerPulsadoAlternativo == "N") ALT = false;
            if (MantenerPulsadoControl == "N") CONTROL = false;
        }

        public void DesactivarXulia()
        {
            frmAIML.CambiarIcono(aimlForm.TipoIcono.desactiva);
            if (MODO != MODO_CANCELAR)
                VolverModo(MODO, MODO_CANCELAR);
            XULIA_ACTIVA = false;
            MODO = MODO_CANCELAR;
            Estado.MostrarIconoDesactivarXulia(true);
            ServWeb.EnviarComando("PARAR");
        }
        public void ActivarXulia()
        {
            XULIA_ACTIVA = true;
            frmAIML.CambiarIcono(aimlForm.TipoIcono.activa);
            Estado.MostrarIconoDesactivarXulia(false);
        }

        public Process EjecutarChromeRecVoz(string Idioma, string CodigoIdiomaGoogle, string auto, int EsperaMS)
        {
            Process p = new Process();
            String activo;

            BuscarClaseAppActiva();
            ServWeb.ConfigVar(Idioma, CodigoIdiomaGoogle, auto);
            EnviarComando("PARAR");

            bool ChromeAbierto = false;
            //Comprobamos si el navegador se encuentra ya levantado
            //Si quedó mal cerrada la ventana de Chrome, primero la cierra
            if (BuscaTituloVentanaChrome("RECVOZ.GOOGLE." + Idioma + ".DESACTIVO"))
            {
                //Si está abierta la cierro
                ChromeAbierto = true;
                ActivarVentanaTitulo("RECVOZ.GOOGLE." + Idioma + ".DESACTIVO");
                MaximizarMinimizarVentanaActiva(SW_SHOWMAXIMIZED);
                SendKeys.Send("%{F4}");

                //Espero a que se cierre
                int contador = 0;
                while (contador < MAX_ESPERA_CIERRE_CHROME_100MS)
                {
                    if (!BuscaTituloVentanaChrome("RECVOZ.GOOGLE." + Idioma + ".DESACTIVO"))
                    {
                        ChromeAbierto = false;
                        break;
                    }
                    else
                        contador++;
                    Thread.Sleep(100);
                }
            }

            if (ChromeAbierto)
            {
                //Esperamos 3 segundos antes de mostrar el error de que sigue abierto
                while (EsperaTituloVentanaChrome("RECVOZ.GOOGLE." + Idioma + ".DESACTIVO", 3000))
                    MessageBox.Show("Cierre el navegador Chrome antes de ejecutar Xulia");
            }

            p.StartInfo.FileName = "chrome";
            p.StartInfo.Arguments = "--app=" + URLBaseIdioma + "?idioma=" + Idioma + "&codigo=" + CodigoIdiomaGoogle + "&auto=" + auto;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p.Start();

            //Espero TiempoEsperaArranqueChrome segundos para comprobar que se ejecuta
            if (!EsperaTituloVentanaChrome("RECVOZ.GOOGLE." + Idioma + ".DESACTIVO", TiempoEsperaArranqueChrome * 1000))
            {
                //No se ha ejecutado chrome correctamente, cancelamos el modo
                MODO = MODO_CANCELAR;
                return null;
            }

            if (MinimizarChrome)
            {
                ActivarVentanaTitulo("RECVOZ.GOOGLE." + Idioma + ".DESACTIVO");
                MaximizarMinimizarVentanaActiva(SW_SHOWMINIMIZED);
            }

            if (auto == "S")
                EnviarComando("REANUDAR." + Idioma);

            //Activamos la aplicación desde la que se lanzó el dictado de idiomas
            ActivarVentanaTitulo(TituloVentanaAppActiva);

            return p;
        }
        public void EnviarComando(String comando)
        {
            if (SR_TOMCAT)
            {
                System.IO.File.WriteAllText(ArchivoRecVoz, "");
                System.IO.File.WriteAllText(ArchivoComandos, comando);
            }
            else
                ServWeb.EnviarComando(comando);
        }

        public void DesactivarReconocimiento()
        {
            if (MODO.IndexOf("·CONVER") == 0)
            { //Estamos en modo conversación
                string Idioma = MODO.Substring(9);
                EnviarComando("PARAR");
                if (!EsperaTituloVentanaChrome("RECVOZ.GOOGLE." + Idioma + ".DESACTIVO", 4000))
                    //No se ha ejecutado chrome correctamente, cancelamos el modo
                    LOG("Error en DesactivarReconocimiento: Tiempo de espera superado para desactivar reconocimiento google");
                //Thread.Sleep(ESPERA_PARADA_REC_GOOGLE);
            }
            else
                recognizer.RecognizeAsyncCancel();
        }
        public void ActivarReconocimiento()
        {
            if (MODO.IndexOf("·CONVER") == 0)
            { //Estamos en modo conversación
                string Idioma = MODO.Substring(9);
                EnviarComando("REANUDAR." + Idioma);
                if (!EsperaTituloVentanaChrome("RECVOZ.GOOGLE." + Idioma + ".ACTIVO", 4000))
                    //No se ha ejecutado chrome correctamente, cancelamos el modo
                    LOG("Error en ActivarReconocimiento: Tiempo de espera superado para activar reconocimiento google");
            }
            else
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

#if SAPI11_5
        public void ActivarReconocedorDictado()
        { //recognizerDictado es SAPI 5 y recognizer es SAPI 11
            if (DictadoSAPI5)
                recognizerDictado.RecognizeAsync(System.Speech.Recognition.RecognizeMode.Multiple);
            //recognizer.RecognizeAsyncStop();
        }
        public void DesactivarReconocedorDictado()
        { //recognizerDictado es SAPI 5 y recognizer es SAPI 11
            if (DictadoSAPI5)
                recognizerDictado.RecognizeAsyncStop();
            //recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }
#endif
        #endregion reconocimiento 
        //*****************************************************************************************************************************************************************************************
        #region EjecutarComandos

        //Ejecución de comandos o cambio de modo de operación
        string SustituirParamatros(string comandos, string[] par, int num_par)
        {
            for (int i = 0; i < num_par; i++)
                comandos = comandos.Replace("*" + (i + 1).ToString(), par[i]);
            return comandos;
        }
        bool InterpretarSecuencia(sGramatica ElementoGram)
        {
            try
            {
                string comandos = ElementoGram.resultado;
                //Si hay parámetros por ser un comando extendido, los sustituímos *1,*2,...
                if (ElementoGram.parametros != null)
                    comandos = SustituirParamatros(comandos, ElementoGram.parametros, ElementoGram.parametros.Length);
                string[] secuencia = comandos.Split('|');
                for (int i = 0; i < secuencia.Length; i++)
                {
                    if (secuencia[i][0] == '[')
                    { //Comando de secuencia
                        EjecutarComando(secuencia[i], ElementoGram.parametros);

                        //Si el comando no es de activación de acentos, eliminamos el modificador de acentos ctrl y alt
                        if (!Modificador(secuencia[i]))
                            ACENTO = ACENTO_GRAVE = TILDE = ACENTO_CIRCUNFLEJO = DIERESIS = false;
                    }
                    else if (secuencia[i][0] == '·')
                    { //Comando de secuencia
                        CambioModo(secuencia[i]);
                        DIR_RATON = MovimientoRaton.Parar;
                    }
                    else
                    {
                        EnviarTecla(secuencia[i]);
                        DIR_RATON = MovimientoRaton.Parar;
                    }
                }
                //Si hemos ejecutado [UN_COMANDO] retornamos al modo guardado
                if (m_ModoRetornoUnComando != "")
                {
                    if (m_UnComando)
                    {
                        CambioModo(m_ModoRetornoUnComando);
                        DIR_RATON = MovimientoRaton.Parar;
                        m_ModoRetornoUnComando = "";
                        m_UnComando = false;
                    }
                    else
                        m_UnComando = true;
                }
            }
            catch (Exception e)
            {
                string resultado = "";
                if (ElementoGram.resultado != null) resultado = ElementoGram.resultado;
                LOG("Error en InterpretarSecuencia, resultado:" + resultado + ", Error:" + e.Message);
            }
            return true;
        }
        public bool CambioModo(string Modo)
        {
            try
            {
                if (Modo.Contains("XULIA·")) OkXulia = true;

                if ((MostrarGloboModos == "S") && (MostrarGloboComandos == "N"))
                    frmAIML.MostrarGlobo("", "Modo: " + Modo, "");

                bool DictadoIdiomas = false;
                //Control de cambio de estado
                if (Modo == "·REJILLA·")
                {
                    Me.MODORERIJA = ModoRejilla.Columna;
                    Me.DatosRejilla.AjusteFino = 0;
                    Me.Rejilla.Show();
                }
                else if ((Modo == "·CONVERSACION") && !frmAIML.ActivarSAPI4 && !(ModoComandoGoogle == "S"))
                {
                    if (!frmAIML.ActivarSAPI4)
                        if (!GramaticaDictadoCargada)
                        {
                            ActivarGramaticaModoDictado();
                        }
                    frmAIML.CambiarIcono(aimlForm.TipoIcono.dictado);
                }
                else if ((Strings.InStr(Modo, "·DICTUWP·") > 0) || (Strings.InStr(Modo, "·OUXULIA·") > 0))
                { //Dictado contínuo Windows UWP
                    string Idioma = Modo.Substring(9);
                    frmAIML.CambiarIcono(aimlForm.TipoIcono.dictado);
                    DictadoContinuo dc = CargarCadenasSustitucion(Idioma);
                    DictadoUWP = new DictadoWindowsMedia();
                    DictadoUWP.Show();
                    SetForegroundWindow(DictadoUWP.Handle);
                    DictadoUWP.pc = this;
                    DictadoUWP.ActivarReconocimiento();
                }
                else if ((Strings.InStr(Modo, "·DICTWIN·") > 0) || (Strings.InStr(Modo, "·OWXULIA·") > 0))
                { //Dictado contínuo Windows con SAPI 5
                    if ((!frmAIML.ActivarSAPI4) && !(ModoComandoGoogle == "S"))
                        if (!GramaticaDictadoCargada)
                        {
                            ActivarGramaticaModoDictado();
                        }
                    string Idioma = Modo.Substring(9);
                    frmAIML.CambiarIcono(aimlForm.TipoIcono.dictado);
                    DictadoContinuo dc = CargarCadenasSustitucion(Idioma);
                }
                else if ((Strings.InStr(Modo, "·DICTW10") > 0))
                { //Dictado contínuo con cuadro de reconocimiento de voz de windows 10
                    frmAIML.CambiarIcono(aimlForm.TipoIcono.dictado);
                    ActivarDesactivarCuadroDictadoWindows10();
                }
                else if ((Strings.InStr(Modo, "·DICTADO·") > 0) || (Strings.InStr(Modo, "·CONVERS·") > 0) || (Strings.InStr(Modo, "·OKXULIA·") > 0))
                { //Dictado contínuo multi-idioma Google Chrome
                    frmAIML.CambiarIcono(aimlForm.TipoIcono.dictado);
                    DictadoIdiomas = true;
                    string Idioma = Modo.Substring(9);

                    bool ChromeEjecutandose = false;
                    EnviarComando("PARAR");

                    ChromeEjecutandose = BuscaTituloVentanaChrome("RECVOZ.GOOGLE." + Idioma + ".ACTIVO") || BuscaTituloVentanaChrome("RECVOZ.GOOGLE." + Idioma + ".DESACTIVO");
                    try
                    {
                        System.IO.File.WriteAllText(ArchivoRecVoz, "");
                    }
                    catch (Exception ex) { }

                    DictadoContinuo dc = CargarCadenasSustitucion(Idioma);

                    if (!ChromeEjecutandose)
                        dc.proceso = EjecutarChromeRecVoz(Idioma, dc.codigo, ModoComandoGoogle, 1000);


                    //Activamos el nuevo idioma parando todos los demás que pudieran estar activos
                    EnviarComando("REANUDAR." + Idioma);
                    if (!EsperaTituloVentanaChrome("RECVOZ.GOOGLE." + Idioma + ".ACTIVO", 4000))
                        //No se ha ejecutado chrome correctamente, cancelamos el modo
                        MODO = MODO_CANCELAR;
                }

                Me.MODO_ANT = Me.MODO;
                Me.MODO = Modo;
                //La gramatica de dictado windows se carga arriba
                DesactivarGramaticas(false);
                CargarGramaticasModo(Me.MODO, false);
                if (DescargarGramaticasCambioMODO == "S")
                {
                    //Si no es dictado idiomas y no permite descargar el resto de las gramáticas, siempre se descargan
                    if (!((DictadoIdiomas) && (DescargarRestoGramaticasDictado == "N")))
                        DescargarGramaticasDesactivas(false);

                }
                //El modo actual no soporta gramáticas de aplicacicón
                //if (!AdmiteGramaticasAplicacion(MODO))
                //    DesactivarGramaticas(true);
            }
            catch (Exception e)
            {
                LOG("Error en CambioModo:" + Modo + ", " + e.Message);
            }
            return true;
        }
        void VolverModo(string actual, string nuevo)
        {
            try
            {
                if (actual == nuevo) return;
                if (actual.Contains("XULIA·")) OkXulia = false;

                if ((MostrarGloboModos == "S") && (MostrarGloboComandos == "N"))
                    frmAIML.MostrarGlobo("", "Modo: " + nuevo, "");
                DesactivarGramaticas(false);
                Me.CargarGramaticasModo(nuevo, false);

                if ((Strings.InStr(actual, "·DICTUWP·") > 0) || (Strings.InStr(actual, "·OUXULIA·") > 0))
                    CerrarVentanaDictadoUWP();
                else if ((Strings.InStr(actual, "·DICTWIN·") > 0) || (actual == "·CONVERSACION") || (Strings.InStr(actual, "·OWXULIA·") > 0)) //La descarga de la gramática de dictado tiene cfg propia
                {
                    InteractuarConXulia = true;
                    if ((GramaticaDictadoCargada) && (DescargarGramaticasDictado == "S"))
                    {
#if (SAPI5 || SAPI11_5) //En SAPI11 no funciona el dictado contínuo
                        DescargarGramaticaDictado(GramaticaDictado);
                        GramaticaDictadoCargada = false;
#if SAPI11_5
                        if (DictadoSAPI5)
                            DesactivarReconocedorDictado();
#endif
#endif
                    }
                    frmAIML.CambiarIcono(aimlForm.TipoIcono.activa);
                }
                else if ((Strings.InStr(actual, "·DICTADO·") > 0) || (Strings.InStr(actual, "·OKXULIA·") > 0) || (Strings.InStr(actual, "·CONVERS·") > 0))
                { //Dictado de idiomas google
                    if (ModoComandoGoogle != "S")
                    {
                        EnviarComando("PARAR");
                        frmAIML.CambiarIcono(aimlForm.TipoIcono.activa);
                    }
                }
                else if (Strings.InStr(actual, "·DICTW10") > 0)
                {
                    ActivarDesactivarCuadroDictadoWindows10();
                    BorrarComandoVolverDictadoWin10();
                }

                    //if (!AdmiteGramaticasAplicacion (this.MODO))
                    //{//El modo actual no soporta gramáticas de aplicacicón
                    //    DesactivarGramaticas(true);
                    //    if (DescargarGramaticasCambioMODO == "S")
                    //        DescargarGramaticasDesactivas(true);
                    //}

                    if (DescargarGramaticasCambioMODO == "S")
                    DescargarGramaticasDesactivas(false);
            }
            catch (Exception e)
            {
                LOG("Error en VolverModo, modo actual: " + actual + ", modo nuevo:" + nuevo + ", Error:" + e.Message);
            }
        }
        string[] ExtraerParametros(string comando, char separador)
        {
            int pos = 0;
            pos = Strings.InStr(comando, "]");
            comando = comando.Substring(pos);

            return comando.Split(separador);
        }
        public string EjecutarComando(string comando, string[] parametros)
        {
            try
            {
                bool ejecutar = true;
                //Comprobamos si es necesario tener activas teclas modificadoras
                int pos = comando.Length - 1;
                bool Modificador = true;
                while ((comando[pos] != ']') && (pos > 0) && Modificador)
                {
                    Modificador = false;
                    switch (comando[pos])
                    {
                        case '+':
                            {
                                if (!Me.SELECCION) ejecutar = false;
                                Modificador = true;
                                break;
                            }
                        case '^':
                            {
                                if (!Me.CONTROL) ejecutar = false;
                                Modificador = true;
                                break;
                            }
                        case '%':
                            {
                                if (!Me.ALT) ejecutar = false;
                                Modificador = true;
                                break;
                            }
                        case '$':
                            {
                                if (!Me.ATENCION) ejecutar = false;
                                Modificador = true;
                                break;
                            }
                    }
                    if (Modificador) pos--;
                }
                if (!ejecutar) return "NO_EJECUTAR";

                //Eliminamos los modificadores de ejecución
                comando = comando.Substring(0, pos + 1);

                #region comandos
                if (Strings.InStr(comando, "[EJECUTAR]") > 0)
                {
                    string arg;
                    Process p = new Process();
                    string[] app = comando.Substring(10).Split('·');
                    //Recuperamos la gramática
                    if (app.Length > 1)
                        if (app[1].Length > 1)
                            Me.CargarGramaticasModo(app[1], true);
                    //Recuperamos la lista de argumentos
                    arg = "";
                    if (app.Length > 2)
                    {
                        if (app[2].Length > 1)
                            arg = ProcesarCadena(app[2]);
                    }
                    p.StartInfo.FileName = app[0];
                    p.StartInfo.Arguments = arg;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    p.Start();
                }
                else if (Strings.InStr(comando, "[EJECUTAR_CMD]") > 0)
                {
                    string arg;
                    string[] app = comando.Substring(14).Split('·');
                    //Recuperamos la gramática
                    if (app.Length > 1)
                        if (app[1].Length > 1)
                            Me.CargarGramaticasModo(app[1], true);
                    //Recuperamos la lista de argumentos
                    arg = "";
                    if (app.Length > 2)
                    {
                        if (app[2].Length > 1)
                            arg = ProcesarCadena(app[2]);
                    }
                    EjecutarAplicacion(app[0], arg);
                }
                else if (comando == "[ACTUALIZAR_XULIA]")
                {
                    Process p = new Process();
                    p.StartInfo.FileName = "XULIARUN.EXE";
                    p.StartInfo.Arguments = "UPDATE " + Application.ProductVersion;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    p.Start();
                    Application.Exit();
                }
                else if (Strings.InStr(comando, "[ATENCION]") > 0)
                {
                    Process p = new Process();
                    int ESPERA_ATENCION = Convert.ToInt16(comando.Substring(10));
                    frmAIML.ActivarControlAtencion(ESPERA_ATENCION);
                }
                else if (Strings.InStr(comando, "[ACTIVAR_APLICACION]") > 0)
                {
                    string app = comando.Substring(20);
                    try
                    {
                        Interaction.AppActivate(app);
                    }
                    catch { }
                }
                else if (comando == "[MOSTRAR_XULIA_CONTROL]")
                    Estado.Visible = true;
                else if (comando == "[OCULTAR_XULIA_CONTROL]")
                    Estado.Visible = false;
                else if (comando == "[COMILLAS]")
                    EnviarTecla("\"");
                else if (comando == "[BARRA_VERTICAL]")
                    EnviarTecla("|");
                else if (Strings.InStr(comando, "[ESPERA]") == 1)
                {
                    int Espera = Convert.ToInt16(comando.Substring(8));
                    Thread.Sleep(Espera);
                }
                else if (comando == "[CONTROL]")
                    Me.CONTROL = !Me.CONTROL;
                else if (comando == "[ALT]")
                    Me.ALT = !Me.ALT;
                else if (comando == "[MAYUSCULA]")
                    if (Me.MAYUSCULAS)
                    {
                        Me.UNAMAYUSCULA = false;
                        Me.MAYUSCULAS = false;
                    }
                    else if (Me.UNAMAYUSCULA)
                    {
                        Me.UNAMAYUSCULA = false;
                        Me.MAYUSCULAS = true;
                    }
                    else
                        Me.UNAMAYUSCULA = true;
                else if (comando == "[MAYUSCULAS]")
                    Me.MAYUSCULAS = true;
                else if (comando == "[UNAMAYUSCULA]")
                    if (Me.MAYUSCULAS || Me.UNAMAYUSCULA)
                    {
                        Me.UNAMAYUSCULA = false;
                        Me.MAYUSCULAS = false;
                    }
                    else
                        Me.UNAMAYUSCULA = true;
                else if (comando == "[FIN_MAYUSCULAS]")
                {
                    Me.UNAMAYUSCULA = false;
                    Me.MAYUSCULAS = false;
                }
                else if (comando == "[MENOR]")
                    EnviarTecla("<");
                else if (comando == "[AMP]")
                    EnviarTecla("&");
                else if (comando == "[LLAVE_IZQ]")
                    EnviarTecla("{{}");
                else if (comando == "[LLAVE_DER]")
                    EnviarTecla("{}}");
                else if (comando == "[CORCHETE_IZQ]")
                    EnviarTecla("[");
                else if (comando == "[CORCHETE_DER]")
                    EnviarTecla("]");
                else if (comando == "[ACENTO]")
                    Me.ACENTO = !Me.ACENTO;
                else if (comando == "[ACENTO_GRAVE]")
                    Me.ACENTO_GRAVE = !Me.ACENTO_GRAVE;
                else if (comando == "[ACENTO_CIRCUNFLEJO]")
                    Me.ACENTO_CIRCUNFLEJO = !Me.ACENTO_CIRCUNFLEJO;
                else if (comando == "[ACENTO_TILDE]")
                    Me.TILDE = !Me.TILDE;
                else if (comando == "[DIERESIS]")
                    Me.DIERESIS = !Me.DIERESIS;
                else if (Strings.InStr(comando, "[POR]") == 1)
                    Me.Multiplicador = Convert.ToInt16(comando.Substring(5));
                else if (comando == "[CANCELAR]")
                {
                    VolverModo(Me.MODO, Me.MODO_CANCELAR);
                    Me.MODO = Me.MODO_CANCELAR;
                }
                else if (comando == "[VOLVER]")
                {
                    VolverModo(Me.MODO, Me.MODO_ANT);
                    Me.MODO = Me.MODO_ANT;
                }
                else if (comando == "[SELECCIONAR_APLICACION]")
                    SendKeys.SendWait("^%{TAB}");
                else if (comando == "[MOSTRAR_POSICION_CURSOR]")
                    txtSalida.AppendText("Pos.cursor: X->" + Cursor.Position.X + " Y->" + Cursor.Position.Y + Environment.NewLine);
                else if (comando == "[COPIAR_POSICION_CURSOR]")
                    Clipboard.SetText(Cursor.Position.X + "," + Cursor.Position.Y);
                else if (comando == "[MOSTRAR_VERSIONES]")
                {
                    frmVersiones.Show();
                    SiempreEncima((int)frmVersiones.Handle);
                    Interaction.AppActivate("Versiones");
                }
                else if (comando == "[OCULTAR_VERSIONES]")
                    frmVersiones.Hide();
                else if (comando == "[AYUDA]")
                {
                    Ayuda.MostrarAyuda(this);
                    Interaction.AppActivate("Ayuda");
                }
                else if (comando == "[CERRAR_AYUDA]")
                    Ayuda.Hide();
                else if (comando == "[MOSTRAR_BUSCAR_COMANDOS]")
                {
                    frmBuscarComandos.ActivarBuscarComandos();
                    Interaction.AppActivate("Buscar Comandos Xulia");
                }
                else if (comando == "[OCULTAR_BUSCAR_COMANDOS]")
                    frmBuscarComandos.Hide();
                else if (comando == "[ACTIVAR]")
                {
                    XULIA_ACTIVA = true;
                    DesactivarTelcasModificadoras();
                }
                else if (comando == "[DESACTIVAR]")
                {
                    XULIA_ACTIVA = false;
                    DesactivarTelcasModificadoras();
                    Estado.DesactivarTimerDesactivacion();
                }
                else if (comando == "[AUTO_DESACTIVAR]")
                {
                    Estado.ActivarTimerDesactivacion();
                }
                else if (comando == "[APAGAR_AUTO_DESACTIVAR]")
                {
                    AutoDesactivacion = true;
                    Estado.DesactivarTimerDesactivacion();
                }
                else if (comando == "[MOSTRAR_LOG]")
                {
                    frmAIML.Size = new Size(frmAIML.Size.Width, 522);
                }
                else if (comando == "[OCULTAR_LOG]")
                {
                    frmAIML.Size = new Size(frmAIML.Size.Width, 212);
                }
                else if (comando == "[ACTIVAR_AVISO]")
                    Aviso = true;
                else if (comando == "[DESACTIVAR_AVISO]")
                    Aviso = false;
                else if (Strings.InStr(comando, "[DESACTIVAR_AVISO]") > 0)
                {
                    salida = comando.Substring(18);
                }
                else if (Strings.InStr(comando, "[OCULTAR_XULIA]") > 0)
                {
                    frmAIML.Visible = false;
                }
                else if (Strings.InStr(comando, "[MOSTRAR_XULIA]") > 0)
                {
                    SiempreEncima((int)frmAIML.Handle);
                    frmAIML.Visible = true;
                    frmAIML.Refresh();
                    SetForegroundWindow((IntPtr)frmAIML.Handle);
                    //Interaction.AppActivate("XULIA");
                }
                else if (Strings.InStr(comando, "[GLOBOS_ACTIVOS]") > 0)
                    MostrarGloboComandos = "S";
                else if (Strings.InStr(comando, "[GLOBOS_DESACTIVOS]") > 0)
                    MostrarGloboComandos = "N";
                else if (Strings.InStr(comando, "[CONVERSACION_XULIA]") == 1)
                {
                    string activar = comando.Substring(20);
                    if (activar == "S")
                        InteractuarConXulia = true;
                    else
                        InteractuarConXulia = false;
                }
                else if (Strings.InStr(comando, "[SALIR_XULIA]") == 1)
                {
                    SalirDeXulia();
                }
                else if (Strings.InStr(comando, "[CERRAR_CHROME]") == 1)
                {
                    CerrarGoogleChrome();
                    Thread.Yield();
                    Thread.Sleep(1000);
                    AbrirGoogleChrome();
                }
                else if ((comando == "[CERRAR_AYUDA_RAPIDA]") || ((comando == "[AYUDA_RAPIDA]") && AYUDA_RAPIDA))
                {
                    frmAyudaRapida.Hide();
                    AYUDA_RAPIDA = false;
                }
                else if (comando == "[AYUDA_RAPIDA]")
                {
                    try
                    {
                        if (frmAyudaRapida.Cargado)
                        {
                            frmAyudaRapida.Show();
                            AYUDA_RAPIDA = true;
                        }
                    }
                    catch (Exception e)
                    {
                        frmAyudaRapida = new AyudaRapida();
                        frmAyudaRapida.Show();
                    }
                }
                #endregion comandos

                #region lectura
                else if (comando == "[LEER]")
                {
                    if (Clipboard.ContainsText())
                    {
                        string cad = Clipboard.GetText();

                        if (cad != "")
                            Hablar(cad);
                    }
                    EstadoLectura = EstadosLectura.Parar;
                    LECTURA_VELOCIDAD = LecturaEspera;
                    frmAIML.ActualizarLectura();
                }
                else if (comando == "[LECTURA_PARAR]")
                {
                    EstadoLectura = EstadosLectura.Parar;
                    LECTURA_VELOCIDAD = LecturaEspera;
                    frmAIML.ActualizarLectura();
                }
                else if (comando == "[LECTURA_AVANZAR]")
                {
                    EstadoLectura = EstadosLectura.Abajo;
                    frmAIML.ActualizarLectura();
                }
                else if (comando == "[LECTURA_RETROCEDER]")
                {
                    EstadoLectura = EstadosLectura.Arriba;
                    frmAIML.ActualizarLectura();
                }
                else if (comando == "[LECTURA_RAPIDO]")
                    LECTURA_VELOCIDAD -= LecturaIncrementoEspera;
                else if (comando == "[LECTURA_LENTO]")
                    LECTURA_VELOCIDAD += LecturaIncrementoEspera;
                else if (Strings.InStr(comando, "[LECTURA_VELOCIDAD]") == 1)
                {
                    int v = Convert.ToInt16(comando.Substring(19));

                    LECTURA_VELOCIDAD = v;
                }
                #endregion lectura

                #region precision_ventanas_cfg
                else if (Strings.InStr(comando, "[ASIGNAR_PRECISION]") == 1)
                {
                    float v = Convert.ToSingle(comando.Substring(19));
                    PrecisionMinima = 100;
                }
                else if (Strings.InStr(comando, "[GUARDAR_TITULO_VENTANA]") == 1)
                {
                    int numero = Convert.ToInt16(comando.Substring(24));

                    long hWnd = (long)GetForegroundWindow();
                    StringBuilder s = new StringBuilder(201);
                    GetWindowText((IntPtr)hWnd, s, 200);

                    if (numero < MAX_TITULOS_VENTANAS)
                        mTitulosVentanas[numero] = s.ToString();
                }
                else if (Strings.InStr(comando, "[ACTIVAR_TITULO_VENTANA]") == 1)
                {
                    int numero = Convert.ToInt16(comando.Substring(24));
                    if (numero < MAX_TITULOS_VENTANAS)
                    {
                        try
                        {
                            Interaction.AppActivate(mTitulosVentanas[numero]);
                        }
                        catch { }
                    }

                }
                else if (Strings.InStr(comando, "[GUARDAR_MANEJADOR_VENTANA]") == 1)
                {
                    int numero = Convert.ToInt16(comando.Substring(27));

                    long hWnd = (long)GetForegroundWindow();
                    if (numero < MAX_TITULOS_VENTANAS)
                        mhWndVentanas[numero] = hWnd;
                }
                else if (Strings.InStr(comando, "[ACTIVAR_MANEJADOR_VENTANA]") == 1)
                {
                    int numero = Convert.ToInt16(comando.Substring(27));
                    if (numero < MAX_TITULOS_VENTANAS)
                    {
                        try
                        {
                            SetForegroundWindow((IntPtr)mhWndVentanas[numero]);
                        }
                        catch { }
                    }

                }
                else if (Strings.InStr(comando, "[ACTIVAR_MANEJADOR_VENTANA_TITULO]") == 1)
                {
                    string titulo = comando.Substring(34);
                    ActivarVentanaManejadorTitulo(titulo);
                }
                else if (comando == "[CORRECCION]")
                {
                    Correccion frm = new Correccion();
                    Clipboard.Clear();
                    SendKeys.SendWait("^c");
                    if (CortarTextoCorreccion == "S")
                        SendKeys.SendWait("^x");
                    Thread.Sleep(500);
                    if (Clipboard.ContainsText())
                    {
                        ModoActualCorreccion = this.MODO;
                        string salida = frm.Corregir(Clipboard.GetText(), this);
                    }
                }
                else if (comando == "[MOSTRAR_XULIA_CFG]")
                {
                    frmConfiguracion.Show();
                    SetForegroundWindow(frmConfiguracion.Handle);
                    Interaction.AppActivate("Configuracion Xulia");
                }
                else if (comando == "[OCULTAR_XULIA_CFG]")
                {
                    frmConfiguracion.Hide();
                }
                else if (comando == "[REACTIVAR_DICTUWP]")
                {
                    DictadoUWP.ReactivarReconocedor();
                }
                else if (comando == "[RECARGAR_CONFIGURACION]")
                {
                    RecargarConfiguracion();
                    //Borrar listas de sustitución de dictados
                    DictadosActivos.Clear();
                }
                else if (Strings.InStr(comando, "[UN_COMANDO]") == 1)
                {
                    string modo = comando.Substring(12);
                    m_ModoRetornoUnComando = this.MODO;
                    VolverModo(this.MODO, modo);
                    CambioModo(modo);
                }
                #endregion precision_ventanas_cfg

                #region ventanas
                else if (Strings.InStr(comando, "[MOSTRAR_VENTANAS]") > 0)
                {
                    SiempreEncima((int)frmTasks.Handle);
                    frmTasks.MostrarVentanasActivas(mVentanas);
                    Interaction.AppActivate("WindowsManager");
                }
                else if (Strings.InStr(comando, "[OCULTAR_VENTANAS]") > 0)
                {
                    frmTasks.Hide();
                }
                else if (Strings.InStr(comando, "[CERRAR_APP_CHROME]") > 0)
                {
                    try
                    {
                        if (BuscaTituloVentanaChrome("RECVOZ.GOOGLE." + IdiomaComandoGoogle + ".DESACTIVO"))
                        {
                            ActivarVentanaTitulo("RECVOZ.GOOGLE." + IdiomaComandoGoogle + ".DESACTIVO");
                            MaximizarMinimizarVentanaActiva(SW_SHOWMAXIMIZED);
                            Thread.Sleep(1000);
                            SendKeys.Send("%{F4}");
                        }
                    }
                    catch
                    {
                    }
                }
                else if (Strings.InStr(comando, "[SELECCIONAR_VENTANA]") > 0)
                {
                    int numero = Convert.ToInt16(comando.Substring(21));
                    string Titulo = mVentanas.BuscarVentanaNumero(numero);
                    if (Titulo != "")
                    {
                        mVentanas.ActivarVentana(numero);
                    }
                }
                else if (comando == "[MAXIMIZAR]")
                    MaximizarMinimizarVentanaActiva(SW_SHOWMAXIMIZED);
                else if (comando == "[MINIMIZAR]")
                    MaximizarMinimizarVentanaActiva(SW_SHOWMINIMIZED);
                else if (comando == "[VENTANA_NORMAL]")
                    MaximizarMinimizarVentanaActiva(SW_SHOWNORMAL);
                else if (comando == "[VENTANA_DERECHA]")
                    MoverVentanaActiva(VentanaPosicion.derecha);
                else if (comando == "[VENTANA_IZQUIERDA]")
                    MoverVentanaActiva(VentanaPosicion.izquierda);
                else if (comando == "[MOSTRAR_CLASE_VENTANA]")
                    txtSalida.AppendText("Clase Ventana Activa->" + BuscarClaseAppActiva() + Environment.NewLine);
                else if (comando == "[COPIAR_CLASE_VENTANA]")
                    Clipboard.SetText(BuscarClaseAppActiva());
                else if (comando == "[VENTANA_ARRIBA_W8]")
                    MovimientoVentanasW8(VentanaPosicion.arriba);
                else if (comando == "[VENTANA_ABAJO_W8]")
                    MovimientoVentanasW8(VentanaPosicion.abajo);
                else if (comando == "[VENTANA_DERECHA_W8]")
                    MovimientoVentanasW8(VentanaPosicion.derecha);
                else if (comando == "[VENTANA_IZQUIERDA_W8]")
                    MovimientoVentanasW8(VentanaPosicion.izquierda);
                #endregion ventanas

                #region comandos_codigos_teclado
                else if (Strings.InStr(comando, "[VK_PRESS]") > 0)
                {
                    byte codigo = Convert.ToByte(comando.Substring(10));
                    KeyboardPress(codigo, KEYEVENTF_EXTENDEDKEY | 0);
                }
                else if (Strings.InStr(comando, "[VK_RELEASE]") > 0)
                {
                    byte codigo = Convert.ToByte(comando.Substring(12));
                    KeyboardPress(codigo, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP);
                }
                else if (comando == "[VK_RELEASE_ALL]")
                {
                    foreach (byte key in PressKey)
                        keybd_event((byte)key, 0, KEYEVENTF_SILENT | KEYEVENTF_KEYUP, 0);

                    PressKey.RemoveAll(x => x >= 0);
                }
                else if (comando == "[VK_RELEASE_CONTROLES]")
                {
                    KeyboardPress(VK_SHIFT, KEYEVENTF_SILENT | KEYEVENTF_KEYUP);
                    KeyboardPress(VK_LSHIFT, KEYEVENTF_SILENT | KEYEVENTF_KEYUP);
                    KeyboardPress(VK_RSHIFT, KEYEVENTF_SILENT | KEYEVENTF_KEYUP);
                    KeyboardPress(VK_LCONTROL, KEYEVENTF_SILENT | KEYEVENTF_KEYUP);
                    KeyboardPress(VK_RCONTROL, KEYEVENTF_SILENT | KEYEVENTF_KEYUP);
                    KeyboardPress(VK_LMENU, KEYEVENTF_SILENT | KEYEVENTF_KEYUP);
                    KeyboardPress(VK_RMENU, KEYEVENTF_SILENT | KEYEVENTF_KEYUP);
                    KeyboardPress(VK_LWIN, KEYEVENTF_SILENT | KEYEVENTF_KEYUP);
                    KeyboardPress(VK_RWIN, KEYEVENTF_SILENT | KEYEVENTF_KEYUP);
                }
                // Teclas modificadores
                else if (comando == "[VK_LWIN_PRESS]")
                    KeyboardPress(VK_LWIN, HW_LWIN, KEYEVENTF_SILENT | 0);
                else if (comando == "[VK_LWIN_RELEASE]")
                    KeyboardPress(VK_LWIN, HW_LWIN, KEYEVENTF_SILENT | KEYEVENTF_KEYUP);
                else if (comando == "[VK_LSHIFT_PRESS]")
                    KeyboardPress(VK_LSHIFT, HW_LSHIFT, KEYEVENTF_SILENT | 0);
                else if (comando == "[VK_LSHIFT_RELEASE]")
                    KeyboardPress(VK_LSHIFT, HW_LSHIFT, KEYEVENTF_SILENT | KEYEVENTF_KEYUP);
                else if (comando == "[VK_LCONTROL_PRESS]")
                    KeyboardPress(VK_LCONTROL, HW_LCONTROL, KEYEVENTF_SILENT | 0);
                else if (comando == "[VK_LCONTROL_RELEASE]")
                    KeyboardPress(VK_LCONTROL, HW_LCONTROL, KEYEVENTF_SILENT | KEYEVENTF_KEYUP);
                else if (comando == "[VK_LMENU_PRESS]")
                    KeyboardPress(VK_LMENU, HW_LMENU, KEYEVENTF_SILENT | 0);
                else if (comando == "[VK_LMENU_RELEASE]")
                    KeyboardPress(VK_LMENU, HW_LMENU, KEYEVENTF_SILENT | KEYEVENTF_KEYUP);
                // -------------------------------------------------------------------
                else if (comando == "[VK_SHIFT_PRESS]")
                    KeyboardPress(VK_SHIFT, KEYEVENTF_EXTENDEDKEY | 0);
                else if (comando == "[VK_SHIFT_RELEASE]")
                    KeyboardPress(VK_SHIFT, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP);
                else if (comando == "[VK_PAUSE_PRESS]")
                    KeyboardPress(VK_PAUSE, KEYEVENTF_EXTENDEDKEY | 0);
                else if (comando == "[VK_PAUSE_RELEASE]")
                    KeyboardPress(VK_PAUSE, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP);
                else if (comando == "[VK_SNAPSHOT_PRESS]")
                    KeyboardPress(VK_SNAPSHOT, KEYEVENTF_EXTENDEDKEY | 0);
                else if (comando == "[VK_SNAPSHOT_RELEASE]")
                    KeyboardPress(VK_SNAPSHOT, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP);
                else if (comando == "[VK_INSERT_PRESS]")
                    KeyboardPress(VK_INSERT, KEYEVENTF_EXTENDEDKEY | 0);
                else if (comando == "[VK_INSERT_RELEASE]")
                    KeyboardPress(VK_INSERT, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP);
                else if (comando == "[VK_DELETE_PRESS]")
                    KeyboardPress(VK_DELETE, KEYEVENTF_EXTENDEDKEY | 0);
                else if (comando == "[VK_DELETE_RELEASE]")
                    KeyboardPress(VK_DELETE, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP);
                else if (comando == "[VK_NUMLOCK_PRESS]")
                    KeyboardPress(VK_NUMLOCK, KEYEVENTF_EXTENDEDKEY | 0);
                else if (comando == "[VK_NUMLOCK_RELEASE]")
                    KeyboardPress(VK_NUMLOCK, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP);
                else if (comando == "[VK_RWIN_PRESS]")
                    KeyboardPress(VK_RWIN, KEYEVENTF_EXTENDEDKEY | 0);
                else if (comando == "[VK_RWIN_RELEASE]")
                    KeyboardPress(VK_RWIN, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP);
                else if (comando == "[VK_SCROLL_PRESS]")
                    KeyboardPress(VK_SCROLL, KEYEVENTF_EXTENDEDKEY | 0);
                else if (comando == "[VK_SCROLL_RELEASE]")
                    KeyboardPress(VK_SCROLL, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP);
                else if (comando == "[VK_PRINT_PRESS]")
                    KeyboardPress(VK_PRINT, KEYEVENTF_EXTENDEDKEY | 0);
                else if (comando == "[VK_PRINT_RELEASE]")
                    KeyboardPress(VK_PRINT, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP);
                else if (comando == "[VK_RSHIFT_PRESS]")
                    KeyboardPress(VK_RSHIFT, KEYEVENTF_EXTENDEDKEY | 0);
                else if (comando == "[VK_RSHIFT_RELEASE]")
                    KeyboardPress(VK_RSHIFT, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP);
                else if (comando == "[VK_RCONTROL_PRESS]")
                    KeyboardPress(VK_RCONTROL, KEYEVENTF_EXTENDEDKEY | 0);
                else if (comando == "[VK_RCONTROL_RELEASE]")
                    KeyboardPress(VK_RCONTROL, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP);
                else if (comando == "[VK_RMENU_PRESS]")
                    KeyboardPress(VK_RMENU, KEYEVENTF_EXTENDEDKEY | 0);
                else if (comando == "[VK_RMENU_RELEASE]")
                    KeyboardPress(VK_RMENU, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP);
                else if (comando == "[VK_CAPITAL_PRESS]")
                    KeyboardPress(VK_CAPITAL, KEYEVENTF_EXTENDEDKEY | 0);
                else if (comando == "[VK_CAPITAL_RELEASE]")
                    KeyboardPress(VK_CAPITAL, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP);
                else if (comando == "[VK_SPACE_PRESS]")
                    KeyboardPress(VK_SPACE, KEYEVENTF_EXTENDEDKEY | 0);
                else if (comando == "[VK_SPACE_RELEASE]")
                    KeyboardPress(VK_SPACE, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP);
                else if (comando == "[VK_RETURN_PRESS]")
                    KeyboardPress(VK_RETURN, KEYEVENTF_EXTENDEDKEY | 0);
                else if (comando == "[VK_RETURN_RELEASE]")
                    KeyboardPress(VK_RETURN, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP);
                else if (comando == "[VK_BACK_PRESS]")
                    KeyboardPress(VK_BACK, KEYEVENTF_EXTENDEDKEY | 0);
                else if (comando == "[VK_BACK_RELEASE]")
                    KeyboardPress(VK_BACK, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP);
                else if (comando == "[VK_CANCEL_PRESS]")
                    KeyboardPress(VK_CANCEL, KEYEVENTF_EXTENDEDKEY | 0);
                else if (comando == "[VK_CANCEL_RELEASE]")
                    KeyboardPress(VK_CANCEL, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP);
                else if (comando == "[VK_TAB_PRESS]")
                    KeyboardPress(VK_TAB, KEYEVENTF_EXTENDEDKEY | 0);
                else if (comando == "[VK_TAB_RELEASE]")
                    KeyboardPress(VK_TAB, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP);
                else if (comando == "[VK_ESCAPE_PRESS]")
                    KeyboardPress(VK_ESCAPE, KEYEVENTF_EXTENDEDKEY | 0);
                else if (comando == "[VK_ESCAPE_RELEASE]")
                    KeyboardPress(VK_ESCAPE, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP);
                #endregion comandos_codigos_teclado
                #region portapapeles
                else if (comando == "[SELECCIONAR]")
                    Me.SELECCION = true;
                else if (comando == "[FIN_SELECCION]")
                    Me.SELECCION = false;
                else if (comando == "[SELECCIONAR_FRASE]")
                {

                }
                else if (comando == "[SELECCIONAR_PARRAFO]")
                {
                    int pos_ini;
                    string cad = "··";
                    //Localizamos los límites del párrafo
                    //Desactivamos selección
                    SendKeys.SendWait("{END}");

                    bool BuscarInicio = true;
                    while (true)
                    {
                        if (BuscarInicio)
                            SendKeys.SendWait("+{UP}");
                        else
                            SendKeys.SendWait("+{DOWN}");

                        Application.DoEvents();
                        SendKeys.SendWait("^c");
                        Application.DoEvents();
                        if (Clipboard.ContainsText())
                        {
                            if (cad == Clipboard.GetText())
                                break;
                            cad = Clipboard.GetText();
                            pos = LocalizarSaltoParrafo(cad);

                            if (pos >= 0)
                            {
                                //Esta línea contiene el inicio del párrafo
                                if (BuscarInicio)
                                {
                                    pos_ini = pos;
                                    BuscarInicio = false;
                                    SendKeys.SendWait("{HOME}{DOWN}");
                                    Application.DoEvents();
                                }
                                else
                                {
                                    SendKeys.SendWait("+{UP}");
                                    Application.DoEvents();
                                    break;
                                }
                            }
                        }
                        else
                            break;
                    }
                }
                else if (Strings.InStr(comando, "[CORTAR_PORTAPAPELES]") > 0)
                {
                    int i = Convert.ToByte(comando.Substring(21));
                    SendKeys.SendWait("^x");
                    Portapapeles[i] = Clipboard.GetText();
                }
                else if (Strings.InStr(comando, "[COPIAR_PORTAPAPELES]") > 0)
                {
                    int i = Convert.ToByte(comando.Substring(21));
                    SendKeys.SendWait("^c");
                    Portapapeles[i] = Clipboard.GetText();
                }
                else if (Strings.InStr(comando, "[PEGAR_PORTAPAPELES]") > 0)
                {
                    int i = Convert.ToByte(comando.Substring(20));
                    if (Portapapeles[i] != "")
                        Clipboard.SetText(Portapapeles[i]);
                    SendKeys.SendWait("^v");
                }
                #endregion portapapeles

                #region comandosExtendidos
                else if (Strings.InStr(comando, "[ABRE_VENTANA_O_EJECUTA]") == 1)
                {
                    string[] par = ExtraerParametros(comando, '·');
                    string app = par[0];
                    if (BuscaTituloVentanaChrome(app))
                    {
                        ActivateAppChrome(app);
                    }
                    else
                    {
                        EjecutarAplicacion(par[1], "");
                    }


                }
                else if (Strings.InStr(comando, "[BUSCAR_INTERNET]") == 1)
                {
                    string arg;
                    Process p = new Process();
                    //Recuperamos la lista de argumentos
                    arg = "";
                    p.StartInfo.FileName = "CHROME.EXE";
                    p.StartInfo.Arguments = "https://www.google.es/search?q=" + parametros[0].Replace(" ", "%20");
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    p.Start();
                }
                else if (Strings.InStr(comando, "[QUE_ES]") == 1)
                {
                    web.Buscar(parametros[0], this, Estado.Location.X, Estado.Location.Y, Preguntas.QueEs);
                }
                else if (Strings.InStr(comando, "[QUIEN_ES]") == 1)
                {
                    web.Buscar(parametros[0], this, Estado.Location.X, Estado.Location.Y, Preguntas.QuienEs);
                }
                else if (Strings.InStr(comando, "[TIEMPO_EN]") == 1)
                {
                    web.Buscar(parametros[0], this, Estado.Location.X, Estado.Location.Y, Preguntas.QueTiempoHace);
                }
                else if (Strings.InStr(comando, "[CORREO]") == 1)
                {
                    string asunto;
                    string cuerpo;

                    //Localizamos el contacto
                    List<string> contactos = BuscarContactosOutLook(parametros[0]);
                    if (contactos.Count == 1)
                    {
                        foreach (string s in contactos)
                            EnviarCorreoOutlook("", "", new List<string>(), s);
                    }
                    else //Hay varios contactos con el nombre
                    {
                        SeleccionarContacto frmContacto = new SeleccionarContacto();
                        frmContacto.SeleccionarContactoCorreo("", "", new List<string>(), contactos, EnviarCorreoOutlook);
                    }
                }
                else if (Strings.InStr(comando, "[CORREO_ASUNTO]") == 1)
                {
                    //Localizamos el contacto
                    List<string> contactos = BuscarContactosOutLook(parametros[0]);
                    if (contactos.Count == 1)
                    {
                        foreach (string s in contactos)
                            EnviarCorreoOutlook(parametros[1], "", new List<string>(), s);
                    }
                    else //Hay varios contactos con el nombre
                    {
                        SeleccionarContacto frmContacto = new SeleccionarContacto();
                        frmContacto.SeleccionarContactoCorreo(parametros[1], "", new List<string>(), contactos, EnviarCorreoOutlook);
                    }
                }
                else if (Strings.InStr(comando, "[CORREO_MENSAJE]") == 1)
                {
                    //Localizamos el contacto
                    List<string> contactos = BuscarContactosOutLook(parametros[0]);
                    if (contactos.Count == 1)
                    {
                        foreach (string s in contactos)
                            EnviarCorreoOutlook(parametros[1], parametros[2], new List<string>(), s);
                    }
                    else //Hay varios contactos con el nombre
                    {
                        SeleccionarContacto frmContacto = new SeleccionarContacto();
                        frmContacto.SeleccionarContactoCorreo(parametros[1], parametros[2], new List<string>(), contactos, EnviarCorreoOutlook);
                    }
                }
                else if (Strings.InStr(comando, "[MENSAJE_RAPIDO]") == 1)
                {
                    parametros = InvertirParametros(parametros);
                    return EnviarMensajeRapido(parametros[0], parametros[1]);
                }
                else if (Strings.InStr(comando, "[GRABAR_RECUERDO]") == 1)
                {
                    //Buscamos la lista
                    parametros[0] = parametros[0].ToLower();
                    sListasRecuerdos l = ListasRecuerdos.Find(x => x.CodigoLista == parametros[0]);
                    if (l.NombreLista != "")
                    {
                        sRecordatorio r = new sRecordatorio();
                        r.Numero = l.Recuerdos.Count + 1;
                        r.recordar = parametros[1];
                        l.Recuerdos.Add(r);

                        mCfgListas.SetKeyValue(l.CodigoLista + "_Recuerdos", r.Numero.ToString(), r.recordar);
                        mCfgListas.Save();

                        return parametros[1] + " añadido a la lista de " + parametros[0];
                    }
                    else
                        return "No he encontrado la lista " + parametros[0];
                }
                else if (Strings.InStr(comando, "[LEER_LISTA]") == 1)
                {
                    string recuerdos = "";
                    //Buscamos la lista
                    parametros[0] = parametros[0].ToLower();
                    sListasRecuerdos l = ListasRecuerdos.Find(x => x.CodigoLista == parametros[0]);

                    if (l.NombreLista != "")
                    {
                        if (l.Recuerdos.Count == 0)
                        {
                            return "La lista está vacía";
                        }
                        else
                        {
                            recuerdos = "La lista contiene los siguientes elementos: " + Environment.NewLine;
                            List<sRecordatorio> Recuerdos = l.Recuerdos.OrderBy(x => x.Numero).ToList();
                            foreach (sRecordatorio r in l.Recuerdos)
                            {
                                recuerdos += r.Numero + ". " + r.recordar + Environment.NewLine;
                            }
                            return recuerdos;
                        }
                    }
                    else
                        return "No he encontrado la lista " + parametros[0];
                }
                else if (Strings.InStr(comando, "[BORRAR_LISTA]") == 1)
                {
                    //Buscamos la lista
                    parametros[0] = parametros[0].ToLower();

                    if (parametros[0] == "compra")
                    {
                        return "La lista de la compra no se puede borrar, solo se puede vaciar";
                    }
                    else
                    {
                        sListasRecuerdos l = ListasRecuerdos.Find(x => x.CodigoLista == parametros[0]);

                        if (l.NombreLista != "")
                        {
                            ListasRecuerdos.Remove(l);

                            return "lista " + parametros[0] + " borrada";
                        }
                        else
                            return "No he encontrado la lista " + parametros[0];
                    }
                }
                else if (Strings.InStr(comando, "[VACIAR_LISTA]") == 1)
                {
                    //Buscamos la lista
                    parametros[0] = parametros[0].ToLower();

                    sListasRecuerdos l = ListasRecuerdos.Find(x => x.CodigoLista == parametros[0]);

                    if (l.NombreLista != "")
                    {
                        foreach (sRecordatorio r in l.Recuerdos)
                            mCfgListas.cfgDeleteKey(l.CodigoLista + "_Recuerdos", r.Numero.ToString());

                        l.Recuerdos = new List<sRecordatorio>();
                        mCfgListas.RemoveSection(l.CodigoLista + "_Recuerdos");
                        mCfgListas.Save();

                        return "lista " + parametros[0] + " vaciada";
                    }
                    else
                        return "No he encontrado la lista " + parametros[0];
                }
                #endregion comandosExtendidos

                #region raton
                else
                {
                    if (Strings.InStr(comando, "[RATON_POS_RELATIVA]") > 0)
                    {
                        string[] pos1 = comando.Substring(20).Split(',');
                        int PosX = Convert.ToInt16(pos1[0]);
                        int PosY = Convert.ToInt16(pos1[1]);
                        WINDOWPLACEMENT wp = new WINDOWPLACEMENT();
                        int X = 0, Y = 0;

                        IntPtr handle = (IntPtr)GetForegroundWindow();
                        GetWindowPlacement(handle, out wp);

                        switch (wp.showCmd)
                        {
                            case SW_SHOWMINIMIZED:
                                {
                                    X = wp.minPosition.X;
                                    Y = wp.minPosition.Y;
                                    break;
                                }
                            case SW_SHOWMAXIMIZED:
                                {
                                    X = wp.maxPosition.X;
                                    Y = wp.maxPosition.Y;
                                    break;
                                }
                            case SW_SHOWNORMAL:
                                {
                                    X = wp.normalPosition.Left;
                                    Y = wp.normalPosition.Top;
                                    break;
                                }

                        }
                        Me.ControlRaton.MouseMove(new Point(PosX + X, PosY + Y));
                        MoverVentanaRaton(new Point(PosX + X, PosY + Y));
                    }
                    else if (Strings.InStr(comando, "[RATON_POS]") > 0)
                    {
                        string[] pos1 = comando.Substring(11).Split(',');
                        int PosX = Convert.ToInt16(pos1[0]);
                        int PosY = Convert.ToInt16(pos1[1]);
                        Me.ControlRaton.MouseMove(new Point(PosX, PosY));
                        MoverVentanaRaton(new Point(PosX, PosY));
                    }
                    else if (comando == "[RATON_CENTRADO]")
                    {
                        int alto = Screen.PrimaryScreen.Bounds.Height;
                        int ancho = Screen.PrimaryScreen.Bounds.Width;
                        Me.ControlRaton.MouseMove(new Point(ancho / 2, alto / 2));
                        MoverVentanaRaton(new Point(ancho / 2, alto / 2));
                    }
                    else if (Strings.InStr(comando, "[RATON_CUADRANTE]") > 0)
                    {
                        int alto = Screen.PrimaryScreen.Bounds.Height;
                        int ancho = Screen.PrimaryScreen.Bounds.Width;
                        int CuadrosX = Convert.ToInt16(comando.Substring(17, 1));
                        int CuadrosY = Convert.ToInt16(comando.Substring(19, 1));
                        int Pos = Convert.ToInt16(comando.Substring(21, 1));
                        int X = ancho / CuadrosX * ((Pos - 1) % CuadrosX + 1) - ancho / CuadrosX / 2;
                        int Y = alto / CuadrosY * ((Pos - 1) / CuadrosX + 1) - alto / CuadrosY / 2;
                        Me.ControlRaton.MouseMove(new Point(X, Y));
                        MoverVentanaRaton(new Point(X, Y));
                    }
                    else if (comando == "[RATON_MOSTRAR]")
                    {
                        if (frmCursor == null)
                        {
                            frmCursor = new CursorRaton();
                        }
                        frmCursor.Show();
                        frmCursor.Left = Cursor.Position.X;
                        frmCursor.Top = Cursor.Position.Y;
                    }
                    else if (comando == "[RATON_OCULTAR]")
                    {
                        if (frmCursor != null)
                            frmCursor.Dispose();
                        frmCursor = null;
                    }
                    else if (comando == "[RATON_CLIC]")
                        Me.ControlRaton.sendMouseClick(Cursor.Position);
                    else if (comando == "[RATON_DBL_CLIC]")
                        Me.ControlRaton.sendMouseDoubleClick(Cursor.Position);
                    else if (comando == "[RATON_CLIC_DER]")
                        Me.ControlRaton.sendMouseRightclick(Cursor.Position);
                    else if (comando == "[RATON_CLIC_CENTRO]")
                        Me.ControlRaton.sendMouseMiddleClick(Cursor.Position);
                    else if (comando == "[RATON_PULSAR_IZQ]")
                        Me.ControlRaton.sendMouseDown(Cursor.Position);
                    else if (comando == "[RATON_LEVANTAR_IZQ]")
                        Me.ControlRaton.sendMouseUp(Cursor.Position);
                    else if (comando == "[RATON_PULSAR_CENTRO]")
                        Me.ControlRaton.sendMouseCenterDown(Cursor.Position);
                    else if (comando == "[RATON_LEVANTAR_CENTRO]")
                        Me.ControlRaton.sendMouseCenterUp(Cursor.Position);
                    else if (comando == "[RATON_DERECHA]")
                    {
                        DIR_RATON = MovimientoRaton.Derecha;
                        for (int i = 1; i <= Multiplicador; i++)
                            MoverRaton();
                        Multiplicador = 1;
                    }
                    else if (comando == "[RATON_DIAGONAL_1]")
                    {
                        DIR_RATON = MovimientoRaton.Diagonal1;
                        for (int i = 1; i <= Multiplicador; i++)
                            MoverRaton();
                        Multiplicador = 1;
                    }
                    else if (comando == "[RATON_DIAGONAL_2]")
                    {
                        DIR_RATON = MovimientoRaton.Diagonal2;
                        for (int i = 1; i <= Multiplicador; i++)
                            MoverRaton();
                        Multiplicador = 1;
                    }
                    else if (comando == "[RATON_DIAGONAL_3]")
                    {
                        DIR_RATON = MovimientoRaton.Diagonal3;
                        for (int i = 1; i <= Multiplicador; i++)
                            MoverRaton();
                        Multiplicador = 1;
                    }
                    else if (comando == "[RATON_DIAGONAL_4]")
                    {
                        DIR_RATON = MovimientoRaton.Diagonal4;
                        for (int i = 1; i <= Multiplicador; i++)
                            MoverRaton();
                        Multiplicador = 1;
                    }
                    else if (comando == "[RATON_IZQUIERDA]")
                    {
                        DIR_RATON = MovimientoRaton.Izquierda;
                        for (int i = 1; i <= Multiplicador; i++)
                            MoverRaton();
                        Multiplicador = 1;
                    }
                    else if (comando == "[RATON_ARRIBA]")
                    {
                        DIR_RATON = MovimientoRaton.Arriba;
                        for (int i = 1; i <= Multiplicador; i++)
                            MoverRaton();
                        Multiplicador = 1;
                    }
                    else if (comando == "[RATON_ABAJO]")
                    {
                        DIR_RATON = MovimientoRaton.Abajo;
                        for (int i = 1; i <= Multiplicador; i++)
                            MoverRaton();
                        Multiplicador = 1;
                    }
                    else if (comando == "[PARAR_RATON]")
                        DIR_RATON = MovimientoRaton.Parar;
                    else if (comando == "[RATON_CONTINUO]")
                    {
                        DIR_RATON = MovimientoRaton.Parar;
                        RATON_CONTINUO = true;
                        RATON_VELOCIDAD = 1;
                        frmAIML.TimerRatonActivo(true);
                    }
                    else if (comando == "[RATON_DISCRETO]")
                    {
                        DIR_RATON = MovimientoRaton.Parar;
                        RATON_CONTINUO = false;
                        frmAIML.TimerRatonActivo(false);
                    }
                    else if (comando == "[RATON_RAPIDO]")
                    {
                        RATON_VELOCIDAD *= RatonSaltoVelocidad;
                        if (RATON_VELOCIDAD > LIMITE_VELOCIDAD_RATON)
                            RATON_VELOCIDAD = LIMITE_VELOCIDAD_RATON;
                    }
                    else if (comando == "[RATON_DESPACIO]")
                    {
                        RATON_VELOCIDAD /= RatonSaltoVelocidad;
                        if (RATON_VELOCIDAD < 1)
                            RATON_VELOCIDAD = 1;
                    }
                    else if (Strings.InStr(comando, "[RATON_VELOCIDAD]") > 0)
                    {
                        int velocidad = Convert.ToInt16(comando.Substring(17));
                        RATON_VELOCIDAD = velocidad;
                    }
                    else
                    {
                        //No hemos ejecutado ningún comando
                    }
                    return "";
                }
                #endregion raton //return true
                //return true
            }
            catch (Exception e)
            {
                LOG("Error en EjecutarComando: " + comando + ", Error:" + e.Message);
            }
            //Cualquier comando que no sea de ratón para el ratón
            DIR_RATON = MovimientoRaton.Parar;
            return "";
        }

        #endregion EjecutarComandos
        //*****************************************************************************************************************************************************************************************
        #region funciones_correo_outlook
        public static Boolean EnviarCorreoOutlook(string mailSubject, string mailContent, List<string> Anexos, string mailDirection)
        {
            try
            {
                var oApp = new Microsoft.Office.Interop.Outlook.Application();

                Microsoft.Office.Interop.Outlook.NameSpace ns = oApp.GetNamespace("MAPI");
                var f = ns.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);

                System.Threading.Thread.Sleep(1000);

                var mailItem = (Microsoft.Office.Interop.Outlook.MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
                mailItem.Subject = mailSubject;
                mailItem.HTMLBody = mailContent;
                mailItem.To = mailDirection;
                foreach (string s in Anexos)
                    mailItem.Attachments.Add(s);
                //mailItem.Send();
                mailItem.Display(false);

            }
            catch (System.Exception ñex)
            {
                return false;
            }
            finally
            {
            }
            return true;
        }

        public List<string> BuscarContactosOutLook(string findLastName)
        {
            List<string> contactos = new List<string>();
            Microsoft.Office.Interop.Outlook.Application outlookApp = new Microsoft.Office.Interop.Outlook.Application();
            Microsoft.Office.Interop.Outlook.NameSpace outlookNS = null;

            outlookNS = outlookApp.GetNamespace("MAPI");

            Microsoft.Office.Interop.Outlook.MAPIFolder folderContacts = outlookNS.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderContacts);
            Microsoft.Office.Interop.Outlook.Items searchFolder = folderContacts.Items;
            foreach (Object item in searchFolder)
            {
                try
                {
                    Microsoft.Office.Interop.Outlook.ContactItem foundContact = (Microsoft.Office.Interop.Outlook.ContactItem)item;
                    string Nombre = foundContact.FirstName + " " + foundContact.LastName;
                    if (Nombre.ToUpper().IndexOf(findLastName.ToUpper()) >= 0)
                        contactos.Add(Nombre + "<" + foundContact.Email1Address + ">");
                }
                catch (Exception ex)
                {
                }
            }
            return contactos;
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
            mailSent = true;
        }
        static bool mailSent = false;
        #endregion funciones_correo_outlook
        //*****************************************************************************************************************************************************************************************
        #region funcionesauxiliares
        public void ActivarDesactivarCuadroDictadoWindows10()
        {
            KeyboardPress(VK_LWIN, HW_LWIN, KEYEVENTF_SILENT | 0);
            SendKeys.Send("h");
            KeyboardPress(VK_LWIN, HW_LWIN, KEYEVENTF_SILENT | KEYEVENTF_KEYUP);
        }
        public void BorrarComandoVolverDictadoWin10()
        {
            for (int i = 0; i < 1; i++)
            {
                KeyboardPress(VK_LCONTROL, HW_LCONTROL, KEYEVENTF_SILENT | 0);
                KeyboardPress(VK_LSHIFT, HW_LSHIFT, KEYEVENTF_SILENT | 0);
                SendKeys.Send("{LEFT}");
                KeyboardPress(VK_LSHIFT, HW_LSHIFT, KEYEVENTF_SILENT | KEYEVENTF_KEYUP);
                KeyboardPress(VK_LCONTROL, HW_LCONTROL, KEYEVENTF_SILENT | KEYEVENTF_KEYUP);
                SendKeys.Send("^c");

                bool borrado = false;
                String s = Clipboard.GetText();
                String[] palabras = PalabrasVolverModo.Split(',');
                foreach (string p in palabras)
                {
                    if (s.ToUpper() == p.ToUpper())
                    {
                        SendKeys.Send("{DEL}");
                        borrado = true;
                    }
                }
                if (!borrado) break;
            }
            SendKeys.Send("{RIGHT}");
        }
        public void TraducirMenu(MenuStrip menuStrip1, CultureInfo CI)
        {
            // obtener el tipo de letra original de las opciones de menú
            // para restaurarlo en las opciones desplegables

            Font fntOriginal = menuStrip1.Items[0].Font;
            // crear un nuevo tipo de letra para las opciones de la barra de menú
            Font fntTipoLetra = new Font(menuStrip1.Items[0].Font.FontFamily,
                menuStrip1.Items[0].Font.Size,
                FontStyle.Bold | FontStyle.Italic);

            // recorrer las opciones del menú
            foreach (ToolStripMenuItem mnuitOpcion in menuStrip1.Items)
            {
                // cambiar el tipo de letra
                mnuitOpcion.Font = fntTipoLetra;

                string texto = Cadenas.ResourceManager.GetString("mnu_" + mnuitOpcion.Name, CI);
                if (texto != null) mnuitOpcion.Text = texto;

                // si esta opción despliega un submenú
                // llamar a un método para hacer cambios
                // en las opciones del submenú
                if (mnuitOpcion.DropDownItems.Count > 0)
                {
                    this.CambiarOpcionesMenu(mnuitOpcion.DropDownItems, fntOriginal, CI);
                }
            }
        }
        private void CambiarOpcionesMenu(ToolStripItemCollection colOpcionesMenu, Font fntTipoOriginal, CultureInfo CI)
        {
            // recorrer el submenú
            foreach (ToolStripItem itmOpcion in colOpcionesMenu)
            {
                // restaurar el tipo de letra original
                itmOpcion.Font = fntTipoOriginal;

                string texto = Cadenas.ResourceManager.GetString("mnu_" + itmOpcion.Name, CI);
                if (texto != null) itmOpcion.Text = texto;

                // si la opción de menú es de tipo caja de texto
                // pasar su contenido a mayúsculas
                if (itmOpcion.GetType() == typeof(ToolStripTextBox))
                {
                    itmOpcion.Text = itmOpcion.Text.ToUpper();
                }

                // si es una opción de menú normal…
                if (itmOpcion.GetType() == typeof(ToolStripMenuItem))
                {
                    // … y el primer caracter está en minúscula
                    // poner en la opción una marca de verificación
                    if (char.IsLower(itmOpcion.Text.ToCharArray(0, 1)[0]))
                    {
                        ((ToolStripMenuItem)itmOpcion).Checked = true;
                    }

                    // si esta opción a su vez despliega un nuevo submenú
                    // llamar recursivamente a este método para cambiar sus opciones
                    if (((ToolStripMenuItem)itmOpcion).DropDownItems.Count > 0)
                    {
                        this.CambiarOpcionesMenu(((ToolStripMenuItem)itmOpcion).DropDownItems, fntTipoOriginal, CI);
                    }
                }
            }
        }
        public void ActivateAppChrome(string processName)
        {
            IntPtr v = FindWindowEx((IntPtr)0, (IntPtr)0, "Chrome_WidgetWin_0", "WhatsApp");
            if ((int)v == 0)
                v = FindWindowEx((IntPtr)0, (IntPtr)0, "Chrome_WidgetWin_1", "WhatsApp");
            if ((int)v != 0)
                SetForegroundWindow(v);

            //Process[] p = Process.GetProcessesByName(processName);
            //// Activate the first application we find with this name
            //for (int i = 0; i < p.Count(); i++)
            //{
            //    SetForegroundWindow(p[i].MainWindowHandle);
            //}

        }
        Process EjecutarAplicacion(string app, string arg)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = " /c start " + app + " " + arg;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            p.Start();

            return p;
        }
        void RecuperarAlmacenamientos()
        {
            Hashtable h;
            h = cfg.ReadSection("Almacenamiento");
            foreach (DictionaryEntry entry in h)
            {
                string k = entry.Key.ToString();
                string v = entry.Value.ToString();
                Almacenamiento.directorio = k;
                Almacenamiento.path = v;
                Directorios.Add(Almacenamiento);
            }
        }
        string BuscarUltimoFicheroModificado(string directorio)
        {
            return "";
        }
        public void RecargarConfiguracion()
        {
            cfg.IniCfg();
            CargarVariablesConfiguracion();
            DireccionesDestino = CargarDireccionesDestino();


            if (frmAIML.ActivarSAPI4)
                frmAIML.ResetearReconocedor();
            else
                DescargarTodasLasGramaticas();

            GramaticasCargadas.Clear();
            InicializarGramaticas();
        }
        bool AdmiteGramaticasAplicacion(string modo)
        {
            if (Strings.Right(modo, 1) == "·")
                return true;
            else
                return false;
        }
        string BuscarClaseAppActiva()
        {
            long handle = (long)GetForegroundWindow();
            Process[] p = System.Diagnostics.Process.GetProcesses();

            ClaseAppActiva = "";
            TituloVentanaAppActiva = "";

            foreach (Process proceso in p)
            {
                if (handle == (long)proceso.MainWindowHandle)
                {
                    ClaseAppActiva = proceso.ProcessName;
                    TituloVentanaAppActiva = proceso.MainWindowTitle;
                    return proceso.ProcessName;
                }
            }
            return "";
        }

        public void ActualizarPantallas(bool ForzarActualizacion)
        {
            frmAIML.ActualizaPantalla(this, salida.ToUpper(), ClaseAppActiva, TituloVentanaAppActiva);
            if ((Estado.Visible) || ForzarActualizacion)
            {
                Estado.ActualizaPantalla(this, salida.ToUpper(), ClaseAppActiva, TituloVentanaAppActiva);
                SiempreEncima((int)Estado.Handle);
            }
        }

        void MaximizarMinimizarVentanaActiva(int cmd)
        {
            IntPtr handle = (IntPtr)GetForegroundWindow();
            if ((handle == Estado.Handle) || ((IntPtr)handle == (IntPtr)0))
                return;

            WINDOWPLACEMENT wp = new WINDOWPLACEMENT();

            GetWindowPlacement((IntPtr)handle, out wp);
            wp.showCmd = SW_SHOWNORMAL;
            SetWindowPlacement((IntPtr)handle, ref wp);
            wp.showCmd = (uint)cmd;
            SetWindowPlacement((IntPtr)handle, ref wp);
        }
        void MovimientoVentanasW8(VentanaPosicion pos)
        {
            long handle = (long)GetForegroundWindow();
            if (handle == (long)Estado.Handle)
                return;

            switch (pos)
            {
                case VentanaPosicion.arriba:
                    {
                        KeyboardPress(VK_LWIN, HW_LWIN, KEYEVENTF_SILENT | 0);
                        SendKeys.SendWait("{UP}");
                        KeyboardPress(VK_LWIN, HW_LWIN, KEYEVENTF_SILENT | KEYEVENTF_KEYUP);
                        break;
                    }
                case VentanaPosicion.abajo:
                    {
                        KeyboardPress(VK_LWIN, HW_LWIN, KEYEVENTF_SILENT | 0);
                        SendKeys.SendWait("{DOWN}");
                        KeyboardPress(VK_LWIN, HW_LWIN, KEYEVENTF_SILENT | KEYEVENTF_KEYUP);
                        break;
                    }
                case VentanaPosicion.derecha:
                    {
                        KeyboardPress(VK_LWIN, HW_LWIN, KEYEVENTF_SILENT | 0);
                        SendKeys.SendWait("{RIGHT}");
                        KeyboardPress(VK_LWIN, HW_LWIN, KEYEVENTF_SILENT | KEYEVENTF_KEYUP);
                        break;
                    }
                case VentanaPosicion.izquierda:
                    {
                        KeyboardPress(VK_LWIN, HW_LWIN, KEYEVENTF_SILENT | 0);
                        SendKeys.SendWait("{LEFT}");
                        KeyboardPress(VK_LWIN, HW_LWIN, KEYEVENTF_SILENT | KEYEVENTF_KEYUP);
                        break;
                    }
            }
        }

        void MoverVentanaActiva(VentanaPosicion pos)
        {
            long handle = (long)GetForegroundWindow();
            if (handle == (long)Estado.Handle)
                return;
            int height = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height;
            int width = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width;

            switch (pos)
            {
                case VentanaPosicion.derecha:
                    {
                        MoveWindow((IntPtr)handle, width / 2 + 1, 0, width / 2, height - AltoBarraEstado, true);
                        break;
                    }
                case VentanaPosicion.izquierda:
                    {
                        MoveWindow((IntPtr)handle, 0, 0, width / 2, height - AltoBarraEstado, true);
                        break;
                    }
            }
        }

        public static void SiempreEncima(int handle)
        {
            SetWindowPos(handle, HWND_TOPMOST, 0, 0, 0, 0, wFlags);
        }

        public static void NoSiempreEncima(int handle)
        {
            SetWindowPos(handle, HWND_NOTOPMOST, 0, 0, 0, 0, wFlags);
        }
        private static bool BuscarGramaticasCargadas(GramaticaCargada gc)
        {
            return true;
        }

        public bool BuscaTituloVentana(string titulo)
        {
            Process[] Processes = Process.GetProcesses();
            IntPtr hWnd = IntPtr.Zero;
            foreach (Process p in Processes)
            {
                if (Strings.InStr(p.MainWindowTitle, titulo) > 0)
                    return true;
            }
            return false;
        }

        bool BuscaTituloVentanaChrome(string titulo)
        {
            bool salida = false;
            List<string> ventanas = new List<string>();
            foreach (var title in WindowsByClassFinder.WindowTitlesForClass("Chrome_WidgetWin_0"))
                if (!string.IsNullOrWhiteSpace(title))
                    if (Strings.InStr(title, titulo) > 0)
                        salida = true;

            foreach (var title in WindowsByClassFinder.WindowTitlesForClass("Chrome_WidgetWin_1"))
                if (!string.IsNullOrWhiteSpace(title))
                    if (Strings.InStr(title, titulo) > 0)
                        salida = true;

            return salida;
        }

        static List<IntPtr> GetAllChildrenWindowHandles(IntPtr hParent, int maxCount)
        {
            List<IntPtr> result = new List<IntPtr>();
            int ct = 0;
            IntPtr prevChild = IntPtr.Zero;
            IntPtr currChild = IntPtr.Zero;
            while (true && ct < maxCount)
            {
                currChild = FindWindowEx(hParent, prevChild, null, null);
                if (currChild == IntPtr.Zero) break;
                result.Add(currChild);
                prevChild = currChild;
                ++ct;
            }
            return result;
        }

        bool EsperaTituloVentanaChrome(string titulo, int tiempo)
        {
            const int ESPERA_ITERACION = 300;
            int cont = 0;
            while (cont * ESPERA_ITERACION < tiempo)
            {
                cont++;
                Application.DoEvents();
                Thread.Yield();
                Thread.Sleep(ESPERA_ITERACION);
                if (BuscaTituloVentanaChrome(titulo))
                    return true;
            }
            return false;
        }

        bool EsperaTituloVentana(string titulo, int tiempo)
        {
            try
            {
                const int ESPERA_ITERACION = 300;
                int cont = 0;
                while (cont * 300 < tiempo)
                {
                    cont++;
                    Thread.Sleep(ESPERA_ITERACION);
                    if (BuscaTituloVentana(titulo))
                        return true;
                }
            }
            catch (Exception e)
            {
                LOG("ERROR: EsperaTituloVentana " + tiempo.ToString() + " msg: " + e.Message.ToString());
            }
            return false;
        }


        public void MostrarGramaticasActivas(TextBox salida)
        {
            salida.Text = "";
            List<GramaticaCargada> GramActivas = GramaticasCargadas.FindAll(x => ((x.Activa == true) && (x.GramaticaAplicacion == true)));

            //Buscamos en las gramáticas activas del tipo especificado
            foreach (GramaticaCargada gra in GramActivas)
            {
                salida.Text = salida.Text + gra.Nombre + Constants.vbCrLf;
            }

            salida.Text = salida.Text + "---------------------------" + Constants.vbCrLf;
            GramActivas = GramaticasCargadas.FindAll(x => ((x.Activa == true) && (x.GramaticaAplicacion == false)));

            //Buscamos en las gramáticas activas del tipo especificado
            foreach (GramaticaCargada gra in GramActivas)
            {
                salida.Text = salida.Text + gra.Nombre + Constants.vbCrLf;
            }
        }

        public int KeyboardPress(byte tecla, uint press)
        {
            KeyboardPress(tecla, 0, press);
            return 0;
        }
        public int KeyboardPress(byte tecla, byte hwkey, uint press)
        {

            //Press the key
            keybd_event((byte)tecla, hwkey, press | 0, 0);
            Thread.Sleep(200);

            if ((press == (KEYEVENTF_EXTENDEDKEY | 0)) || (press == (KEYEVENTF_SILENT | 0)))
            {
                if (!PressKey.Exists(x => x == tecla))
                    PressKey.Add(tecla);
            }
            else if ((press == (KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP)) || (press == (KEYEVENTF_SILENT | KEYEVENTF_KEYUP)))
            {
                if (PressKey.Exists(x => x == tecla))
                    PressKey.Remove(tecla);
            }

            return 0;

        }

        bool ActivarVentanaTitulo(string titulo)
        {
            try
            {
                Interaction.AppActivate(titulo);
                return true;
            }
            catch
            {
            }
            return false;
        }

        void LOG(string log)
        {
            frmAIML.LOG(log);
            frmAIML.Size = new Size(frmAIML.Size.Width, 522);
        }

        bool RecValorBoolCfg(string valor)
        {
            if (valor == "S")
                return true;
            else
                return false;
        }

        public void MoverRaton()
        {
            PosCursor = Cursor.Position;
            switch (DIR_RATON)
            {
                case ProcesamientoComandos.MovimientoRaton.Abajo:
                    PosCursor.Y += RATON_VELOCIDAD;
                    break;
                case ProcesamientoComandos.MovimientoRaton.Arriba:
                    PosCursor.Y -= RATON_VELOCIDAD;
                    break;
                case ProcesamientoComandos.MovimientoRaton.Derecha:
                    PosCursor.X += RATON_VELOCIDAD;
                    break;
                case ProcesamientoComandos.MovimientoRaton.Izquierda:
                    PosCursor.X -= RATON_VELOCIDAD;
                    break;
                case ProcesamientoComandos.MovimientoRaton.Parar:
                    return;
                case ProcesamientoComandos.MovimientoRaton.Diagonal1:
                    PosCursor.Y -= RATON_VELOCIDAD;
                    PosCursor.X -= RATON_VELOCIDAD;
                    break;
                case ProcesamientoComandos.MovimientoRaton.Diagonal2:
                    PosCursor.Y -= RATON_VELOCIDAD;
                    PosCursor.X += RATON_VELOCIDAD;
                    break;
                case ProcesamientoComandos.MovimientoRaton.Diagonal3:
                    PosCursor.Y += RATON_VELOCIDAD;
                    PosCursor.X -= RATON_VELOCIDAD;
                    break;
                case ProcesamientoComandos.MovimientoRaton.Diagonal4:
                    PosCursor.Y += RATON_VELOCIDAD;
                    PosCursor.X += RATON_VELOCIDAD;
                    break;
            }
            ControlRaton.MouseMove(PosCursor);
            MoverVentanaRaton(PosCursor);
        }
        void MoverVentanaRaton(Point p)
        {
            if (frmCursor != null)
            {
                frmCursor.Top = p.Y;
                frmCursor.Left = p.X;
            }
        }

        void CopiarTextoDictadoUWP()
        {
            Clipboard.SetText(DictadoUWP.CopiarTextoUWP());
        }
        void CerrarVentanaDictadoUWP()
        {
            string texto = DictadoUWP.CopiarTextoUWP();
            if (texto != "")
                Clipboard.SetText(texto);
            DictadoUWP.CerrarVentanaUWP();
            DictadoUWP.Dispose();
        }
        void EnviarTeclasVentanaUWP(string texto)
        {
            DictadoUWP.EnviarTexto(texto);
        }

        void EnviarEventosTeclas(string teclas)
        {
            try
            {
                SendKeys.SendWait(teclas);
            }
            catch (Exception e)
            {
                LOG("Error enviando SendKeys:" + teclas + ", Error:" + e.Message);
            }
        }

        string ProcesarCadena(string c)
        {
            c = c.Replace("[AMP]", "&");
            c = c.Replace("[MENOR]", "&");
            c = c.Replace("[COMILLAS]", "&");

            return c;
        }

        void ActivarVentanaManejadorTitulo(string titulo)
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.MainWindowTitle == titulo)
                {
                    SetForegroundWindow(p.MainWindowHandle);
                    return;
                }
            }
        }
        public void EstadoOcultarBarraPrecision()
        {
            if (OcultarBarraPrecision == "S")
                Estado.OcultarBarraPrecision();
        }
        public void EstadoMostrarBarraPrecision()
        {
            if (OcultarBarraPrecision == "S")
                Estado.MostrarBarraPrecision();
        }

        void CargarVariablesConfiguracion()
        {
            IdiomaGramaticas = cfg.ReadAppSettingsKey("IdiomaGramaticas");
            string ComandosDesactivados;

            Cultura = cfg.ReadAppSettingsKey("Cultura" + IdiomaGramaticas);
            MODO = MODO_ANT = MODO_CANCELAR = cfg.ReadAppSettingsKey("ModoDefecto" + IdiomaGramaticas);
            MarcarSeleccionRejilla = (cfg.ReadAppSettingsKey("MarcarSeleccionRejilla" + IdiomaGramaticas) == "S" ? true : false);
            DesplazamientoVentanaEstado = Convert.ToInt16(cfg.ReadAppSettingsKey("DesplazamientoVentanaEstado" + IdiomaGramaticas));
            AltoBarraEstado = Convert.ToInt16(cfg.ReadAppSettingsKey("AltoBarraEstado" + IdiomaGramaticas));
            PrecisionMinima = (float)Convert.ToDouble(cfg.ReadAppSettingsKey("Precision" + IdiomaGramaticas)) / 100;
            ArchivoRecVoz = cfg.ReadAppSettingsKey("ArchivoRecVoz" + IdiomaGramaticas);
            ArchivoComandos = cfg.ReadAppSettingsKey("ArchivoComandos" + IdiomaGramaticas);
            URLBaseIdioma = cfg.ReadAppSettingsKey("URLBaseIdioma" + IdiomaGramaticas);
            DescargarGramaticasCambioMODO = cfg.ReadAppSettingsKey("DescargarGramaticasCambioMODO" + IdiomaGramaticas);
            MantenerPulsadoControl = cfg.ReadAppSettingsKey("MantenerPulsadoControl" + IdiomaGramaticas);
            MantenerPulsadoAlternativo = cfg.ReadAppSettingsKey("MantenerPulsadoAlternativo" + IdiomaGramaticas);
            MantenerPulsadoSeleccion = cfg.ReadAppSettingsKey("MantenerPulsadoSeleccion" + IdiomaGramaticas);
            DescargarGramaticasDictado = cfg.ReadAppSettingsKey("DescargarGramaticasDictado" + IdiomaGramaticas);
            PrecisionModoDictado = (float)Convert.ToDouble(cfg.ReadAppSettingsKey("PrecisionModoDictado" + IdiomaGramaticas)) / 100;
            DescargarRestoGramaticasDictado = cfg.ReadAppSettingsKey("DescargarRestoGramaticasDictado" + IdiomaGramaticas);
            ComandosDesactivados = cfg.ReadAppSettingsKey("ComandosDesactivados" + IdiomaGramaticas);
            XuliaActivar = cfg.ReadAppSettingsKey("XuliaActivar" + IdiomaGramaticas);
            XuliaDesactivar = cfg.ReadAppSettingsKey("XuliaDesactivar" + IdiomaGramaticas);
            XuliaAtencion = cfg.ReadAppSettingsKey("XuliaAtencion" + IdiomaGramaticas);
            OcultarEstadoSegundos = Convert.ToInt16(cfg.ReadAppSettingsKey("OcultarEstadoSegundos" + IdiomaGramaticas));
            ArranqueTomcat = cfg.ReadAppSettingsKey("ArranqueTomcat" + IdiomaGramaticas);
            ArranqueTomcat_unidad = cfg.ReadAppSettingsKey("ArranqueTomcat_Unidad" + IdiomaGramaticas);
            ArranqueTomcat_parametros = cfg.ReadAppSettingsKey("ArranqueTomcat_Parametro" + IdiomaGramaticas);
            TiempoArranqueTomcat = Convert.ToInt16(cfg.ReadAppSettingsKey("TiempoArranqueTomcat" + IdiomaGramaticas));
            TiempoEsperaArranqueChrome = Convert.ToInt16(cfg.ReadAppSettingsKey("TiempoEsperaArranqueChrome" + IdiomaGramaticas));
            MinimizarChrome = RecValorBoolCfg(cfg.ReadAppSettingsKey("MinimizarChrome" + IdiomaGramaticas));
            RatonContinuoInicial = cfg.ReadAppSettingsKey("RatonContinuoInicial" + IdiomaGramaticas);
            RutaCerebroXulia = cfg.ReadAppSettingsKey("RutaCerebroXulia" + IdiomaGramaticas);
            ArranqueXuliaVisible = cfg.ReadAppSettingsKey("ArranqueXuliaVisible" + IdiomaGramaticas);
            RatonSaltoVelocidad = Convert.ToInt16(cfg.ReadAppSettingsKey("RatonSaltoVelocidad" + IdiomaGramaticas));
            LECTURA_VELOCIDAD = LecturaEspera = Convert.ToInt16(cfg.ReadAppSettingsKey("LecturaEspera" + IdiomaGramaticas));
            LecturaIncrementoEspera = Convert.ToInt16(cfg.ReadAppSettingsKey("LecturaIncremento" + IdiomaGramaticas));
            OcultarBarraPrecision = cfg.ReadAppSettingsKey("OcultarBarraPrecision" + IdiomaGramaticas);
            voz = cfg.ReadAppSettingsKey("Voz" + IdiomaGramaticas);
            ModoCorreccion = cfg.ReadAppSettingsKey("ModoCorreccion" + IdiomaGramaticas);
            DictadoGoogleSiempreMinusculas = cfg.ReadAppSettingsKey("DictadoGoogleSiempreMinusculas" + IdiomaGramaticas);
            String ActivarSAPI4 = cfg.ReadAppSettingsKey("ActivarSAPI4" + IdiomaGramaticas);
            CortarTextoCorreccion = cfg.ReadAppSettingsKey("CortarTextoCorreccion" + IdiomaGramaticas);
            ActivarBDCoreccion = cfg.ReadAppSettingsKey("ActivarBDCoreccion" + IdiomaGramaticas);
            ProveedorBD = cfg.ReadAppSettingsKey("ProveedorBD" + IdiomaGramaticas);
            BDCoreccion = cfg.ReadAppSettingsKey("BDCoreccion" + IdiomaGramaticas);
            GrabarCoreccion = cfg.ReadAppSettingsKey("GrabarCoreccion" + IdiomaGramaticas);
            MostrarGloboComandos = cfg.ReadAppSettingsKey("MostrarGloboComandos" + IdiomaGramaticas);
            MostrarGloboModos = cfg.ReadAppSettingsKey("MostrarGloboModos" + IdiomaGramaticas);
            ModoGloboBasico = cfg.ReadAppSettingsKey("ModoGloboBasico" + IdiomaGramaticas);
            ModoComandoGoogle = cfg.ReadAppSettingsKey("ModoComandoGoogle" + IdiomaGramaticas);
            EsperaArranqueTomcatModoComandoGoogle = Convert.ToInt16(cfg.ReadAppSettingsKey("EsperaArranqueTomcatModoComandoGoogle" + IdiomaGramaticas));
            IdiomaComandoGoogle = cfg.ReadAppSettingsKey("IdiomaComandoGoogle" + IdiomaGramaticas);
            CodigoIdiomaComandoGoogle = cfg.ReadAppSettingsKey("CodigoIdiomaComandoGoogle" + IdiomaGramaticas);
            ArranqueDesactivado = cfg.ReadAppSettingsKey("ArranqueDesactivado" + IdiomaGramaticas);
            ActivarAvatar = (cfg.ReadAppSettingsKey("ActivarAvatar" + IdiomaGramaticas) == "S" ? true : false);
            MaximizarAvatar = (cfg.ReadAppSettingsKey("MaximizarAvatar" + IdiomaGramaticas) == "S" ? true : false);
            MinutosResetControlVoz = Convert.ToInt16(cfg.ReadAppSettingsKey("MinutosResetControlVoz" + IdiomaGramaticas));
            DictadoSAPI5 = (cfg.ReadAppSettingsKey("DictadoSAPI5" + IdiomaGramaticas) == "S" ? true : false);
            PalabrasVolverModo= cfg.ReadAppSettingsKey("VolverModo" + IdiomaGramaticas);
            AutoDesactivacion = (cfg.ReadAppSettingsKey("AutoDesactivar" + IdiomaGramaticas) == "S" ? true : false);
            EsperaCompilacionGramaticaUWP = Convert.ToInt16(cfg.ReadAppSettingsKey("EsperaCompilacionGramaticaUWP" + IdiomaGramaticas));
            OK_XULIA_UnComando = (cfg.ReadAppSettingsKey("OK_XULIA_UnComando" + IdiomaGramaticas) == "S" ? true : false);

            ServidorSMTP = cfg.ReadAppSettingsKey("ServidorSMTP" + IdiomaGramaticas);
            UsuarioSMTP = cfg.ReadAppSettingsKey("UsuarioSMTP" + IdiomaGramaticas);
            ClaveSMTP = cfg.ReadAppSettingsKey("ClaveSMTP" + IdiomaGramaticas);
            OrigenSMTP = cfg.ReadAppSettingsKey("OrigenSMTP" + IdiomaGramaticas);

            if (ComandosDesactivados != "")
                XuliaComandosDesactivados = ComandosDesactivados.Split(',');

            if (ActivarSAPI4 == "S")
                frmAIML.ActivarSAPI4 = true;
            else
                frmAIML.ActivarSAPI4 = false;

            DireccionIP = cfg.ReadAppSettingsKey("IP" + IdiomaGramaticas);
            Puerto = (int)Conversion.Val(cfg.ReadAppSettingsKey("Puerto" + IdiomaGramaticas));
        }
        public static void ActivarVentana(IntPtr hWnd)
        {
            SetForegroundWindow(hWnd);

            Thread.Sleep(500);
            IntPtr handle = (IntPtr)GetForegroundWindow();
            WINDOWPLACEMENT wp = new WINDOWPLACEMENT();
            GetWindowPlacement(handle, out wp);

            switch (wp.showCmd)
            {
                case SW_SHOWMINIMIZED:
                    {
                        GetWindowPlacement(handle, out wp);
                        wp.showCmd = SW_SHOWNORMAL;
                        SetWindowPlacement(handle, ref wp);
                        break;
                    }
            }
        }

        public void SalirDeXulia()
        {
            ServWeb.CerrarServidor();
            try
            {
                ProcesoChrome.CloseMainWindow();
            }
            catch (Exception e) { }
            //Si quedó mal cerrada la ventana de Chrome, primero la cierra

            if (BuscaTituloVentanaChrome("RECVOZ.GOOGLE." + IdiomaComandoGoogle + ".DESACTIVO"))
            {
                ActivarVentanaTitulo("RECVOZ.GOOGLE." + IdiomaComandoGoogle + ".DESACTIVO");
                MaximizarMinimizarVentanaActiva(SW_SHOWMAXIMIZED);
                SendKeys.Send("%{F4}");
            }
            Application.Exit();
        }

        public void CerrarGoogleChrome()
        {
            if (BuscaTituloVentanaChrome("RECVOZ.GOOGLE." + IdiomaComandoGoogle ))
            {
                ActivarVentanaTitulo("RECVOZ.GOOGLE." + IdiomaComandoGoogle );
                MaximizarMinimizarVentanaActiva(SW_SHOWMAXIMIZED);
                SendKeys.Send("%{F4}");
            }
        }

        public void AbrirGoogleChrome()
        {
            Estado.ActualizarComando("Estatus: Open Google Chrome");
            Process p = null;
            if ((p = EjecutarChromeRecVoz(IdiomaComandoGoogle, CodigoIdiomaComandoGoogle, ModoComandoGoogle, 5000)) == null)
            {
                LOG("ERROR FATAL: no se ha podido ejecutar Google Chrome para reconocimiento");
                Estado.ActualizarComando("Error Chrome");
            }
            else
            {
                Estado.ActualizarComando("Estatus: Google Chrome Ok");
                ProcesoChrome = p;
            }
        }

        #endregion funcionesauxiliares
        //*****************************************************************************************************************************************************************************************
        #region SistemaReconocimientoVoz
        void InicializarReconocedorVoz()
        {
            if ((!frmAIML.ActivarSAPI4) && (!(ModoComandoGoogle == "S")))
            {
                recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo(Cultura));
#if SAPI11_5
                if (DictadoSAPI5)
                    recognizerDictado = new System.Speech.Recognition.SpeechRecognitionEngine(new System.Globalization.CultureInfo(Cultura));
#endif
            }
        }
        void ActivarReconocedorSAPI()
        {
            if ((!frmAIML.ActivarSAPI4) && (!(ModoComandoGoogle == "S")))
            {
                try
                {
                    // Add a handler for the speech recognized event.
                    recognizer.SpeechRecognized +=
                      new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);
                    // Configure input to the speech recognizer.
                    recognizer.SetInputToDefaultAudioDevice();
                    // Start asynchronous, continuous speech recognition.
                    recognizer.RecognizeAsync(RecognizeMode.Multiple);

#if SAPI11_5
                    // Add a handler for the speech recognized event.
                    if (DictadoSAPI5)
                    {
                        recognizerDictado.SpeechRecognized +=
                                  new EventHandler<System.Speech.Recognition.SpeechRecognizedEventArgs>(recognizer_SpeechRecognized_dictado);
                        // Configure input to the speech recognizer.
                        recognizerDictado.SetInputToDefaultAudioDevice();
                    }
#endif
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error inicializando sistema de reconocimiento, compruebe si tiene un micro conectado, error:" + e.Message.ToString());
                    Application.Exit();
                }
            }
        }
        public void CancelarReconocimiento()
        { }
        public void RestaurarReconocimienot()
        { }

        public void Hablar(string texto)
        {
            DesactivarReconocimiento();
            System.Speech.Synthesis.SpeechSynthesizer voz = new System.Speech.Synthesis.SpeechSynthesizer();
            voz.SelectVoice(this.voz);
            voz.Speak(texto);
            voz.Dispose();
            ActivarReconocimiento();
        }

        public void ResetearControlVoz()
        {
            InicializarReconocedorVoz();
            InicializarGramaticas();
        }
        #endregion SistemaReconocimientoVoz
        //*****************************************************************************************************************************************************************************************

        #region MensajesRapidos
        String EnviarMensajeRapido(String destino, String mensaje)
        {

            String ServidorSMTP = "smtp.gmail.com";
            String UsuarioSMTP = "antonio.losada@gmail.com";
            String ClaveSMTP = "Loguito1060.";
            String OrigenSMTP = "antonio.losada@gmail.com";
            String DestinoSMTP = "";

            //Intentamos localizar la identificación de la dirección
            foreach (sDireccion d in DireccionesDestino)
            {
                if (d.comando.ToLower() == destino.ToLower())
                {
                    DestinoSMTP = d.direccion;
                    break;
                }
            }

            if (DestinoSMTP == "")
            {
                return "No he encontado el contacto para " + destino;
            }
            // Command line argument must the the SMTP host.
            SmtpClient client = new SmtpClient(ServidorSMTP);
            // Specify the e-mail sender.
            // Create a mailing address that includes a UTF8 character
            // in the display name.
            MailAddress from = new MailAddress(OrigenSMTP, OrigenSMTP, System.Text.Encoding.UTF8);
            // Set destinations for the e-mail message.
            MailAddress to = new MailAddress(DestinoSMTP);
            // Specify the message content.
            MailMessage message = new MailMessage(from, to);
            message.Body = "Mesaje enviado a través del BOT XULIA:" + Environment.NewLine + Environment.NewLine + mensaje;
            // Include some non-ASCII characters in body and subject.
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = "[XULIA] " + mensaje;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            // Set the method that is called back when the send operation ends.
            client.SendCompleted += new
            SendCompletedEventHandler(SendCompletedCallback);
            // The userState can be any object that allows your callback 
            // method to identify this send operation.
            // For this example, the userToken is a string constant.
            string userState = "test message1";

            client.Credentials = new System.Net.NetworkCredential(UsuarioSMTP, ClaveSMTP);
            client.Port = 587;
            client.Host = ServidorSMTP;
            client.EnableSsl = true;
            client.SendAsync(message, userState);

            return "Mensaje enviado a " + destino;

            while (!mailSent)
                Application.DoEvents();
            Console.WriteLine("Sending message... press c to cancel mail. Press any other key to exit.");
            message.Dispose();
            Console.WriteLine("Goodbye.");
            return "Mensaje enviado a " + destino;
        }

        private List<sDireccion> CargarDireccionesDestino()
        {
            ConfigXml mCfg;
            String IdiomaGramaticas = cfg.ReadAppSettingsKey("IdiomaGramaticas");

            //Recuperamos la información de configuración
            List<sDireccion> Dir = new List<sDireccion>();
            String fic = Application.StartupPath + @"\XULIA.exe.config";
            // La clase debemos instanciarla indicando el path a usar
            // y opcionalmente si se guarda cada vez que se asigne un valor.
            mCfg = new ConfigXml(fic, true);
            Hashtable h = mCfg.Claves(SECCION_DIRECTORIO);
            sDireccion Direccion = new sDireccion();

            foreach (DictionaryEntry entry in h)
            {
                Direccion.direccion = entry.Key.ToString();
                Direccion.comando = entry.Value.ToString();
                Dir.Add(Direccion);
            }

            return Dir;
        }
        private List<sListasRecuerdos> CargarListasRecuerdos()
        {
            //Recuperamos la información de configuración
            List<sListasRecuerdos> ListasRecuerdos = new List<sListasRecuerdos>();
            sListasRecuerdos ListaRecuerdos = new sListasRecuerdos();
            String fic = Application.StartupPath + @"\Listas\XULIA.listas.config";
            // La clase debemos instanciarla indicando el path a usar
            // y opcionalmente si se guarda cada vez que se asigne un valor.
            mCfgListas = new ConfigXml(fic, true);
            Hashtable h = mCfgListas.Claves("ListasRecuerdos");

            //Localzamos las listas de recuerdos
            foreach (DictionaryEntry entry in h)
            {
                ListaRecuerdos.CodigoLista = entry.Key.ToString();
                ListaRecuerdos.NombreLista = entry.Value.ToString();

                ListasRecuerdos.Add(ListaRecuerdos);
            }

            foreach (sListasRecuerdos l in ListasRecuerdos)
            {
                string Codigo = l.CodigoLista;

                h = mCfgListas.Claves(Codigo + "_Recuerdos");

                //Localzamos las listas de recuerdos
                foreach (DictionaryEntry entry in h)
                {
                    sRecordatorio Recuerdo = new sRecordatorio();
                    Recuerdo.Numero = Convert.ToInt16(entry.Key.ToString());
                    Recuerdo.recordar = entry.Value.ToString();
                    l.Recuerdos.Add(Recuerdo);
                }
            }

            return ListasRecuerdos;
        }
        String[] InvertirParametros(String[] parametros)
        {
            String[] par_tmp;

            int s = parametros.GetLength(0);
            par_tmp = new String[s];

            for (int i = 0; i < s; i++)
            {
                par_tmp[i] = parametros[s - i - 1];
            }
            return par_tmp;
        }
        #endregion

    }
}

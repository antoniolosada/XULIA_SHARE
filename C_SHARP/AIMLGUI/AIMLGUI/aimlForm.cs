
using System.Data.OleDb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SpeechLib;
using AIMLbot;
using System.Threading.Tasks;
using System.Threading;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Configuration;
using Microsoft.VisualBasic;
using System.Windows;
using System.Speech;
using System.Net;
using System.Net.Sockets;
using XULIA;
using _09_HTTPLISTENER_WEBSERVER;
using System.Collections;

using System.Globalization;
using System.Resources;

namespace AIMLGUI
{


    public partial class aimlForm : Form
    {
        #region variables

        public ServidorWeb srw = new ServidorWeb();
        ProcesamientoComandos ControlVoz = new ProcesamientoComandos();

        const string DIR_ACT = @"C:\TMP\XULIA\";
        const int TIEMPO_GLOBO = 5000;
        public const string URL_VERSIONES = @"https://sites.google.com/site/accesibilidadinteligente/xulia---xestion-unificada-da-linguaxe-con-intelixencia-artificial/descarga-e-instalacion/versiones.txt?attredirects=0&d=1";
        long PosicionLecturaFicheroRecVoz = 0;
        private Bot myBot;
        private User myUser;
        private Request lastRequest = null;
        private Result lastResult = null;
        string salidaDictado = "";
        public long CONTADOR_LECTURA;
        frmEstado m_Estado;
        public bool ActivarSAPI4 = false;
        public NotifyIcon trayIcon;
        private ContextMenu trayMenu;
        public enum TipoIcono : int { activa, desactiva, dictado, gramatica_incorrecta, falta_precision };
        public Icon[] Iconos = new Icon[5];

        public static Avatar frmAvatar;
        static ProcesamientoComandos static_ControlVoz;
        static System.Speech.Synthesis.SpeechSynthesizer static_voz;
        static Avatar static_avatar;
        static bool VozFin = false;
        static string[] static_palabras;
        static int contador = 0;
        static string static_frase = "";
        Queue TextoGoogle = new Queue();

        #endregion variables

        #region Inicializacion
        public aimlForm()
        {
            InitializeComponent();
            this.toolStripMenuItemSpeech.Checked = true;
            this.richTextBoxInput.Focus();
            myBot = new Bot();
            myBot.loadSettings();
            myUser = new User("DefaultUser", this.myBot);
            myBot.WrittenToLog += new Bot.LogMessageDelegate(myBot_WrittenToLog);
        }

        private void aimlForm_Load(object sender, EventArgs e)
        {
            //Traducimos el menú
            ControlVoz.TraducirMenu(menuStripMain, System.Threading.Thread.CurrentThread.CurrentCulture);

            //Arrancamos el servidor web de reconocimiento de voz para procesar las llamadas AJAX
            //Le pasamos el delegado para que lo llame cuando reconozca texto
            //Los ficheros de configuración AIML están en el directori CONFIG
            //Hemos cambiado todos los tags <bot por get name="bot_ para poder emplear GaitoBot
            srw.ArrancarSR(TextoRecuperadoGoogleChrome);

            frmEstado Estado = new frmEstado();
            m_Estado = Estado;
            m_Estado.ActualizarComando("Estatus: Loading Bot");

            AIMLbot.Utils.AIMLLoader loader = new AIMLbot.Utils.AIMLLoader(this.myBot);

            m_Estado.ActualizarComando("Estatus: Loaded ChatBot");

            //Activa el reconocimiento y configura el servidor de captura chrome. También carga los ficheros AIML para el BOT
            m_Estado.ActualizarComando("Estatus:  Init system voice recognition");
            ControlVoz.ActivarReconocimientoVoz(ControlVoz, this, Estado, this.richTextBoxOutput, srw);

            Estado.EnlaceFormXulia(this, ControlVoz);

            this.Text = this.Text + "(v" + Application.ProductVersion + ")";

            m_Estado.ActualizarComando("Estatus:  Check Updates");
            //Comprobando actualizaciones
            ComprobarActualizaciones();
            SysTrayApp();

            if (ControlVoz.ArranqueDesactivado == "S") ControlVoz.DesactivarXulia();

            ControlVoz.ARRANQUE_COMPLETADO = true;

            static_avatar = frmAvatar;
            if (ControlVoz.ActivarAvatar)
            {
                if (ControlVoz.MaximizarAvatar)
                    frmAvatar.WindowState = FormWindowState.Maximized;
                frmAvatar.MostrarAvatar();
            }

            m_Estado.ActualizarComando("Ready for Work ("+ ControlVoz.CodigoIdiomaComandoGoogle + ")");

        }

        #endregion Inicializacion
        //delegate para que srw comunique el texto reconocido por chrome
        public void TextoRecuperadoGoogleChrome(String texto)
        {
            //Se recupera con un timer para poder interactuar con los componentes del form
            TextoGoogle.Enqueue(texto);
        }

        public void ActivarTimerProcesarComandosGoogle()
        {
            //Este timer está siempre activo
            tmrRecuperarTextoGoogle.Enabled = true;
        }

        public void SysTrayApp()
        {
            Iconos[(int)TipoIcono.activa] = new Icon(Application.StartupPath + @"\Iconos\activa.ico", 40, 40);
            Iconos[(int)TipoIcono.desactiva ] = new Icon(Application.StartupPath + @"\Iconos\desactiva.ico", 40, 40);
            Iconos[(int)TipoIcono.dictado ] = new Icon(Application.StartupPath + @"\Iconos\dictado.ico", 40, 40);
            Iconos[(int)TipoIcono.gramatica_incorrecta ] = new Icon(Application.StartupPath + @"\Iconos\gramatica_incorrecta.ico", 40, 40);
            Iconos[(int)TipoIcono.falta_precision ] = new Icon(Application.StartupPath + @"\Iconos\falta_precision.ico", 40, 40);
            // Create a simple tray menu with only one item.
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Salir", OnExit);
            trayMenu.MenuItems.Add("Activar", OnActivate);
            trayMenu.MenuItems.Add("Desactivar", OnDisable);
            trayMenu.MenuItems.Add("Ocultar", OnHide);
            trayMenu.MenuItems.Add("Ayuda", OnAyuda);
            trayMenu.MenuItems.Add("Mostrar", OnShow);

            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            trayIcon = new NotifyIcon();
            trayIcon.Text = "XULIA";
            trayIcon.Icon = Iconos[(int)TipoIcono.activa];
 
            // Add menu to tray icon and show it.
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible     = true;

            frmAvatar = new Avatar();
        }
        public void CambiarIcono(TipoIcono Tipo)
        {
            switch(Tipo)
            {
                case TipoIcono.activa:
                    trayIcon.Icon = Iconos[(int)TipoIcono.activa];
                    break;
                case TipoIcono.desactiva:
                    trayIcon.Icon = Iconos[(int)TipoIcono.desactiva ];
                    break;
                case TipoIcono.dictado:
                    trayIcon.Icon = Iconos[(int)TipoIcono.dictado ];
                    break;
                case TipoIcono.falta_precision:
                    trayIcon.Icon = Iconos[(int)TipoIcono.falta_precision ];
                    break;
                case TipoIcono.gramatica_incorrecta:
                    trayIcon.Icon = Iconos[(int)TipoIcono.gramatica_incorrecta ];
                    break;
            }
        }

        public void MostrarGlobo(string texto, string modo, string aplicacion)
        {
            //Si estamos mostrando la notificación anterior no se muestran nuevas
            //El tiempo de notificación del sistema está definido en Configuración de Accesibilidad y tiene un mínimo de 5s
            if (!tmrNuevaNotificacion.Enabled)
            {
                if (ControlVoz.ModoGloboBasico == "S")
                    trayIcon.ShowBalloonTip(TIEMPO_GLOBO, "", texto, ToolTipIcon.Info);
                else
                    trayIcon.ShowBalloonTip(TIEMPO_GLOBO, "", texto + Environment.NewLine + modo + Environment.NewLine + aplicacion, ToolTipIcon.Info);
                tmrNuevaNotificacion.Interval = TIEMPO_GLOBO;
                tmrNuevaNotificacion.Enabled = true;
            }
        }

        private void OnExit(object sender, EventArgs e)
        {
            ControlVoz.SalirDeXulia();
        }
        private void OnActivate(object sender, EventArgs e)
        {
            ControlVoz.ActivarXulia();
        }
        private void OnDisable(object sender, EventArgs e)
        {
            ControlVoz.DesactivarXulia();
        }
        private void OnHide(object sender, EventArgs e)
        {
            ControlVoz.Estado.Hide();
        }
        private void OnAyuda(object sender, EventArgs e)
        {
            Form frmAyuda = new AyudaRapida();
            frmAyuda.Show();
        }
        private void OnShow(object sender, EventArgs e)
        {
            ControlVoz.Estado.Show();
        }
        public void CargarFicherosAIML(string Path)
        {
            try
            {
                AIMLbot.Utils.AIMLLoader loader = new AIMLbot.Utils.AIMLLoader(this.myBot);
                this.myBot.isAcceptingUserInput = false;
                loader.loadAIML(Path);
                this.myBot.isAcceptingUserInput = true;
            }
            catch (Exception ex)
            {
                LOG("Error cargando ficheros AIML (UserSession.xml): " + ex.Message.ToString());
            }
        }
        
        void myBot_WrittenToLog()
        {
            this.richTextBoxOutput.Text += this.myBot.LastLogMessage+Environment.NewLine+Environment.NewLine;
            this.richTextBoxOutput.ScrollToCaret();
        }

        #region Menu Item Events
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlVoz.SalirDeXulia();
        }

        private void saveBotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo fi = new FileInfo(Application.ExecutablePath);
                saveFileDialogDump.InitialDirectory = fi.DirectoryName;
                saveFileDialogDump.AddExtension = true;
                saveFileDialogDump.DefaultExt = "dat";
                saveFileDialogDump.FileName = "Graphmaster.dat";
                DialogResult dr = saveFileDialogDump.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    if (this.myBot.Size > 0)
                    {
                        this.myBot.isAcceptingUserInput = false;
                        this.myBot.saveToBinaryFile(saveFileDialogDump.FileName);
                        this.myBot.isAcceptingUserInput = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.richTextBoxOutput.Text += ex.Message + Environment.NewLine;
            }
        }

        private void fromAIMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                folderBrowserDialogAIML.RootFolder = Environment.SpecialFolder.MyComputer;
                folderBrowserDialogAIML.SelectedPath = this.myBot.PathToAIML;
                DialogResult dr = folderBrowserDialogAIML.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    AIMLbot.Utils.AIMLLoader loader = new AIMLbot.Utils.AIMLLoader(this.myBot);
                    this.myBot.isAcceptingUserInput = false;
                    if (folderBrowserDialogAIML.SelectedPath.Length > 0)
                    {
                        loader.loadAIML(folderBrowserDialogAIML.SelectedPath);
                    }
                    else
                    {
                        loader.loadAIML(this.myBot.PathToAIML);
                    }
                    this.myBot.isAcceptingUserInput = true;
                }
            }
            catch (Exception ex)
            {
                this.richTextBoxOutput.Text += ex.Message + Environment.NewLine;
            }
        }

        private void fromDatFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo fi = new FileInfo(Application.ExecutablePath);
                openFileDialogDump.InitialDirectory = fi.DirectoryName;
                openFileDialogDump.AddExtension = true;
                openFileDialogDump.DefaultExt = "dat";
                openFileDialogDump.FileName = "Graphmaster.dat";
                DialogResult dr = openFileDialogDump.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    this.myBot.isAcceptingUserInput = false;
                    this.myBot.loadFromBinaryFile(openFileDialogDump.FileName);
                    this.myBot.isAcceptingUserInput = true;
                }
            }
            catch (Exception ex)
            {
                this.richTextBoxOutput.Text += ex.Message + Environment.NewLine;
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder();

            result.Append("Bot Settings:" + Environment.NewLine + Environment.NewLine);
            foreach (string setting in this.myBot.GlobalSettings.SettingNames)
            {
                result.Append(setting + ": " + this.myBot.GlobalSettings.grabSetting(setting)+Environment.NewLine);
            }

            this.showInformation(result);
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder();

            result.Append("User Information:" + Environment.NewLine + Environment.NewLine);

            result.Append("UserID: " + this.myUser.UserID + Environment.NewLine);
            result.Append("Topic: " + this.myUser.Topic + Environment.NewLine + Environment.NewLine);

            result.Append("User Predicate List:" + Environment.NewLine);
            foreach (string setting in this.myUser.Predicates.SettingNames)
            {
                result.Append(setting + ": " + this.myUser.Predicates.grabSetting(setting)+Environment.NewLine);
            }
            this.showInformation(result);
        }

        private void lastRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!object.Equals(null, this.lastRequest))
            {
                StringBuilder result = new StringBuilder();

                result.Append("Last Request:" + Environment.NewLine + Environment.NewLine);

                result.Append("Raw Input: " + this.lastRequest.rawInput.Replace(Environment.NewLine,"") + Environment.NewLine);
                result.Append("Started On: " + this.lastRequest.StartedOn + Environment.NewLine);
                result.Append("Has Timed Out: " + Convert.ToString(this.lastRequest.hasTimedOut) + Environment.NewLine + Environment.NewLine);
                this.showInformation(result);
            }
        }

        private void lastResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!object.Equals(null, this.lastResult))
            {
                StringBuilder result = new StringBuilder();

                result.Append("Last Result:" + Environment.NewLine + Environment.NewLine);

                result.Append("Raw Input: " + this.lastResult.RawInput + Environment.NewLine);
                result.Append("Output: " + this.lastResult.Output + Environment.NewLine);
                result.Append("Raw Output: " + this.lastResult.RawOutput + Environment.NewLine);
                result.Append("Duration: "+this.lastResult.Duration.ToString() + Environment.NewLine + Environment.NewLine);
                result.Append("Sentences: " + Environment.NewLine);
                foreach (string sentence in this.lastResult.InputSentences)
                {
                    result.Append(sentence + Environment.NewLine);
                }
                result.Append(Environment.NewLine);
                
                result.Append(Environment.NewLine);
                result.Append("Sub Queries: " + Environment.NewLine);
                result.Append(Environment.NewLine);
                foreach (AIMLbot.Utils.SubQuery query in this.lastResult.SubQueries)
                {
                    result.Append("Path: " + query.FullPath + Environment.NewLine);
                    result.Append("Template: " + Environment.NewLine + query.Template + Environment.NewLine);
                    result.Append(Environment.NewLine);
                    result.Append("Input Stars:" + Environment.NewLine);
                    foreach (string star in query.InputStar)
                    {
                        result.Append(star + Environment.NewLine);
                    }
                    result.Append(Environment.NewLine);
                    result.Append("That Stars:" + Environment.NewLine);
                    foreach (string that in query.ThatStar)
                    {
                        result.Append(that + Environment.NewLine);
                    }
                    result.Append(Environment.NewLine);
                    result.Append("Topic Stars:" + Environment.NewLine);
                    foreach (string topic in query.TopicStar)
                    {
                        result.Append(topic + Environment.NewLine);
                    }
                    result.Append(Environment.NewLine);
                }
                result.Append(Environment.NewLine);
                result.Append("Output Sentences: " + Environment.NewLine);
                foreach (string outputSentence in this.lastResult.OutputSentences)
                {
                    result.Append(outputSentence + Environment.NewLine);
                }
                this.showInformation(result);
            }
        }

        private void showInformation(StringBuilder result)
        {
            ViewInformation vi = new ViewInformation();
            vi.OutputMessage = result.ToString();
            vi.ShowDialog(this);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string about = @"XULIA - Xestión Unificada do Lenguaxe con Intelixencia Artificial

Xulia és un sistema de control integral del ordenador por voz totalmente configurable y adaptable a las necesidades de cada usuario.
Adicionalmente, uno de sus módulos es un bot conversacional desarrollado sobre tecnología AIML.
El cerebro de Xulia está basado en el de Alexia, que ha sido desarrollado en la Universidad de Vigo.
Para el sistema de reconocimiento de comandos, se ha empleado las librearías SAPI de Windows, como también para el reconocimiento contínuo en modo Windows.

Para el reconocimiento contínuo mediante tecnología Google, se ha empleado el API de reconocimiento de voz de JavaScript que ha implementado Google Chrome.

Para poder extraer la información reconocida por el API de javascript se ha empleado un pequeño aplicativo desarrollado por Carlos Docampo Ramos que empleando tecnología AJAX transfiere el texto reconocido a un servidor Tomcat que a su vez éste coloca en un fichero para Xulia.

Para interpretar el cerebro de Xulia hemos empleado Program# desarrollado por Nicholas H.Tollervey.

Transcripción de la información original de Program#
--------------------------------------------
AIMLGui, Program# / AIMLBot (c) 2006 Nicholas H.Tollervey.
http://ntoll.org

This is a .NET implementation of the ALICE chatterbot using the AIML specification. Put simply, this software will allow you to chat (by entering text) with your computer using natural language.

Program# is a complete re-write of an earlier C# AIML implementation called AIMLBot. It is available under the Gnu LGPL. This means that you are free to download, modify and share it. Links to download Program# can be found at the bottom of the page.

";
            MessageBox.Show(about, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void licenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string content = @"
Program# / AIMLBot - a .Net implementation of the AIML standard.
Copyright (C) 2006  Nicholas H.Tollervey (http://ntoll.org)

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 2.1 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
";
            MessageBox.Show(content, "License", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        private void buttonGo_Click(object sender, EventArgs e)
        {
            this.processInputFromUser();
        }

        public string ProcesarEntrada(string entrada)
        {
            entrada = ConvertirASCIngles(entrada);
            this.richTextBoxInput.Text = entrada;
            return processInputFromUser();
        }
        private string processInputFromUser()
        {
            try
            {
                if (this.myBot.isAcceptingUserInput)
                {
                    string rawInput = "";
                    string entrada = this.richTextBoxInput.Text;
                    string comandos = "";

                    if (this.richTextBoxInput.Text == "") return "";
                    static_avatar = frmAvatar;
                    if (ControlVoz.ActivarAvatar)
                    {
                        if (ControlVoz.MaximizarAvatar)
                            frmAvatar.WindowState = FormWindowState.Maximized;
                        frmAvatar.ActivarAvatar();
                        frmAvatar.PensarActivo();
                        frmAvatar.TextoReconocido(entrada);
                    }

                    entrada = ProcesarCaracteresEntrada(entrada);
                    //Si la entrada comienza por '.' se sustituye por la salida de la ejecución del comando
                    bool salida_comando = entrada.Substring(0, 1) == ".";
                    //Separamos la salida de los comandos que empiezan por '·'
                    int pos = entrada.IndexOf("·");
                    if (pos >= 0)
                    {
                        rawInput = entrada.Substring(0, pos);
                        comandos = entrada.Substring(pos + 1);
                    }
                    else
                        rawInput = entrada;

                    this.richTextBoxInput.Text = string.Empty;
                    this.richTextBoxOutput.AppendText("You: " + rawInput + Environment.NewLine);
                    Request myRequest = new Request(rawInput, this.myUser, this.myBot);

                    //Realizamos las sustituciones de caracteres
                    Result myResult = this.myBot.Chat(myRequest);
                    this.lastRequest = myRequest;
                    this.lastResult = myResult;
                    string salida = ProcesarCaracteresSalida(myResult.Output);
                    static_ControlVoz = ControlVoz;

                    //Comprobamos si el retorno tiene un comando ****************************************
                    int poscmd = salida.IndexOf("·");
                    if (poscmd >= 0)
                    {
                        int fin_comando = salida.IndexOf("]");
                        String texto_comando = "";
                        String retorno_comando = salida.Substring(poscmd + 1, 1);
                        String comando = salida.Substring(poscmd + 2, fin_comando-(poscmd + 2)+1);
                        String parString = salida.Substring(fin_comando+1);

                        //Quitamos el . final
                        parString = parString.Substring(0, parString.Length - 1);
                        String[] parametros = parString.Split('|');

                        texto_comando = static_ControlVoz.EjecutarComando(comando, parametros);

                        if (retorno_comando == "?") //Si el comando comienza por ·> se sustituye la salida aiml por la del comando
                            salida = texto_comando;
                        else
                            salida = salida.Substring(0, poscmd - 1);
                    }
                    this.richTextBoxOutput.AppendText("Bot: " + salida + Environment.NewLine + Environment.NewLine);
                    if (this.toolStripMenuItemSpeech.Checked)
                    {
                        if (ControlVoz.voz != "")
                        {
                            ControlVoz.DesactivarReconocimiento();
                            System.Speech.Synthesis.SpeechSynthesizer voz = new System.Speech.Synthesis.SpeechSynthesizer();
                            voz.SpeakProgress += new EventHandler<SpeakProgressEventArgs>(synth_SpeakProgress);
                            voz.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(synth_SpeakCompleted);
                            voz.SelectVoice(ControlVoz.voz);
                            static_voz = voz;
                            static_frase = salida;
                            contador = 0;
                            voz.SpeakAsync(salida);
                            //ControlVoz.recognizer.RecognizeAsync(RecognizeMode.Multiple); //Activamos el reconocimiento en el evento SpeakCompleted
                        }
                        else
                        {
                            SpVoice objSpeech = new SpVoice();
                            objSpeech.Speak(salida, SpeechVoiceSpeakFlags.SVSFlagsAsync);
                            objSpeech.SynchronousSpeakTimeout = 20;
                            objSpeech.WaitUntilDone(3000);
                            objSpeech.Rate = 1;
                        }

                    }
                    return myResult.Output;
                }
                else
                {
                    this.richTextBoxInput.Text = string.Empty;
                    this.richTextBoxOutput.AppendText("Bot not accepting user input." + Environment.NewLine);
                    return "";
                }
            }
            catch (Exception e)
            {
                LOG("Error en processInputFromUser: " + e.Message.ToString());
            }
            return "";
        }
        static void synth_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            static_ControlVoz.ActivarReconocimiento();
            VozFin = true;
            static_voz.Dispose();
            if (static_ControlVoz.ActivarAvatar)
            {
                static_avatar.PararAvatar();
                frmAvatar.PensarDesactivo();
            }
        }
        static void synth_SpeakProgress(object sender, SpeakProgressEventArgs e)
        {
            try
            {
                static_avatar.Palabra(static_frase.Substring(e.CharacterPosition, e.CharacterCount));
                //Localizamos si segue un signo de puntuación
                for (int i = e.CharacterPosition + e.CharacterCount; i < static_frase.Length; i++)
                {
                    char car = static_frase.Substring(i, 1).ToLower().ToCharArray()[0];
                    if ((car == '.') || (car == ',') || (car == ';') || (car == '?') || (car == '!'))
                    {
                        static_avatar.Pausa();
                    }
                    else if ((car >= 'a') && (car <= 'z'))
                    {
                        //Comienza una nueva palabra
                        break;
                    }
                }
                if (e.CharacterPosition + e.CharacterCount + 1 >= static_frase.Length)
                {
                    //Hemos llegado al final de la frase
                    static_avatar.PararAvatar();
                }
            }
            catch (Exception ex)
            {
            }
        }
        List<string> DividirPalabras(string frase)
        {
            List<string> palabras = new List<string>();
            string palabra = "";
            bool espacio = false;

            for (int i = 0; i < frase.Length; i++)
            { 
                char car = frase.Substring(i,1).ToCharArray()[0];
                if (car <= 64) //Caracteres especiales
                {
                    if (palabra.Length > 0)
                    {
                        //Los signos de puntuación ocupacion una posición del array solos
                        palabras.Add(palabra);
                        palabra = "";
                        palabras.Add(car.ToString());
                    }
                    espacio = false;
                }
                else if ((car == ' '))
                {
                    //Si es el primer espacio y tenemos una palabra la grabamos
                    if (!espacio)
                    {
                        if (palabra.Length > 0)
                        {
                            //Los signos de puntuación ocupacion una posición del array solos
                            palabras.Add(palabra);
                            palabra = "";
                        }
                    }
                    espacio = true;
                }
                else
                {
                    espacio = false;
                    palabra = palabra + car;
                }

            }
            if (palabra.Length > 0)
                palabras.Add(palabra);
            return palabras;
        }
        private void singleFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo fi = new FileInfo(Path.Combine(Application.ExecutablePath, "aiml"));
                openFileDialogDump.InitialDirectory = fi.DirectoryName;
                openFileDialogDump.AddExtension = true;
                openFileDialogDump.DefaultExt = "aiml";
                openFileDialogDump.FileName = "Reduce.aiml";
                DialogResult dr = openFileDialogDump.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    AIMLbot.Utils.AIMLLoader loader = new AIMLbot.Utils.AIMLLoader(this.myBot);
                    this.myBot.isAcceptingUserInput = false;
                    loader.loadAIMLFile(openFileDialogDump.FileName);
                    this.myBot.isAcceptingUserInput = true;
                }
            }
            catch (Exception ex)
            {
                this.richTextBoxOutput.Text += ex.Message + Environment.NewLine;
            }
        }

        private void richTextBoxInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.processInputFromUser();
            }
        }

        private void richTextBoxOutput_TextChanged(object sender, EventArgs e)
        {
            this.richTextBoxOutput.ScrollToCaret();
        }

        private void toolStripMenuItemCustomLib_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo fi = new FileInfo(Application.ExecutablePath);
                openFileDialogDump.InitialDirectory = fi.DirectoryName;
                openFileDialogDump.AddExtension = true;
                openFileDialogDump.DefaultExt = "dll";
                DialogResult dr = openFileDialogDump.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    if (openFileDialogDump.FileName.Length > 0)
                    {
                        this.myBot.isAcceptingUserInput = false;
                        this.myBot.loadCustomTagHandlers(openFileDialogDump.FileName);
                        this.myBot.isAcceptingUserInput = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.richTextBoxOutput.Text += ex.Message + Environment.NewLine;
            }
        }

        private void toolStripMenuItemSpeech_Click(object sender, EventArgs e)
        {
            this.toolStripMenuItemSpeech.Checked = !this.toolStripMenuItemSpeech.Checked;
        }

        private void fromDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AIMLbot.Utils.AIMLLoader loader = new AIMLbot.Utils.AIMLLoader(this.myBot);
                this.myBot.isAcceptingUserInput = false;
                loader.loadAIML(this.myBot.PathToAIML);
                this.myBot.isAcceptingUserInput = true;
            }
            catch (Exception ex)
            {
                this.richTextBoxOutput.Text += ex.Message + Environment.NewLine;
            }
        }

        private void toolStripMenuItemLoadSession_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo fi = new FileInfo(Application.ExecutablePath);
                openFileDialogDump.InitialDirectory = fi.DirectoryName;
                openFileDialogDump.AddExtension = true;
                openFileDialogDump.DefaultExt = "xml";
                openFileDialogDump.FileName = "UserSession.xml";
                DialogResult dr = openFileDialogDump.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    this.myUser.Predicates.loadSettings(openFileDialogDump.FileName);
                }
            }
            catch (Exception ex)
            {
                this.richTextBoxOutput.Text += ex.Message + Environment.NewLine;
            }
        }

        private void toolStripMenuItemSaveSession_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo fi = new FileInfo(Application.ExecutablePath);
                saveFileDialogDump.InitialDirectory = fi.DirectoryName;
                saveFileDialogDump.AddExtension = true;
                saveFileDialogDump.DefaultExt = "xml";
                saveFileDialogDump.FileName = "UserSession.xml";
                DialogResult dr = saveFileDialogDump.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    this.myUser.Predicates.DictionaryAsXML.Save(saveFileDialogDump.FileName);
                }
            }
            catch (Exception ex)
            {
                this.richTextBoxOutput.Text += ex.Message + Environment.NewLine;
            }
        }
        private void ComprobarActualizaciones()
        {
            string InfoVersion = "";
            WebClient miwebclient = new WebClient();

            Uri miurl = new Uri(URL_VERSIONES);

            try 
            {
                miwebclient.DownloadFile(miurl, DIR_ACT + "versiones.txt");

                string[] versionApp = Application.ProductVersion.Split('.');
                bool mostrarVersion = false;
                System.IO.StreamReader sr;
                sr = new System.IO.StreamReader(DIR_ACT + "versiones.txt", Encoding.Default);
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    if (mostrarVersion)
                    {
                        if (line == "@")
                        {
                            mostrarVersion = false;
                            line = "";
                        }
                        InfoVersion += line + Environment.NewLine;
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
                            else if (line.Substring(0, 1) == "#")  //comienza la zona de comandos
                                break;

                        }
                    }
                }
                sr.Close();

                if (InfoVersion != "")
                {
                    InfoActualizar  Info = new InfoActualizar();
                    Info.MostrarInfoVersion (InfoVersion);
                }
            }
            catch (Exception e)
            {
                LOG("Comprobando actualizaciones: " + e.Message.ToString());
                LOG("El directorio " + DIR_ACT + " debe existir");
            }
        }

        
        private void buttonGo_Click_1(object sender, EventArgs e)
        {
            this.processInputFromUser();
        }

        private void richTextBoxInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBoxInput_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.processInputFromUser();
                richTextBoxInput.Focus();
            }
        }

        private void richTextBoxOutput_TextChanged_1(object sender, EventArgs e)
        {
            this.richTextBoxOutput.ScrollToCaret();
        }


        private void aimlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Impedir que el formulario se cierre pulsando X o Alt + F4 
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    e.Cancel = true;
                    this.Hide();
                    return;
            }
            if (ActivarSAPI4)
            {
                this.DesactivarTimerSockSAPI4();
                aimlForm.CloseSck();
            }
            ControlVoz.EliminarRecursos();
            trayIcon.Dispose();
        }

        private void Cfg_Click(object sender, EventArgs e)
        {


        }

        private void tmrRaton_Tick(object sender, EventArgs e)
        {
            ControlVoz.MoverRaton();
        }

        private void Menu_Click(object sender, EventArgs e)
        {
            menuStripMain.Visible = !menuStripMain.Visible;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public void ActualizaPantalla(ProcesamientoComandos pc, string Comando, string ClaseVetnana, string TituloVentana)
        {
            lblAcento.Visible = pc.ACENTO;
            lblMAY.Visible = pc.MAYUSCULAS;

            lblControl.Visible = pc.CONTROL;
            lblShift.Visible = pc.SELECCION;
            lblALT.Visible = pc.ALT;

            tbClase.Text = ClaseVetnana;
            tbModo.Text = pc.MODO;
            tbTitulo.Text = TituloVentana;
            tbNumero.Text = pc.Multiplicador.ToString();
        }

        private void tmrLeerFicheroCargaGramApp_Tick(object sender, EventArgs e)
        {
            bool ModoDictadoConversacion = false;
            tmrLeerFichero_CargaGRamApp.Enabled = false;
            ControlVoz.RecargaGramaticasAplicacion();
            ControlVoz.ActualizarPantallas(false);
            ControlVoz.MostrarGramaticasActivas(tbSalida);

            try
            {
                //ModoDictadoConversación = Debe haber fichero de voz y el modo o es dictado o es conversación con información de idioma
                ModoDictadoConversacion = (ControlVoz.ArchivoRecVoz != "") &&
                    (((Strings.InStr(ControlVoz.MODO, "·DICTADO·") > 0) || (Strings.InStr(ControlVoz.MODO, "·CONVERS·") > 0)) && (ControlVoz.MODO.Length > 10));
                //Solo recuperamos el contenido del fichero en modo conversación o modo dictado o modo comando google
                if ((!ModoDictadoConversacion) && !(ControlVoz.ModoComandoGoogle == "S"))
                {
                    tmrLeerFichero_CargaGRamApp.Enabled = true;
                    return;
                } 

                using (FileStream fsSource = new FileStream(ControlVoz.ArchivoRecVoz,
                    FileMode.Open, FileAccess.Read))
                {
                    if (fsSource.Length < PosicionLecturaFicheroRecVoz) PosicionLecturaFicheroRecVoz = 0;
                    // Read the source file into a byte array.
                    byte[] bytes = new byte[fsSource.Length - PosicionLecturaFicheroRecVoz + 1];
                    long numBytesToRead = fsSource.Length;
                    int numBytesRead = 0;
                    int numBytesReadOp;

                    if (PosicionLecturaFicheroRecVoz > 0)
                    {
                        fsSource.Position = PosicionLecturaFicheroRecVoz;
                        numBytesToRead -= PosicionLecturaFicheroRecVoz;
                    }
                    if (numBytesToRead < 32760)
                        numBytesReadOp = (int)numBytesToRead;
                    else
                        numBytesReadOp = 32760;

                    if (numBytesToRead > 0)
                    { 
                        while (numBytesToRead > 0)
                        {
                            // Read may return anything from 0 to numBytesToRead.
                            int n = fsSource.Read(bytes, numBytesRead, numBytesReadOp);

                            // Break when the end of the file is reached.
                            if (n == 0)
                                break;

                            numBytesRead += n;
                            numBytesToRead -= n;
                            if (numBytesToRead < numBytesReadOp)
                                numBytesReadOp = (int)numBytesToRead;
                        }
                        numBytesToRead = bytes.Length;
                        //Convertimos el array de bytes en una cadena
                        for (long i = 0; i < numBytesToRead; i++)
                        {
                            char c = Convert.ToChar(bytes[i]);
                            if (c == '·')
                            {
                                tbSalida.Text = salidaDictado + Constants.vbCrLf + tbSalida.Text;
                                if (ModoDictadoConversacion)
                                    ControlVoz.TextoReconocido(salidaDictado, (float)0.999999, true);
                                else if (ControlVoz.ModoComandoGoogle == "S")
                                    ControlVoz.TextoReconocidoComandoGoogle(salidaDictado);
                                salidaDictado = "";
                            }
                            else if ((bytes[i] != 10) && (bytes[i] != 13) && (bytes[i] != 0))
                                salidaDictado += c;
                        }
                        PosicionLecturaFicheroRecVoz = fsSource.Position;
                    }
                }
                tmrLeerFichero_CargaGRamApp.Enabled = true;
                //Una vez que nos llega el primer reconocimiento podemos eliminar el comando de arranque
                System.IO.File.WriteAllText(ControlVoz.ArchivoComandos,"");
            }
            catch (Exception ioEx)
            {
                Console.WriteLine(ioEx.Message);
                tmrLeerFichero_CargaGRamApp.Enabled = true;
            }
        }

        private void PRUEBA_Click(object sender, EventArgs e)
        {
            ControlVoz.TextoReconocido(tbComando.Text , (float)0.999, true);
            tbComando.Text = "";
            //ControlVoz.TextoReconocido("dictadoespañol", (float)0.999, true);
            //ControlVoz.TextoReconocido("Esta es una prueba de reconocimiento nuevo párrafo par controlar las nuevas líenas.", (float)0.999, true);
            //ControlVoz.TextoReconocido("Esta es una prueba de reconocimientonuevo párrafo par controlar las nuevas líenas.", (float)0.999, true);
            //ControlVoz.TextoReconocido("Esta es una prueba de reconocimiento nuevo párrafo ", (float)0.999, true);
            //ControlVoz.TextoReconocido("Esta es una prueba de reconocimiento nuevo párrafo", (float)0.999, true);
        }

        private void ayudaXuliaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAyuda frm = new frmAyuda();
            frm.MostrarAyuda(ControlVoz);
        }

        private void tmrAtencion_Tick(object sender, EventArgs e)
        {
            ControlVoz.CONTROL = false;
            ControlVoz.ALT = false;
            tmrAtencion.Enabled = false;
            ControlVoz.ATENCION = false;
        }
        public void ActivarControlAtencion(int Tiempo)
        {
            ControlVoz.ATENCION = true;
            tmrAtencion.Interval = Tiempo * 1000;
            tmrAtencion.Enabled = true;
        }

        public void LOG(string log)
        {
            tbLOG.Text = log + Constants.vbCrLf + tbLOG.Text;
        }
        public void TimerRatonActivo(bool activo)
        {
            tmrRaton.Enabled = activo;
        }

        private void configuraciónXULIAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuracion cfg = new XULIA.Configuracion();
            cfg.ControlVoz = ControlVoz;

            cfg.Show();
        }

        private void versionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Versiones frmVersion = new Versiones();

            frmVersion.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TaskManager tm = new TaskManager();

            tm.Show();
        }

        private void aimlForm_Activated(object sender, EventArgs e)
        {
            if (ControlVoz.ArranqueXuliaVisible == "N")
            {
                this.Visible = false;
                ControlVoz.ArranqueXuliaVisible = "S";
            }

        }

        private void tmrLectura_Tick(object sender, EventArgs e)
        {
            ActualizarLectura();
        }

        public void ActualizarLectura()
        { 
            if (CONTADOR_LECTURA >= ControlVoz.LECTURA_VELOCIDAD)
                CONTADOR_LECTURA = 0;
            else
                CONTADOR_LECTURA++;

            switch(ControlVoz.EstadoLectura)
            {
                case ProcesamientoComandos.EstadosLectura.Parar:
                    {
                        CONTADOR_LECTURA = 0;
                        this.tmrLectura.Enabled = false;
                        break;
                    }
                case ProcesamientoComandos.EstadosLectura.Abajo:
                    {
                        if (tmrLectura.Enabled == false)
                            tmrLectura.Enabled = true;
                        if (CONTADOR_LECTURA == 0)
                            SendKeys.SendWait("{DOWN}");
                        break;
                    }
                case ProcesamientoComandos.EstadosLectura.Arriba:
                    {
                        if (tmrLectura.Enabled == false)
                            tmrLectura.Enabled = true;
                        if (CONTADOR_LECTURA == 0)
                            SendKeys.SendWait("{UP}");
                        break;
                    }
            }
        }

        private void cmdEscribirConfig_Click(object sender, EventArgs e)
        {
            //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ////config.AppSettings.Settings.Add("Opcion1", "True");
            //config.GetSection("Cancelar").Settings.Add("Opcion1", "True");
            //config.Save(ConfigurationSaveMode.Modified);
        }

        private void recargarConfiguraciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlVoz.RecargarConfiguracion();
        }

        private void biscarComandosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlVoz.frmBuscarComandos.ActivarBuscarComandos();
            Interaction.AppActivate("Buscar Comandos Xulia");
        }

        IDataObject[] myRetrievedObject = new IDataObject[10];
        int idata = 0;
        private void cmdCorreccion_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Clipboard.GetText());
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(myRetrievedObject[0], true);
        }

        private void aimlForm_SizeChanged(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void cmdPRUEBACorreccion_Click(object sender, EventArgs e)
        {
            Correccion frm = new Correccion();

            frm.Corregir("Esto es una prueba de corrección de una frase un poco grande", ControlVoz);
        }

        #region ConexionSAPI4
        static TcpClient client;
        static NetworkStream stream;
        static bool ConnectSck(string serverIP, Int32 port)
        {
            string output = "";

            try
            {
                // Create a TcpClient.
                // The client requires a TcpServer that is connected
                // to the same address specified by the server and port
                // combination.
                client = new TcpClient(serverIP, port);
                stream = client.GetStream();

                return true;
            }
            catch (ArgumentNullException e)
            {
                output = "ArgumentNullException: " + e;
                MessageBox.Show(output);
                return false;
            }
            catch (SocketException e)
            {
                output = "SocketException: " + e.ToString();
                MessageBox.Show(output);
                return false;
            }
        }
        public void ActivarTimerSockSAPI4()
        {
            tmrSockSAPI4.Enabled = true;
        }
        public void DesactivarTimerSockSAPI4()
        {
            tmrSockSAPI4.Enabled = false;
        }



        static void WriteSck(string message)
        {
            string output = "";

            try
            {
                // Translate the passed message into ASCII and store it as a byte array.
                Byte[] data = new Byte[256];
                data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                // Stream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);
            }
            catch (ArgumentNullException e)
            {
                output = "ArgumentNullException: " + e;
                MessageBox.Show(output);
            }
            catch (SocketException e)
            {
                output = "SocketException: " + e.ToString();
                MessageBox.Show(output);
            }
        }


        static void ReadSck(ref string message)
        {
            string output = "";

            try
            {
                Byte[] data = new Byte[256];
                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                if (bytes > 0)
                    message = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                else
                    message = "";
            }
            catch (ArgumentNullException e)
            {
                output = "ArgumentNullException: " + e;
                MessageBox.Show(output);
            }
            catch (SocketException e)
            {
                output = "SocketException: " + e.ToString();
                MessageBox.Show(output);
            }
        }

        static void CloseSck()
        {
            string output = "";

            try
            {
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                output = "ArgumentNullException: " + e;
                MessageBox.Show(output);
            }
            catch (SocketException e)
            {
                output = "SocketException: " + e.ToString();
                MessageBox.Show(output);
            }
        }

        private void cmdSAPI4_Click(object sender, EventArgs e)
        {
            aimlForm.ConnectSck("127.0.0.1", 1060);
        }

        private void cmdEnviarSAPI4_Click(object sender, EventArgs e)
        {
            aimlForm.WriteSck("hola");
        }

        private void cmdDesconectarSAPI4_Click(object sender, EventArgs e)
        {
            aimlForm.CloseSck();
        }

        static string sock = "";
        private void tmrSockSAPI4_Tick(object sender, EventArgs e)
        {
            string msg = "";
            if (aimlForm.stream.DataAvailable)
            {
                aimlForm.ReadSck(ref msg);
                if (msg != "")
                {
                    sock += msg;
                    if (sock.Substring(0, 2) == "%%")
                    {
                        sock = sock.Substring(2);
                        int pos = sock.IndexOf('$');
                        if (pos > 0)
                        {
                            string comando = sock.Substring(0, pos);
                            sock = sock.Substring(pos + 1);
                            ControlVoz.TextoReconocidoSAPI4(comando);
                        }
                    }
                }
            }
        }
        public int CargarGramatica(string[] comandos, string nombre)
        {
            string msg = "&&CARGAR_GRAMATICA: "+nombre+"&";
            DesactivarTimerSockSAPI4();

            foreach(string c in comandos)
                msg += CambiarFormato(c) + ",";

            msg = msg.Substring(0, msg.Length-1);
            msg += "$";

            aimlForm.WriteSck(msg);

            //Esperamos por la respuestos
            while (true)
            {
                Thread.Yield();
                aimlForm.ReadSck(ref msg);
                int pos = Strings.InStr(msg, "&&");
                if (pos > 0)
                {
                    int posFin = Strings.InStr(msg.Substring(pos), "$");
                    int respuestaComando = (int)Conversion.Val(msg.Substring(pos - 1, posFin - pos + 1));
                    ActivarTimerSockSAPI4();
                    return respuestaComando;
                }
            }
            ActivarTimerSockSAPI4();
            return -1;
        }

        string CambiarFormato(string c)
        {
            char[] ac = c.ToCharArray();
            string salida = "";
            char char_salida;

            for (int i = 0; i < ac.Length; i++)
            {
                switch (ac[i])
                { 
                    case 'á':
                    case 'ä':
                        char_salida = 'a';
                        break;
                    case 'é':
                    case 'ë':
                        char_salida = 'e';
                        break;
                    case 'í':
                    case 'ï':
                        char_salida = 'i';
                        break;
                    case 'ó':
                    case 'ö':
                        char_salida = 'o';
                        break;
                    case 'ú':
                    case 'ü':
                        char_salida = 'u';
                        break;
                    default:
                        char_salida = ac[i];
                        break;
                }
                salida += char_salida;
            }
            return salida;
        }

        public void ActivarGramatica(int gramatica)
        {
            string msg = "&&ACTIVAR:" + gramatica + "&$";
            aimlForm.WriteSck(msg);
        }
        public void DesactivarGramatica(int gramatica)
        {
            string msg = "&&DESACTIVAR:" + gramatica + "&$";
            aimlForm.WriteSck(msg);
        }
        public void ResetearReconocedor()
        {
            string msg = "&&RESET:1&$";
            aimlForm.WriteSck(msg);
        }
        public void ConectarSAPI4(string IP, Int32 puerto)
        {
            aimlForm.ConnectSck(IP, puerto);
        }

        #endregion ConexionSAPI4

        private void cmdMAtch_Click(object sender, EventArgs e)
        {
            string[] salida = new string[1];
            ControlVoz.Match("* es un * grande *", "un lobo es un animal grande y feroz", ref salida);
        }

        private void cmdCorregirFicherosAIML_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + ControlVoz.RutaCerebroXulia;
            // process the AIML

            string[] fileEntries = Directory.GetFiles(path, "*.aiml");
            if (fileEntries.Length > 0)
            {
                System.IO.StreamReader sr;
                System.IO.StreamWriter sw;
                foreach (string filename in fileEntries)
                {
                    try
                    {
                        sr = new System.IO.StreamReader(filename, Encoding.UTF8);
                        sw = new System.IO.StreamWriter(filename + ".aiml", false, Encoding.Default);

                        while (!sr.EndOfStream)
                        {
                            string linea = sr.ReadLine(); //Leemos la cabecera
                            if (linea != null)
                            {
                                linea = ConvertirASCIngles(linea);

                                sw.WriteLine(linea);
                            }
                            else
                                break;
                        }
                        sr.Close();
                        sw.Close();
                    }
                    catch (Exception ex)
                    {
                        LOG("Error en cmdCorregirFicherosAIML_Click:" + ex.Message.ToString());
                    }
                }
                MessageBox.Show("Operación finalizada");
            }
            else
            {
            }
        }

        public string ConvertirASCIngles(string linea)
        {
            linea = linea.ToLower();

            linea = linea.Replace("á", "a");
            linea = linea.Replace("é", "e");
            linea = linea.Replace("í", "i");
            linea = linea.Replace("ó", "o");
            linea = linea.Replace("ú", "u");

            linea = linea.Replace("à", "a");
            linea = linea.Replace("è", "e");
            linea = linea.Replace("ì", "i");
            linea = linea.Replace("ò", "o");
            linea = linea.Replace("ù", "u");

            linea = linea.Replace("ä", "a");
            linea = linea.Replace("ë", "e");
            linea = linea.Replace("ï", "i");
            linea = linea.Replace("ö", "o");
            linea = linea.Replace("ü", "u");

            linea = linea.Replace("ñ", "nh");
            linea = linea.Replace("¿", "");
            linea = linea.Replace("¡", "");
            linea = linea.Replace("ç", "ss");

            return linea;
        }
        private void cmdNavegador_Click(object sender, EventArgs e)
        {
            NavegadorWeb web = new NavegadorWeb();

            web.Show();
        }

        private void cmdCargarDiccionario_Click(object sender, EventArgs e)
        {
            const string filename = "D:\\GOOGLEDRIVE\\DATOS\\PROY\\XULIA\\dicc_pt.src";
            OleDbCommand cmd = new OleDbCommand();
            System.IO.StreamReader sr;
            long cont = 0;

            sr = new System.IO.StreamReader(filename, Encoding.UTF8);
            cmd.Connection = ControlVoz.conn;
            cmd.CommandText = "DELETE FROM dic";
            cmd.ExecuteNonQuery();
            while (!sr.EndOfStream)
            {
                string cad = sr.ReadLine();
                string[] partes = cad.Split(' ');

                cmd.CommandText = "INSERT INTO dic VALUES ('" + partes[0] + "','" + partes[1] + "','" + partes[2] + "'";
                if (partes.Length > 3)
                {
                    string propiedades = "";
                    for (int i=3; i < partes.Length; i++)
                        propiedades += partes[i]+ " ";
                    cmd.CommandText += ",'" + propiedades + "'";
                }
                else
                    cmd.CommandText += ",''";
                cmd.CommandText += ")";
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                cont++;
                tbCont.Text = cont.ToString();
                tbCont.Refresh();
            }
            MessageBox.Show("Operación finalizada");
            
        }

        private void cmdEnviarCorreo_Click(object sender, EventArgs e)
        {
            SendEmailWithOutlook("alosada@xunta.es", "hola", "hola");
        }
        public static Boolean SendEmailWithOutlook(string mailDirection, string mailSubject, string mailContent)
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
                mailItem.Attachments.Add(@"C:\temp\SETUP.LST");
                //mailItem.Send();

            }
            catch (System.Exception ex)
            {
                return false;
            }
            finally
            {
            }
            return true;
        }

        private void cmdLeerCorreos_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Outlook.Application outlookApp = new Microsoft.Office.Interop.Outlook.Application();
            Microsoft.Office.Interop.Outlook.NameSpace outlookNS = null;
            Microsoft.Office.Interop.Outlook.MAPIFolder mails = null;

            outlookNS = outlookApp.GetNamespace("MAPI");
            mails = outlookNS.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);
            int i = 0;
            foreach (object obj in mails.Items)
            {
                Microsoft.Office.Interop.Outlook.MailItem item = obj as Microsoft.Office.Interop.Outlook.MailItem;
                if (item != null)
                {
                    listBox1.Items.Add("Hora :" + item.ReceivedTime + " , Remitente :" + item.SenderName + " , Asunto :" + item.Subject);
                }
                if (i++ == 20) break;
            }
        }


        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {

        }

        List<string> AccessContacts(string findLastName)
        {
            List<string> contactos = new List<string>();
            Microsoft.Office.Interop.Outlook.Application outlookApp = new Microsoft.Office.Interop.Outlook.Application();
            Microsoft.Office.Interop.Outlook.NameSpace outlookNS = null;

            outlookNS = outlookApp.GetNamespace("MAPI");

            Microsoft.Office.Interop.Outlook.MAPIFolder folderContacts = outlookNS.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderContacts);
            Microsoft.Office.Interop.Outlook.Items searchFolder = folderContacts.Items;
            int counter = 0;
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

        private void cmdBuscarContacto_Click(object sender, EventArgs e)
        {
            foreach (string s in AccessContacts("eduardo"))
            {
                listBox1.Items.Add(s);
            }
        }

        private void cmdPruebaRaton_Click(object sender, EventArgs e)
        {
            CursorRaton c = new CursorRaton();

            c.Show();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cmdPruebaVoz_Click(object sender, EventArgs e)
        {
        }

        private void tmrRecuperarTextoGoogle_Tick(object sender, EventArgs e)
        {
            if (TextoGoogle.Count > 0)
            {
                bool ModoDictadoConversacion = false;
                String texto = TextoGoogle.Dequeue().ToString();

                //ModoDictadoConversación = Debe haber fichero de voz y el modo o es dictado o es conversación con información de idioma
                ModoDictadoConversacion = (((Strings.InStr(ControlVoz.MODO, "·DICTADO·") > 0) || (Strings.InStr(ControlVoz.MODO, "·CONVERS·") > 0) || (Strings.InStr(ControlVoz.MODO, "·OKXULIA·") > 0)) && (ControlVoz.MODO.Length > 10));

                //Solo recuperamos el contenido en modo conversación o modo dictado o modo comando google
                if ((ModoDictadoConversacion) || (ControlVoz.ModoComandoGoogle == "S"))
                {
                    if (ModoDictadoConversacion)
                        ControlVoz.TextoReconocido(texto, (float)0.999999, true);
                    else if (ControlVoz.ModoComandoGoogle == "S")
                        ControlVoz.TextoReconocidoComandoGoogle(texto);
                    salidaDictado = "";
                }
            }
        }

        private void tmrNuevaNotificacion_Tick(object sender, EventArgs e)
        {
            tmrNuevaNotificacion.Enabled = false;
        }

        string ProcesarCaracteresEntrada(string entrada)
        {
            string s = "";
            string salida = "";
            entrada = entrada.ToLower();
            for (int i = 0; i < entrada.Length; i++)
            {
                string c = entrada.Substring(i, 1);
                switch (c)
                {
                    case "á":
                    case "ä":
                    case "à":
                            s = "a";
                            break;
                    case "é":
                    case "ë":
                    case "è":
                            s = "e";
                            break;
                    case "í":
                    case "ì":
                    case "ï":
                            s = "i";
                            break;
                    case "ó":
                    case "ò":
                    case "ö":
                            s = "o";
                            break;
                    case "ú":
                    case "ù":
                    case "ü":
                            s = "u";
                            break;
                    case "ñ":
                            s = "nh";
                            break;
                    default:
                            s = c;
                            break;
                }
                salida += s;
            }
            return salida;
        }

        string ProcesarCaracteresSalida(string entrada)
        {
            entrada = entrada.Replace("nh", "ñ");
            return entrada;
        }

        private void btActivarApp_Click(object sender, EventArgs e)
        {
            ControlVoz.ActivateAppChrome("WhatsApp");
        }

        private void TraducirMenu_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(CultureInfo.CurrentCulture.DisplayName);
            //MessageBox.Show(Cadenas.ResourceManager.GetString("mnu_licenseToolStripMenuItem"));
            //System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("pt-BR");
            //Cadenas.Culture = CultureInfo.CreateSpecificCulture("pt-BR");
            //MessageBox.Show(Cadenas.cadena1);
            //MessageBox.Show(Cadenas.ResourceManager.GetString("cadena1", CultureInfo.CreateSpecificCulture("pt-BR")));

            ControlVoz.TraducirMenu(menuStripMain, System.Threading.Thread.CurrentThread.CurrentCulture);
        }

        private void btClipMouse_Click(object sender, EventArgs e)
        {
            Cursor.Clip = new Rectangle(10,10,500,500);


        }
    }
}
#define VOZ_UWP

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
#if VOZ_UWP
    using Windows.Media.SpeechRecognition;
    using Windows.ApplicationModel.Resources.Core;
    using System.Threading.Tasks;
#endif
using System.Threading;
using AIMLGUI;

namespace XULIA
{
    public partial class DictadoWindowsMedia : Form
    {
        public string Idioma = "";
        bool Salida = false;
        public ProcesamientoComandos pc;
        private SpeechRecognizer speechRecognizer;
        private ResourceMap speechResourceMap;
        public static string texto = "";

        private void DictadoWindowsMedia_Load(object sender, EventArgs e)
        {
            //ActivarReconocimiento();
        }
        public DictadoWindowsMedia()
        {
            InitializeComponent();
        }

        private void btnCopiar_Click(object sender, EventArgs e)
        {
            ActivarReconocimiento();
        }
#if VOZ_UWP

        public void ActivarReconocimiento()
        {
            tbSalida.BackColor = Color.LightSalmon;
            tbSalida.Refresh();
            CompilarGramatica();
            Thread.Sleep(500);
            ActivarEventos();
            ActivarReconocedor();
            tbSalida.BackColor = Color.White;
            tbSalida.Refresh();
        }
        public string CopiarTextoUWP()
        {
            string texto = tbSalida.Text;
            tbSalida.Text = "";
            return texto;
        }
        public void CerrarVentanaUWP()
        {
            try
            {
                this.Hide();
                this.tmrSalida.Enabled = false;

                try
                {
                    speechRecognizer.ContinuousRecognitionSession.CancelAsync();
                    speechRecognizer.ContinuousRecognitionSession.StopAsync();
                    Thread.Sleep(1000);
                    CerrarReconocedor();
                }
                catch (Exception ex)
                {
                }

            }
            catch (Exception ex)
            {

            }
        }
        public void EnviarTexto(string texto)
        {
            tbSalida.AppendText(" " + texto);
        }

        private async Task<bool> CompilarGramatica()
        {
            this.speechRecognizer = new SpeechRecognizer(SpeechRecognizer.SystemSpeechLanguage);

            // Provide feedback to the user about the state of the recognizer. This can be used to provide visual feedback in the form
            // of an audio indicator to help the user understand whether they're being heard.
            speechRecognizer.StateChanged += SpeechRecognizer_StateChanged;

            // Build a command-list grammar. Commands should ideally be drawn from a resource file for localization, and 
            // be grouped into tags for alternate forms of the same command.
            var dictationConstraint = new SpeechRecognitionTopicConstraint(SpeechRecognitionScenario.Dictation, "dictation");
            speechRecognizer.Constraints.Add(dictationConstraint);

            Object result = speechRecognizer.CompileConstraintsAsync();

            return true;
        }
        private void ActivarEventos()
        {
            speechRecognizer.ContinuousRecognitionSession.Completed += ContinuousRecognitionSession_Completed;
            speechRecognizer.ContinuousRecognitionSession.ResultGenerated += ContinuousRecognitionSession_ResultGenerated;
            speechRecognizer.StateChanged += SpeechRecognizer_StateChanged;
        }
        public void CerrarReconocedor()
        {
            speechRecognizer.ContinuousRecognitionSession.Completed -= ContinuousRecognitionSession_Completed;
            speechRecognizer.ContinuousRecognitionSession.ResultGenerated -= ContinuousRecognitionSession_ResultGenerated;
            speechRecognizer.StateChanged -= SpeechRecognizer_StateChanged;
            speechRecognizer.Dispose();
        }
        private void ActivarReconocedor()
        {

            if (speechRecognizer.State == SpeechRecognizerState.Idle)
            {
                try
                {
                    speechRecognizer.ContinuousRecognitionSession.StartAsync();

                    //speechRecognizer.RecognizeWithUIAsync();

                }
                catch (Exception ex)
                {
                }
            }
        }
        private async void SpeechRecognizer_StateChanged(SpeechRecognizer sender, SpeechRecognizerStateChangedEventArgs args)
        {
        }
        private async void ContinuousRecognitionSession_Completed(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionCompletedEventArgs args)
        {
            if (args.Status != SpeechRecognitionResultStatus.Success)
            {
                //Cuando finalice el reconocedor lo iniciamos de nuevo
                if (!Salida)
                    speechRecognizer.ContinuousRecognitionSession.StartAsync();
            }
        }
        public void ReactivarReconocedor()
        {
            this.Show();
            try
            {
                if (speechRecognizer.State == SpeechRecognizerState.Idle)
                {
                    speechRecognizer.ContinuousRecognitionSession.CancelAsync();
                    //Thread.Sleep(1000);
                    //speechRecognizer.ContinuousRecognitionSession.StartAsync();
                }
            }
            catch (Exception ex)
            {
            }
        }
        private async void ContinuousRecognitionSession_ResultGenerated(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionResultGeneratedEventArgs args)
        {
            // The garbage rule will not have a tag associated with it, the other rules will return a string matching the tag provided
            // when generating the grammar.
            string tag = "unknown";
            if (args.Result.Constraint != null)
            {
                tag = args.Result.Constraint.Tag;
            }

            // Developers may decide to use per-phrase confidence levels in order to tune the behavior of their 
            // grammar based on testing.
            if (args.Result.Confidence == SpeechRecognitionConfidence.Medium ||
                args.Result.Confidence == SpeechRecognitionConfidence.High)
            {
                texto = args.Result.Text;
            }
        }

        private void tmrSalida_Tick(object sender, EventArgs e)
        {
            if (texto != "")
            {
                tbSalidaDirecta.Text = texto;
                tbSalidaDirecta.Refresh();
                string tmp = texto;
                texto = "";
                pc.TextoReconocido(tmp, (float)0.9999, true);
            }
        }

        private void tmrOkXulia_Tick(object sender, EventArgs e)
        {
        }

#endif
    }
}

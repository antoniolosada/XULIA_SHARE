using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Speech.Recognition;


namespace REcVoz
{

        public class Program
        {
            //static void Main(string[] args)

            static String salida = "";

            public String LeerSalida()
            {
                return salida;
            }
            public void IniciarRecVoz()
            {
                // Create an in-process speech recognizer for the en-US locale.
                SpeechRecognitionEngine recognizer =
                  new SpeechRecognitionEngine(
                    new System.Globalization.CultureInfo("es-ES"));
                    //new System.Globalization.CultureInfo("en-US")))

                    // Create and load a dictation grammar.

                    //recognizer.LoadGrammar(new DictationGrammar());

                    GrammarBuilder grammar = new GrammarBuilder();
                    grammar.Culture = new System.Globalization.CultureInfo("es-ES");
                    grammar.Append(new Choices("Alfa", "Bravo", "Charlie"));
                    recognizer.UnloadAllGrammars();
                    recognizer.LoadGrammar(new Grammar(grammar));


                    // Add a handler for the speech recognized event.
                    recognizer.SpeechRecognized +=
                      new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

                    // Configure input to the speech recognizer.
                    recognizer.SetInputToDefaultAudioDevice();

                    // Start asynchronous, continuous speech recognition.
                    recognizer.RecognizeAsync(RecognizeMode.Multiple);

            }

            // Handle the SpeechRecognized event.
            static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
            {
                salida = e.Result.Text;
            }

        }
}

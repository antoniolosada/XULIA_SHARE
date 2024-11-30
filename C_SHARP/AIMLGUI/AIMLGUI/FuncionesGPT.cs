using AIMLGUI;
using OllamaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XULIA
{
    public class FuncionesGPT
    {
        #region GPT
        public frmGPT fGPT = new frmGPT();
        CorreoOffice mailOffice = new CorreoOffice();
        Chat chatGPT;
        ProcesamientoComandos procesamientoComandos;

        public FuncionesGPT(ProcesamientoComandos pc)
        {
            procesamientoComandos = pc;
        }

        void DescomponerTokens(string texto)
        {
            // Patrón regex para dividir en palabras pero mantener las cadenas entre comillas como un solo token
            string patron = "\"[^\"]*\"|[:{}]|\\S+";
            texto = Regex.Replace(texto, "[{},:]", "");
            // Lista para almacenar los tokens
            List<string> tokens = new List<string>();

            // Buscar coincidencias
            foreach (Match match in Regex.Matches(texto, patron))
            {
                if (match.Value != "\"parameters\"")
                    tokens.Add(match.Value);
            }
            string nombre = "";
            // Imprimir los tokens
            foreach (string token in tokens)
            {
                string par = token.Replace("\"", "");
                if (nombre == "")
                    nombre = par;
                else
                {
                    try
                    {
                        parametros.Add(nombre, par);
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); };
                    nombre = "";
                }
            }
        }
        Dictionary<string, string> parametros;


        // EDIT: funciones
        public void LlamarFuncion(string texto)
        {
            parametros = new Dictionary<string, string>();
            DescomponerTokens(texto);

            string funcion = parametros["name"];

            switch (funcion)
            {
                case "leer_cabecera_mensajes_correo":
                    string antiguedad;
                    string emisor = "";

                    fGPT.EjecFuncion(true);
                    parametros.TryGetValue("n_dias_antiguedad", out antiguedad);
                    parametros.TryGetValue("s_emisor", out emisor);
                    ProcesamientoComandos.sDireccion lemisor = procesamientoComandos.DireccionesDestino.Find(x => x.comando == emisor);
                    if (lemisor.comando != null) emisor = lemisor.direccion.ToString();
                    if (emisor == null) emisor = "";
                    string Mensajes = mailOffice.LeerBandejaEntada(int.Parse(antiguedad), emisor);
                    fGPT.Pregunta(Mensajes);
                    fGPT.EjecFuncion(false);
                    break;
            }
        }
        public void LlamarFuncionOld(string texto)
        {
            Token("{", ref texto);
            LeerParametro(ref texto);
            Token("\"parameters\"", ref texto);
            Token("{", ref texto);
            while (LeerParametro(ref texto)) ;
            Token("}", ref texto);
            //Token("}", ref texto);

        }
        bool LeerParametro(ref string texto)
        {
            string nombre = "";
            string valor = "";

            if (texto.Trim().Substring(0, 1) == "}")
            {
                texto = texto.Trim();
                return false;
            }
            LeerCadena(ref nombre, ref texto);
            if (nombre == "name")
                LeerCadena(ref valor, ref texto);
            else
            {
                switch (nombre[0])
                {
                    case 's':
                        LeerCadena(ref valor, ref texto);
                        break;
                    case 'n':
                        LeerNumero(ref valor, ref texto);
                        break;
                }
            }
            parametros.Add(nombre, valor);

            return true;
        }
        void LeerNumero(ref string valor, ref string texto)
        {
            int lon = 0;
            List<char> validos = new List<char> { ' ', '+', '-', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            char[] caracteres = texto.ToCharArray();
            foreach (char c in caracteres)
            {
                if ((c == '\"') || (c == '}') || (c == '\r'))
                    break;
                if (!validos.Contains(c))
                    throw new Exception("Caracter no válido:" + c);
                else
                    lon++;
            }
            valor = texto.Substring(0, lon).Trim();
            texto = texto.Substring(lon);
        }
        void LeerCadena(ref string valor, ref string texto)
        {
            Token("\"", ref texto);
            int pos = texto.IndexOf("\"");
            if (pos > -1)
            {
                valor = texto.Substring(0, pos);
                texto = texto.Substring(pos);
            }
            else throw new Exception("Cadena no encontrada");
            Token("\"", ref texto);
        }
        public void Token(string token, ref string texto)
        {
            texto = texto.Trim();
            if (texto.Substring(0, token.Length) != token)
                throw new Exception("Token " + token + " no encontrado.");
            texto = texto.Substring(token.Length);
        }
        async Task<List<string>> GPT(string texto)
        {
            return await chatGPT.SendAsEnumerable("hola", null, null, default);
        }
        public async Task<string> GPT(string texto, OllamaSharp.Chat.RespuestaGPT RespuestaChatGPT)
        {
            // se llama al delegado RespuestaChatGPT por cada frase de la respuesta
            await chatGPT.SendAsEnumerableDelegado(texto, RespuestaChatGPT, null, null, default);
            return "";
        }

        async public void IniciarOllama(string url)
        {
            OllamaApiClient ollama = null;
            var connected = false;

            try
            {
                if (string.IsNullOrWhiteSpace(url))
                    url = "http://localhost:11434";

                if (!url.StartsWith("http"))
                    url = "http://" + url;

                if (url.IndexOf(':', 5) < 0)
                    url += ":11434";

                var uri = new Uri(url);

                ollama = new OllamaApiClient(url);
                connected = await ollama.IsRunning();

                var models = await ollama.ListLocalModels();
                if (!models.Any())
                {
                    MessageBox.Show("Ollama no tiene modelos cargados.Se desactiva el GPT.");
                    procesamientoComandos.GPT_API = "";
                    return;
                }
                ollama.SelectedModel = procesamientoComandos.GPT_Modelo;
                chatGPT = new Chat(ollama, procesamientoComandos.GPT_OllamaPrompt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        #endregion    
    }
    }

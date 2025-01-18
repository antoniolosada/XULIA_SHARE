using AIMLGUI;
using OllamaSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XULIA
{
    public class FuncionesGPT
    {
        public frmGPT fGPT = new frmGPT();
        CorreoOffice mailOffice = new CorreoOffice();
        Chat chatGPT;
        ProcesamientoComandos procesamientoComandos;
        Dictionary<string, string> parametros;

        public FuncionesGPT(ProcesamientoComandos pc)
        {
            procesamientoComandos = pc;
        }
        // EDIT: funciones
        public string LlamarFuncion(string texto)
        {
            try 
            {
                parametros = new Dictionary<string, string>();
                DescomponerTokens(texto);

                string funcion = parametros["name"];

                switch (funcion)
                {
                    case "leer_cabecera_mensajes_correo":
                        {
                            string antiguedad;
                            string emisor = "";

                            fGPT.EjecFuncion(true);
                            parametros.TryGetValue("n_dias_antiguedad", out antiguedad);
                            parametros.TryGetValue("s_emisor", out emisor);
                            ProcesamientoComandos.sDireccion lemisor = procesamientoComandos.DireccionesDestino.Find(x => x.comando == emisor);
                            if (lemisor.comando != null) emisor = lemisor.direccion.ToString();
                            if (emisor == null) emisor = "";
                            string Mensajes = mailOffice.LeerBandejaEntada(int.Parse(antiguedad), emisor);
                            fGPT.Pregunta(Mensajes+Environment.NewLine+ "Cuenta los correos electrónicos.");
                            fGPT.EjecFuncion(false);
                            break;
                        }
                    case "lee_solicitud_moura":
                        {
                            string codigo = "";
                            parametros.TryGetValue("s_codigo", out codigo);
                            if ((codigo == "") || (codigo == null) || (!int.TryParse(codigo, out _)))
                                codigo = Clipboard.GetText();
                            if (int.TryParse(codigo, out _))
                                procesamientoComandos.webdriver.AbrirMoura(SeleniumWeb.eFuncionesMoura.ConsultarSolicitudGestion, procesamientoComandos.UsuarioRCP,
                                                                            procesamientoComandos.ClaveRCP, procesamientoComandos.GrupoMouraGestion, codigo);
                            else
                                ErrorGPT("ERROR: Código de Moura no válido");
                            break;
                        }
                    case "leer_cuadro_morfeo":
                        {
                            procesamientoComandos.webdriver.AbrirMorfeo(SeleniumWeb.eFuncionesMorfeo.ConsultaCuadro, procesamientoComandos.UsuarioDA, procesamientoComandos.ClaveDA);
                            break;
                        }
                    case "valida_permisos_morfeo":
                        {
                            procesamientoComandos.webdriver.AbrirMorfeo(SeleniumWeb.eFuncionesMorfeo.ValidarPermisos, procesamientoComandos.UsuarioDA, procesamientoComandos.ClaveDA);
                            break;
                        }
                    case "consulta_personal_matrix":
                    case "consulta_permisos_matrix":
                    case "informes_almacenados_matrix":
                        {
                            LlamadasMatrhix(funcion);
                            break;
                        }
                    case "consulta_experiencia_fides":
                    case "consulta_formacion_academica_fides":
                    case "consulta_idiomas_fides":
                    case "consulta_solicitudes_listas_fides":
                    case "consulta_solicitudes_carrera_fides":
                    case "consulta_solicitudes_ps_fides":
                    case "consulta_baremo_fides":
                    case "consulta_expediente_fides":
                        {
                            LlamadasFIDES(funcion);
                            break;
                        }
                    case "recuperar_dni_portapapeles":
                        {
                            string dni = RecuperarDniPortapapeles();
                            if (dni == "")
                                return ProcesamientoComandos.GPT_msg_ErrorNoDNI;
                            else
                                return ProcesamientoComandos.GPT_msg_SalidaDNI + " " + dni;
                        }
                    case "mostrar_recuerdos":
                        {
                            procesamientoComandos.fRecuerdos.LeerRecuerdos();
                            break;
                        }
                    case "recuerda_que":
                        {
                            Recuerda();
                            break;
                        }
                }
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message);
            }
            return "";
        }
        #region FuncionesGPT ---------------------------------------------------------------------------------------------------------------
        void LlamadasFIDES(string op)
        {
            string nif = "";
            if (!RecuperarNIF(ref nif))
            {
                MessageBox.Show("NIF no válido");
                return;
            }
            nif = nif.PadLeft(14, '0');
            SeleniumWeb.eFuncionesFides funcion;
            switch (op)
            {
                case "consulta_experiencia_fides":
                    funcion = SeleniumWeb.eFuncionesFides.Experiencia;
                    break;
                case "consulta_formacion_academica_fides":
                    funcion = SeleniumWeb.eFuncionesFides.FormacionAcademica;
                    break;
                case "consulta_idiomas_fides":
                    funcion = SeleniumWeb.eFuncionesFides.Idiomas;
                    break;
                case "consulta_solicitudes_listas_fides":
                    funcion = SeleniumWeb.eFuncionesFides.InscripcionListas;
                    break;
                case "consulta_solicitudes_carrera_fides":
                    funcion = SeleniumWeb.eFuncionesFides.InscripcionCarrera;
                    break;
                case "consulta_solicitudes_ps_fides":
                    funcion = SeleniumWeb.eFuncionesFides.InscripcionPS;
                    break;
                case "consulta_baremo_fides":
                    funcion = SeleniumWeb.eFuncionesFides.Baremo;
                    break;
                case "consulta_expediente_fides":
                    funcion = SeleniumWeb.eFuncionesFides.Expediente;
                    break;
                default:
                    funcion = SeleniumWeb.eFuncionesFides.Expediente;
                    break;
            }

            procesamientoComandos.webdriver.AbrirFIDES(funcion, procesamientoComandos.UsuarioDA, procesamientoComandos.ClaveDA, nif);
        }
        void LlamadasMatrhix(string op)
        {
            string nif = "";
            SeleniumWeb.eFuncionesMatrhix funcion;

            if (op != "informes_almacenados_matrix")
            {
                if (!RecuperarNIF(ref nif))
                {
                    ErrorGPT("NIF no válido");
                    return;
                }
            }
            switch (op)
            {
                case "consulta_personal_matrix":
                    funcion = SeleniumWeb.eFuncionesMatrhix.ConsultaPersonal;
                    break;
                case "consulta_permisos_matrix":
                    funcion = SeleniumWeb.eFuncionesMatrhix.ConsultaPermisos;
                    break;
                case "informes_almacenados_matrix":
                    funcion = SeleniumWeb.eFuncionesMatrhix.InformesAlmacenados;
                    break;
                default:
                    funcion = SeleniumWeb.eFuncionesMatrhix.ConsultaPersonal;
                    break;
            }

            procesamientoComandos.webdriver.AbrirMatrhix(funcion, procesamientoComandos.UsuarioRCP, procesamientoComandos.ClaveRCP, nif, "");
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

        string RecuperarDniPortapapeles()
        {
            string dni = Clipboard.GetText();

            if (!EsNifValido(dni))
                dni = CalcularLetraDNI(dni);

            if (dni == "")
            {
                ErrorGPT(ProcesamientoComandos.GPT_msg_ErrorNoDNI);
                return "";
            }
            else
                return dni;
        }
        public void Recuerda()
        {
            string recuerdo = "";
            parametros.TryGetValue("s_recuerdo", out recuerdo);
            if (recuerdo == "")
            {
                recuerdo = Clipboard.GetText();
            }
            if (recuerdo != "")
                procesamientoComandos.fRecuerdos.Recuerda(recuerdo);
        }
        #endregion FuncionesGPT ----------------------------------------------------------------------------------------------------------- 

        #region Auxiliares ----------------------------------------------------------------------------------------------------------------

        public string CalcularLetraDNI(string dni)
        {
            string letras = "TRWAGMYFPDXBNJZSQVHLCKE";
            if (dni.Length == 8 && int.TryParse(dni, out int dniNum)) // Comprueba si el DNI tiene 8 dígitos
            {
                char letra = letras[dniNum % 23]; // Calcula la letra correspondiente
                return dni + letra; // Devuelve el DNI completo con letra
            }
            else if (dni.Length == 9 && int.TryParse(dni.Substring(0, 8), out int _) && char.IsLetter(dni[8]))
            {
                return dni; // Si ya tiene 8 dígitos y una letra al final, devuelve el DNI tal cual
            }
            else
                return ""; // Devuelve un mensaje de error si el formato es incorrecto
        }
        public void ErrorGPT(string error)
        {
            MessageBox.Show(error);
        }
        static bool EsNifValido(string nif)
        {
            // Comprobar longitud
            if (nif.Length != 9)
            {
                return false;
            }

            // Comprobar formato: 8 dígitos seguidos de una letra
            Regex regex = new Regex(@"^\d{8}[A-Z]$");
            if (!regex.IsMatch(nif))
            {
                return false;
            }

            // Obtener los dígitos y la letra del NIF
            string numero = nif.Substring(0, 8);
            char letra = nif[8];

            // Calcular la letra correcta
            char letraCalculada = CalcularLetraNif(numero);

            // Comprobar si la letra proporcionada es la correcta
            return letra == letraCalculada;
        }

        static char CalcularLetraNif(string numero)
        {
            string letras = "TRWAGMYFPDXBNJZSQVHLCKE";
            int resto = int.Parse(numero) % 23;
            return letras[resto];
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
        bool RecuperarNIF(ref string nif)
        {
            parametros.TryGetValue("s_nif", out nif);

            if (!EsNifValido(nif))
                nif = CalcularLetraDNI(nif);

            if (nif == "")
            {
                nif = RecuperarDniPortapapeles();
                if (nif == "")
                    return false;
            }
            return true;
        }
        #endregion Auxiliares ----------------------------------------------------------------------------------------------------------------

    }

}


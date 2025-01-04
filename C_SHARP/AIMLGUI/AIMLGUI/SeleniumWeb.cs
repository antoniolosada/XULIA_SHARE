using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using OpenQA.Selenium.DevTools.V129.Debugger;


namespace XULIA
{
    public class SeleniumWeb
    {
        IWebDriver driver;
        public enum eFuncionesFides
        {
            FormacionAcademica,
            Idiomas,
            Experiencia,
            InscripcionListas,
            InscripcionCarrera,
            InscripcionPS,
            Baremo
        };
        public enum eFuncionesMatrhix
        {
            ConsultaPersonal,
            HistoricoPersonal,
            ConsultaPuesto,
            ConsultaPermisos,
            InformesAlmacenados
        };
        public enum eFuncionesMorfeo
        {
            ConsultaCuadro,
            ValidarPermisos
        };
        public enum eFuncionesMoura
        {
            ConsultarSolicitudGestion,
            ConsultaSolicitudesGeneracion
        };
        public enum eFuncionesTrasno
        {
            ConsultaExpedientes,
            ConsultarExpedienteIndividual
        };
        public enum eCombo
        {
            Convocatoria,
            Categoria
        };
        public SeleniumWeb()
        {
        }
        static char CalcularLetraNIF(int dniNumber)
        {
            string letters = "TRWAGMYFPDXBNJZSQVHLCKE";
            int remainder = dniNumber % 23;
            return letters[remainder];
        }
        static bool IsLastCharLetter(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;  // Maneja el caso de cadena nula o vacía
            }

            char lastChar = str[str.Length - 1];
            return Char.IsLetter(lastChar);
        }
        static string ExtractNumbers(string input)
        {
            // Definir la expresión regular para encontrar números
            Regex regex = new Regex(@"\d+");

            // Buscar todas las coincidencias de números en la cadena
            MatchCollection matches = regex.Matches(input);

            Match firstMatch = matches[0];
            string result = firstMatch.Value;

            return result;
        }
        List<string> RecuperarElementoLista(EdgeDriver driver, eCombo combo)
        {
            EsperarCargaPagina("Listas Xeradas");

            string sCombo ="";
            // Localizar la lista desplegable por su ID
            switch (combo)
            {
                case eCombo.Convocatoria:
                    sCombo = "id_convocatoria";
                    break;
                case eCombo.Categoria:
                    sCombo = "filtroListados.id_categoria";
                    break;

            }
            IWebElement sSel = driver.FindElement(By.Id(sCombo));

            // Crear un objeto SelectElement para interactuar con la lista desplegable
            SelectElement seleccion = new SelectElement(sSel);

            // Obtener todos los elementos de la lista
            IList<IWebElement> options = seleccion.Options;

            List<string> opciones = new List<string>();
            // Iterar sobre los elementos y mostrar su texto
            foreach (IWebElement option in options)
                opciones.Add(option.Text);

            return opciones;
        }
        static List<string> BuscarListaCadenas(List<string> listaCadenas, string[] palabrasABuscar)
        {
            //string[] palabrasABuscar = { "palabras", "en", "orden" };

            var resultados = BuscarCadenas(listaCadenas, palabrasABuscar);

            List<string> lresultado = new List<string>();    
            foreach (var resultado in resultados)
                lresultado.Add(resultado.ToString());   

            return lresultado;
        }

        static IEnumerable<string> BuscarCadenas(List<string> lista, string[] palabras)
        {
            return lista.Where(cadena => ContienePalabrasEnOrden(cadena, palabras));
        }

        static bool ContienePalabrasEnOrden(string cadena, string[] palabras)
        {
            int index = 0;

            foreach (var palabra in palabras)
            {
                index = cadena.IndexOf(palabra, index);
                if (index == -1)
                {
                    return false;
                }
                index += palabra.Length;
            }

            return true;
        }

        public void AbrirFIDES(eFuncionesFides Funcion, string usuario, string usrclave, string nif)
        {
            if (!IsLastCharLetter(nif))
            {
                nif = ExtractNumbers(nif);
                string lnif = CalcularLetraNIF(int.Parse(nif)).ToString();
                nif = nif + lnif;
                nif = nif.PadLeft(14, '0');
            }
            else
                nif = nif.PadLeft(14, '0');

            AbrirNavegadorSelenium(ref driver, "https://cas.xunta.local/cas/login?service=https%3A%2F%2Ffides.xunta.gal%2Ffides%2Findex.jsp");

            EsperarCargaPagina("Sistema para a xestión do Expedient-e");
            var login = driver.FindElement(By.Name("username"));
            var clave = driver.FindElement(By.Name("password"));
            var btsubmit = driver.FindElement(By.Id("submit"));

            clave.SendKeys(usrclave);
            login.SendKeys(usuario);
            Thread.Sleep(500);
            btsubmit.Click();

            EsperarCargaPagina("Validador/a");
            if (Funcion == eFuncionesFides.Baremo)
            {
                driver.Navigate().GoToUrl("https://fides.xunta.gal/fides/html/private/PerfilAdministradorAction.action?idTipoPerfil=6");
                EsperarCargaPagina("ADMINISTRADOR");
                driver.Navigate().GoToUrl("https://fides.xunta.gal/fides/html/private/BaremacionMenuAction!accesoMenuSuperior.action?idElementoMenu=133&amp;tipElemPerfil=1");
                EsperarCargaPagina("Xestión Listados");
            }
            else
            {
                driver.Navigate().GoToUrl("https://fides.xunta.gal/fides/html/private/PerfilValidadorAction.action?idTipoPerfil=3");
                EsperarCargaPagina("Buscas de persoas");
                driver.Navigate().GoToUrl("https://fides.xunta.gal/fides/html/private/BuscarProfesionalAction.action?profesional.ipf=" + nif + "&profesional.tipoPerfil=0");
                EsperarCargaPagina("Datos Persoais");
            }

            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
            switch (Funcion)
            {
                case eFuncionesFides.Baremo:
                    {
                        driver.Navigate().GoToUrl("https://fides.xunta.gal/fides/html/private/DistribuidorPlantillaAction!accesoMenu.action?idElemento=134");
                        EsperarCargaPagina("Xestión Listas");
                        List<string> elementos = RecuperarElementoLista((EdgeDriver)driver, eCombo.Convocatoria);
                        string[] lista = { "A1", "Concurso" };
                        elementos = BuscarListaCadenas(elementos, lista);
                        IWebElement sel_convocatoria = driver.FindElement(By.Id("id_convocatoria"));
                        SelectElement sConv = new SelectElement(sel_convocatoria);
                        sConv.SelectByText(elementos[0]);

                        elementos = RecuperarElementoLista((EdgeDriver)driver, eCombo.Categoria);
                        string[] lista_cat = {"MINAS" };
                        elementos = BuscarListaCadenas(elementos, lista_cat);
                        sel_convocatoria = driver.FindElement(By.Id("filtroListados.id_categoria"));
                        SelectElement sCat = new SelectElement(sel_convocatoria);
                        sCat.SelectByText(elementos[0]);

                        btsubmit = driver.FindElement(By.Id("submit"));
                        btsubmit.Click();

                        break;
                    }
                case eFuncionesFides.InscripcionPS:
                    {
                        driver.Navigate().GoToUrl("https://fides.xunta.gal/fides/html/private/ProcesosMenuAction!accesoMenuSuperior.action?idElementoMenu=5&tipElemPerfil=2");
                        EsperarCargaPagina("Solicitude Carreira Administrativa");
                        driver.Navigate().GoToUrl("https://fides.xunta.gal/fides/html/private/DistribuidorPlantillaAction!accesoMenu.action?idElemento=137");
                        break;
                    }
                case eFuncionesFides.InscripcionListas:
                    {
                        driver.Navigate().GoToUrl("https://fides.xunta.gal/fides/html/private/ProcesosMenuAction!accesoMenuSuperior.action?idElementoMenu=5&tipElemPerfil=2");
                        EsperarCargaPagina("Solicitude Carreira Administrativa");
                        driver.Navigate().GoToUrl("https://fides.xunta.gal/fides/html/private/DistribuidorPlantillaAction!accesoMenu.action?idElemento=138");
                        break;
                    }
                case eFuncionesFides.InscripcionCarrera:
                    {
                        driver.Navigate().GoToUrl("https://fides.xunta.gal/fides/html/private/ProcesosMenuAction!accesoMenuSuperior.action?idElementoMenu=5&tipElemPerfil=2");
                        EsperarCargaPagina("Solicitude Carreira Administrativa");
                        driver.Navigate().GoToUrl("https://fides.xunta.gal/fides/html/private/DistribuidorPlantillaAction!accesoMenu.action?idElemento=190");
                        break;
                    }
                case eFuncionesFides.Idiomas:
                    {
                        driver.Navigate().GoToUrl("https://fides.xunta.gal/fides/html/private/DistribuidorPlantillaAction!accesoMenu.action?idElemento=31");
                        EsperarCargaPagina("Nivel de Idiomas");
                        //Formación académica
                        ((IJavaScriptExecutor)driver).ExecuteScript("window.open(\"https://fides.xunta.gal/fides/html/private/DistribuidorPlantillaAction!accesoMenu.action?idElemento=43\", \"_blank\")");
                        //Formación continuada
                        ((IJavaScriptExecutor)driver).ExecuteScript("window.open(\"https://fides.xunta.gal/fides/html/private/DistribuidorPlantillaAction!accesoMenu.action?idElemento=244\", \"_blank\")");
                        //Formación continuada
                        ((IJavaScriptExecutor)driver).ExecuteScript("window.open(\"https://fides.xunta.gal/fides/html/private/DistribuidorPlantillaAction!accesoMenu.action?idElemento=328\", \"_blank\")");
                        break;
                    }
                case eFuncionesFides.FormacionAcademica:
                    {
                        //Formación
                        driver.Navigate().GoToUrl("https://fides.xunta.gal/fides/html/private/DistribuidorPlantillaAction!accesoMenu.action?idElemento=4");
                        EsperarCargaPagina("Formación Continuada");
                        //Formación académica
                        ((IJavaScriptExecutor)driver).ExecuteScript("window.open(\"https://fides.xunta.gal/fides/html/private/DistribuidorPlantillaAction!accesoMenu.action?idElemento=52\", \"_blank\")");
                        //Formación continuada
                        ((IJavaScriptExecutor)driver).ExecuteScript("window.open(\"https://fides.xunta.gal/fides/html/private/DistribuidorPlantillaAction!accesoMenu.action?idElemento=53\", \"_blank\")");
                        break;
                    }
                case eFuncionesFides.Experiencia:
                    {
                        //Experiencia profesional
                        driver.Navigate().GoToUrl("https://fides.xunta.gal/fides/html/private/DistribuidorPlantillaAction!accesoMenu.action?idElemento=25");
                        EsperarCargaPagina("Experiencia Interna");
                        //Experiencia interna
                        ((IJavaScriptExecutor)driver).ExecuteScript("window.open(\"https://fides.xunta.gal/fides/html/private/DistribuidorPlantillaAction!accesoMenu.action?idElemento=70\", \"_blank\")");
                        //Experiencia externa
                        ((IJavaScriptExecutor)driver).ExecuteScript("window.open(\"https://fides.xunta.gal/fides/html/private/DistribuidorPlantillaAction!accesoMenu.action?idElemento=71\", \"_blank\")");
                        //Inactividades
                        ((IJavaScriptExecutor)driver).ExecuteScript("window.open(\"https://fides.xunta.gal/fides/html/private/DistribuidorPlantillaAction!accesoMenu.action?idElemento=325\", \"_blank\")");
                        break;
                    }
            }
        }
        public void AbrirMatrhix(eFuncionesMatrhix Funcion, string usuario, string usrclave, string nif, string puesto)
        {
            // Navegar a la página web
            AbrirNavegadorSelenium(ref driver, "https://matrhix.xunta.es/rcp/NUC/LoginForm.do");

            var clave = driver.FindElement(By.Name("claveAcceso")); 
            var login = driver.FindElement(By.Name("idLogin")); 
            var btsubmit = driver.FindElement(By.Name("login"));

            clave.Clear();
            clave.SendKeys(usrclave);
            login.Clear();
            login.SendKeys(usuario);
            btsubmit.Click();

            switch (Funcion)
            {
                case eFuncionesMatrhix.ConsultaPersonal:
                    {
                        driver.Navigate().GoToUrl("https://matrhix.xunta.es/rcp/NUC/PersonaConsulta0.do");
                        EsperarCargaPagina("O sistema enviarame un PIN");

                        var nifextranjero = driver.FindElement(By.Name("nifExtranjero"));
                        var dni = driver.FindElement(By.Name("dni"));
                        var letra = driver.FindElement(By.Name("letraNif"));
                        btsubmit = driver.FindElement(By.Name("login"));

                        string sdni = ExtractNumbers(nif);
                        sdni = sdni.PadLeft(8, '0');
                        dni.SendKeys(sdni);
                        Thread.Sleep(2000);
                        btsubmit.Click();

                        EsperarCargaPagina("Consulta de Persoal");

                        var button = driver.FindElement(By.XPath("//input[@value='Histórico']"));
                        button.Click();
                        break;
                    }
                case eFuncionesMatrhix.ConsultaPermisos:
                    {
                        driver.Navigate().GoToUrl("https://matrhix.xunta.es/rcp/NUC/PermisosCons0.do");
                        EsperarCargaPagina("O sistema enviarame un PIN");

                        var nifextranjero = driver.FindElement(By.Name("nifExtranjero"));
                        var dni = driver.FindElement(By.Name("dni"));
                        var letra = driver.FindElement(By.Name("letraNif"));
                        btsubmit = driver.FindElement(By.Name("login"));

                        string sdni = ExtractNumbers(nif);
                        sdni = sdni.PadLeft(8, '0');
                        dni.SendKeys(sdni);
                        Thread.Sleep(2000);
                        btsubmit.Click();

                        EsperarCargaPagina("Datos Persoais e Administrativos");

                        var btCalendario = driver.FindElement(By.Name("boton_fec_fin"));
                        btCalendario.Click();
                        break;
                    }
                case eFuncionesMatrhix.ConsultaPuesto:
                    {
                        driver.Navigate().GoToUrl("https://matrhix.xunta.es/rcp/NUC/PuestoCon0.do");
                        EsperarCargaPagina("O sistema enviarame un PIN");

                        var conselleria = driver.FindElement(By.Name("conselleria"));
                        var centroDirectivo = driver.FindElement(By.Name("centroDirectivo"));
                        var campo3 = driver.FindElement(By.Name("campo3"));
                        var campo4 = driver.FindElement(By.Name("campo4"));
                        var municipio = driver.FindElement(By.Name("municipio"));
                        var numeroPuesto = driver.FindElement(By.Name("numeroPuesto"));
                        var btlogin = driver.FindElement(By.Name("login"));

                        if (puesto.Length == 18)
                        {
                            conselleria.SendKeys(puesto.Substring(0, 2));
                            centroDirectivo.SendKeys(puesto.Substring(2, 3));
                            campo3.SendKeys(puesto.Substring(5, 2));
                            campo4.SendKeys(puesto.Substring(7, 3));
                            municipio.SendKeys(puesto.Substring(10, 5));
                            numeroPuesto.SendKeys(puesto.Substring(15, 3));
                        }

                        btlogin.Click();

                        break;
                    }
                case eFuncionesMatrhix.InformesAlmacenados:
                    {
                        driver.Navigate().GoToUrl("https://matrhix.xunta.es/rcp/NUC/InformesAlmacenados.do");
                        EsperarCargaPagina("O sistema enviarame un PIN");

                        string texto = "";
                        if (Clipboard.ContainsText())
                        {
                            // Recuperar el texto del portapapeles
                            texto = Clipboard.GetText();
                            Console.WriteLine($"Texto recuperado del portapapeles: {texto}");
                        }
                        else
                        {
                            Console.WriteLine("El portapapeles no contiene texto.");
                        }

                        // Usar Split para dividir la cadena por saltos de línea
                        string[] lineas = texto.Split(new[] { "\r\n" }, StringSplitOptions.None);

                        if (lineas[1] == "$RUN")
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("recuperaParametros('" + lineas[1] + "' , '" + lineas[1] + "', 'NNNNNNNNNN');");
                            // Mostrar cada línea por separado
                            for (int i = 2; i < lineas.Length; i++)
                            {
                                var p = driver.FindElement(By.Id("valor_parametro_" + i.ToString()));
                                p.Click();
                                var pv = driver.FindElement(By.Id("valor_param"));
                                pv.SendKeys(lineas[i]);
                            }
                            var btejec = driver.FindElement(By.Name("enviar"));
                            btejec.Click();
                        }
                        else
                        { 
                        }

                        break;
                    }
            }


            // Realizar alguna acción (opcional)
            Console.WriteLine("Título de la página: " + driver.Title);
        }
        public void AbrirMorfeo(eFuncionesMorfeo Funcion, string usuario, string usrclave)
        {
            AbrirNavegadorSelenium(ref driver, "https://morfeo.xunta.es/rcp/FAFPSOLVAC/");

            var clave = driver.FindElement(By.Name("claveAcceso"));
            var login = driver.FindElement(By.Name("idLogin"));
            var btsubmit = driver.FindElement(By.Name("submit"));

            clave.Clear();
            clave.SendKeys(usrclave);
            login.Clear();
            login.SendKeys(usuario);
            btsubmit.Click();

            switch (Funcion)
            {
                case eFuncionesMorfeo.ConsultaCuadro:
                    {
                        var cuadro = driver.FindElement(By.Id("botonAccDirecCadroAusencias"));
                        cuadro.Click();
                        break;
                    }
                case eFuncionesMorfeo.ValidarPermisos:
                    {
                        var cuadro = driver.FindElement(By.Id("btnIrBuzonSolic"));
                        cuadro.Click();

                        var marcar_todo = driver.FindElement(By.Id("checkMarcaTodo"));
                        marcar_todo.Click();
                        break;
                    }
            }
        }
        public void AbrirMoura(eFuncionesMoura Funcion, string usuario, string usrclave, string Grupo, string codigo)
        {
            AbrirNavegadorSelenium(ref driver, "https://www.xunta.es/factv/Login.do?inicio=true");

            var clave = driver.FindElement(By.Name("pwd"));
            var login = driver.FindElement(By.Name("usuario"));

            clave.Clear();
            clave.SendKeys(usrclave);
            login.Clear();
            login.SendKeys(usuario);

            IWebElement boton = driver.FindElement(By.CssSelector("input[type='submit'][value='aceptar'].boton"));
            boton.Click();

            IWebElement sel_grupo = driver.FindElement(By.Name("grp"));
            SelectElement sGrupo = new SelectElement(sel_grupo);
            sGrupo.SelectByText(Grupo);

            boton = driver.FindElement(By.CssSelector("input[type='submit'][value='aceptar'].boton"));
            boton.Click();

            driver.Navigate().GoToUrl("https://www.xunta.es/factv/ConsultarSolicitude.do?id="+codigo.ToString());
        }

        void AbrirNavegadorSelenium(ref IWebDriver driver, string url)
        {
            var options = new EdgeOptions();
            options.AddArgument("--start-maximized");
            try
            {
                driver.Navigate().GoToUrl(url);
            }
            catch (Exception ex)
            {
                driver = new EdgeDriver(options);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                driver.Navigate().GoToUrl(url);
            }

        }

        void EsperarCargaPagina(string Elemento)
        {
            while (true)
            {
                try
                {
                    var element = driver.FindElement(By.XPath("//*[contains(text(), '"+Elemento+"')]"));
                    break;
                }
                catch
                {
                    Application.DoEvents();
                }
            }
        }
    }
}

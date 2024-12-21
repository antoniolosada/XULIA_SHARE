using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Edge;
using Microsoft.Extensions.Options;
using OpenQA.Selenium.Support.UI;
using System.Windows.Forms;
using System.Threading;

namespace XULIA
{
    class SeleniumWeb
    {
        IWebDriver driver;
        public SeleniumWeb() 
        {
        }
        public void AbrirNAvegador()
        {
            // Navegar a la página web
            var options = new EdgeOptions();
            options.AddArgument("--start-maximized");
            driver = new EdgeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            driver.Navigate().GoToUrl("https://cas.xunta.local/cas/login?service=https%3A%2F%2Ffides.xunta.gal%2Ffides%2Findex.jsp");
            Thread.Sleep(2000);
            var login = driver.FindElement(By.Name("username"));
            var clave = driver.FindElement(By.Name("password"));
            var btsubmit = driver.FindElement(By.Id("submit"));

            clave.SendKeys("DaniXulia1082.");
            login.SendKeys("alosgon");
            Thread.Sleep(1000);
            btsubmit.Click();
            Thread.Sleep(5000);

            driver.Navigate().GoToUrl("https://fides.xunta.gal/fides/html/private/PerfilValidadorAction.action?idTipoPerfil=3");
            Thread.Sleep(3000);
            driver.Navigate().GoToUrl("https://fides.xunta.gal/fides/html/private/BuscarProfesionalAction.action?profesional.ipf=0000034996197H&profesional.tipoPerfil=0");
            Thread.Sleep(3000);
            driver.Navigate().GoToUrl("https://fides.xunta.gal/fides/html/private/DistribuidorPlantillaAction!accesoMenu.action?idElemento=25");
            Thread.Sleep(3000);
            ((IJavaScriptExecutor)driver).ExecuteScript("window.open(\"https://fides.xunta.gal/fides/html/private/DistribuidorPlantillaAction!accesoMenu.action?idElemento=70\", \"_blank\")");

            //driver.Navigate().GoToUrl("https://fides.xunta.gal/fides/html/private/DistribuidorPlantillaAction!accesoMenu.action?idElemento=70");


            // Realizar alguna acción (opcional)
            Console.WriteLine("Título de la página: " + driver.Title);
        }
        public void AbrirNAvegador1()
        {
            // Navegar a la página web
            var options = new EdgeOptions();
            options.AddArgument("--start-maximized");
            driver = new EdgeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            driver.Navigate().GoToUrl("https://matrhix.xunta.es/rcp/NUC/");
            var clave = driver.FindElement(By.Name("claveAcceso")); 
            var login = driver.FindElement(By.Name("idLogin")); 
            var btsubmit = driver.FindElement(By.Name("login"));

            clave.SendKeys("logos1060");
            login.SendKeys("34996197H");
            btsubmit.Click();


            driver.Navigate().GoToUrl("https://matrhix.xunta.es/rcp/NUC/PersonaConsulta0.do");
            EsperarCargaPagina("O sistema enviarame un PIN");

            var nifextranjero = driver.FindElement(By.Name("nifExtranjero"));
            var dni = driver.FindElement(By.Name("dni"));
            var letra = driver.FindElement(By.Name("letraNif"));
            btsubmit = driver.FindElement(By.Name("login"));

            dni.SendKeys("34996197");
            Thread.Sleep(2000);
            btsubmit.Click();

            EsperarCargaPagina("Consulta de Persoal");

            var button = driver.FindElement(By.XPath("//input[@value='Histórico']"));
            button.Click();


            // Realizar alguna acción (opcional)
            Console.WriteLine("Título de la página: " + driver.Title);
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

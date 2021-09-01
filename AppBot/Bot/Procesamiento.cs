using AppBot.Modelos;
using AppBot.Utilidades;
using AppBotVUR.Modelos;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppBot.Bot
{
    public static class Procesamiento
    {

        public static void iniciarProcesamiento(IWebDriver driver, List<DatosBusqueda> datosBusqueda)
        {
            //Datos
            EstadoForm.totalRegistros = datosBusqueda.Count() + "";

            for (int i = 0; i < datosBusqueda.Count(); i++)
            {
                var itemActual = datosBusqueda[i];
                var resultado = Procesamiento.procesarRegistro(driver, itemActual);
                EstadoForm.listadoResultados.Add(resultado);

                EstadoForm.resultados.Add($"Num=>{itemActual.NumIdentificacion} Tipo=>{itemActual.TipoDocumento} {System.Environment.NewLine}Resultado=> {resultado.resultado}{System.Environment.NewLine}=========================");

                if (EstadoForm.procesando == false)
                {
                    break;
                }
            }

        }

        public static DatosObtenidos procesarRegistro(IWebDriver driver, DatosBusqueda datosBusqueda)
        {
            WebDriverWait wait = null;
            IWebElement element = null;
            Boolean registroExiste = true;
            DatosObtenidos resultadoProceso = new DatosObtenidos();

            // procesarPaginaConsulta(driver, datosBusqueda);
            procesarPaginaInformacion(driver);

            try
            {


                try
                {
                    //Wait 10 seconds till alert is present
                    wait = new WebDriverWait(driver, new TimeSpan(0, 0, 2));
                    var alert = wait.Until(ExpectedConditions.AlertIsPresent());

                    //Accepting alert.
                    alert.Accept();
                    registroExiste = false;
                }
                catch (Exception e)
                {
                }

                if (registroExiste == true)
                {


                    //resultadoProceso.datosObtenidos = listadoDatos;
                    //resultadoProceso.resultado = "CON_INFORMACION";
                }
                else
                {
                    throw new Exception();
                }


            }
            catch (Exception ex)
            {
                LogEventos.registrarLog(ex.Message);
                Console.WriteLine(ex.Message);

                resultadoProceso.datosBusqueda = datosBusqueda;
                resultadoProceso.resultado = "SIN_INFORMACION";

                Thread.Sleep(1000);

                return resultadoProceso;
            }
            finally
            {
                try
                {

                    element = driver.FindElement(By.XPath("//button[@ng-click='reiniciar()']"));
                    element.Click();
                }
                catch (Exception)
                {

                }
            }

            return resultadoProceso;
        }

        private static ResultadoProceso procesarPaginaConsulta(IWebDriver driver, DatosBusqueda datosBusqueda)
        {
            IWebElement element = null;
            ResultadoProceso resultadoProceso = new ResultadoProceso();

            try
            {
                seleccionarTipoIdentificacion(driver, datosBusqueda.TipoDocumento);

                element = driver.FindElement(By.XPath("//*[@name='noDocumento']"));
                element.Clear();
                element.SendKeys(datosBusqueda.NumIdentificacion);

                element = driver.FindElement(By.XPath("//*[@id='captcha']"));
                element.SendKeys("ffe45");


                element = driver.FindElement(By.XPath("//*[contains(text(), 'Consultar')]"));
                element.Click();

                Thread.Sleep(1000);

                element = driver.FindElement(By.XPath("//*[@id='dlgConsulta']"));
                var res = element.Displayed;

                element = driver.FindElement(By.XPath("//*[@id='msgConsulta']"));
                resultadoProceso.Mensaje = element.Text;
                resultadoProceso.Resultado = element.Text.Trim().Length > 0 ? false : true;

            }
            catch (Exception ex)
            {
                resultadoProceso.Mensaje = ex.Message;
                resultadoProceso.Resultado = false;
            }

            return resultadoProceso;

        }


        private static ResultadoProceso procesarPaginaInformacion(IWebDriver driver)
        {
            IWebElement element = null;
            ResultadoProceso resultadoProceso = new ResultadoProceso();
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 4)); ;

            try
            {
                element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("i_licencias")));
                element.Click();
                Thread.Sleep(500);

                var listadoDatos = obtenerDatosLicencias(driver);

            }
            catch (Exception ex)
            {
                resultadoProceso.Mensaje = ex.Message;
                resultadoProceso.Resultado = false;
            }

            return resultadoProceso;

        }


        private static void seleccionarTipoIdentificacion(IWebDriver driver, string tipo)
        {
            try
            {
                driver.SwitchTo().Frame("page");
            }
            catch (Exception)
            {

            }


            SelectElement select = new SelectElement(driver.FindElement(By.XPath("//select")));

            switch (tipo)
            {
                case "CARNET DIPLOMATICO":
                    select.SelectByValue("D");
                    break;
                case "CEDULA CIUDADANIA":
                    select.SelectByValue("C");
                    break;
                case "CEDULA DE EXTRANJERIA":
                    select.SelectByValue("E");
                    break;
                case "PASAPORTE":
                    select.SelectByValue("P");
                    break;
                case "REGISTRO CIVIL":
                    select.SelectByValue("U");
                    break;
                case "TARJETA IDENTIDAD":
                    select.SelectByValue("T");
                    break;
                default:
                    break;
            }

        }

        private static List<Datos> obtenerDatosLicencias(IWebDriver driver)
        {
            List<Datos> listado = new List<Datos>();
            Datos datos = new Datos();
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));

            var ubicacionTabla = "//*[@id='pnlInformacionLicencias']/div/div/table/tbody/tr";
            var filas = driver.FindElements(By.XPath(ubicacionTabla));

            for (int i = 0; i < filas.Count(); i++)
            {
                var item = filas[i];
                var classCss = item.GetAttribute("class");
                
                if (classCss.ToLower().Contains("hide")==false)
                {
                    var dataTd = item.FindElements(By.TagName("td"));
                    datos.Estado = dataTd[3].Text;
                    dataTd[5].FindElement(By.TagName("a")).Click();

                    var ubicacionTablaCategorias = ubicacionTabla + $"[{i + 2}]//table/tbody/tr"; 
                    Thread.Sleep(500);

                    try
                    {
                        var filasCategorias = driver.FindElements(By.XPath(ubicacionTablaCategorias));
                        foreach (var filaCategoria in filasCategorias)
                        {
                            var dataTdCategoria = filaCategoria.FindElements(By.TagName("td"));
                            datos.Categoria = dataTdCategoria[0].Text;
                            datos.FechaExpedicion = dataTdCategoria[1].Text;
                            datos.FechaVencimiento = dataTdCategoria[2].Text;
                            datos.CategoriaAntigua = dataTdCategoria[3].Text;
                        }
                    }
                    catch (Exception)
                    {

                    }

                    dataTd[5].FindElement(By.TagName("a")).Click();
                    Thread.Sleep(500);

                    listado.Add(datos);
                    datos = new Datos();
                }
            }
            return listado;
        }

    }
}

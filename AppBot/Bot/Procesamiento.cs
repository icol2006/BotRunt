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

        public static void iniciarProcesamiento(IWebDriver driver,List<DatosBusqueda> datosBusqueda)
        {
            //Datos
            EstadoForm.totalRegistros = datosBusqueda.Count() + "";

            for (int i = 0; i < datosBusqueda.Count(); i++)
            {
                var itemActual = datosBusqueda[i];
                var resultado = Procesamiento.procesarRegistro(driver,itemActual);
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

                if(registroExiste==true)
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
            ResultadoProceso resultadoProceso =new ResultadoProceso();

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
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 2)); ;
      

            try
            {
                element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("pnlInformacionLicencias")));
                element = driver.FindElement(By.ClassName("i_licencias"));
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


        private static void seleccionarTipoIdentificacion(IWebDriver driver,string tipo)
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

            var filas = driver.FindElements(By.XPath("//*[@id='pnlInformacionLicencias']/div/div/table/tbody/tr"));

                foreach (var item in filas)
                {
                var classCss = item.GetAttribute("class");

                if (classCss.ToLower().Contains("hide"))
                {
                    var filasCategorias = item.FindElements(By.XPath("//table/tbody/tr"));
                    foreach (var filaCategoria in filasCategorias)
                    {
                        var dataTd = filaCategoria.FindElements(By.TagName("td"));
                        datos.Categoria = dataTd[0].Text;
                        datos.FechaExpedicion = dataTd[1].Text;
                        datos.FechaVencimiento = dataTd[2].Text;
                        datos.CategoriaAntigua = dataTd[3].Text;
                    }
                }
                else
                {
                    var dataTd = item.FindElements(By.TagName("td"));
                    datos.Estado = dataTd[3].Text;
                }
                // var dataTd = item.FindElements(By.TagName("td"));

                //datos.Propietario = dataTd[1].Text;
                //if(datos.Propietario.IndexOf("Total")>-1)
                //{
                //    datos.Propietario = procesarString(datos.Propietario, "Total");
                //}

                //datos.TipoIdentificacion = dataTd[2].Text;
                //datos.NumIdentificacion = dataTd[3].Text;
                //datos.Direccion = dataTd[4].Text;
                //if(datos.Direccion.IndexOf("(DIRECCION")>-1)
                //{
                //    datos.Direccion = procesarString(datos.Direccion, "(DIRECCION");
                //}             
                //datos.ReferenciaCatastral = dataTd[6].Text;
                //datos.Departamento = dataTd[7].Text;
                //datos.Municipio = dataTd[8].Text;
                //   listado.Add(datos);
                datos = new Datos();
                }
            


            return listado;
        }

        private static String procesarString(String palabra, String valorBuscar)
        {
            String resultado = "";
            int indiceFinal = 0;
            indiceFinal = palabra.IndexOf(valorBuscar);
            resultado = palabra.Substring(0, indiceFinal);

            return resultado;
        }

    }
}

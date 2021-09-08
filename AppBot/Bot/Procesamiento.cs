using AppBotVUR.Modelos;
using AppBotVUR.Utilidades;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppBotVUR.Bot
{
    public static class Procesamiento
    {

        public async static Task<Boolean> iniciarProcesamiento(IWebDriver driver, List<DatosBusqueda> datosBusqueda)
        {
            //Datos
            EstadoForm.totalRegistros = datosBusqueda.Count() + "";
            DatosObtenidos resultado = new DatosObtenidos();
            Thread.Sleep(3000);
            for (int i = 0; i < datosBusqueda.Count(); i++)
            {
                var itemActual = datosBusqueda[i];
                resultado = await Procesamiento.procesarRegistro(driver, itemActual);
                EstadoForm.listadoResultados.Add(resultado);

                String descripcionResultado = resultado.ResultadoProceso.Procesado == true ? "Procesado" : "No Procesado";
                EstadoForm.resultados.Add($"Num=>{itemActual.NumIdentificacion} " +
                    $"Tipo=>{itemActual.TipoDocumento} {System.Environment.NewLine} " +
                    $"Resultado=> {descripcionResultado}" +
                    $"{Environment.NewLine}=========================");

                EstadoForm.actualizarGridDatos();

                if (EstadoForm.procesando == false)
                {
                    break;
                }
            }

            return true;
        }

        public async static Task<DatosObtenidos> procesarRegistro(IWebDriver driver, DatosBusqueda datosBusqueda)
        {
            ResultadoProceso resultadoProceso = new ResultadoProceso();
            DatosObtenidos datosObtenidos = new DatosObtenidos();
            List<Datos> listadoDatosObtenidos = new List<Datos>();
            
            try
            {
                resultadoProceso = await procesarPaginaConsulta(driver, datosBusqueda);

                if (resultadoProceso.Procesado == true)
                {
                    listadoDatosObtenidos = procesarPaginaInformacion(driver);                
            

                    foreach (var item in listadoDatosObtenidos)
                    {
                        item.NumIdentificacion = datosBusqueda.NumIdentificacion;
                        item.TipoIdentificacion = datosBusqueda.TipoDocumento;
                    }
                  
                }
            }
            catch (Exception ex)
            {
               // hacerClickElemento(driver, "//*[@id='accordion']/div[1]//a");

                resultadoProceso.Procesado = false;
                resultadoProceso.Mensaje = ex.Message;
            }

            //Finalizar consulta
            // hacerClickElementoTryCatch(driver, "//*[@type='submit']");
            hacerClickElementoTryCatch(driver, "//button[1]");
            

            datosObtenidos.TipoDocumento = datosBusqueda.TipoDocumento;
            datosObtenidos.NumIdentificacion = datosBusqueda.NumIdentificacion;
            datosObtenidos.listadoDatos = listadoDatosObtenidos;
            datosObtenidos.ResultadoProceso = resultadoProceso;           

            return datosObtenidos;
        }

        private async static Task<ResultadoProceso> procesarPaginaConsulta(IWebDriver driver, DatosBusqueda datosBusqueda)
        {
            IWebElement element = null;
            ResultadoProceso resultadoProceso = new ResultadoProceso();

            try
            {
                hacerClickElementoTryCatch(driver, "//*[@class='modal-body']//button");

                seleccionarTipoIdentificacion(driver, datosBusqueda.TipoDocumento);

                element = driver.FindElement(By.XPath("//*[@name='noDocumento']"));
                element.Clear();
                element.SendKeys(datosBusqueda.NumIdentificacion);

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                var scriptCanvas = File.ReadAllText("canvas.js");
                var scriptRemoveCanvas = File.ReadAllText("canvas _remove.js");

                Thread.Sleep(1000);
                var stringBase64 = (String)js.ExecuteScript(scriptCanvas);
                var imagen = Utiles.Base64ToImage(stringBase64);
                imagen.Save(Parametros.imageCaptchaPath);

                var codigo = await Utiles.solvecatcha(Parametros.imageCaptchaPath, _2CaptchaAPI.Enums.FileType.Png);
                element = driver.FindElement(By.XPath("//*[@id='captcha']"));
                element.SendKeys(codigo);

                element = driver.FindElement(By.XPath("//*[@type='submit']"));
                element.Click();

                Thread.Sleep(500);
                element = driver.FindElement(By.XPath("//*[@id='msgConsulta']"));
                resultadoProceso.Mensaje = element.Text;
                resultadoProceso.Procesado = element.Text.Trim().Length > 0 ? false : true;

                Thread.Sleep(500);
                hacerClickElementoTryCatch(driver, "//*[@class='modal-body']//button");         
            }
            catch (Exception ex)
            {
                resultadoProceso.Mensaje = ex.Message;
                resultadoProceso.Procesado = false;
            }

            return resultadoProceso;
        }

        private static void hacerClickElementoTryCatch(IWebDriver driver,string elemento)
        {
            try
            {
                var element = driver.FindElement(By.XPath(elemento));
                element.Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void hacerClickElemento(IWebDriver driver, string elemento)
        {
                var element = driver.FindElement(By.XPath(elemento));
                element.Click();
        }



        private static List<Datos> procesarPaginaInformacion(IWebDriver driver)
        {
            IWebElement element = null;
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 4)); ;

            ////*[@id='accordion']//i
            Thread.Sleep(1000);
            element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='accordion']/div[1]")));
            //element=driver.FindElement(By.XPath("//*[@id='accordion']/div[1]//a"));
            //element.Click();
            hacerClickElemento(driver, "//*[@id='accordion']/div[1]//a");
         

            Thread.Sleep(500);

            var listadoDatos = obtenerDatosLicencias(driver);
            return listadoDatos;
        }

        private static void seleccionarTipoIdentificacion(IWebDriver driver, string tipo)
        {
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

        public static Bitmap GetElementScreenShot(IWebDriver driver, IWebElement element)
        {
            //Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            //ss.SaveAsFile(Parametros.imageCaptchaPath,
            //ScreenshotImageFormat.Bmp);
            //return null;

            Screenshot sc = ((ITakesScreenshot)driver).GetScreenshot();
            var img = Image.FromStream(new MemoryStream(sc.AsByteArray)) as Bitmap;
            var loca = element.Location;
            return img.Clone(new Rectangle(element.Location, element.Size), img.PixelFormat);

            //   return img.Clone(new Rectangle(element.Location,element.Size), img.PixelFormat);
        }

        private static List<Datos> obtenerDatosLicencias(IWebDriver driver)
        {
            List<Datos> listado = new List<Datos>();
            Datos datos = new Datos();
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            IWebElement element = null;
            String estado = "";

            var ubicacionTabla = "//*[@id='pnlInformacionLicencias']/div/div/table/tbody/tr";
            var filas = driver.FindElements(By.XPath(ubicacionTabla));

            for (int i = 0; i < filas.Count(); i++)
            {
                var item = filas[i];
                var classCss = item.GetAttribute("class");

                if (classCss.ToLower().Contains("hide") == false)
                {
                   
                    var dataTd = item.FindElements(By.TagName("td"));
                    estado = dataTd[3].Text;

                    var linkVerDetalle = dataTd[5].FindElement(By.TagName("a"));

                    try
                    {
                        linkVerDetalle.Click();
                    }
                    catch (Exception)
                    {
                        hacerClickElemento(driver, "//*[@id='accordion']/div[1]//a");
                        Thread.Sleep(500);
                        linkVerDetalle.Click();
                        Thread.Sleep(500);
                        estado = dataTd[3].Text;
                    }                

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
                            datos.Estado = estado;
                            listado.Add(datos);
                            datos = new Datos();
                        }
                    }
                    catch (Exception)
                    {

                    }

                    dataTd[5].FindElement(By.TagName("a")).Click();
                    Thread.Sleep(500);
                             
                }
            }
            return listado;
        }

    }
}

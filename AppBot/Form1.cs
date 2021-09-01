using AppBot.Bot;
using AppBot.Modelos;
using AppBot.Utilidades;
using AppBotVUR;
using AppDriverChrome;
using AppDriverFirefox;
using AppDriverIExplorer;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppBot
{
    public partial class Form1 : Form
    {
        DriverChrome driverChrome = null;
        List<DatosBusqueda> listadoDatosBusqueda = new List<DatosBusqueda>();
        List<String> datosProcesados = new List<string>();
        Boolean refrescarEstado = true;

        public Form1()
        {
            InitializeComponent();
            
        }



        private void button7_Click(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void cargarDatos()
        {
            try
            {
                limpiarDatos();
                var archivoExcel = Utiles.abrirArchivo();
                var datosBusqueda = Utiles.obtenerDatosExcel(archivoExcel);
                this.listadoDatosBusqueda= this.mapearDatosExcel(datosBusqueda);

                this.dgvDatos.Rows.Clear();
                foreach (var item in listadoDatosBusqueda)
                {
                    Utiles.cargarGridDatosProcesar(this.dgvDatos, "Num=> "+ item.NumIdentificacion+ " || Tipo documento=> " + item.TipoDocumento);
                }
                EstadoForm.totalRegistros = listadoDatosBusqueda.Count()+"";
                EstadoForm.listadoResultados.Clear();

            }
            catch (Exception ex)
            {
        
            }

        }

        public List<DatosBusqueda> mapearDatosExcel(List<List<string>> datos)
        {
            var listadoDatosBusqueda = (from d in datos
                   select new DatosBusqueda { TipoDocumento=d[0], NumIdentificacion = d[1] })
                   .ToList();

            return listadoDatosBusqueda;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            limpiarDatos();
        }

        private void limpiarDatos()
        {
            Utiles.limpiarDatagridView(this.dgvDatos);
            this.listadoDatosBusqueda.Clear();
            EstadoForm.listadoResultados.Clear();
        }


        private void button4_Click(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {



        }

        private void button1_Click(object sender, EventArgs e)
        {
            EstadoForm.procesando = false;            
            toogleBotones();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.refrescarEstado = false;
      
            EstadoForm.procesando = false;
            if (driverChrome != null)
                driverChrome.CloseDriver();

            Thread.Sleep(500);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            iniciarEscaneo();
        }


        private void iniciarEscaneo()
        {
            if (this.listadoDatosBusqueda.Count()>0)
            {
                this.toogleBotones();

                EstadoForm.listadoResultados.Clear();

                Thread thread = new Thread(() =>
                {
                    try
                    {
                        EstadoForm.procesando = true;
                        Procesamiento.iniciarProcesamiento(this.driverChrome.driver, this.listadoDatosBusqueda);
                     
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            MessageBox.Show(ex.Message);
                        }));
                    }

                    try
                    {
                        this.Invoke(new Action(() =>
                        {
                            this.toogleBotones();
                        }));
                    }
                    catch (Exception ex)
                    {

                    }

                }      
                );               
                try
                {
                    thread.Start();             

                }
                catch (Exception ex)
                {
                    LogEventos.registrarLog(ex.Message);
                }
            }

        }

        private void actualizarEstado()
        {
            Thread thread2 = new Thread(() =>
            {
                try
                {
                    while (this.refrescarEstado == true)
                    {
                        try
                        {
                            Thread.Sleep(500);
                            String estado = EstadoForm.procesando == true ? "Procesando" : "Detenido";

                            this.Invoke(new Action(() =>
                            {
                                lblEstado.Text = estado;
                                var result = String.Join(System.Environment.NewLine, EstadoForm.resultados.ToArray());
                                txtResultado.Text = result;

                                lblCantidadRegistros.Text = EstadoForm.totalRegistros;
                                lblTotalProcesado.Text =EstadoForm.listadoResultados.Count()+"";
                                int rowActualGridDatos = Convert.ToInt32(EstadoForm.listadoResultados.Count());
                                if(dgvDatos.Rows.Count>1)
                                {
                                    dgvDatos.CurrentCell = dgvDatos.Rows[rowActualGridDatos].Cells[0];
                                }                                            
                            }));
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    this.Invoke(new Action(() =>
                    {
                        lblEstado.Text = EstadoForm.estado;

                    }));
                    EstadoForm.estado = "Detenido";
                }
                catch (Exception ex)
                {

                }
                finally
                {


                }
            });
            thread2.Start();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            abrirBot();
        }

        private void toogleBotones()
        {
            btnDetener.Enabled = !(btnDetener.Enabled);
            btnProcesar.Enabled = !(btnProcesar.Enabled);
            btnCargarDatos.Enabled = !(btnCargarDatos.Enabled);
            btnBorrarDatos.Enabled = !(btnBorrarDatos.Enabled);
            btnExportar.Enabled = !(btnExportar.Enabled);
        }

        public void abrirBot()
        {
            try
            {
                driverChrome = new DriverChrome();
                driverChrome.iniciarNavegador();

                this.btnAbrirBot.Enabled = false;
                this.btnProcesar.Enabled = true;
                AbrirPaginaInicio();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void AbrirPaginaInicio()
        {
            Thread thread = new Thread(() =>
            {
                try
                {
                    EstadoForm.procesando = true;
                    while (EstadoForm.procesando == true && driverChrome.driver == null)
                    {
                        
                    }
                    driverChrome.driver.Url = "https://www.runt.com.co/consultaCiudadana/#/consultaPersona";
                }
                catch (Exception ex)
                {
                }

            });
            thread.Start();


        }


        private void btnExportar_Click(object sender, EventArgs e)
        {
            exportarDatos();
        }

        public static void exportarDatos()
        {
            try
            {        
                //Create a workbook
                Workbook workbook = new Workbook();
                var datosObtenidos = EstadoForm.obtenerListadoResultados();

                //Initailize worksheet
                Worksheet sheet = workbook.Worksheets[0];

                sheet.Range["A" + 1].Text = "";
                sheet.Range["B" + 1].Text = "Propietario";
                sheet.Range["C" + 1].Text = "Tipo Identificación";
                sheet.Range["D" + 1].Text = "Numero de identificacion";
                sheet.Range["E" + 1].Text = "Direccion del inmueble";
                sheet.Range["F" + 1].Text = "Referencia Catastral";
                sheet.Range["G" + 1].Text = "Departamento";
                sheet.Range["H" + 1].Text = "Municipio";
                sheet.Name = "CON_INFORMACION";

                for (int i = 0; i < datosObtenidos.Count(); i++)
                {
                    sheet.Range["A" + (i+2)].Text = (i) + "";
                    sheet.Range["C" + (i + 2)].Text = datosObtenidos[i].TipoIdentificacion ?? "";
                    sheet.Range["D" + (i + 2)].Text = datosObtenidos[i].NumIdentificacion ?? "";
                    sheet.Range["E" + (i + 2)].Text = datosObtenidos[i].Categoria ?? "";
                    sheet.Range["F" + (i + 2)].Text = datosObtenidos[i].FechaExpedicion ?? "";
                    sheet.Range["G" + (i + 2)].Text = datosObtenidos[i].FechaVencimiento ?? "";
                    sheet.Range["H" + (i + 2)].Text = datosObtenidos[i].CategoriaAntigua ?? "";
                }

                //SIN INFORMACION
                Worksheet sheet3 = workbook.Worksheets[1];
                sheet3.Name = "SIN_INFORMACION";

                var datosSinInformacion = EstadoForm.listadoResultados.Where(x => (x.resultado.Equals("SIN_INFORMACION"))).ToList();

                sheet3.Range["A" + 1].Text = "";
                sheet3.Range["B" + 1].Text = "Num identificacion";
                sheet3.Range["C" + 1].Text = "Tipo doc";


                for (int i = 0; i < datosSinInformacion.Count(); i++)
                {
                    sheet3.Range["A" + (i + 2)].Text = (i) + "";
                    sheet3.Range["B" + (i + 2)].Text = datosSinInformacion[i].datosBusqueda.NumIdentificacion;
                    sheet3.Range["C" + (i + 2)].Text = datosSinInformacion[i].datosBusqueda.TipoDocumento;

                }




                //Save the file
                workbook.SaveToFile("datos.xls", ExcelVersion.Version97to2003);

                //Launch the file
                System.Diagnostics.Process.Start("datos.xls");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
   
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            actualizarEstado();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            obtenerDatos();

            //var fsd = driverSelenium.driver.FindElements(By.XPath("//*[@class='panel-heading']"));
            //SelectElement select = new SelectElement(driverSelenium.driver.FindElement(By.XPath("//*[@id='criterio']")));
            ////select.SelectByValue("Documento");
            //select.SelectByText("Documento");
  

      
        }

        private void escribirNumIdentificacion()
        {
           var element= driverChrome.driver.FindElement(By.XPath("//*[@id='numeroDocumento']"));
            element.SendKeys("79858092");            
        }

        private void obtenerDatos()
        {
            try
            {
                driverChrome.driver.SwitchTo().Frame("page");
            }
            catch (Exception)
            {

            }

            try
            {
                List<Datos> listado = new List<Datos>();
                Datos datos = new Datos();
                var filas = driverChrome.driver.FindElements(By.XPath("//div[@ng-show='pantallaBusqueda']/div/table/tbody/tr"));

                foreach (IWebElement item in filas)
                {
                    var dataTd = item.FindElements(By.TagName("td"));


                    //datos.Propietario= dataTd[1].Text;
                    //datos.Propietario = procesarString(datos.Propietario, "Total");
                    //datos.TipoIdentificacion = dataTd[2].Text;
                    //datos.NumIdentificacion = dataTd[3].Text;
                    //datos.Direccion = dataTd[4].Text;
                    //datos.Direccion = procesarString(datos.Direccion, "(DIRECCION");
                    //datos.ReferenciaCatastral = dataTd[6].Text;
                    listado.Add(datos);
                    datos = new Datos();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            int adf = 0;
        }

        private String procesarString(String palabra, String valorBuscar)
        {
            String resultado = "";
            int indiceFinal = 0;
            indiceFinal = palabra.IndexOf(valorBuscar);
            resultado = palabra.Substring(0, indiceFinal);            

            return resultado;
        }

        private void tiposDeDocumentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTipoDocumentos frmTipoDocumentos = new FrmTipoDocumentos();
            frmTipoDocumentos.Show();
        }
    }
}
using AppBotVUR.Bot;
using AppBotVUR.Modelos;
using AppBotVUR.Utilidades;
using AppDriverChrome;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace AppBotVUR
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
                this.listadoDatosBusqueda=Utiles.mapearDatosExcel(datosBusqueda);

                this.dgvDatos.Rows.Clear();
                foreach (var item in listadoDatosBusqueda)
                {
                    Utiles.cargarGridDatosProcesar(this.dgvDatos, "Num=> "+ item.NumIdentificacion+ " || " + item.TipoDocumento);
                }
                EstadoForm.totalRegistros = listadoDatosBusqueda.Count()+"";
                EstadoForm.listadoResultados.Clear();

            }
            catch (Exception)
            {
        
            }
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
          var res=  Utiles.exportarDatos();

            if(res.Procesado==false)
            {
                MessageBox.Show(res.Mensaje);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            actualizarEstado();
            EstadoForm.dataGridView = this.dgvDatos;
            EstadoForm.form = this;
        }
 
        private void tiposDeDocumentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTipoDocumentos frmTipoDocumentos = new FrmTipoDocumentos();
            frmTipoDocumentos.Show();
        }

        private void apiCaptchaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmApiCaptcha frmApiCaptcha = new FrmApiCaptcha();
            frmApiCaptcha.Show();
        }
    }
}
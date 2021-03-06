using _2CaptchaAPI;
using _2CaptchaAPI.Enums;
using AppBotVUR.Modelos;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppBotVUR.Utilidades
{
    public static class Utiles
    {

        public static string abrirArchivo()
        {
            string filename = "";
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Excel File";
            theDialog.Filter = "XLSX files|*.xlsx";

            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    filename = theDialog.FileName;
                }
                catch (Exception ex)
                {
                }
            }
            return filename;
        }

        public static List<string> abrirArchivoTexto()
        {
            List<string> filelines = new List<string>();
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filename = theDialog.FileName;
                    filelines = File.ReadAllLines(filename).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            return filelines;
        }

        public static List<List<String>> obtenerDatosExcel(String filePath)
        {
            List<List<String>> resultado = new List<List<String>>();
            List<String> item = new List<String>();

            Workbook workbook = new Workbook();
            workbook.LoadFromFile(filePath);
            Worksheet sheet = workbook.Worksheets[0];

            int columnCount = sheet.Columns.Count();
            int rowCount = sheet.Rows.Count();


            for (int rowNum = 2; rowNum <= sheet.Rows.Count(); rowNum++)
            {
                for (int colNum = 1; colNum <= sheet.Columns.Count(); colNum++)
                {
                    var value = sheet.Range[rowNum, colNum].Value;
                    item.Add(value.ToString());
                }

                resultado.Add(item);
                item = new List<string>();
            }

            return resultado;
        }

        public static List<DatosBusqueda> mapearDatosExcel(List<List<string>> datos)
        {
            var listadoDatosBusqueda = (from d in datos
                                        select new DatosBusqueda { TipoDocumento = d[0], NumIdentificacion = d[1] })
                   .ToList();
            
            return listadoDatosBusqueda;
        }

        public static void cargarGridDatosProcesar(DataGridView dataGridView, string dato)
        {
            DataGridViewRow row = (DataGridViewRow)dataGridView.Rows[0].Clone();
            row.Cells[0].Value = dato;
            dataGridView.Rows.Add(row);
        }

        public static void limpiarDatagridView(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            dataGridView.Refresh();
        }

        public static ResultadoProceso exportarDatos()
        {
            ResultadoProceso resultadoProceso = new ResultadoProceso();
            resultadoProceso.Procesado = true;

            try
            {
                //Create a workbook
                Workbook workbook = new Workbook();
                var datosObtenidos = EstadoForm.obtenerListadoResultados();

                //Initailize worksheet
                Worksheet sheet = workbook.Worksheets[0];

                sheet.Range["A" + 1].Text = "#";
                sheet.Range["B" + 1].Text = "Estado";
                sheet.Range["C" + 1].Text = "TipoIdentificacion";
                sheet.Range["D" + 1].Text = "NumIdentificacion";
                sheet.Range["E" + 1].Text = "Categoria";
                sheet.Range["F" + 1].Text = "FechaExpedicion";
                sheet.Range["G" + 1].Text = "FechaVencimiento";
                sheet.Range["H" + 1].Text = "Categoría antigua";
                sheet.Name = "CON_INFORMACION";

                for (int i = 0; i < datosObtenidos.Count(); i++)
                {
                    sheet.Range["A" + (i + 2)].Text = (i) + "";
                    sheet.Range["B" + (i + 2)].Text = datosObtenidos[i].Estado ?? "";
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

                var datosSinInformacion = EstadoForm.listadoResultados.Where(x => (x.ResultadoProceso.Procesado == false)).ToList();

                sheet3.Range["A" + 1].Text = "3";
                sheet3.Range["B" + 1].Text = "Num identificacion";
                sheet3.Range["C" + 1].Text = "Tipo doc";
                sheet3.Range["D" + 1].Text = "Error";

                for (int i = 0; i < datosSinInformacion.Count(); i++)
                {
                    sheet3.Range["A" + (i + 2)].Text = (i) + "";
                    sheet3.Range["B" + (i + 2)].Text = datosSinInformacion[i].NumIdentificacion;
                    sheet3.Range["C" + (i + 2)].Text = datosSinInformacion[i].TipoDocumento;
                    sheet3.Range["D" + (i + 2)].Text = datosSinInformacion[i].ResultadoProceso.Mensaje;
                }

                //Save the file
                workbook.SaveToFile("datos.xls", ExcelVersion.Version97to2003);

                //Launch the file
                System.Diagnostics.Process.Start("datos.xls");

            }
            catch (Exception ex)
            {
                resultadoProceso.Procesado = false;
                resultadoProceso.Mensaje = ex.Message;
            }
            return resultadoProceso;
        }

        public async static Task<String> solvecatcha(String imagePath, FileType e)
        {
            String res = "";
            var captcha = new _2Captcha(Parametros.apiKey);
            var image2 = await captcha.SolveImage(File.ReadAllBytes(imagePath), e);
            res = image2.Response;

            return res;
        }

        public static Configuraciones obtenerJSONConfiguraciones()
        {
            var json = File.ReadAllText(Parametros.pathConfiguraciones);
            var result = JsonConvert.DeserializeObject<Configuraciones>(json);

            return result;           
        }

        public static void salvarJSONConfiguraciones(Configuraciones configuraciones)
        {
            var jsonString = JsonConvert.SerializeObject(configuraciones);
            File.WriteAllText(Parametros.pathConfiguraciones, jsonString);
        }

        public static Image Base64ToImage(String data)
        {
            //data:image/gif;base64,
            //this image is a single pixel (black)
            byte[] bytes = Convert.FromBase64String(data);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }
    }
}

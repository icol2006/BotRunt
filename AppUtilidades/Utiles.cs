using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AppBot.Utilidades
{
    public static class Utiles
    {


        public static string abrirArchivo()
        {
            string filename = "";
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Excel File";
            theDialog.Filter = "XLSX files|*.xlsx";
            // theDialog.InitialDirectory = @"C:\";
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

        public static List<string> obtenerDatosLinksDatagridView(DataGridView dataGridView)
        {
            List<string> res = new List<string>();

            for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                res.Add(dataGridView.Rows[i].Cells[0].Value.ToString());
            }

            return res;
        }

       
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppBotVUR.Modelos
{
    public static class EstadoForm
    {
        public static List<DatosObtenidos> listadoResultados = new List<DatosObtenidos>();

        public static List<String> resultados = new List<String>();

        public static string estadoActual="Detenido";

        public static string totalRegistros = "0";

        public static Boolean procesando=false;

        public static string estado = "Detenido";

        public static Form form { get; set; }
        public static DataGridView dataGridView { get; set; }

        public static List<Datos> obtenerListadoResultados()
        {
            List<Datos> listado = new List<Datos>();
            var listaProcesar = EstadoForm.listadoResultados.Where(x => x.ResultadoProceso.Procesado==true).ToList();

            foreach (DatosObtenidos items in listaProcesar)
            {
                foreach (var item in items.listadoDatos)
                {
                        listado.Add(item);                                     
                }              
            }

            return listado;
        }

        public static void actualizarGridDatos()
        {
            int rowActualGridDatos = Convert.ToInt32(EstadoForm.listadoResultados.Count());

            form.Invoke(new Action(() =>
            {
                if (dataGridView.Rows.Count > 1)
                {
                    dataGridView.CurrentCell = dataGridView.Rows[rowActualGridDatos].Cells[0];
                }
            }));         
        }

    }
}
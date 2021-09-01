using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBot.Modelos
{
    public static class EstadoForm
    {
        public static List<ResultadoProceso> listadoResultados = new List<ResultadoProceso>();

        public static List<String> resultados = new List<String>();

        public static string estadoActual="Detenido";

        public static string totalRegistros = "0";

        public static Boolean procesando=false;

        public static string estado = "Detenido";


        public static List<Datos> obtenerListadoResultados()
        {
            List<Datos> listado = new List<Datos>();
            var listaProcesar = EstadoForm.listadoResultados.Where(x => x.resultado.Equals("CON_INFORMACION")).ToList();

            foreach (ResultadoProceso items in listaProcesar)
            {
                foreach (var item in items.datosObtenidos)
                {
                        listado.Add(item);                                     
                }              
            }

            return listado;
        }

    }
}

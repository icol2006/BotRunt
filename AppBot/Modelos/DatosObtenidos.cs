using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBot.Modelos
{
    public class DatosObtenidos
    {
        public DatosBusqueda datosBusqueda { get; set; }
        public List<Datos> datosObtenidos { get; set; }
        public string resultado { get; set; }
    }
}

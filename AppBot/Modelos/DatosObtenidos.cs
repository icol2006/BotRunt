using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBotVUR.Modelos
{
    public class DatosObtenidos
    {
        public String TipoDocumento { get; set; }
        public String NumIdentificacion { get; set; }
        public List<Datos> listadoDatos { get; set; }
        public ResultadoProceso ResultadoProceso { get; set; }
    }
}

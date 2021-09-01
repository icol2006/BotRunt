using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBot.Utilidades
{
    public static class Parametros
    {

        public static readonly Dictionary<string, string> tiposIdentificacion= new Dictionary<string, string>
        {
            { "CARNET DIPLOMATICO", "D" },
            { "CEDULA CIUDADANIA", "C" },
            { "CEDULA DE EXTRANJERIA", "E" },
            { "PASAPORTE", "P" },
            { "REGISTRO CIVIL", "U" },
            { "TARJETA IDENTIDAD", "T" },
        };

    }



}

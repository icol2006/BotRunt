using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBotVUR.Utilidades
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

        public static string apiKey = "827f004c5dc23232133d2c9ca3dab1ff";

        public static string pathConfiguraciones = "config.txt";

        public static string imageCaptchaPath = "ima.bmp";
    }



}


public class Parametrosx
{

    public  Dictionary<string, string> tiposIdentificacion = new Dictionary<string, string>
        {
            { "CARNET DIPLOMATICO", "D" },
            { "CEDULA CIUDADANIA", "C" },
            { "CEDULA DE EXTRANJERIA", "E" },
            { "PASAPORTE", "P" },
            { "REGISTRO CIVIL", "U" },
            { "TARJETA IDENTIDAD", "T" },
        };

    public  string apiKey = "827f004c5dc23232133d2c9ca3dab1ff";

}
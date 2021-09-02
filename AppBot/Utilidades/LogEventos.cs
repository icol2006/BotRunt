using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBotVUR.Utilidades
{
    public static class LogEventos
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void registrarLog(String mensaje)
        {
            log.Info(string.Format("Input: {0} - Time: {1}ms", mensaje, DateTime.Now.ToString()));
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WeatherForecast.Services
{
    public class CombinedLogger : ILogger
    {
        public void Log(LogLevel level, string message)
        {
            using (StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath("~/Log/log.txt"), true))
            {
                sw.WriteLine("{0} [{1}] {2}", DateTime.Now, level, message);
                System.Diagnostics.Debug.WriteLine("{0} [{1}] {2}", DateTime.Now, level, message);
            }
        }
    }
}
using System;
using System.IO;
using System.Web;

namespace WeatherForecast.Services
{
    public class FileLogger : ILogger
    {
        public void Log(LogLevel level, string message)
        {
            using (StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath("~/Log/log.txt"), true))
            {
                sw.WriteLine("{0} [{1}] {2}", DateTime.Now, level, message);
            }
        }
    }
}
using System;

namespace WeatherForecast.Services
{
    public class ConsoleLogger : ILogger
    {
        public void Log(LogLevel level, string message)
        {
            System.Diagnostics.Debug.WriteLine("{0} [{1}] {2}", DateTime.Now, level, message);
        }
    }
}
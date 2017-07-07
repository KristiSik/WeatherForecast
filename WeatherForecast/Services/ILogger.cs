public enum LogLevel
{
    Error,
    Info,
    Success
}
namespace WeatherForecast.Services
{
    public interface ILogger
    {
        void Log(LogLevel level, string message);
    }
}
using System;
using System.Net.Http;
using System.Web.Configuration;
using WeatherForecast.Models;
using WeatherForecast.Services;

namespace WeatherForecast.Json
{
    public class JsonOperations
    {
        private ILogger _logger;
        public JsonOperations(ILogger logger)
        {
            _logger = logger;
        }
        public async System.Threading.Tasks.Task<String> GetJsonFromUrl(SearchCity city)
        {
            var json = "";
            using (var httpClient = new HttpClient())
            {
                try
                {
                    _logger.Log(LogLevel.Info, "Getting JSON for " + city.Name + "...");
                    string apiKey = WebConfigurationManager.AppSettings["ApiKey"];
                    var url = string.Empty;
                    url = String.Format(WebConfigurationManager.AppSettings["ForecastWeatherUrl"], city.Name, city.Period, apiKey);
                    json = await httpClient.GetStringAsync(url);
                    _logger.Log(LogLevel.Success, "Received JSON data for " + city.Name + ".");
                }
                catch (Exception e)
                {
                    _logger.Log(LogLevel.Error, "Can't get JSON for " + city.Name + ". " + e.Message);
                    return null;
                }
            }
            return json;
        }
    }
}
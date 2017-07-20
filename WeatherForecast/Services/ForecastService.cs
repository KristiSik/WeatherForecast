using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Web.Configuration;
using WeatherForecast.Models;
using WeatherForecast.Services;

namespace WeatherForecast.Json
{
    public class ForecastService : IForecastService
    {
        private ILogger _logger;
        public ForecastService(ILogger logger)
        {
            _logger = logger;
        }
        public async System.Threading.Tasks.Task<Forecast> GetJsonFromUrl(SearchCity city)
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
            Forecast forecast;
            try
            {
                _logger.Log(LogLevel.Info, "Deserializing JSON for " + city.Name + "...");
                forecast = JsonConvert.DeserializeObject<Forecast>(json);
            } 
            catch(Exception e)
            {
                _logger.Log(LogLevel.Error, "Can't deserialize JSON. " + e.Message);
                forecast = null;
            }
            return forecast;
        }
    }
}
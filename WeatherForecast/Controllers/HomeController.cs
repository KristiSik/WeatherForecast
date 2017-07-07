using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Web.Mvc;
using WeatherForecast.Models;
using WeatherForecast.Services;

namespace WeatherForecast.Controllers
{
    public class HomeController : Controller
    {
        private ILogger _logger;
        public HomeController(ILogger logger)
        {
            _logger = logger;
        }
        private async System.Threading.Tasks.Task<String> getJsonFromUrl(SearchCity city)
        {
            var json = "";
            using (var httpClient = new HttpClient())
            {
                try
                {
                    _logger.Log(LogLevel.Info, "Getting JSON for " + city.Name + "...");
                    json = await httpClient.GetStringAsync("http://api.openweathermap.org/data/2.5/forecast/daily?q=" + city.Name + "&cnt=" + city.Period + "&units=metric&APPID=320c4641f279991c76782dd2d146a7b5");
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
        [HandleError]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Index(SearchCity searchCity)
        {
            ViewBag.Search = true;
            Forecast forecast;
            string content = await getJsonFromUrl(searchCity);
            try
            {
                _logger.Log(LogLevel.Info, "Deserializing JSON for " + searchCity.Name + "...");
                forecast = JsonConvert.DeserializeObject<Forecast>(content);
                _logger.Log(LogLevel.Success, "Deserialized JSON for " + searchCity.Name + ".");
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, "Can't deserialize JSON. " + e.Message);
                return View("~/Views/Home/NotFoundCity.cshtml", new Forecast() { city = new City() { name = searchCity.Name }, cnt = 1 });
            }
            forecast.Period = searchCity.Period;
            return View(forecast);
        }
        public ActionResult SearchForm()
        {
            SearchCity city = new SearchCity();
            return View(city);
        }
        public ActionResult FilterForm()
        {
            SearchCity city = new SearchCity();
            return View(city);
        }
    }
}
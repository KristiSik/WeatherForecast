using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Web.Mvc;
using WeatherForecast.Models;

namespace WeatherForecast.Controllers
{
    public class HomeController : Controller
    {
        private async System.Threading.Tasks.Task<String> getJsonFromUrl(SearchCity city)
        {
            var json = "";
            using (var httpClient = new HttpClient())
            {
                try
                {
                    json = await httpClient.GetStringAsync("http://api.openweathermap.org/data/2.5/forecast/daily?q=" + city.Name + "&cnt=" + city.Period + "&units=metric&APPID=320c4641f279991c76782dd2d146a7b5");
                }
                catch(Exception e)
                {
                    return null;
                }
            }
            return json;
        }
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
                forecast = JsonConvert.DeserializeObject<Forecast>(content);
            }
            catch(Exception e)
            {
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
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
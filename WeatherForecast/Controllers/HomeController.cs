using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;
using WeatherForecast.Context;
using WeatherForecast.Json;
using WeatherForecast.Models;
using WeatherForecast.Models.Profile;
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
            JsonOperations jsonGetter = new JsonOperations(_logger);
            string content = await jsonGetter.GetJsonFromUrl(searchCity);
            Forecast forecast;
            try
            {
                _logger.Log(LogLevel.Info, "Deserializing JSON for " + searchCity.Name + "...");
                forecast = JsonConvert.DeserializeObject<Forecast>(content);
                forecast.Period = searchCity.Period;
                _logger.Log(LogLevel.Success, "Deserialized JSON for " + searchCity.Name + ".");
                if (Session["UserId"] != null && searchCity.Period == 1)
                {
                    using (var ctx = new WeatherForecastContext())
                    {
                        int userId = (int) Session["UserId"];
                        var s = ctx.Users.SingleOrDefault(u => u.Id == userId);
                        s.History.Add(new Request() { CityName = searchCity.Name, Date = DateTime.Now });
                        ctx.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, "Can't deserialize JSON. " + e.Message);
                return View("~/Views/Home/NotFoundCity.cshtml", new Forecast() { City = new Models.City() { Name = searchCity.Name }, Cnt = 1 });
            }

            return View(forecast);
        }
        public ActionResult SearchForm()
        {
            SearchCity city = new SearchCity();
            return PartialView(city);
        }
        public ActionResult FilterForm()
        {
            SearchCity city = new SearchCity();
            return PartialView(city);
        }
        public ActionResult ProfileInHead()
        {
            return PartialView();
        }
        public ActionResult FavoriteButton(string cityName)
        {
            if (Session["UserId"] != null)
            {
                using (var ctx = new WeatherForecastContext())
                {
                    int userId = (int)Session["UserId"];
                    var favorites = ctx.Users.Where(u => u.Id == userId).Select(f => f.Favorites).FirstOrDefault();
                    if (!favorites.Any(f => f.Name == cityName))
                    {
                        return PartialView(Tuple.Create(new Models.Profile.City() { Name = cityName }, true));
                    }
                }
            }
            return PartialView(Tuple.Create(new Models.Profile.City() { Name = cityName }, false));
        }
    }
}
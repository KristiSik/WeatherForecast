using DataLayer;
using DataLayer.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;
using WeatherForecast.Json;
using WeatherForecast.Models;
using WeatherForecast.Services;

namespace WeatherForecast.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork ctx = new UnitOfWork(new WeatherForecastContext());
        private ILogger _logger;
        public HomeController(ILogger logger)
        {
            _logger = logger;
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
                    int userId = (int)Session["UserId"];
                    ctx.Users.AddRequestFromUser(userId, searchCity.Name);
                    ctx.Complete();
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
                int userId = (int)Session["UserId"];
                bool cityInFavorites = ctx.Users.IsCityInFavorites(userId, cityName);
                return PartialView(Tuple.Create(new DataLayer.Models.City() { Name = cityName }, cityInFavorites));
            }
            return PartialView(Tuple.Create(new DataLayer.Models.City() { Name = cityName }, false));
        }
    }
}
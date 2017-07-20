using DataLayer;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using WeatherForecast.Json;
using WeatherForecast.Models;
using WeatherForecast.Services;

namespace WeatherForecast.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork ctx;
        private ILogger _logger;
        private IForecastService forecastService;
        private IUserAccount userAccount;
        public HomeController(ILogger logger, IForecastService jsonOperations, IUnitOfWork uow, IUserAccount _userAccount)
        {
            _logger = logger;
            forecastService = jsonOperations;
            ctx = uow;
            userAccount = _userAccount;
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
            Forecast forecast = await forecastService.GetJsonFromUrl(searchCity);
            if (forecast == null)
            {
                return View("~/Views/Home/NotFoundCity.cshtml", new Forecast() { City = new Models.City() { Name = searchCity.Name }, Cnt = 1 });
            }
            forecast.Period = searchCity.Period;
            userAccount.Request(searchCity);
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
            if (userAccount.IsAutorized)
            {
                bool cityInFavorites = ctx.Users.IsCityInFavorites(userAccount.Id, cityName);
                return PartialView(Tuple.Create(new DataLayer.Models.City() { Name = cityName }, cityInFavorites));

            }
            return PartialView(Tuple.Create(new DataLayer.Models.City() { Name = cityName }, false));
        }
        public ActionResult DropDownListCities()
        {
            var defaultCities = ctx.DefaultCities.GetAllCities();
            return PartialView(defaultCities);
        }
    }
}
using DataLayer;
using DataLayer.Models;
using System.Web.Http;
using WeatherForecast.Services;

namespace WeatherForecast.ApiControllers
{
    public class DefaultCityController : ApiController
    {
        private IUnitOfWork ctx;
        private ILogger logger;
        public DefaultCityController()
        {
            logger = new CombinedLogger();
            ctx = new UnitOfWork(new WeatherForecastContext());
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            var defaultCities = ctx.DefaultCities.GetAllCities();
            return Json(defaultCities);
        }
        [HttpPost]
        public IHttpActionResult Add([FromUri]DefaultCity city)
        {
            if (city.Name.Length > 0)
            {
                ctx.DefaultCities.AddCity(city);
                ctx.Complete();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        public IHttpActionResult Delete([FromUri]DefaultCity city)
        {
            if (ctx.DefaultCities.RemoveCity(city))
            {
                ctx.Complete();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}

using DataLayer;
using Newtonsoft.Json;
using System.Web.Http;
using WeatherForecast.Json;
using WeatherForecast.Models;
using WeatherForecast.Services;

namespace WeatherForecast.ApiControllers
{
    public class ForecastController : ApiController
    {
        private IUnitOfWork ctx;
        private ILogger logger;
        public ForecastController()
        {
            logger = new ConsoleLogger();
            ctx = new UnitOfWork(new WeatherForecastContext());
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<IHttpActionResult> Get([FromUri] SearchCity request)
        {
            if (request.Period > 17)
            {
                request.Period = 17;
            }
            JsonOperations getJson = new JsonOperations(logger);
            string json = await getJson.GetJsonFromUrl(request);
            Forecast forecast = JsonConvert.DeserializeObject<Forecast>(json);
            return Json(forecast);
        }
    }
}

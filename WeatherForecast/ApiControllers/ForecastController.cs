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
        private IForecastService forecastService;
        public ForecastController(ILogger _logger, IUnitOfWork uow, IForecastService jsonOperations)
        {
            logger = _logger;
            ctx = uow;
            forecastService = jsonOperations;
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<IHttpActionResult> Get([FromUri] SearchCity request)
        {
            if (request.Period > 17)
            {
                request.Period = 17;
            }
            Forecast forecast = await forecastService.GetJsonFromUrl(request);
            return Json(forecast);
        }
    }
}

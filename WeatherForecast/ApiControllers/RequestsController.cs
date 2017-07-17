using DataLayer;
using System.Linq;
using System.Web.Http;
using WeatherForecast.Services;

namespace WeatherForecast.ApiControllers
{
    public class RequestsController : ApiController
    {
        IUnitOfWork ctx;
        ILogger logger;
        public RequestsController()
        {
            logger = new ConsoleLogger();
            ctx = new UnitOfWork(new WeatherForecastContext());
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            var requests = ctx.Requests.GetAllRequests().OrderByDescending(t => t.Date);
            return Json(requests);
        }
    }
}

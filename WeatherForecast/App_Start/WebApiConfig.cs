using System.Net.Http.Headers;
using System.Web.Http;

namespace WeatherForecast
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "GetWeatherForCity",
                routeTemplate: "api/{controller}/{action}/{name}/{period}",
                defaults: new { name = RouteParameter.Optional, period = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}

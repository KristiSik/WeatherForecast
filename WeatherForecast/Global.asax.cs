using Ninject;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WeatherForecast.Services;
using WeatherForecast.Util;
using DataLayer;
using System.Web.Http;

namespace WeatherForecast
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

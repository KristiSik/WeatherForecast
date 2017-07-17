using Ninject;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WeatherForecast.Services;
using WeatherForecast.Util;
using DataLayer;

namespace WeatherForecast
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IKernel _kernel = new StandardKernel();
            _kernel.Bind<ILogger>().To<CombinedLogger>();
            _kernel.Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument("context", new WeatherForecastContext());
            _kernel.Bind<IUserAccount>().To<UserAccount>();

            DependencyResolver.SetResolver(new NinjectDependencyResolver(_kernel));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

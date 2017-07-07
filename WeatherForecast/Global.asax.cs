using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WeatherForecast.Services;

namespace WeatherForecast
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IKernel _kernel = new StandardKernel();
            _kernel.Bind<ILogger>().To<CombinedLogger>();

            DependencyResolver.SetResolver(new MyDependencyInjection(_kernel));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
    public class MyDependencyInjection : IDependencyResolver
    {
        private IKernel _kernel;
        public MyDependencyInjection(IKernel kernel)
        {
            _kernel = kernel;
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType, new IParameter[0]);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType, new IParameter[0]);
        }
    }
}

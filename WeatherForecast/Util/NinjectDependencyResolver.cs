using DataLayer;
using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WeatherForecast.Json;
using WeatherForecast.Services;

namespace WeatherForecast.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;
        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType, new IParameter[0]);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType, new IParameter[0]);
        }
        public void AddBindings()
        {
            _kernel.Bind<ILogger>().To<CombinedLogger>();
            _kernel.Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument("context", new WeatherForecastContext());
            _kernel.Bind<IUserAccount>().To<UserAccount>();
            _kernel.Bind<IForecastService>().To<ForecastService>().WithConstructorArgument("logger", c => _kernel.Get<ILogger>());
        }
    }
}
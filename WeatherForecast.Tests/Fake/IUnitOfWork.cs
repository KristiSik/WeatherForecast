using DataLayer.Repositories;
using System;

namespace WeatherForecast.Tests.Fake
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;
        void SaveChanges();
    }
}

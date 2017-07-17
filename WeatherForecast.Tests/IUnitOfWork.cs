using WeatherForecast.Tests.FakeRepository;
using System;

namespace WeatherForecast.Tests
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        int Complete();
    }
}

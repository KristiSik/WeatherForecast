using DataLayer.Repositories;
using System;

namespace DataLayer
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ICityRepository DefaultCities { get; }
        IRequestRepository Requests { get; }
        int Complete();
    }
}

using DataLayer.Repositories;
using System;

namespace DataLayer
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        int Complete();
    }
}

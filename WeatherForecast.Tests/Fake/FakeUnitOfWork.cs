﻿using DataLayer.Repositories;
using System;
using System.Collections.Generic;

namespace WeatherForecast.Tests.Fake
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public void SetRepository<T>(IRepository<T> repository) where T : class
        {
            _repositories[typeof(T)] = repository;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            object repository;
            return _repositories.TryGetValue(typeof(T), out repository)
                       ? (IRepository<T>)repository
                       : new FakeRepository<T>();
        }

        public void SaveChanges()
        {
        }

        public void Dispose()
        {

        }
    }
}

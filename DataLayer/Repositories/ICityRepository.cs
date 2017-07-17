using DataLayer.Models;
using System;
using System.Collections.Generic;

namespace DataLayer.Repositories
{
    public interface ICityRepository : IRepository<City>
    {
        IEnumerable<DefaultCity> GetAllCities();
        void AddCity(DefaultCity city);
        bool RemoveCity(DefaultCity city);
    }
}
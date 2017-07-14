using DataLayer.Models;
using System;
using System.Collections.Generic;

namespace DataLayer.Repositories
{
    public interface ICityRepository : IRepository<City>
    {
        void AddCity(City city);
    }
}
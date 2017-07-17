using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Exceptions;

namespace DataLayer.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(WeatherForecastContext context) : base(context)
        {
        }


        public void AddCity(DefaultCity city)
        {
            if (Db.DefaultCities.FirstOrDefault(c => c.Name == city.Name) != null)
            {
                throw new NotUniqueDefaulCityException("City " + city.Name + " already exists in table.");
            }
            else
            {
                Db.DefaultCities.Add(city);
            }
        }

        public bool RemoveCity(DefaultCity city)
        {
            DefaultCity cityToRemove = Db.DefaultCities.FirstOrDefault(c => c.Name == city.Name);
            if (city != null)
            {
                Db.DefaultCities.Remove(cityToRemove);
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<DefaultCity> GetAllCities()
        {
            return Db.DefaultCities.ToList();
        }

        public WeatherForecastContext Db
        {
            get { return Context as WeatherForecastContext; }
        }
    }
}

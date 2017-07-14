using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataLayer.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(WeatherForecastContext context) : base(context)
        {
        }

        public void AddCity(City city)
        {
            Db.Cities.Add(city);
        }
        public WeatherForecastContext Db
        {
            get { return Context as WeatherForecastContext; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WeatherForecast.Models.Profile;

namespace WeatherForecast.Context
{
    public class WeatherForecastContext : DbContext
    {
        public WeatherForecastContext() : base("name=firstConnectionString")
        {

        }
        public DbSet<City> Cities { get; set; }
        public DbSet<User> Users { get; set; }

        public System.Data.Entity.DbSet<WeatherForecast.Models.Profile.Request> Requests { get; set; }
    }
}
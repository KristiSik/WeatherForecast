using DataLayer.Models;
using System.Data.Entity;

namespace DataLayer
{
    public class WeatherForecastContext : DbContext
    {
        public WeatherForecastContext() : base("name=firstConnectionString")
        {

        }
        public DbSet<City> Cities { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Request> Requests { get; set; }
    }
}
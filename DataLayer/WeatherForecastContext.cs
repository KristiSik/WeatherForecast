using DataLayer.Models;
using System.Data.Entity;

namespace DataLayer
{
    public class WeatherForecastContext : DbContext
    {
        public WeatherForecastContext() : base("name=firstConnectionString")
        {
            Database.SetInitializer<WeatherForecastContext>(new CreateDatabaseIfNotExists<WeatherForecastContext>());
        }
        public DbSet<DefaultCity> DefaultCities { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }
    }
}
using DataLayer.Models;
using System.Data.Entity;

namespace WeatherForecast.Tests
{
    public class FakeContext : DbContext
    {
        public FakeContext() : base("name=fakeConnectionString")
        {
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }
    }

}
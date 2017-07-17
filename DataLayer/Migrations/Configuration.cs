namespace DataLayer.Migrations
{
    using DataLayer.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataLayer.WeatherForecastContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataLayer.WeatherForecastContext context)
        {
            List<DefaultCity> defaultCities = new List<DefaultCity>()
            {
                new DefaultCity(){
                    Id = 1,
                    Name = "Kiev"
                },
                new DefaultCity()
                {
                    Id = 2,
                    Name = "Lviv"
                },
                new DefaultCity(){
                    Id = 3,
                    Name = "Kharkiv"
                },
                new DefaultCity()
                {
                    Id = 4,
                    Name = "Dnipropetrovsk"
                },
                new DefaultCity(){
                    Id = 5,
                    Name = "Odessa"
                }
            };
            foreach (var city in defaultCities)
            {
                context.DefaultCities.AddOrUpdate(city);
            }
        }
    }
}

using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    class WeatherForecastDbInitializer : CreateDatabaseIfNotExists<WeatherForecastContext>
    {
        protected override void Seed(WeatherForecastContext context)
        {
            
            base.Seed(context);
        }
    }
}

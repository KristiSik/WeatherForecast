using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherForecast.Models.Profile
{
    public class Request
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public DateTime Date { get; set; }
    }
}
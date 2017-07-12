using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeatherForecast.Models.Profile
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Requests { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeatherForecast.Models
{
    public class SearchCity
    {
        public string Name { get; set; }
        public int Period { get; set; }
    }
}
﻿using System.ComponentModel.DataAnnotations;

namespace WeatherForecast.Models
{
    public class LoginUser
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
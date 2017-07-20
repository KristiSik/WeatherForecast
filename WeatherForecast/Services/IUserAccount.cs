using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherForecast.Models;

namespace WeatherForecast.Services
{
    public interface IUserAccount
    {
        bool IsAutorized { get; }
        int Id { get; }
        bool Login(LoginUser user);
        void Logout();
        void Request(SearchCity searchCity);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherForecast.Models;

namespace WeatherForecast.Services
{
    public interface IUserAccount
    {
        bool Login(LoginUser user);
        bool IsAutorized();
        void Logout();
        int GetId();
    }
}
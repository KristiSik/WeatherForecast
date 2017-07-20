using DataLayer;
using DataLayer.Models;
using System.Web;
using WeatherForecast.Models;
using System;

namespace WeatherForecast.Services
{
    public class UserAccount : IUserAccount
    {
        private IUnitOfWork ctx;
        public UserAccount(IUnitOfWork uow)
        {
            ctx = uow;
        }
        public bool IsAutorized {
            get
            {
                if (HttpContext.Current.Session["UserId"] != null)
                {
                    int id = (int)HttpContext.Current.Session["UserId"];
                    if (id > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public int Id
        {
            get
            {
                return (int)HttpContext.Current.Session["UserId"];
            }
        }

        public bool Login(LoginUser user)
        {
            User usr = ctx.Users.GetUserByLoginPassword(user.Login, user.Password);
            if (usr != null)
            {
                HttpContext.Current.Session["UserId"] = usr.Id;
                HttpContext.Current.Session["UserName"] = usr.Firstname;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Logout()
        {
            HttpContext.Current.Session["UserId"] = null;
            HttpContext.Current.Session["UserName"] = null;
        }

        public void Request(SearchCity searchCity)
        {
            if (IsAutorized && searchCity.Period == 1)
            {
                ctx.Users.AddRequestFromUser(Id, searchCity.Name);
                ctx.Complete();
            }
        }
    }
}
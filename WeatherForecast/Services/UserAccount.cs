using DataLayer;
using DataLayer.Models;
using System.Web;
using WeatherForecast.Models;

namespace WeatherForecast.Services
{
    public class UserAccount : IUserAccount
    {
        private UnitOfWork ctx = new UnitOfWork(new WeatherForecastContext());

        public int GetId()
        {
            return (int)HttpContext.Current.Session["UserId"];
        }
        public bool IsAutorized()
        {
            if (HttpContext.Current.Session["UserId"] != null) {
                int id = (int)HttpContext.Current.Session["UserId"];
                if (id > 0)
                {
                    return true;
                }
            }
            return false;
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
    }
}
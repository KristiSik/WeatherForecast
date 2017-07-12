using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WeatherForecast.Context;
using WeatherForecast.Models.Profile;

namespace WeatherForecast.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Registration()
        {
            return View(new User());
        }
        [HttpPost]
        public ActionResult Registration(User user)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new WeatherForecastContext())
                {
                    ctx.Users.Add(user);
                    ctx.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginUser user)
        {
            using (var ctx = new WeatherForecastContext())
            {
                var usr = ctx.Users.SingleOrDefault(u => u.Login == user.Login && u.Password == user.Password);
                if (usr != null)
                {
                    Session["UserId"] = usr.Id;
                    Session["UserName"] = usr.Firstname;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Wrong Login or Password";
                    return View(user);
                }
            }
        }
        public JsonResult IsLoginAvailable(string login)
        {
            using (var ctx = new WeatherForecastContext())
            {
                return Json(!ctx.Users.Any( u => u.Login == login ), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ProfileInHead()
        {
            return PartialView();
        }
        public ActionResult History()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            using (var ctx = new WeatherForecastContext())
            {
                int userId = (int) Session["UserId"];
                var requests = ctx.Users.Where(u => u.Id == userId).Select(x => x.History).First().OrderByDescending(y => y.Date);
                return View(requests.ToList());
            }
        }
        public ActionResult Logout()
        {
            Session["UserId"] = null;
            Session["UserName"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Favorites()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            using (var ctx = new WeatherForecastContext())
            {
                int userId = (int)Session["UserId"];
                var favorites = ctx.Users.Where(u => u.Id == userId).Select(x => x.Favorites).First().OrderByDescending(y => y.Requests);
                return View(favorites.ToList());
            }
        }
        public ActionResult AddFavorite(string cityName) {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            using (var ctx = new WeatherForecastContext())
            {
                int userId = (int)Session["UserId"];
                User user = ctx.Users.Where(u => u.Id == userId).FirstOrDefault();
                if (!user.Favorites.Any(f => f.Name == cityName))
                {
                    user.Favorites.Add(new City() { Name = cityName, Requests = 0 });
                }
                ctx.SaveChanges();
            }
            return RedirectToAction("Favorites", "Profile");
        }
        public ActionResult DeleteFavorite(string cityName)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            using (var ctx = new WeatherForecastContext())
            {
                int userId = (int)Session["UserId"];
                User user = ctx.Users.Where(u => u.Id == userId).FirstOrDefault();
                City favorite = ctx.Users.Where(u => u.Id == userId).Select(x => x.Favorites.FirstOrDefault(s => s.Name == cityName)).FirstOrDefault();
                user.Favorites.Remove(favorite);
                ctx.SaveChanges();
            }
            return RedirectToAction("Favorites", "Profile");
        }
        [HttpGet]
        public ActionResult EditFavorite(string cityName)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            using (var ctx = new WeatherForecastContext())
            {
                int userId = (int)Session["UserId"];
                City favorite = ctx.Users.Where(u => u.Id == userId).Select(x => x.Favorites.FirstOrDefault(s => s.Name == cityName)).FirstOrDefault();
                return View(favorite);
            }
        }
        [HttpPost]
        public ActionResult EditFavorite(City city)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            using (var ctx = new WeatherForecastContext())
            {
                int userId = (int)Session["UserId"];
                City favorite = ctx.Users.Where(u => u.Id == userId).Select(x => x.Favorites.FirstOrDefault(s => s.Id == city.Id)).FirstOrDefault();
                favorite.Name = city.Name;
                ctx.SaveChanges();
                return RedirectToAction("Favorites");
            }
        }
    }
}
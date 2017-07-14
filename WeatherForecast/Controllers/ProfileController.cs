using DataLayer;
using DataLayer.Models;
using System.Web.Mvc;
using WeatherForecast.Models;

namespace WeatherForecast.Controllers
{
    public class ProfileController : Controller
    {
        UnitOfWork ctx = new UnitOfWork(new WeatherForecastContext());
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
                ctx.Users.AddUser(user);
                ctx.Complete();
                return RedirectToAction("Index", "Home");
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
            User usr = ctx.Users.GetUserByLoginPassword(user.Login, user.Password);
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
        public JsonResult IsLoginAvailable(string login)
        {
            return Json(!ctx.Users.IsLoginAvailable(login), JsonRequestBehavior.AllowGet);
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
            int userId = (int)Session["UserId"];
            return View(ctx.Users.GetHistory(userId));
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
            int userId = (int)Session["UserId"];
            return View(ctx.Users.GetFavorites(userId));
        }
        public ActionResult AddFavorite(string cityName)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int userId = (int)Session["UserId"];
            ctx.Users.AddFavorite(userId, cityName);
            ctx.Complete();
            return RedirectToAction("Favorites", "Profile");
        }
        public ActionResult DeleteFavorite(string cityName)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int userId = (int)Session["UserId"];
            ctx.Complete();
            return RedirectToAction("Favorites", "Profile");
        }
        [HttpGet]
        public ActionResult EditFavorite(string cityName)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int userId = (int)Session["UserId"];
            return View(ctx.Users.GetFavorite(userId, cityName));
        }
        [HttpPost]
        public ActionResult EditFavorite(DataLayer.Models.City city)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int userId = (int)Session["UserId"];
            ctx.Users.EditFavorite(userId, city.Id, city.Name);
            ctx.Complete();
            return RedirectToAction("Favorites");
        }
    }
}
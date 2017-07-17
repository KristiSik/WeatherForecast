using DataLayer;
using DataLayer.Models;
using System.Web.Mvc;
using WeatherForecast.Models;
using WeatherForecast.Services;

namespace WeatherForecast.Controllers
{
    public class ProfileController : Controller
    {
        private ILogger _logger;
        private IUnitOfWork ctx;
        private IUserAccount userAccount; 
        public ProfileController(ILogger logger, IUnitOfWork uow, IUserAccount user)
        {
            _logger = logger;
            ctx = uow;
            userAccount = user;
        }
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
                _logger.Log(LogLevel.Success, "User " + user.Login + " registered.");
                userAccount.Login(new LoginUser() { Login = user.Login, Password = user.Password });
                return RedirectToAction("Index", "Home");
            }
            else
            {
                _logger.Log(LogLevel.Error, "Failed to create a new user with login " + user.Login + ".");
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
            if (userAccount.Login(user))
            {
                _logger.Log(LogLevel.Success, "User " + user.Login + " signed in.");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                _logger.Log(LogLevel.Error, "Failed to sign in with login " + user.Login + ".");
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
            if (!userAccount.IsAutorized())
            {
                return RedirectToAction("Index", "Home");
            }
            return View(ctx.Users.GetHistory(userAccount.GetId()));
        }
        public ActionResult Logout()
        {
            _logger.Log(LogLevel.Success, userAccount.GetId() + " logged out.");
            userAccount.Logout();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Favorites()
        {
            if (!userAccount.IsAutorized())
            { 
                return RedirectToAction("Index", "Home");
            }
            return View(ctx.Users.GetFavorites(userAccount.GetId()));
        }
        public ActionResult AddFavorite(string cityName)
        {
            if (!userAccount.IsAutorized())
            {
                return RedirectToAction("Index", "Home");
            }
            if (ctx.Users.AddFavorite(userAccount.GetId(), cityName))
            {
                ctx.Complete();
            };
            return RedirectToAction("Favorites", "Profile");
        }
        public ActionResult DeleteFavorite(string cityName)
        {
            if (!userAccount.IsAutorized())
            {
                return RedirectToAction("Index", "Home");
            }
            ctx.Users.DeleteFavorite(userAccount.GetId(), cityName);
            ctx.Complete();
            return RedirectToAction("Favorites", "Profile");
        }
        [HttpGet]
        public ActionResult EditFavorite(string cityName)
        {
            if (!userAccount.IsAutorized())
            {
                return RedirectToAction("Index", "Home");
            }
            return View(ctx.Users.GetFavorite(userAccount.GetId(), cityName));
        }
        [HttpPost]
        public ActionResult EditFavorite(DataLayer.Models.City city)
        {
            if (!userAccount.IsAutorized())
            {
                return RedirectToAction("Index", "Home");
            }
            ctx.Users.EditFavorite(userAccount.GetId(), city.Id, city.Name);
            ctx.Complete();
            return RedirectToAction("Favorites");
        }
    }
}
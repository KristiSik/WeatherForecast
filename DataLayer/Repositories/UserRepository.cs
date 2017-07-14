using DataLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DataLayer.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(WeatherForecastContext context) : base(context)
        {
        }

        public IEnumerable<User> GetAllUsers()
        {
            return Db.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return Db.Users.FirstOrDefault(u => u.Id == id);
        }

        public void AddUser(User user)
        {
            Db.Users.Add(user);
        }

        public void AddRequestFromUser(int userId, string city)
        {
            var s = Db.Users.SingleOrDefault(u => u.Id == userId);
            s.History.Add(new Request() { CityName = city, Date = DateTime.Now });
        }

        public bool IsCityInFavorites(int userId, string city)
        {
            var favorites = Db.Users.Where(u => u.Id == userId).Select(f => f.Favorites).FirstOrDefault();
            return favorites.Any(f => f.Name == city);
        }

        public User GetUserByLoginPassword(string login, string password)
        {
            return Db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
        }

        public bool IsLoginAvailable(string login)
        {
            return Db.Users.Any(u => u.Login == login);
        }

        public IEnumerable<Request> GetHistory(int userId)
        {
            return Db.Users.Where(u => u.Id == userId).Select(x => x.History).First().OrderByDescending(y => y.Date).ToList();
        }

        public IEnumerable<City> GetFavorites(int userId)
        {
            return Db.Users.Where(u => u.Id == userId).Select(x => x.Favorites).First().OrderByDescending(y => y.Requests).ToList();
        }

        public void AddFavorite(int userId, string city)
        {
            User user = Db.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (!user.Favorites.Any(f => f.Name == city))
            {
                user.Favorites.Add(new City() { Name = city, Requests = 0 });
            }
        }

        public void DeleteFavorite(int userId, string city)
        {
            User user = Db.Users.Where(u => u.Id == userId).FirstOrDefault();
            City favorite = Db.Users.Where(u => u.Id == userId).Select(x => x.Favorites.FirstOrDefault(s => s.Name == city)).FirstOrDefault();
            user.Favorites.Remove(favorite);
        }

        public City GetFavorite(int userId, string city)
        {
            return Db.Users.Where(u => u.Id == userId).Select(x => x.Favorites.FirstOrDefault(s => s.Name == city)).FirstOrDefault();
        }

        public void EditFavorite(int userId, int cityId, string newCity)
        {
            City favorite = Db.Users.Where(u => u.Id == userId).Select(x => x.Favorites.FirstOrDefault(s => s.Id == cityId)).FirstOrDefault();
            favorite.Name = newCity;
        }

        public WeatherForecastContext Db
        {
            get { return Context as WeatherForecastContext; }
        }
    }
}
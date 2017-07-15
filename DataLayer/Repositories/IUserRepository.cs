using DataLayer.Models;
using System.Collections.Generic;
namespace DataLayer.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<Request> GetHistory(int userId);
        IEnumerable<City> GetFavorites(int userId);
        User GetUserById(int id);
        User GetUserByLoginPassword(string login, string password);
        void AddUser(User user);
        void DeleteUser(string login, string password);
        void AddRequestFromUser(int userId, string city);
        City GetFavorite(int userId, string city);
        void AddFavorite(int userId, string city);
        void EditFavorite(int userId, int cityId, string newCity);
        void DeleteFavorite(int userId, string city);
        bool IsCityInFavorites(int userId, string city);
        bool IsLoginAvailable(string login);
    }
}
using Shop.Data;
using Shop.Services.Base;
using ShopProject.Data.Users;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Services
{
    public class UserService : IUserService
    {
        private DataBase _data { get; } 
        public UserService(DataBase data)
        {
            _data = data;
        }
        public BaseUser GetUserByName(string username)
        {
            return _data.Users.First(x => x.Name == username);
        }
        public List<Purchase> GetUserPurchases(string userName)
        {
            return _data.PurchaseHistory.Where(x => x.CustomerName == userName).ToList();
        }
        public bool IsUserCreated(string userName)
        {
            return _data.Users.Any(x => x.Name == userName);
        }
    }
}

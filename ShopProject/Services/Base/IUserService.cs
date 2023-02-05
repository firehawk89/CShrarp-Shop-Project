using Shop.Data;
using ShopProject.Data.Users;
using System.Collections.Generic;

namespace Shop.Services.Base
{
    public interface IUserService
    {
        public BaseUser GetUserByName(string username);
        public List<Purchase> GetUserPurchases(string username);
        public bool IsUserCreated(string name);
    }
}

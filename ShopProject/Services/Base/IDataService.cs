using Shop.Data;
using ShopProject.Data.Users;
using System.Collections.Generic;

namespace Shop.Services.Base
{
    public interface IDataService
    {
        public void AddUser(BaseUser user);
        public void AddPurchase(Purchase purchase);
        public List<Product> GetProducts();
    }
}

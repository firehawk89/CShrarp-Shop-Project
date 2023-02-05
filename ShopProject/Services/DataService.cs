using Shop.Data;
using Shop.Services.Base;
using ShopProject.Data.Users;
using System.Collections.Generic;

namespace Shop.Services
{
    public class DataService : IDataService
    {
        private DataBase _data { get; } 
        public DataService(DataBase data)
        {
            _data = data;
        }
        public void AddUser(BaseUser user)
        {
            _data.Users.Add(user);
        }
        public void AddPurchase(Purchase purchase)
        {
            _data.PurchaseHistory.Add(purchase);
        }
        public List<Product> GetProducts()
        {
            return _data.Products;
        }
    }
}

using Shop.Data;

namespace ShopProject.Data.Users
{
    public class User : BaseUser
    {
        public User(string name, string password) : base(name, password) { }
        public override void MakePurchase(Product product, int quantity)
        {
            product.Quantity -= quantity;
            float finalPrice = product.Price * quantity;
            Balance -= finalPrice;
        }
    }
}

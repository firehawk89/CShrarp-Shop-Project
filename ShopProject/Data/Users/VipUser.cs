using Shop.Data;

namespace ShopProject.Data.Users
{
    public class VipUser : BaseUser
    {
        private float _discount = 0.9F;
        public VipUser(string name, string password) : base(name, password) { }
        public override void MakePurchase(Product product, int quantity)
        {
            product.Quantity -= quantity;
            float finalPrice = product.Price * quantity * _discount;
            Balance -= finalPrice;
        }
    }
}

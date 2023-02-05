using Shop.Data;
using Shop.Services;
using ShopProject.Data.Users;
using ShopProject.Services;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShopProject
{
    public partial class PurchaseWindow : Window
    {
        private BaseUser _user;
        private DataBase _data;
        private DataService _dataService;
        private string _productName;
        private int _productQuantity;
        public PurchaseWindow(DataBase data, BaseUser user)
        {
            InitializeComponent();
            _data = data;
            _user = user;
            _dataService = new(_data);
            products.ItemsSource = _data.Products;
            products.SelectedItem = _data.Products.First();
        }
        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            string userName = _user.Name;
            int value;
            if (!int.TryParse(quantity.Text, out value))
            {
                MessageBox.Show("Please, enter quantity!");
            }
            else
            {
                _productQuantity = value;
                int totalPrice = ((Product)products.SelectedItem).Price * _productQuantity;
                if (!((Product)products.SelectedItem).IsAvaliable)
                {
                    MessageBox.Show("Product is no longer avaliable!");
                    quantity.Text = String.Empty;
                }
                else if (totalPrice > _user.Balance)
                {
                    MessageBox.Show("Not enough money!");
                    quantity.Text = String.Empty;
                }
                else
                {
                    try
                    {
                        _user.MakePurchase((Product)products.SelectedItem, _productQuantity);
                        _dataService.AddPurchase(new Purchase(userName, _productName, _productQuantity, totalPrice));
                        Serializer.SerializeUsers(_data.Users);
                        Serializer.SerializePurchases(_data.PurchaseHistory);
                        Serializer.SerializeProducts(_data.Products);
                        this.DialogResult = true;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Quantity is too big");
                        quantity.Text = String.Empty;
                    }
                }
            }
        }
        private void products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (products.SelectedItem != null)
            {
                _productName = ((Product)products.SelectedItem).Name.ToString();
            }
        }
        private void NumericTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
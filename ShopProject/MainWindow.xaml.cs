using Shop.Data;
using Shop.Services;
using ShopProject.Data.Users;
using ShopProject.Services;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ShopProject
{
    public partial class MainWindow : Window
    {
        public DataBase Data;
        private BaseUser _activeUser { get; set; }
        private UserService _userService;
        public MainWindow()
        {
            InitializeComponent();
            Data = new();
            Data.Products = Serializer.DeserializeProducts();
            Data.Users = Serializer.DeserializeUsers();
            Data.PurchaseHistory = Serializer.DeserializePurchases();
            _userService = new(Data);
            products.ItemsSource = Data.Products;
            LoginWindow login = new(Data);
            if (login.ShowDialog() == true)
            {
                _activeUser = login.User;
            }
            if (login.DialogResult == false)
            {
                this.Close();
                return;
            }
            purchases.ItemsSource = _userService.GetUserPurchases(_activeUser.Name);
            this.DataContext = _activeUser;
        }
        private void logOutBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new(Data);
            if (login.ShowDialog() == true)
            { 
                _activeUser = login.User;
                this.DataContext = _activeUser;
                purchases.ItemsSource = _userService.GetUserPurchases(_activeUser.Name);
            }
        }
        private void depositBtn_Click(object sender, RoutedEventArgs e)
        {
            int value;
            if (!int.TryParse(depositValue.Text, out value))
            {
                MessageBox.Show("Please, enter deposit value!");
            }
            else
            {
                _activeUser.MakeDeposit(value);
                Serializer.SerializeUsers(Data.Users);
                depositValue.Text = String.Empty;
            }
        }
        private void makePurchaseBtn_Click(object sender, RoutedEventArgs e)
        {
            PurchaseWindow purchaseWindow = new(Data, _activeUser);
            purchaseWindow.ShowDialog();
            if (purchaseWindow.DialogResult == true)
            {
                purchases.ItemsSource = _userService.GetUserPurchases(_activeUser.Name);
            }
        }
        private void NumericTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
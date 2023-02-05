using Shop.Data;
using Shop.Services;
using ShopProject.Data.Users;
using ShopProject.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ShopProject
{
    public partial class RegistrationWindow : Window
    {
        private DataBase _data;
        private DataService _dataService;
        private UserService _userService;
        private string _accountType;
        public RegistrationWindow(DataBase data)
        {
            InitializeComponent();
            _data = data;
            _dataService = new(_data);
            _userService = new(_data);
        }
        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            string userName = username.Text;
            string userPassword = password.Password;
            if (userName == "" || userPassword == "")
            {
                MessageBox.Show("Some fields are empty!");
            }
            else
            {
                if (_userService.IsUserCreated(userName))
                {
                    MessageBox.Show($"User {userName} already exists!");
                    username.Text = String.Empty;
                    password.Password = String.Empty;
                }
                else
                {
                    if (_accountType == "Standart")
                    {
                        _dataService.AddUser(new User(userName, Hasher.Hash(userPassword)));
                    }
                    else if (_accountType == "VIP")
                    {
                        _dataService.AddUser(new VipUser(userName, Hasher.Hash(userPassword)));
                    }
                    Serializer.SerializeUsers(_data.Users);
                    MessageBox.Show($"User {userName} created!");
                    this.Close();
                }
            }
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            _accountType = pressed.Content.ToString();
        }
    }
}
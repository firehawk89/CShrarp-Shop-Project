using Shop.Data;
using Shop.Services;
using ShopProject.Data.Users;
using ShopProject.Services;
using System;
using System.Windows;

namespace ShopProject
{
    public partial class LoginWindow : Window
    {
        public BaseUser User { get; set; }
        private DataBase _data; 
        private UserService _userService { get; set; }
        public LoginWindow(DataBase data)
        {
            InitializeComponent();
            _data = data;
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
                try
                {
                    var tempUser = _userService.GetUserByName(userName);
                    if (Hasher.CompareHashValues(tempUser.Password, Hasher.Hash(userPassword)) == false)
                    {
                        MessageBox.Show("Invalid data! Try again!");
                        password.Password = String.Empty;
                    }
                    else
                    {
                        User = tempUser;
                        this.DialogResult = true;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show($"Error! User {userName} doesn't exists!");
                    username.Text = String.Empty;
                    password.Password = String.Empty;
                }
            }
        }
        private void registrationBtn_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registartionWindow = new(_data);
            registartionWindow.ShowDialog();
        }
    }
}

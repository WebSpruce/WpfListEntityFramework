using CommunityToolkit.Mvvm.Input;
using entityFramework_2WPF.Data;
using entityFramework_2WPF.Models;
using entityFramework_2WPF.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace entityFramework_2WPF.ViewModels
{
    public class SessionsViewModel : INotifyPropertyChanged
    {
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged();
            }
        }
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged();
            }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged();
            }
        }
        private string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged();
            }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginToAppCommand { get; private set; }
        public ICommand ChangeMethodCommand { get; private set; }
        public ICommand RegisterToAppCommand { get; private set; }

        private ShopContext shopContext;

        public static SessionsViewModel? instance;
        public SessionsViewModel()
        {
            instance = this;

            shopContext = new ShopContext();
            CheckDatabaseConnection();

            LoginToAppCommand = new RelayCommand(() => Login());
            ChangeMethodCommand = new RelayCommand(() => { Pages.Register reg = new Pages.Register(); reg.Show(); Pages.Login.instance.Close(); });
            RegisterToAppCommand = new RelayCommand(() => Register());

        }

        private async void CheckDatabaseConnection()
        {
            if (!await shopContext.Database.CanConnectAsync())
            {
                await shopContext.Database.EnsureCreatedAsync();
            }
        }
        private void Login()
        {
            var user = shopContext.Customers.FirstOrDefault(x => x.Email == Email);
            if (user != null && BCrypt.Net.BCrypt.Verify(Pages.Login.instance.LoginPassword.ToString(), user.Password))
            {
                Trace.WriteLine($"you did logged in!");
                if (user.Permission == "administration")
                {
                    System.Windows.Application.Current.Resources["sessionLoggedInUser"] = user;
                    
                    var result = shopContext.Customers.SingleOrDefault(b => b.Id == user.Id);
                    if (result != null)
                    {
                        result.isLoggedIn = true;
                        result.LastLoginDate = DateTime.Now;
                        shopContext.SaveChanges();
                    }

                    MainWindow mw = new MainWindow();
                    mw.Show();
                    Pages.Login.instance.Close();
                }
                else
                {
                    System.Windows.Application.Current.Resources["sessionLoggedInUser"] = user;

                    var result = shopContext.Customers.SingleOrDefault(b => b.Id == user.Id);
                    if (result != null)
                    {
                        result.isLoggedIn = true;
                        result.LastLoginDate = DateTime.Now;
                        shopContext.SaveChanges();
                    }

                    ShopMainPage smp = new ShopMainPage();
                    smp.Show();
                    Pages.Login.instance.Close();
                }
                
            }
            else
            {
                Trace.WriteLine($"you did not log in!");
            }
        }
        private async void Register()
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Pages.Register.instance.RegisterPassword, salt);

            Customer newCustomer = new Customer { Address = Address, Email = Email, FirstName = FirstName, LastName = LastName, Phone = Phone, Password = hashedPassword };
            var exists = shopContext.Customers.SingleOrDefault(x => x.Permission == "Customer" && x.Email == Email);
            if(exists==null)
            {
                shopContext.Customers.Add(newCustomer);
                await shopContext.SaveChangesAsync();

                Login lg = new Login();
                lg.Show();
                Pages.Register.instance.Close();
                MessageBox.Show("You registered your account.", "Success", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("User with this email exists.", "Nope", MessageBoxButton.OK);
            }
            
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

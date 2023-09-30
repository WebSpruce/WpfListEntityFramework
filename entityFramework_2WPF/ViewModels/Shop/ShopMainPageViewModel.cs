using CommunityToolkit.Mvvm.Input;
using entityFramework_2WPF.Data;
using entityFramework_2WPF.Models;
using entityFramework_2WPF.Pages;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace entityFramework_2WPF.ViewModels.Shop
{
    public class ShopMainPageViewModel : INotifyPropertyChanged
    {
        public ICommand ShopViewCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }
        public ICommand ShopProductsPageCommand { get; private set; }
        public ICommand CartPageCommand { get; private set; }

        private ShopContext shopContext;

        public static ShopMainPageViewModel? instance;
        public ShopMainPageViewModel()
        {
            instance = this;

            ShopProductsPageCommand = new RelayCommand(() => { Uri myUri = new Uri("ShopProductsPage.xaml", UriKind.Relative); ShopMainPage.instance.frame.Source = myUri; });
            CartPageCommand = new RelayCommand(() => { Uri myUri = new Uri("CartPage.xaml", UriKind.Relative); ShopMainPage.instance.frame.Source = myUri; });

            shopContext = new ShopContext();
            LogoutCommand = new RelayCommand(() => {
                Customer? user = Application.Current.Resources["sessionLoggedInUser"] as Customer;

                var result = shopContext.Customers.SingleOrDefault(b => b.Id == user.Id);
                if (result != null)
                {
                    result.isLoggedIn = false;
                    result.LastLoginDate = DateTime.Now;
                    shopContext.SaveChanges();

                    Login lg = new Login();
                    lg.Show();
                    ShopMainPage.instance?.Close();
                }
            });
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

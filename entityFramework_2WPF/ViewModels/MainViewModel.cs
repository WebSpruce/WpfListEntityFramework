using CommunityToolkit.Mvvm.Input;
using entityFramework_2WPF.Data;
using entityFramework_2WPF.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Linq;
using System.Windows;
using entityFramework_2WPF.Pages;
using System.Diagnostics;

namespace entityFramework_2WPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Order> orderData;
        public ObservableCollection<Order> OrderData
        {
            get { return orderData; }
            set
            {
                orderData = value;
                OnPropertyChanged();
            }
        }
        private bool addValueOrderIsChecked;
        public bool AddValueOrderIsChecked
        {
            get { return addValueOrderIsChecked; }
            set
            {
                addValueOrderIsChecked = value;
                OnPropertyChanged();
            }
        }
        private bool resetDataIsChecked;
        public bool ResetDataIsChecked
        {
            get { return resetDataIsChecked; }
            set
            {
                resetDataIsChecked = value;
                OnPropertyChanged();
            }
        }
        private string firstName;
        public string? FirstName
        {
            get { return firstName; }
            set
            {
                if (value != null)
                {
                    firstName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string lastName;
        public string? LastName
        {
            get { return lastName; }
            set
            {
                if (value != null)
                {
                    lastName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string email;
        public string? Email
        {
            get { return email; }
            set
            {
                if (value != null)
                {
                    email = value;
                    OnPropertyChanged();
                }
            }
        }
        private string phone;
        public string? Phone
        {
            get { return phone; }
            set
            {
                if (value != null)
                {
                    phone = value;
                    OnPropertyChanged();
                }
            }
        }
        private string address;
        public string? Address
        {
            get { return address; }
            set
            {
                if (value != null)
                {
                    address = value;
                    OnPropertyChanged();
                }
            }
        }
        private string permission;
        public string? Permission
        {
            get { return permission; }
            set
            {
                if (value != null)
                {
                    permission = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public ICommand CustomersViewCommand { get; private set; }
        public ICommand OrdersViewCommand { get; private set; }
        public ICommand ProductsViewCommand { get; private set; }

        public ICommand CancelBtnCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }

        private ShopContext shopContext;
        private ObservableCollection<Order> ordersList = new ObservableCollection<Order>();

        public static MainViewModel? instance;
        public MainViewModel()
        {
            instance = this;

            shopContext = new ShopContext();

            CustomersViewCommand = new RelayCommand(() => { Uri myUri = new Uri("Pages/DashboardCustomers.xaml", UriKind.Relative); MainWindow.instance.frame.Source = myUri;});
            OrdersViewCommand = new RelayCommand(() => { Uri myUri = new Uri("Pages/DashboardOrders.xaml", UriKind.Relative); MainWindow.instance.frame.Source = myUri; });
            ProductsViewCommand = new RelayCommand(() => { Uri myUri = new Uri("Pages/DashboardProducts.xaml", UriKind.Relative); MainWindow.instance.frame.Source = myUri; });

            LogoutCommand = new RelayCommand(Logout);
        }
        
       private void Logout()
        {
            Customer? user = Application.Current.Resources["sessionLoggedInUser"] as Customer;

            var result = shopContext.Customers.SingleOrDefault(b => b.Id == user.Id);
            if (result != null)
            {
                result.isLoggedIn = false;
                result.LastLoginDate = DateTime.Now;
                shopContext.SaveChanges();

                Login lg = new Login();
                lg.Show();
                MainWindow.instance?.Close();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

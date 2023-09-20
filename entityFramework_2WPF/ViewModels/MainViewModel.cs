using CommunityToolkit.Mvvm.Input;
using entityFramework_2WPF.Data;
using entityFramework_2WPF.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Linq;
using BCrypt.Net;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using entityFramework_2WPF.Pages;

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
        private bool addValueIsChecked;
        public bool AddValueIsChecked
        {
            get { return addValueIsChecked; }
            set
            {
                addValueIsChecked = value;
                OnPropertyChanged();
            }
        }
        private bool loginIsChecked;
        public bool LoginIsChecked
        {
            get { return loginIsChecked; }
            set
            {
                loginIsChecked = value;
                OnPropertyChanged();
            }
        }
        private bool registerIsChecked;
        public bool RegisterIsChecked
        {
            get { return registerIsChecked; }
            set
            {
                registerIsChecked = value;
                OnPropertyChanged();
            }
        }  

        public ICommand OpenFileCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }
        public ICommand AddValueCommand { get; private set; }
        public ICommand ResetDataCommand { get; private set; }
        
        
        public ICommand CustomersViewCommand { get; private set; }
        public ICommand OrdersViewCommand { get; private set; }


        public ICommand AddValueBtnCommand { get; private set; }
        public ICommand CancelBtnCommand { get; private set; }

        public ICommand LogoutCommand { get; private set; }

        private ShopContext shopContext;
        private ObservableCollection<Order> ordersList = new ObservableCollection<Order>();

        public static MainViewModel? instance;
        public MainViewModel()
        {
            instance = this;

            shopContext = new ShopContext();

            OpenFileCommand = new RelayCommand(() => OpenFile());
            ExportCommand = new RelayCommand(() => ExportFile());
            AddValueCommand = new RelayCommand(() => { AddValueIsChecked = true; });
            ResetDataCommand = new RelayCommand(() => ResetData());

            CustomersViewCommand = new RelayCommand(() => { Uri myUri = new Uri("Pages/DashboardCustomers.xaml", UriKind.Relative); MainWindow.instance.frame.Source = myUri;});
            OrdersViewCommand = new RelayCommand(() => { Uri myUri = new Uri("Pages/DashboardOrders.xaml", UriKind.Relative); MainWindow.instance.frame.Source = myUri; });

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
                    MainWindow.instance?.Close();
                }
            });


            AddValueBtnCommand = new RelayCommand(() => AddValue());
            //CancelBtnCommand = new RelayCommand(() => {
            //    if (MainWindow.instance.PopupAddValue.IsOpen)
            //    {
            //        AddValueIsChecked = !MainWindow.instance.PopupAddValue.IsOpen;
            //    }else if (MainWindow.instance.PopupLogin.IsOpen)
            //    {
            //        LoginIsChecked = !MainWindow.instance.PopupLogin.IsOpen;
            //    }
            //});
        }

        
        private void OpenFile()
        {
            
        }
        private void ExportFile()
        {

        }
        private void AddValue()
        {
            
        }
        private void ResetData()
        {

        }
        
       

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

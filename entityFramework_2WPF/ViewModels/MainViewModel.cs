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

        private ShopContext shopContext;
        private ObservableCollection<Order> ordersList = new ObservableCollection<Order>();

        public static MainViewModel? instance;
        public MainViewModel()
        {
            instance = this;

            shopContext = new ShopContext();

            //Customer admin = new Customer() { Id=1, FirstName="Admin", LastName="Admin", Password="admin", Address=" ", Email="admin@admin.com", Phone="555444333"};
            //shopContext.Customers.Add(admin);
            //shopContext.SaveChanges();
            //var customers = from Customer in shopContext.Customers
            //                where Customer.FirstName == FirstName && Customer.LastName == LastName && Customer.Password == MainWindow.instance.LoginPassword
            //                select Customer;

            //foreach (Customer customerItem in customers)
            //{
            //    Console.WriteLine($"{customerItem.Id}. {customerItem.FirstName}, {customerItem.Password}");
            //}

            //Customer customer = new Customer { Id = 1, Address = "asdf", Email = "aksljdflk@kasdf.pl", FirstName = "admin", LastName = "admin", Phone = "123123123" };
            //ordersList.Add(new Order { Id=1, OrderDate = DateTime.Now, CustomerId = 1, Customer = customer, Status = "yes" });
            //OrderData = ordersList;
            //LoginIsChecked = true;
            //RegisterIsChecked = true;

            OpenFileCommand = new RelayCommand(() => OpenFile());
            ExportCommand = new RelayCommand(() => ExportFile());
            AddValueCommand = new RelayCommand(() => { AddValueIsChecked = true; });
            ResetDataCommand = new RelayCommand(() => ResetData());

            CustomersViewCommand = new RelayCommand(() => { Uri myUri = new Uri("Pages/DashboardCustomers.xaml", UriKind.Relative); MainWindow.instance.frame.Source = myUri;});
            OrdersViewCommand = new RelayCommand(() => { Uri myUri = new Uri("Pages/DashboardOrders.xaml", UriKind.Relative); MainWindow.instance.frame.Source = myUri; });


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

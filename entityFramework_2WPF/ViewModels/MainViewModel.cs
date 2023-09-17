
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
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenFileCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }
        public ICommand AddValueCommand { get; private set; }
        public ICommand ResetDataCommand { get; private set; }

        public ICommand LoginToAppCommand { get; private set; }
        public ICommand RegisterToAppCommand { get; private set; }

        public ICommand AddValueBtnCommand { get; private set; }
        public ICommand CancelBtnCommand { get; private set; }

        private ShopContext shopContext;
        private ObservableCollection<Order> ordersList = new ObservableCollection<Order>();

        public static MainViewModel? instance;
        public MainViewModel()
        {
            instance = this;

            shopContext = new ShopContext();
            CheckDatabaseConnection();

            Customer customer = new Customer { Id = 1, Address = "asdf", Email = "aksljdflk@kasdf.pl", FirstName = "admin", LastName = "admin", Phone = "123123123" };
            ordersList.Add(new Order { Id=1, OrderDate = DateTime.Now, CustomerId = 1, Customer = customer, Status = "yes" });
            OrderData = ordersList;
            LoginIsChecked = true;
            //RegisterIsChecked = true;

            OpenFileCommand = new RelayCommand(() => OpenFile());
            ExportCommand = new RelayCommand(() => ExportFile());
            AddValueCommand = new RelayCommand(() => { AddValueIsChecked = true; });
            ResetDataCommand = new RelayCommand(() => ResetData());

            LoginToAppCommand = new RelayCommand(() => Login());
            RegisterToAppCommand = new RelayCommand(() => Register());

            AddValueBtnCommand = new RelayCommand(() => AddValue());
            CancelBtnCommand = new RelayCommand(() => {
                if (MainWindow.instance.PopupAddValue.IsOpen)
                {
                    AddValueIsChecked = !MainWindow.instance.PopupAddValue.IsOpen;
                }else if (MainWindow.instance.PopupLogin.IsOpen)
                {
                    LoginIsChecked = !MainWindow.instance.PopupLogin.IsOpen;
                }
            });
        }

        private async void CheckDatabaseConnection()
        {
            if (!await shopContext.Database.CanConnectAsync())
            {
                await shopContext.Database.EnsureCreatedAsync();
            }
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
        
        private void Login()
        {
            Trace.WriteLine($"username: {FirstName} {LastName} - password: {MainWindow.instance.LoginPassword}");
        }
        private void Register()
        {
            Trace.WriteLine($"reg Username: {FirstName} {LastName} - password: {MainWindow.instance.RegisterPassword}");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

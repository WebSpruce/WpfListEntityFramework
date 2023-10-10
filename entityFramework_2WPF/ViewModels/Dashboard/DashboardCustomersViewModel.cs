using CommunityToolkit.Mvvm.Input;
using entityFramework_2WPF.Data;
using entityFramework_2WPF.Models;
using entityFramework_2WPF.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace entityFramework_2WPF.ViewModels.Dashboard
{
    public class DashboardCustomersViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Customer> customersData;
        public ObservableCollection<Customer> CustomersData
        {
            get { return customersData; }
            set
            {
                customersData = value;
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
        private string addfirstName;
        public string? AddFirstName
        {
            get { return addfirstName; }
            set
            {
                if (value != null)
                {
                    addfirstName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string addlastName;
        public string? AddLastName
        {
            get { return addlastName; }
            set
            {
                if (value != null)
                {
                    addlastName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string addemail;
        public string? AddEmail
        {
            get { return addemail; }
            set
            {
                if (value != null)
                {
                    addemail = value;
                    OnPropertyChanged();
                }
            }
        }
        private string addphone;
        public string? AddPhone
        {
            get { return addphone; }
            set
            {
                if (value != null)
                {
                    addphone = value;
                    OnPropertyChanged();
                }
            }
        }
        private string addaddress;
        public string? AddAddress
        {
            get { return addaddress; }
            set
            {
                if (value != null)
                {
                    addaddress = value;
                    OnPropertyChanged();
                }
            }
        }
        private ComboBoxItem addpermission;
        public ComboBoxItem? AddPermission
        {
            get { return addpermission; }
            set
            {
                if (value != null)
                {
                    addpermission = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool addValueIsCustomerChecked;
        public bool AddValueCustomerIsChecked
        {
            get { return addValueIsCustomerChecked; }
            set
            {
                addValueIsCustomerChecked = value;
                OnPropertyChanged();
            }
        }

        public ICommand ButtonDetailCommand { get; private set; }
        public ICommand RemoveCustomerCommand { get; private set; }
        public ICommand UpdateCustomerInfoCommand { get; private set; }
        public ICommand CancelBtnCommand { get; private set; }

        public ICommand AddValueCommand { get; private set; }
        public ICommand AddValueBtnCommand { get; private set; }

        private ShopContext shopContext;
        private Customer clickedItem;

        public static DashboardCustomersViewModel? instance;
        public DashboardCustomersViewModel()
        {
            instance = this;

            ButtonDetailCommand = new RelayCommand<Customer>((item) => Details(item));
            RemoveCustomerCommand = new RelayCommand(() => DeleteCustomer());
            UpdateCustomerInfoCommand = new RelayCommand(UpdateCustomer);
            CancelBtnCommand = new RelayCommand(CancelOperation);
            AddValueCommand = new RelayCommand(() => { AddValueCustomerIsChecked = true; });
            AddValueBtnCommand = new RelayCommand(() => AddValue());

            shopContext = new ShopContext();

            SetCustomersToList();
        }
        private void SetCustomersToList()
        {
            var customers = from Customer in shopContext.Customers select Customer;
            ObservableCollection<Customer> customersQuery = new ObservableCollection<Customer>(customers.ToList());
            foreach (var item in customersQuery)
            {
                FirstName = item.FirstName;
                LastName = item.LastName;
                Email = item.Email;
                Phone = item.Phone;
                Address = item.Address;
                Permission = item.Permission;
            }
            CustomersData = customersQuery;
        }
        private void Details(Customer item)
        {
            DashboardCustomers.instance.DetailsPopup.IsOpen = true;
            var getDetails = from customer in shopContext.Customers where customer.Id == item.Id select customer;

            foreach (var details in getDetails)
            {
                FirstName = details.FirstName.ToString();
            }

            clickedItem = item;
        }
        private void DeleteCustomer()
        {
            Trace.WriteLine($"removed: {clickedItem.FirstName}");
            shopContext.Customers.Remove(clickedItem);
            shopContext.SaveChanges();

            DashboardCustomers.instance.DetailsPopup.IsOpen = false;
        }
        private void UpdateCustomer()
        {
            var result = shopContext.Customers.SingleOrDefault(b => b.Id == clickedItem.Id);
            if (result != null)
            {
                result.FirstName = FirstName;
                result.LastName = LastName;
                result.Email = Email;
                result.Phone = Phone;
                result.Address = Address;
                result.Permission = Permission;
                shopContext.SaveChanges();
            }
            DashboardCustomers.instance.DetailsPopup.IsOpen = false;
        }
        private void CancelOperation()
        {
            if (DashboardCustomers.instance.DetailsPopup.IsOpen)
            {
                DashboardCustomers.instance.DetailsPopup.IsOpen = false;
            }
            if (AddValueCustomerIsChecked == true)
            {
                AddValueCustomerIsChecked = false;
            }
        }

        private async void AddValue()
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Pages.DashboardCustomers.instance.RegisterPassword, salt);

            Customer newCustomer = new Customer { Address = AddAddress, Email = AddEmail, FirstName = AddFirstName, LastName = AddLastName, Phone = AddPhone, Password = hashedPassword, Permission = AddPermission.Content.ToString() };
            var exists = shopContext.Customers.SingleOrDefault(x => x.Email == AddEmail);
            if (exists == null)
            {
                shopContext.Customers.Add(newCustomer);
                await shopContext.SaveChangesAsync();

                AddValueCustomerIsChecked = false;

                MessageBox.Show("You registered a new account.", "Success", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("User with this email exists.", "Nope", MessageBoxButton.OK);
                Trace.WriteLine($"found: {exists.Id} {exists.FirstName} - {exists.Email}");
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

using entityFramework_2WPF.Data;
using entityFramework_2WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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

        public static DashboardCustomersViewModel? instance;
        public DashboardCustomersViewModel()
        {
            instance = this;

            using ShopContext shopContext = new ShopContext();

            var customers = from Customer in shopContext.Customers select Customer;
            ObservableCollection<Customer> customersQuery = new ObservableCollection<Customer>(customers.ToList());
            foreach (var item in customersQuery)
            {
                Trace.WriteLine($"user: {item.FirstName}");
            }
            CustomersData = customersQuery;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

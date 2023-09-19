using entityFramework_2WPF.Data;
using entityFramework_2WPF.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace entityFramework_2WPF.ViewModels.Dashboard
{
    public class DashboardOrdersViewModel : INotifyPropertyChanged
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

        public static DashboardOrdersViewModel? instance;
        public DashboardOrdersViewModel()
        {
            instance = this;

            using ShopContext shopContext = new ShopContext();

            var orders = from Order in shopContext.Orders select Order;
            ObservableCollection<Order> ordersQuery = new ObservableCollection<Order>(orders.ToList());
            foreach (var item in ordersQuery)
            {
                Trace.WriteLine($"user: {item.Id} - {item.OrderDate}");
            }
            OrderData = ordersQuery;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

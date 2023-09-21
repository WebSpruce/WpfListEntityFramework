using CommunityToolkit.Mvvm.Input;
using entityFramework_2WPF.Data;
using entityFramework_2WPF.Models;
using entityFramework_2WPF.Pages;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

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
        private string quantity;
        public string Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged();
            }
        }
        private string status;
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }
        public ICommand ButtonDetailCommand { get; private set; }
        public ICommand RemoveOrderCommand { get; private set; }
        public ICommand CancelBtnCommand { get; private set; }


        private Order clickedItem;
        private ShopContext shopContext;
        public static DashboardOrdersViewModel? instance;
        public DashboardOrdersViewModel()
        {
            instance = this;

            ButtonDetailCommand = new RelayCommand<Order>((item) => Details(item));
            RemoveOrderCommand = new RelayCommand(() => DeleteOrder());
            CancelBtnCommand = new RelayCommand(CancelOperation);

            shopContext = new ShopContext();

            var orders = from Order in shopContext.Orders select Order;
            ObservableCollection<Order> ordersQuery = new ObservableCollection<Order>(orders.ToList());
            OrderData = ordersQuery;
        }

        private void Details(Order item)
        {
            DashboardOrders.instance.DetailsPopup.IsOpen = true;
            var getDetails = from OrderDetail in shopContext.OrderDetails where OrderDetail.OrderId == item.Id select OrderDetail;

            foreach(var details in getDetails)
            {
                Quantity = details.Quantity.ToString();
            }

            clickedItem = item;
        }
        private void DeleteOrder()
        {
            Trace.WriteLine($"removed: {clickedItem.OrderDate}");
            shopContext.Orders.Remove(clickedItem);
            var detailsItem = from OrderDetail in shopContext.OrderDetails where OrderDetail.OrderId == clickedItem.Id select OrderDetail;
            ObservableCollection<OrderDetail> orderdetails = new ObservableCollection<OrderDetail>(detailsItem.ToList());

            foreach (var itemDetail in orderdetails)
            {
                shopContext.OrderDetails.Remove(itemDetail);
            }
            shopContext.SaveChanges();
            DashboardOrders.instance.DetailsPopup.IsOpen = false;
        }
        private void CancelOperation()
        {
            if (DashboardOrders.instance.DetailsPopup.IsOpen)
            {
                DashboardOrders.instance.DetailsPopup.IsOpen = false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

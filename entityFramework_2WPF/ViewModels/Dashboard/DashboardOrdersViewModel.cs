using CommunityToolkit.Mvvm.Input;
using entityFramework_2WPF.Data;
using entityFramework_2WPF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
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
        private string productId;
        public string ProductId
        {
            get { return productId; }
            set
            {
                productId = value;
                OnPropertyChanged();
            }
        }
        private string orderId;
        public string OrderId
        {
            get { return orderId; }
            set
            {
                orderId = value;
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
        private bool detailsIsChecked;
        public bool DetailsIsChecked
        {
            get { return detailsIsChecked; }
            set
            {
                detailsIsChecked = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Customer> customersList;
        public ObservableCollection<Customer> CustomersList
        {
            get { return customersList; }
            set
            {
                customersList = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Product> productsList;
        public ObservableCollection<Product> ProductsList
        {
            get { return productsList; }
            set
            {
                productsList = value;
                OnPropertyChanged();
            }
        } 
        private ObservableCollection<Product> productsFromOrder;
        public ObservableCollection<Product> ProductsFromOrder
        {
            get { return productsFromOrder; }
            set
            {
                productsFromOrder = value;
                OnPropertyChanged();
            }
        }
        private Customer selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return selectedCustomer; }
            set
            {
                selectedCustomer = value;
                OnPropertyChanged();
            }
        }
        private Product selectedProduct;
        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged();
            }
        }
        private DateTime addDate;
        public DateTime AddDate
        {
            get { return addDate; }
            set
            {
                addDate = value;
                OnPropertyChanged();
            }
        }
        private string addTime;
        public string AddTime
        {
            get { return addTime; }
            set
            {
                addTime = value;
                OnPropertyChanged();
            }
        }
        private string addStatus;
        public string AddStatus
        {
            get { return addStatus; }
            set
            {
                addStatus = value;
                OnPropertyChanged();
            }
        }
        public ICommand ButtonDetailCommand { get; private set; }
        public ICommand CancelBtnCommand { get; private set; }
        public ICommand RemoveOrderCommand { get; private set; }


        public ICommand AddValueCommand { get; private set; }
        public ICommand AddValueBtnCommand { get; private set; }


        private Order clickedItem;
        private OrderDetail clickedDetail;
        private ShopContext shopContext;
        public static DashboardOrdersViewModel? instance;
        public DashboardOrdersViewModel()
        {
            instance = this;

            ButtonDetailCommand = new RelayCommand<Order>((item) => Details(item));
            RemoveOrderCommand = new RelayCommand(() => DeleteOrder());
            CancelBtnCommand = new RelayCommand(CancelOperation);

            AddValueCommand = new RelayCommand(AddValueForm);
            AddValueBtnCommand = new RelayCommand(() => AddValue());

            shopContext = new ShopContext();

            SetOrdersToList();
        }
        private void SetOrdersToList()
        {
            var orders = from Order in shopContext.Orders select Order;
            ObservableCollection<Order> ordersQuery = new ObservableCollection<Order>(orders.ToList());
            OrderData = ordersQuery;
        }
        private void Details(Order item)
        {
            DetailsIsChecked = true;
            var getDetailsAndProducts = shopContext.OrderDetails.Include(od => od.OrderDetailsProducts).ThenInclude(p => p.Product).SingleOrDefault(p=>p.OrderId == item.Id);
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            Quantity = getDetailsAndProducts.Quantity.ToString();
            if(getDetailsAndProducts != null)
            {
                foreach (var orderDetailProduct in getDetailsAndProducts.OrderDetailsProducts)
                {
                    products.Add(orderDetailProduct.Product);
                    Trace.WriteLine($"Product: {orderDetailProduct.Product.Name}, Price: {orderDetailProduct.Product.Price:C}");
                }
            }
            ProductsFromOrder = products;
           
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
            SetOrdersToList();
            DetailsIsChecked = false;
        }
        private void CancelOperation()
        {
            if (DetailsIsChecked)
            {
                DetailsIsChecked = false;
            }
            if (AddValueOrderIsChecked)
            {
                AddValueOrderIsChecked = false;
            }
        }
        private void AddValueForm()
        {
            AddValueOrderIsChecked = true;

            var customers = from Customer in shopContext.Customers where Customer.Permission == "Customer" select Customer;
            ObservableCollection<Customer> tempCustomers = new ObservableCollection<Customer>(customers.ToList());
            CustomersList = tempCustomers;

            var products = from Product in shopContext.Products select Product;
            ObservableCollection<Product> tempProducts = new ObservableCollection<Product>(products.ToList());
            ProductsList = tempProducts;
        }
        private async void AddValue()
        {
            try
            {
                //order
                //DateTime dt1 = DateTime.ParseExact(AddDate, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                Order newOrder = new Order() { Status = addStatus, OrderDate = AddDate, CustomerId = SelectedCustomer.Id };
                shopContext.Orders.Add(newOrder);
                await shopContext.SaveChangesAsync();

                //order details
                OrderDetail newOrderDetails = new OrderDetail() { Quantity = 1, OrderId = newOrder.Id };
                shopContext.OrderDetails.Add(newOrderDetails);
                await shopContext.SaveChangesAsync();

                if (newOrderDetails != null)
                {
                    newOrderDetails.OrderId = newOrder.Id;
                }
                await shopContext.SaveChangesAsync();

                //connect a product to orderdetail
                OrderDetailProduct odp = new OrderDetailProduct() { OrderDetailId = (int)newOrderDetails.Id, ProductId = (int)SelectedProduct.Id };
                shopContext.OrderDetailProducts.Add(odp);
                await shopContext.SaveChangesAsync();


                AddValueOrderIsChecked = false;
                MessageBox.Show($"You added order for customer {SelectedCustomer.FirstName}.", "Success", MessageBoxButton.OK);
                SetOrdersToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Something is wrong.", "Ups");
                Trace.WriteLine($"Order AddValue error: {ex}");
            }

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

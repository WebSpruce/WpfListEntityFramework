using CommunityToolkit.Mvvm.Input;
using entityFramework_2WPF.Data;
using entityFramework_2WPF.Models;
using entityFramework_2WPF.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace entityFramework_2WPF.ViewModels.Shop
{
    public class CartPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Product> product;
        public ObservableCollection<Product> Product
        {
            get { return product; }
            set 
            { 
                product = value; 
                OnPropertyChanged();
            }
        }
        private decimal sumPrice;
        public decimal SumPrice
        {
            get { return sumPrice; }
            set
            {
                sumPrice = value;
                OnPropertyChanged();
            }
        }
        public ICommand DeleteItemCommand { get; private set; }
        public ICommand OrderCommand { get; private set; }

        private Cart currentCart;
        private ShopContext context;
        public static CartPageViewModel? instance;
        public CartPageViewModel() 
        {
            instance = this;

            context = new ShopContext();
            GetProductsAddedToCart();

            DeleteItemCommand = new RelayCommand<Product>((item)=>DeleteItemFromCart(item));
            OrderCommand = new RelayCommand(Order);
        }
        private void GetProductsAddedToCart()
        {
            ObservableCollection<Product> allProducts = new ObservableCollection<Product>();
            Customer user = (Customer)System.Windows.Application.Current.Resources["sessionLoggedInUser"];
            var cProducts = context.Carts.Where(c => c.CustomerId == user.Id).Include(c => c.CartProducts).ThenInclude(cp => cp.Product).FirstOrDefault();
            if (cProducts != null)
            {
                currentCart = cProducts;
                foreach (var cartProduct in cProducts.CartProducts)
                {
                    allProducts.Add(cartProduct.Product);
                    SumPrice += cartProduct.Product.Price;
                }
                Product = allProducts;
            }
            else
            {
                MessageBox.Show("No cart found for you.");
            }
            
        }
        private async void DeleteItemFromCart(Product item)
        {
            try
            {
                var wantedCartProduct = context.CartProducts.Where(cp => cp.Cart == currentCart && cp.Product == item).FirstOrDefault();
                if (wantedCartProduct != null)
                {
                    context.CartProducts.Remove(wantedCartProduct);
                    SumPrice = 0;
                    await context.SaveChangesAsync();

                    GetProductsAddedToCart();
                }
                else
                {
                    MessageBox.Show("Something is wrong with deleting this item from your cart.");
                }
            }
            catch(Exception ex)
            {
                Trace.WriteLine($"DeleteItemFromCart Error: {ex}");
            }
            
        }
        private async void Order()
        {
            Customer user = Application.Current.Resources["sessionLoggedInUser"] as Customer;
            Order newOrder =new Order() { CustomerId = user.Id, OrderDate = DateTime.Now, Status = "Purchased",};
            context.Orders.Add(newOrder);
            await context.SaveChangesAsync();

            OrderDetail newOrderDetail = new OrderDetail(){ Quantity = 1, OrderId = newOrder.Id };
            context.OrderDetails.Add(newOrderDetail);
            await context.SaveChangesAsync();

            if (newOrderDetail != null)
            {
                newOrderDetail.OrderId = newOrder.Id;
            }
            await context.SaveChangesAsync();

            foreach(var item in Product)
            {
                OrderDetailProduct odp = new OrderDetailProduct() { OrderDetail = newOrderDetail, Product = item };
                context.OrderDetailProducts.Add(odp);
                await context.SaveChangesAsync();

                DeleteItemFromCart(item);
            }
            MessageBox.Show($"You have purchased yours products!", $"Congratulations {user.FirstName}");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

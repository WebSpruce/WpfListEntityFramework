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

        public ICommand DeleteItem { get; private set; }

        private ShopContext context;
        public static CartPageViewModel? instance;
        public CartPageViewModel() 
        {
            instance = this;

            context = new ShopContext();
            GetProductsAddedToCart();

            DeleteItem = new RelayCommand<Product>((item)=>DeleteItemFromCart(item));
        }
        private void GetProductsAddedToCart()
        {
            ObservableCollection<Product> allProducts = new ObservableCollection<Product>();
            Customer user = (Customer)System.Windows.Application.Current.Resources["sessionLoggedInUser"];
            var cProducts = context.Carts.Where(c => c.CustomerId == user.Id).Include(c => c.CartProducts).ThenInclude(cp => cp.Product).FirstOrDefault();
            if (cProducts != null)
            {
                foreach (var cartProduct in cProducts.CartProducts)
                {
                    allProducts.Add(cartProduct.Product);
                }
                Product = allProducts;
            }
            else
            {
                MessageBox.Show("No cart found for you.");
            }
            
        }
        private void DeleteItemFromCart(Product item)
        {
            Trace.WriteLine($"selected: {item.Name}");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

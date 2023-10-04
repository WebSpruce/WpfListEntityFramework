using CommunityToolkit.Mvvm.Input;
using entityFramework_2WPF.Data;
using entityFramework_2WPF.Migrations;
using entityFramework_2WPF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cart = entityFramework_2WPF.Models.Cart;

namespace entityFramework_2WPF.ViewModels.Shop
{
    public class ShopProductsPageViewModel
    {
        private ObservableCollection<Product> productsData;
        public ObservableCollection<Product> ProductsData
        {
            get { return productsData; }
            set
            {
                productsData = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddToCartCommand { get; private set; }

        private ShopContext shopContext;
        public static ShopProductsPageViewModel? instance;
        public ShopProductsPageViewModel()
        { 
            instance = this;

            AddToCartCommand = new RelayCommand<Product>((item) => AddToCartAsync(item));

            shopContext = new ShopContext();

            var products = from Product in shopContext.Products select Product;
            ObservableCollection<Product> productsQuery = new ObservableCollection<Product>(products.ToList());
            ProductsData = productsQuery;
        }

        private async Task AddToCartAsync(Product? item)
        {
            var user = (Customer)System.Windows.Application.Current.Resources["sessionLoggedInUser"];
            var cartForCustomer = shopContext.Carts.Where(c => c.CustomerId == user.Id).Include(c => c.CartProducts).FirstOrDefault();
            if (cartForCustomer == null)
            {
                cartForCustomer = new Cart { CustomerId = (int)user.Id };
                shopContext.Carts.Add(cartForCustomer);
            }
            shopContext.CartProducts.Add(new CartProduct { Cart = cartForCustomer, Product = item });
            await shopContext.SaveChangesAsync();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

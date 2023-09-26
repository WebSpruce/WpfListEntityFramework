using CommunityToolkit.Mvvm.Input;
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
using System.Windows.Input;

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

            AddToCartCommand = new RelayCommand<Product>((item) => AddToCart(item));

            shopContext = new ShopContext();

            var products = from Product in shopContext.Products select Product;
            ObservableCollection<Product> productsQuery = new ObservableCollection<Product>(products.ToList());
            ProductsData = productsQuery;
        }

        private void AddToCart(Product? item)
        {
            Trace.WriteLine($"item: {item.Name} - {item.Price}");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

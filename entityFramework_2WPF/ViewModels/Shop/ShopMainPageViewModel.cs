using entityFramework_2WPF.Data;
using entityFramework_2WPF.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace entityFramework_2WPF.ViewModels.Shop
{
    public class ShopMainPageViewModel : INotifyPropertyChanged
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

        public static ShopMainPageViewModel? instance;
        public ShopMainPageViewModel()
        {
            instance = this;

            using ShopContext shopContext = new ShopContext();

            var products = from Product in shopContext.Products select Product;
            ObservableCollection<Product> productsQuery = new ObservableCollection<Product>(products.ToList());
            foreach (var item in productsQuery)
            {
                Trace.WriteLine($"product: {item.Name}");
            }
            ProductsData = productsQuery;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

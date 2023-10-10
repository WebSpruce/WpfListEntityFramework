using CommunityToolkit.Mvvm.Input;
using entityFramework_2WPF.Data;
using entityFramework_2WPF.Models;
using entityFramework_2WPF.Pages;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using Cart = entityFramework_2WPF.Models.Cart;

namespace entityFramework_2WPF.ViewModels.Shop
{
    public class ShopProductsPageViewModel : INotifyPropertyChanged
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
        private ObservableCollection<Product> cpuData;
        public ObservableCollection<Product> CPUData
        {
            get { return cpuData; }
            set
            {
                cpuData = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Product> gpuData;
        public ObservableCollection<Product> GPUData
        {
            get { return gpuData; }
            set
            {
                gpuData = value;
                OnPropertyChanged();
            }
        }
        private BitmapImage detailsImageData;
        public BitmapImage DetailsImageData
        {
            get { return detailsImageData; }
            set
            {
                detailsImageData = value;
                OnPropertyChanged();
            }
        }
        private string detailsName;
        public string DetailsName
        {
            get { return detailsName; }
            set
            {
                detailsName = value;
                OnPropertyChanged();
            }
        }
        private Categories? detailsCategory;
        public Categories? DetailsCategory
        {
            get { return detailsCategory; }
            set
            {
                detailsCategory = value;
                OnPropertyChanged();
            }
        }
        private string detailsDescription;
        public string DetailsDescription
        {
            get { return detailsDescription; }
            set
            {
                detailsDescription = value;
                OnPropertyChanged();
            }
        }
        private string detailsPrice;
        public string DetailsPrice
        {
            get { return detailsPrice; }
            set
            {
                detailsPrice = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddToCartCommand { get; private set; }
        public ICommand DetailsCommand { get; private set; }
        public ICommand BackBtnCommand { get; private set; }

        private ShopContext shopContext;
        public static ShopProductsPageViewModel? instance;
        public ShopProductsPageViewModel()
        { 
            instance = this;

            AddToCartCommand = new RelayCommand<Product>((item) => AddToCartAsync(item));
            DetailsCommand = new RelayCommand<Product>((item) => Details(item));
            BackBtnCommand = new RelayCommand(Back);

            shopContext = new ShopContext();

            SetProductsToList();
            SetCPUToList();
            SetGPUToList();
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
        private void Details(Product product)
        {
            ShopProductsPage.instance.detailPopup.IsOpen = true;
            var getDetails = from Product in shopContext.Products where Product.Id == product.Id select Product;

            foreach (var details in getDetails)
            {
                if (details.ImageData != null)
                {
                    using (MemoryStream stream = new MemoryStream(details.ImageData))
                    {
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = stream;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();

                        DetailsImageData = bitmapImage;
                    }
                }
                else
                {
                    DetailsImageData = null;
                }
                DetailsName = details.Name;
                DetailsDescription = details.Description;
                detailsCategory = details.Category;
                DetailsPrice = details.Price.ToString();
            }
        }
        private void Back()
        {
            if (ShopProductsPage.instance.detailPopup.IsOpen)
            {
                ShopProductsPage.instance.detailPopup.IsOpen = false;
            }
        }
        private void SetProductsToList()
        {
            var products = from Product in shopContext.Products select Product;
            ObservableCollection<Product> productsQuery = new ObservableCollection<Product>(products.ToList());
            ProductsData = productsQuery;
        } 
        private void SetCPUToList()
        {
            var cpus = from Product in shopContext.Products where Product.Category == Categories.CPU select Product;
            ObservableCollection<Product> cpusQuery = new ObservableCollection<Product>(cpus.ToList());
            CPUData = cpusQuery;
        }
        private void SetGPUToList()
        {
            var gpus = from Product in shopContext.Products where Product.Category == Categories.GPU select Product;
            ObservableCollection<Product> gpusQuery = new ObservableCollection<Product>(gpus.ToList());
            GPUData = gpusQuery;
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

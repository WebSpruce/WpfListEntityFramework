using CommunityToolkit.Mvvm.Input;
using entityFramework_2WPF.Data;
using entityFramework_2WPF.Models;
using entityFramework_2WPF.Pages;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace entityFramework_2WPF.ViewModels.Dashboard
{
    public class DashboardProductsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Product> productData;
        public ObservableCollection<Product> ProductData
        {
            get { return productData; }
            set
            {
                productData = value;
                OnPropertyChanged();
            }
        }
        private BitmapImage imageData;
        public BitmapImage ImageData
        {
            get { return imageData; }
            set
            {
                imageData = value;
                OnPropertyChanged();
            }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        private Categories? selectedCategory;
        public Categories? SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                OnPropertyChanged();
            }
        }
        private string? description;
        public string? Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }
        private string price;
        public string Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }
        private BitmapImage addimageData;
        public BitmapImage AddImageData
        {
            get { return addimageData; }
            set
            {
                addimageData = value;
                OnPropertyChanged();
            }
        }
        private string addname;
        public string AddName
        {
            get { return addname; }
            set
            {
                addname = value;
                OnPropertyChanged();
            }
        }
        private Categories? addselectedCategory;
        public Categories? AddSelectedCategory
        {
            get { return addselectedCategory; }
            set
            {
                addselectedCategory = value;
                OnPropertyChanged();
            }
        }
        private string? adddescription;
        public string? AddDescription
        {
            get { return adddescription; }
            set
            {
                adddescription = value;
                OnPropertyChanged();
            }
        }
        private string addprice;
        public string AddPrice
        {
            get { return addprice; }
            set
            {
                addprice = value;
                OnPropertyChanged();
            }
        }

        private bool addValueProductIsChecked;
        public bool AddValueProductIsChecked
        {
            get { return addValueProductIsChecked; }
            set
            {
                addValueProductIsChecked = value;
                OnPropertyChanged();
            }
        }

        public ICommand ButtonDetailCommand { get; private set; }
        public ICommand RemoveProductCommand { get; private set; }
        public ICommand CancelBtnCommand { get; private set; }

        public ICommand UploadImageCommand { get; private set; }
        public ICommand UpdateProductCommand { get; private set; }

        public ICommand AddValueCommand { get; private set; }
        public ICommand AddValueBtnCommand { get; private set; }

        private Product clickedItem;
        private ShopContext shopContext;
        public static DashboardProductsViewModel? instance;
        public DashboardProductsViewModel() 
        {
            instance = this;

            ButtonDetailCommand = new RelayCommand<Product>((item) => Details(item));
            RemoveProductCommand = new RelayCommand(DeleteProduct);
            CancelBtnCommand = new RelayCommand(CancelOperation);

            UploadImageCommand = new RelayCommand(UploadImage);
            UpdateProductCommand = new RelayCommand(UpdateProduct);

            AddValueCommand = new RelayCommand(() => { AddValueProductIsChecked = true; });
            AddValueBtnCommand = new RelayCommand(() => AddValue());


            shopContext = new ShopContext();

            SetProductToList();
        }
        private void SetProductToList()
        {
            var products = from Product in shopContext.Products select Product;
            ObservableCollection<Product> productsQuery = new ObservableCollection<Product>(products.ToList());
            ProductData = productsQuery;
        }
        private void Details(Product item)
        {
            DashboardProducts.instance.ProductDetails.IsOpen = true;
            var getDetails = from Product in shopContext.Products where Product.Id == item.Id select Product;

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

                        ImageData = bitmapImage;
                    }
                }
                else
                {
                    ImageData = null;
                }

                Name = details.Name;
                SelectedCategory = details.Category;
                Description = details.Description;       
                Price = details.Price.ToString();
            }

            clickedItem = item;
        }
        private void DeleteProduct()
        {
            Trace.WriteLine($"removed: {clickedItem.Name}");
            shopContext.Products.Remove(clickedItem);
            shopContext.SaveChanges();
            DashboardProducts.instance.ProductDetails.IsOpen = false;
        }
        private void CancelOperation()
        {
            if (DashboardProducts.instance.ProductDetails.IsOpen)
            {
                DashboardProducts.instance.ProductDetails.IsOpen = false;
            }
            if (AddValueProductIsChecked)
            {
                AddValueProductIsChecked = false;
            }
        }
        private byte[] savedImage;
        private void UploadImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.png, *.bmp)|*.jpg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                byte[] imageData = File.ReadAllBytes(openFileDialog.FileName);

                savedImage = imageData;
                DisplayImage(imageData);
            }
        }
        private void DisplayImage(byte[] imageEntity)
        {
            if (imageEntity != null)
            {
                using (MemoryStream stream = new MemoryStream(imageEntity))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = stream;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();

                    if (AddValueProductIsChecked)
                    {
                        AddImageData = bitmapImage;
                    }
                    else
                    {
                        ImageData = bitmapImage;
                    }
                }
            }
            else
            {
                ImageData = null;
            }
        }
        private async void AddValue()
        {
            try
            {
                Product newProduct = new Product() { Name = AddName , Price = decimal.Parse(AddPrice), ImageData = savedImage, Category = AddSelectedCategory, Description = AddDescription };
                shopContext.Products.Add(newProduct);
                await shopContext.SaveChangesAsync();

                AddValueProductIsChecked = false;

                MessageBox.Show("You added product.", "Success", MessageBoxButton.OK);
            }catch(Exception ex)
            {
                MessageBox.Show($"Something is wrong.","Ups");
            }

        }
        private void UpdateProduct()
        {
            var result = shopContext.Products.SingleOrDefault(b => b.Id == clickedItem.Id);
            if (result != null)
            {
                result.Name = Name;
                result.Price = decimal.Parse(Price);
                result.Category = SelectedCategory;
                result.Description = Description;
                if (savedImage == null)
                {
                    savedImage = result.ImageData;
                }
                result.ImageData = savedImage;
                shopContext.SaveChanges();
            }
            DashboardProducts.instance.ProductDetails.IsOpen = false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

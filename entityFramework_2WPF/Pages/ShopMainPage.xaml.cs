using entityFramework_2WPF.ViewModels.Shop;
using System.Windows;
using System.Windows.Controls;

namespace entityFramework_2WPF.Pages
{
    public partial class ShopMainPage : Window
    {
        public Frame frame;
        public static ShopMainPage? instance;
        public ShopMainPage()
        {
            InitializeComponent();
            instance = this;
            frame = myframe;
            DataContext = new ShopMainPageViewModel();
        }
    }
}

using entityFramework_2WPF.ViewModels.Shop;
using System.Windows;

namespace entityFramework_2WPF.Pages
{
    public partial class ShopMainPage : Window
    {
        public static ShopMainPage? instance;
        public ShopMainPage()
        {
            InitializeComponent();
            instance = this;
            DataContext = new ShopMainPageViewModel();
        }
    }
}

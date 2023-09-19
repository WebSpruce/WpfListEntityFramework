using entityFramework_2WPF.ViewModels.Shop;
using System.Windows;

namespace entityFramework_2WPF.Pages
{
    public partial class ShopMainPage : Window
    {
        public ShopMainPage()
        {
            InitializeComponent();
            DataContext = new ShopMainPageViewModel();
        }
    }
}

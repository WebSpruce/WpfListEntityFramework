using entityFramework_2WPF.ViewModels.Shop;
using System.Windows.Controls;

namespace entityFramework_2WPF.Pages
{
    public partial class CartPage : Page
    {
        public static CartPage? instance;
        public CartPage()
        {
            InitializeComponent();
            instance = this;
            DataContext = new CartPageViewModel();
        }
    }
}

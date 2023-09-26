using System.Windows.Controls;

namespace entityFramework_2WPF.Pages
{
    public partial class ShopProductsPage : Page
    {
        public static ShopProductsPage? instance;
        public ShopProductsPage()
        {
            InitializeComponent();
            instance = this;
        }
    }
}

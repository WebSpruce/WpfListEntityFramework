using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace entityFramework_2WPF.Pages
{
    public partial class ShopProductsPage : Page
    {
        public Popup detailPopup;
        public static ShopProductsPage? instance;
        public ShopProductsPage()
        {
            InitializeComponent();
            detailPopup = detailsPopup;
            instance = this;
        }
    }
}

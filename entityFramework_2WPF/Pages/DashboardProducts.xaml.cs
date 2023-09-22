using entityFramework_2WPF.ViewModels.Dashboard;
using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace entityFramework_2WPF.Pages
{
    public partial class DashboardProducts : Page
    {
        public Popup ProductDetails;
        public Popup AddPopup;
        public static DashboardProducts? instance;
        public DashboardProducts()
        {
            InitializeComponent();
            instance = this;
            ProductDetails = detailsPopup;
            AddPopup = AddValueCustomerPopup;
            DataContext = new DashboardProductsViewModel();
        }
    }
}

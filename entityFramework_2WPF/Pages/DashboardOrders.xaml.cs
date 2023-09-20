using entityFramework_2WPF.ViewModels.Dashboard;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace entityFramework_2WPF.Pages
{
    public partial class DashboardOrders : Page
    {
        public Popup DetailsPopup;
        public static DashboardOrders? instance;
        public DashboardOrders()
        {
            InitializeComponent();
            DetailsPopup = detailsPopup;
            instance = this;
            DataContext = new DashboardOrdersViewModel();
        }
    }
}

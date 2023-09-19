using entityFramework_2WPF.ViewModels.Dashboard;
using System.Windows.Controls;

namespace entityFramework_2WPF.Pages
{
    public partial class DashboardOrders : Page
    {
        public DashboardOrders()
        {
            InitializeComponent();
            DataContext = new DashboardOrdersViewModel();
        }
    }
}

using entityFramework_2WPF.ViewModels.Dashboard;
using System.Windows.Controls;

namespace entityFramework_2WPF.Pages
{
    public partial class DashboardCustomers : Page
    {
        public DashboardCustomers()
        {
            InitializeComponent();
            DataContext = new DashboardCustomersViewModel();
        }
    }
}

using entityFramework_2WPF.ViewModels.Dashboard;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace entityFramework_2WPF.Pages
{
    public partial class DashboardCustomers : Page
    {
        public Popup DetailsPopup;
        public string RegisterPassword;
        public static DashboardCustomers? instance;
        public DashboardCustomers()
        {
            InitializeComponent();
            instance = this;
            DetailsPopup = detailsPopup;
            DataContext = new DashboardCustomersViewModel();
        }

        private void registerPassword_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            RegisterPassword = registerPassword.Password;
        }
    }
}

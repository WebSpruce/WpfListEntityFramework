using entityFramework_2WPF.ViewModels;
using System.Windows;

namespace entityFramework_2WPF.Pages
{
    public partial class Login : Window
    {
        public string LoginPassword;
        public static Login? instance;
        public Login()
        {
            InitializeComponent();
            instance= this;
            DataContext = new SessionsViewModel();
        }

        private void loginPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            LoginPassword = loginPassword.Password;
        }
    }
}

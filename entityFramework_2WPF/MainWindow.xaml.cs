using entityFramework_2WPF.ViewModels;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace entityFramework_2WPF
{
    public partial class MainWindow : Window
    {
        public Popup PopupAddValue;
        public Popup PopupLogin;
        public string RegisterPassword;
        public string LoginPassword;
        public static MainWindow? instance;
        public MainWindow()
        {
            InitializeComponent();
            instance = this;
            PopupAddValue = AddValuePopup;
            PopupLogin = LoginPopup;
            DataContext = new MainViewModel();
        }

        private void registerPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            RegisterPassword = registerPassword.Password;
        }

        private void loginPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            LoginPassword = loginPassword.Password;
        }
    }
}

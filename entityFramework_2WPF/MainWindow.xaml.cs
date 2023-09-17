using entityFramework_2WPF.ViewModels;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace entityFramework_2WPF
{
    public partial class MainWindow : Window
    {
        public Popup PopupAddValue;
        public Popup PopupLogin;
        public static MainWindow? instance;
        public MainWindow()
        {
            InitializeComponent();
            instance = this;
            PopupAddValue = AddValuePopup;
            PopupLogin = LoginPopup;
            DataContext = new MainViewModel();
        }
    }
}

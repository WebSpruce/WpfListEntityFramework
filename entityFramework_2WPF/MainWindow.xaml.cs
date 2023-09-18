using entityFramework_2WPF.ViewModels;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace entityFramework_2WPF
{
    public partial class MainWindow : Window
    {
        public Popup PopupAddValue;
        public static MainWindow? instance;
        public MainWindow()
        {
            InitializeComponent();
            instance = this;
            PopupAddValue = AddValuePopup;
            DataContext = new MainViewModel();
        }
    }
}

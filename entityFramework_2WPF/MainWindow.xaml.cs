using entityFramework_2WPF.ViewModels;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace entityFramework_2WPF
{
    public partial class MainWindow : Window
    {
        public Popup PopupPopup;
        public static MainWindow? instance;
        public MainWindow()
        {
            InitializeComponent();
            instance = this;
            PopupPopup = AddValuePopup;
            DataContext = new MainViewModel();
        }
    }
}

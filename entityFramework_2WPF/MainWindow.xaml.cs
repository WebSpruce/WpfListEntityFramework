using entityFramework_2WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Wpf.Ui.Controls;

namespace entityFramework_2WPF
{
    public partial class MainWindow : Window
    {
        public Popup PopupAddValue;
        public Frame frame;
        public NavigationCompact rootNavigation;
        public static MainWindow? instance;
        public MainWindow()
        {
            InitializeComponent();
            instance = this;
            PopupAddValue = AddValuePopup;
            frame = myframe;
            rootNavigation = RootNavigation;
            DataContext = new MainViewModel();
        }
    }
}

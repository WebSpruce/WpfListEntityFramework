using entityFramework_2WPF.ViewModels;
using System.Windows;

namespace entityFramework_2WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}

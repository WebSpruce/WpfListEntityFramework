using entityFramework_2WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace entityFramework_2WPF.Pages
{
    public partial class Register : Window
    {
        public string RegisterPassword;
        public static Register? instance;
        public Register()
        {
            InitializeComponent();
            instance = this;
            DataContext = new SessionsViewModel();
        }

        private void registerPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            RegisterPassword = registerPassword.Password;
        }
    }
}

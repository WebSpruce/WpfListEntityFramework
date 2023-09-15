
using entityFramework_2WPF.Data;

namespace entityFramework_2WPF.ViewModels
{
    public class MainViewModel
    {
        private ShopContext shopContext;

        public static MainViewModel? instance;
        public MainViewModel()
        {
            instance = this;

            shopContext = new ShopContext();
            CheckDatabaseConnection();
        }

        private async void CheckDatabaseConnection()
        {
            if (!await shopContext.Database.CanConnectAsync())
            {
                await shopContext.Database.EnsureCreatedAsync();
            }
        }
    }
}

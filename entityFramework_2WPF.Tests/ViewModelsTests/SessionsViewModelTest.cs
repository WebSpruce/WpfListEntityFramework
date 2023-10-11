using entityFramework_2WPF.Data;
using entityFramework_2WPF.Models;
using Microsoft.EntityFrameworkCore;

namespace entityFramework_2WPF.Tests.ViewModelsTests
{
    public interface IDBService
    {
        Task<bool> CanConnectAsync();
        Task EnsureCreatedAsync();
    }
    public class ShopContext:DbContext, IDBService
    {
        public async Task<bool> CanConnectAsync()
        {
            return await Database.CanConnectAsync();
        }
        public async Task EnsureCreatedAsync()
        {
            await Database.EnsureCreatedAsync();
        }

        public DbSet<Customer> MockCustomers { get; set; }
    }
    public interface IRegister
    {
        Task<bool> GetCustomerByEmailAsync(string email);
        Task<Customer> AddCustomerAsync(Customer customer);
    }
    public class SessionsViewModelTest
    {
        [Fact]
        public async void SessionsViewModel_CheckDatabaseConnection_ReturnsSuccess()
        {
            var mockDbServer = new Mock<IDBService>();
            mockDbServer.Setup(s=>s.CanConnectAsync()).ReturnsAsync(true);

            string result = await CheckDatabaseConnection(mockDbServer.Object);

            result.Should().Be("Success");
        }

        private async Task<string> CheckDatabaseConnection(IDBService dbService)
        {
            if (!await dbService.CanConnectAsync())
            {
                await dbService.EnsureCreatedAsync();
                return "Couldn't Connect";
            }
            return "Success";
        }
        
    }
}

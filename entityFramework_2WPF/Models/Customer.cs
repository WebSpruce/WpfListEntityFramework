using System;
using System.Collections.Generic;

namespace entityFramework_2WPF.Models
{
    public class Customer
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Permission { get; set; } = "Customer";
        public bool isLoggedIn { get; set; } = false;
        public DateTime LastLoginDate { get; set; } = DateTime.Now;
        public ICollection<Order>? Orders { get; set; }
    }
}

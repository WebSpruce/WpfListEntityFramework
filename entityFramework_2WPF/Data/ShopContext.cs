﻿using entityFramework_2WPF.Models;
using Microsoft.EntityFrameworkCore;

namespace entityFramework_2WPF.Data
{
    public class ShopContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<OrderDetailProduct> OrderDetailProducts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Shop.db");
        }
    }
}

﻿using System;

namespace entityFramework_2WPF.Models
{
    public class Order
    {
        public int? Id { get; set; }
        public string? Status { get; set; } = "Purchased";
        public DateTime? OrderDate { get; set; } = DateTime.Now;
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; } = null!;
        public OrderDetail? OrderDetails { get; set; }
    }
}

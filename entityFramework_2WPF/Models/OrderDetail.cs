using System.Collections.Generic;

namespace entityFramework_2WPF.Models
{
    public class OrderDetail
    {
        public int? Id { get; set; }
        public int? Quantity { get; set; }
        public int? OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public ICollection<OrderDetailProduct> OrderDetailsProducts { get; set; }
    }
}